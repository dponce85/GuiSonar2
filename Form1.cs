﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK;
using cswavrec;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GuiSonar2
{
    public partial class Form1 : Form
    {
        Bitmap bmp, bmp2;
        bool detPin = false;
        int bmpWidth;
        int bmpHeight;

        bool GL1loaded = false;
        bool GL2loaded = false;

        int speed = 1;
        int y, y2;
        int L;
        int NFFT;
        int Fs;
        int nBits;

        int frameRate;
        float xi;
        float MaxFreqLoc = 0;
        SoundFifo sndIn;
        byte[] sndData;
        float[] sndDataNorm;
        float[] asdOlapFrame;

        int AnchoPin;
        int UmbralPin;
        int LowFrec;
        int cont = 0;

        int trackBar1Val = 0;
        int trackBar2Val = 0;
        int widthDEP;
        // int time5s = Fs * 5;

        bool GLTickLoaded = false;

        static double BufferTime = 1.5;

        float[] pru = new float[401];

        ObjBuffer bufferRpm;
        ObjBuffer2 bufferObj;
        Stopwatch stopwatch;
        Random rand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize graphics controls
            initSliders();
            setupBmp();

            // Set initial parameters
            Fs = 22050;
            frameRate = 30;
            L = Fs / frameRate; // 1024;
            nBits = 8;


            // Initialize objects
            bufferRpm = new ObjBuffer(Fs * BufferTime);
            bufferObj = new ObjBuffer2();
            stopwatch = new Stopwatch();
            rand = new Random();


            // Initialize audio capture complement
            NFFT = L; // nextpow2(L);
            sndIn = new SoundFifo(Fs, nBits, 1, L, 3);
            sndIn.Start();
            sndIn.Changed += new cswavrec.SoundFifo.ChangedEventHandler(timerExecute);


            var test = new double[] { 1, 2, 3, 2, 1 };
            test[0] = 5;
        }

        public void initSliders()
        {
            this.sliderAnchoPin.Value = 50;
            this.sliderUmbralPin.Value = 0;
            this.sliderLowFrec.Value = 50;
            AnchoPin = this.sliderAnchoPin.Value * 6;
            UmbralPin = this.sliderUmbralPin.Value - 100;
            LowFrec = this.sliderLowFrec.Value * 15;
            this.textAnchoPin.Text = Convert.ToString(AnchoPin);
            this.textUmbralPin.Text = Convert.ToString(UmbralPin);
            this.textLowFrec.Text = Convert.ToString(LowFrec);
        }

        private void setupBmp()
        {
            bmpWidth = this.pictureBox1.ClientSize.Width;
            bmpHeight = this.pictureBox1.ClientSize.Height;

            bmp = new Bitmap(this.pictureBox1.ClientSize.Width, this.pictureBox1.ClientSize.Height);
            bmp2 = new Bitmap(this.pictureBox1.ClientSize.Width, this.pictureBox1.ClientSize.Height);

            // Initial position
            y = 0;
            y2 = -bmpHeight; //  pictureBox1.ClientSize.Height;

            for (int xx = 0; xx < bmp.Width; xx++)
                for (int yy = 0; yy < bmp.Height; yy++)
                    bmp.SetPixel(xx, yy, Color.FromArgb(0, 0, 0, 255));

            for (int xx = 0; xx < bmp.Width; xx++)
                for (int yy = 0; yy < bmp.Height; yy++)
                    bmp2.SetPixel(xx, yy, Color.FromArgb(0, 0, 0, 255));


            this.textBox1K.Location = new Point((1000 * bmpWidth / 11025 - 10), 11);
            this.textBox2K.Location = new Point((2000 * bmpWidth / 11025 - 10), 11);
            this.textBox3K.Location = new Point((3000 * bmpWidth / 11025 - 10), 11);
            this.textBox4K.Location = new Point((4000 * bmpWidth / 11025 - 10), 11);
            this.textBox5K.Location = new Point((5000 * bmpWidth / 11025 - 10), 11);
            this.textBox6K.Location = new Point((6000 * bmpWidth / 11025 - 10), 11);
            this.textBox7K.Location = new Point((7000 * bmpWidth / 11025 - 10), 11);
            this.textBox8K.Location = new Point((8000 * bmpWidth / 11025 - 10), 11);
            this.textBox9K.Location = new Point((9000 * bmpWidth / 11025 - 10), 11);
            this.textBox10K.Location = new Point((10000 * bmpWidth / 11025 - 10), 11);
            this.textBox11K.Location = new Point((11000 * bmpWidth / 11025 - 23), 11);
        }

        public void timerExecute(object sender, EventArgs e)
        {
            makeFFT();

            bufferRpm.push(sndDataNorm);
            pictureBox1.Invalidate();
            glControl1.Invalidate();
            glControl2.Invalidate();
            Console.WriteLine("execute timer" + DateTime.Now.Millisecond);
        }

        public void makeFFT()
        {
            // Adquirir audio desde tarjeta de sonido 
            sndData = sndIn.getWaveData();

            if (sndDataNorm == null)
                sndDataNorm = new float[sndData.Length];
            if (asdOlapFrame == null)
                asdOlapFrame = new float[nextpow2(sndData.Length) / 2];


            // Convierte arreglo de enteros a floats
            double intMaxRange = (double)(1 << (nBits - 1));
            for (int i = 0; i < sndData.Length; i++)
                sndDataNorm[i] = (float)((sndData[i] - intMaxRange) / intMaxRange);

            
            // Obtiene Densidad Espectral de Amplitud
            asdOlapFrame = getASD(sndDataNorm);
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if ((bmp != null) && (asdOlapFrame != null))
            {
                Bitmap tmp;
                if (y < 0)
                    tmp = bmp;
                else
                    tmp = bmp2;


                // Here the FFT data must fit the bitmap size
                for (int x = 0; x < bmpWidth; x++)   //   x < pictureBox1.ClientSize.Width
                    tmp.SetPixel(x, bmpHeight - Math.Max(y, y2) - 1, 
                        Color.FromArgb((int)((byte)(asdOlapFrame[x] / asdOlapFrame.Max() * 512.0f)), 0, 0, 255));
                    

                float y_s = (y * 1.0f) / bmpHeight * pictureBox1.ClientSize.Height;
                float y2_s = (y2 * 1.0f) / bmpHeight * pictureBox1.ClientSize.Height;

                e.Graphics.DrawImage(bmp, 0, (int)y_s, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                y += speed;
                if (y >= bmpHeight) //pictureBox1.ClientSize.Height
                    y = -bmpHeight; //pictureBox1.ClientSize.Height

                e.Graphics.DrawImage(bmp2, 0, (int)y2_s, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
                y2 += speed;
                if (y2 >= bmpHeight) // pictureBox1.ClientSize.Height
                    y2 = -bmpHeight; //pictureBox1.ClientSize.Height
            }
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL1loaded = true;
            glControl1.MakeCurrent();
            setupViewport();

        }
        private void glControl2_Load(object sender, EventArgs e)
        {
            GL2loaded = true;
            glControl2.MakeCurrent();
            setupViewport();
        }
        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!GL1loaded)
                return;
            glControl1.MakeCurrent();
            setupViewport();
            glControl1.Invalidate();
        }

        private void glControl2_Resize(object sender, EventArgs e)
        {
            if (!GL2loaded)
                return;
            glControl2.MakeCurrent();
            setupViewport();
            glControl2.Invalidate();
        }
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (asdOlapFrame == null)
                return;
            if (!GL1loaded)
                return;

            glControl1.MakeCurrent();
            graficoFFT();
            graficoLineaPin();
            cambioPosLineas();

            glControl1.SwapBuffers();
        }

        public void graficoFFT()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(Color.Blue);

            for (int idx = 0; idx < NFFT / 2; idx++)
                GL.Vertex2((idx / (NFFT / 2.0f)), asdOlapFrame[idx] / asdOlapFrame.Max());

            GL.End();


            //------  Pintado de lineas guia-------
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Gray);
            for (int k = 1; k <= 11; k++)
            {
                for (float paso = 0.10f; paso < 1; paso = paso + 0.15f)
                    GL.Vertex2(k * 1000 / 11025.0f, paso);
            }
            GL.End();
            //--------------------------------------
        }

        public void cambioPosLineas()
        {
            widthDEP = this.pictureBox1.ClientSize.Width;

            this.textBox1K.Location = new Point((1000 * widthDEP / 11025 - 10), 11);
            this.textBox2K.Location = new Point((2000 * widthDEP / 11025 - 10), 11);
            this.textBox3K.Location = new Point((3000 * widthDEP / 11025 - 10), 11);
            this.textBox4K.Location = new Point((4000 * widthDEP / 11025 - 10), 11);
            this.textBox5K.Location = new Point((5000 * widthDEP / 11025 - 10), 11);
            this.textBox6K.Location = new Point((6000 * widthDEP / 11025 - 10), 11);
            this.textBox7K.Location = new Point((7000 * widthDEP / 11025 - 10), 11);
            this.textBox8K.Location = new Point((8000 * widthDEP / 11025 - 10), 11);
            this.textBox9K.Location = new Point((9000 * widthDEP / 11025 - 10), 11);
            this.textBox10K.Location = new Point((10000 * widthDEP / 11025 - 10), 11);
            this.textBox11K.Location = new Point((11000 * widthDEP / 11025 - 23), 11);
        }

        public void graficoLineaPin()
        {
            GL.Begin(PrimitiveType.LineStrip);
            if (detPin)
            {
                MaxFreqLoc = detectFlagPin(asdOlapFrame, Fs, NFFT, (float)AnchoPin, (float)UmbralPin);
                if (MaxFreqLoc > LowFrec)
                {
                    cont = cont + 1;
                    if (cont == 2)  // cada 8/30 seg busca un nuevo pin
                    {
                        xi = MaxFreqLoc / (Fs / 2);  //acondicionamos la coordenada de xi :     0 < xi < 1
                        GL.Color3(Color.GreenYellow);
                        GL.Vertex2(xi, -1.00f);
                        GL.Vertex2(xi, 1.00f);
                        this.textFrecPin.Text = Convert.ToString(MaxFreqLoc);
                        cont = 0;
                    }
                }
            }
            GL.End();

        }

        private void glControl2_Paint(object sender, PaintEventArgs e)
        {
            if (sndData == null)
                return;
            if (!GL2loaded)
                return;
            glControl2.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(Color.Blue);

            for (int ids = 0; ids < 512; ids++)
                try
                {
                    GL.Vertex2(ids / (float)(5 * Fs), (sndDataNorm[ids] + 1) / 2.0);
                }
                catch
                {
                    Console.WriteLine("error_GL_CONTRO2");
                }
            GL.End();

            glControl2.SwapBuffers();
        }

        private void setupViewport()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 1, 0, 1, 0, 12); // Bottom-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height); // Use all of the glControl painting areagl1Width  = glControl1.Width;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sndIn.Stop();
        }


        private void sldFreq2_Scroll(object sender, ScrollEventArgs e)
        {
            AnchoPin = this.sliderAnchoPin.Value * 6;  //  AnchoPin : [0  -  600]
            this.textAnchoPin.Text = Convert.ToString(AnchoPin);

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //  glControl1_Resize(sender, e);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //glControl1_Resize(sender, e);
            // glControl1.Invalidate();
        }

        private void Form1_MaximumSizeChanged(object sender, EventArgs e)
        {
            //glControl1_Resize(sender, e);
        }

        private void sliderUmbralPin_Scroll(object sender, ScrollEventArgs e)
        {
            UmbralPin = this.sliderUmbralPin.Value - 100;  // UmbralPin : [-100 - 0]
            this.textUmbralPin.Text = Convert.ToString(UmbralPin);
        }

        private void sliderLowFrec_Scroll(object sender, ScrollEventArgs e)
        {
            LowFrec = this.sliderLowFrec.Value * 15;   // LowFrec : [0  -  1500]
            this.textLowFrec.Text = Convert.ToString(LowFrec);
        }

        private void chkDetPin_CheckedChanged(object sender, EventArgs e)
        {
            detPin = this.chkDetPin.Checked;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1Val = this.trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            trackBar2Val = this.trackBar2.Value;
            // Console.WriteLine(((widthDEP - 11) * trackBar2Val) / 11025 + 50);
        }

        /* private void glControlBar2_Load(object sender, EventArgs e)
         {
             GL2Barloaded = true;
             glControlBar2.MakeCurrent();
             GL.ClearColor(Color.Red);
             this.glControlBar2.Location = new Point(50, 95);

         }*/

        /* private void glControlBar1_Load(object sender, EventArgs e)
         {
             GL1Barloaded = true;
             glControlBar1.MakeCurrent();
             GL.ClearColor(Color.Red);
             this.glControlBar1.Location = new Point(50, 95);

         }*/

        /*private void glControlBar2_Paint(object sender, PaintEventArgs e)
        {   //Console.WriteLine("glControlBar2_Paint");
            if (!GL2Barloaded) // Play nice
                return;
            glControlBar2.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this.glControlBar2.Location = new Point(((widthDEP - 11) * trackBar2Val) / 11025 + 50, 100);
            glControlBar2.SwapBuffers();

            //Console.WriteLine(((widthDEP - 11) * trackBar2Val) / 11025 + 50);


        }*/

        /* private void glControlBar1_Paint(object sender, PaintEventArgs e)
         {
             if (!GL1Barloaded) // Play nice
                 return;
             glControlBar1.MakeCurrent();
             GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
             this.glControlBar1.Location = new Point(((widthDEP - 11) * trackBar1Val) / 11025 + 50, 100);
             glControlBar1.SwapBuffers();
         }
         */
        private void glControlTicks_Load(object sender, EventArgs e)
        {
            GLTickLoaded = true;
            glControlTicks.MakeCurrent();
            GL.ClearColor(Color.Black);
        }

        private void glControlTicks_Paint(object sender, PaintEventArgs e)
        {
            if (!GLTickLoaded)
                return;
            glControlTicks.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            glControlTicks.SwapBuffers();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
