using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK;
using cswavrec;
using Exocortex.DSP;
using System.Diagnostics;
//using System.Threading;
//using MLApp;

namespace GuiSonar2
{
    public partial class Form1 : Form
    {
        
        Timer tmr;
        Bitmap bmp, bmp2;
        bool detPin = false;
        int bmpWidth;
        int bmpHeight;
        
        bool GL1loaded = false;
        bool GL2loaded = false;
        bool GL1Barloaded = false;
        bool GL2Barloaded = false;

        int speed = 1;
        int y, y2;
        int idx = 0;
        int L;
        int NFFT;
        static int Fs=22050;
        int frameRate;
        float xi;
        float MaxFreqLoc = 0;
        SoundFifo sndIn;
        byte[] sndData;
        float[] sndDataNorm;
        float[] afSamp;
        float[] asdOlapFrame;

        int AnchoPin;
        int UmbralPin;
        int LowFrec;
        int cont = 0;

        int trackBar1Val = 0;
        int trackBar2Val = 0;
        int widthDEP = 0;
        int time5s = Fs * 5;

        bool GLTickLoaded = false;

        static double BufferTime=1.5;

        ObjBuffer  bufferRpm = new ObjBuffer(Fs * BufferTime);
        //ObjBuffer  bufferVis = new ObjBuffer()
        ObjBuffer2 bufferObj = new ObjBuffer2() ;

        Stopwatch stopwatch = new Stopwatch();

        Random rand = new Random();
        //    MLApp.MLApp mEngine;
        //    MLApp.MLApp matlab = new MLApp.MLApp();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Init_Sliders();
            // parámetros iniciales para la captura de la señal  
            Fs = 22050;         //---->frecuencia de muestreo de la señal de audio
            frameRate = 30;  
            L = 1024;            //---->longitud de audio
            //-----------------------------------------------------//
            setup_bmp();
            //tmr = new Timer();
            //tmr.Interval = (int)(1000/(frameRate/1.2));
            //tmr.Tick += new System.EventHandler(timer_execute);
            //tmr.Start();
            
            NFFT = nextpow2(L);
            sndIn = new SoundFifo(Fs, 8, 1, L, 3);
            sndIn.Start();
            sndIn.Changed += new cswavrec.SoundFifo.ChangedEventHandler(timer_execute);
    
        }

        public void Init_Sliders()
        {
            this.sliderAnchoPin.Value  = 50;
            this.sliderUmbralPin.Value = 0;
            this.sliderLowFrec.Value = 50;
            AnchoPin  = this.sliderAnchoPin.Value * 6;
            UmbralPin = this.sliderUmbralPin.Value - 100;
            LowFrec = this.sliderLowFrec.Value * 15;
            this.textAnchoPin.Text = Convert.ToString(AnchoPin);
            this.textUmbralPin.Text  = Convert.ToString(UmbralPin);
            this.textLowFrec.Text = Convert.ToString(LowFrec);
        }
        /* *********************
        sndIn.Start();
        //matlab.Execute("plot([2 2],[3 4 ]);");
        // matlab.Execute("c= ones(10,1).*ones(10,1); plot(c)");
        //matlab.Execute("c= ones(10,1).*ones(10,1); ");
        //atlab.GetVariable("c",base);
        //var activationContext = Type.GetTypeFromProgID("matlab.application.single");
        //var matlab = (MLApp.MLApp)Activator.CreateInstance(activationContext);
        //Console.WriteLine(matlab.Execute("1+2"));
        // matlab.Execute("1+1");
        //mEngine.Execute("1+1");
        //var xx = mEngine.GetVariable("xx", "base");
        */

        private void setup_bmp()
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


            this.textBox1K.Location = new Point((1000 * bmpWidth / 11025-10), 11);
            this.textBox2K.Location = new Point((2000 * bmpWidth / 11025 - 10), 11);
            this.textBox3K.Location = new Point((3000 * bmpWidth / 11025 - 10), 11);
            this.textBox4K.Location = new Point((4000 * bmpWidth / 11025-10), 11);
            this.textBox5K.Location = new Point((5000 * bmpWidth / 11025 - 10), 11);
            this.textBox6K.Location = new Point((6000 * bmpWidth / 11025 - 10), 11);
            this.textBox7K.Location = new Point((7000 * bmpWidth / 11025 - 10), 11);
            this.textBox8K.Location = new Point((8000 * bmpWidth / 11025 - 10), 11);
            this.textBox9K.Location = new Point((9000 * bmpWidth / 11025 - 10), 11);
            this.textBox10K.Location = new Point((10000 * bmpWidth / 11025 - 10), 11);
            this.textBox11K.Location = new Point((11000 * bmpWidth / 11025 - 23), 11);
           
            
        }

        public void timer_execute(object sender, EventArgs e)
        {
            
                stopwatch.Reset();
                stopwatch.Start();
                makeFFT();

                bufferRpm.push(asdOlapFrame);
                pictureBox1.Invalidate();
                glControl1.Invalidate();
                glControl2.Invalidate();
               // glControlBar1.Invalidate();
               // glControlBar2.Invalidate();
           
        }

        public void makeFFT()
        {
            sndData = sndIn.getWaveData();   //----->adquirir audio desde tarjeta de sonido 
            if (sndDataNorm == null)
                sndDataNorm = new float[sndData.Length];
            if (asdOlapFrame == null)
                asdOlapFrame = new float[nextpow2(sndData.Length)/2 ];
                     
            ////---------- NORMALIZAMOS sndDATA  ---------
            for (int i = 0; i < sndData.Length; i++)
            {
               sndDataNorm[i] = (float)((sndData[i] - 128.0) / 128.0);
            }
            //--------------------------------------------
            
            asdOlapFrame=getASD(sndDataNorm);
            
            widthDEP = this.pictureBox1.ClientSize.Width;
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

                for (int x = 0; x < bmpWidth; x++)   //   x < pictureBox1.ClientSize.Width
                   try
                   {
                     tmp.SetPixel(x,bmpHeight-Math.Max(y, y2)-1,Color.FromArgb((int)((byte)(asdOlapFrame[x]/asdOlapFrame.Max()*255.0f)), 0, 0, 255));
                     
                   }
                  catch
                   {
                    Console.Write("Err!");
                   }

                float y_s  = (y * 1.0f) / bmpHeight * pictureBox1.ClientSize.Height;
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
            SetupViewport();
           
        }
        private void glControl2_Load(object sender, EventArgs e)
        {
            GL2loaded = true;
            glControl2.MakeCurrent();
            SetupViewport();
        }
        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!GL1loaded)
                return;
            glControl1.MakeCurrent();
            SetupViewport();
            glControl1.Invalidate();
        }
                
        private void glControl2_Resize(object sender, EventArgs e)
        {
            if (!GL2loaded)
                return;
            glControl2.MakeCurrent();
            SetupViewport();
            glControl2.Invalidate();
        }
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {          
            if (asdOlapFrame == null)
                return;
            if (!GL1loaded)
                return;
           
            glControl1.MakeCurrent();
            GraficoFFT();
            GraficoLineaPin();
            CambioPosLineas();
           
            glControl1.SwapBuffers();
        }

        public void GraficoFFT()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(Color.Blue);

            for (idx = 0; idx < NFFT / 2; idx++)
                try
                {
                    GL.Vertex2(idx / (NFFT / 2.0f), asdOlapFrame[idx] / asdOlapFrame.Max());
                }
                catch
                {
                    Console.WriteLine("error_GL_CONTRO1");
                }
            GL.End();
            //------  Pintado de lineas guia-------
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Gray);
            for (int k = 1; k <= 11; k++)
            {
                for (float paso = 0.10f; paso < 1; paso =paso + 0.15f)
                    GL.Vertex2(k * 1000 / 11025.0f, paso);
            }
            GL.End();
            //--------------------------------------
        }
        public void CambioPosLineas()
        {
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

        public void GraficoLineaPin()
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
                    GL.Vertex2(ids/ (float)time5s, (sndDataNorm[ids]+1)/2.0);
                }
                catch
                {
                    Console.WriteLine("error_GL_CONTRO2");
                }
            GL.End();

            glControl2.SwapBuffers();
        }

        private void SetupViewport()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, 1, 0, 1, 0, 12); // Bottom-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height); // Use all of the glControl painting areagl1Width  = glControl1.Width;
        }

       
        private void LaunchImg(object sender, EventArgs e)
        {

        }
        
        /* private void tmr_tick(object sender, EventArgs e)
         {
             tmr.Stop();
             pictureBox1.Invalidate();
             glControl1.Invalidate();
             glControl2.Invalidate();
             tmr.Start();
         }*/

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            //tmr.Stop();
            //pictureBox1.Invalidate();
           // glControlBar1.Invalidate();
            //glControlBar2.Invalidate();
           // tmr.Start();
        }

        public int detectFlagPin(float[] Poli, int Fs, int NFFT, float ancho, float umbral)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
            int pos=0,an,kp1, kancho;
            float maxFreqLoc,delta, madb, ma, Epor,por1,por2;
            int N, NF, kad,kp2;
            int ij1=0, ij2=0,ban=0;

            N = Fs/frameRate;                   //---> tamaño de sub-bloque
            NF = NFFT/2;                        //---> tamaño de la transformada de [0-Fs/2]
            delta = Fs /(float)(2 * NF);
            kancho = (int)(ancho / delta);
            kad = (int)(100 / delta);           //---> adband=100;
            ma = Poli.Max();
            madb = 10 * (float)Math.Log10(ma);
            maxFreqLoc = 0;
           
            if (madb > umbral)
            {
                //ma = Poli.Max();//al parecer no se usa
                detecPic(Poli,out ij1, out ij2,out pos,out ban);
                if( ban==0 )
                {
                    an = (ij2 - ij1) * (int)delta;
                    if (an <= (kancho * delta))
                    {
                         Epor=SumaElementos(Poli,ij1,ij2);            //-->Calculo de la Potencia del PIN
                         kp1 = ij1 - kad;
                         if( kp1<1 )
                         {
                            kp1=1;
                         }
                         kp2=ij2+kad;
                         if (kp2 > NF)
                         {
                             kp2 = NF;
                         }
            
                         por1=SumaElementos(Poli,kp1,ij1-1)*100/Epor;  //-->Porcentaje de Potencia de banda adyacente inferior
                         por2=SumaElementos(Poli,ij2+1,kp2)*100/Epor;  //-->Porcentaje de Potencia de banda adyacente superior

                        if( por1 < 25 && por2 < 25 )         //--->Validación de existencia de portadora fuerte entre 2 bandas asyacentes
                        {
                         maxFreqLoc=(pos * delta);
                        }
                    }
                    else
                    {
                     maxFreqLoc=0;
                    }
               }
                else
                {
                maxFreqLoc=0;
                }
            }

            return (int)maxFreqLoc;
        }

        void detecPic(float[] Poli, out int ij1, out int ij2, out int pos, out int ban)
         {
            int ed, ij;
            pos=indexMax(Poli);
            ed=Poli.Length;
            ban=0;
            ij=pos;
            ij2=ij;
            if (ij > 3 && ij < (ed - 2))
            {
                while (Poli[ij] > Poli[ij + 1])
                {
                    ij = ij + 1;
                    if (ij >= ed - 1)
                    {
                        ban = 1;
                    }
                    if (ij == ed)
                    { 
                       break;
                    }
                }
                ij2 = ij;
                ij = pos;

                while (Poli[ij] > Poli[ij - 1] && ban == 0)
                {
                    ij = ij - 1;
                    if (ij <= 2)
                    {
                        ban = 1;
                    }

                    if (ij == 1)
                    {
                        break;
                    }
                }
            }
            else
            {
                //Console.WriteLine("ban=1");
                ban = 1;
            }
            ij1=ij;
        }

        int indexMax(float[] array)
        {
            float mayor;
            int i;
            int index=0;
            mayor = array[0]; /* asumimos primero es mayor */

            for (i = 1; i < array.Length; i++) /* buscamos */
            {
                if (mayor <= array[i]) /* si hay otro mayor lo cambiamos */
                {
                    mayor = array[i];
                    index = i;
                }
                
            }
            return index;
        } 

        float SumaElementos(float[] Poli, int m, int n)
        {
            float s = 0;
            for (int i = m; i <= n; i++)
            {
                s = s + Poli[i];
            }

                return s;
        }

        float[] ProductoPunto(float[] Poli, float[] Poli2,float factor)
        {
            float[] Poli3 = new float[Poli.Length];
         
            for (int i = 0; i < Poli.Length; i++)
            {
                Poli3[i] = Poli[i]*Poli2[i]*factor;
            }

            return Poli3;
        }

        float[] ProductoEscalar(float[] Poli,float factor)
        {
            float[] Poli2 = new float[Poli.Length];

            for (int i = 0; i < Poli.Length; i++)
            {
                Poli2[i] = Poli[i]*factor;
            }

            return Poli2;
        }

        int nextpow2(int n)
        {
            int c = 0;
            int res = 0, flag = 0;
            while (n > 1)
            {
                res = n % 2;
                
                if (res == 1)
                {flag = 1;}

                n = n/2;
                c++;
            }
            if (flag == 1)
              return (int)Math.Pow(2,c+1);
            else
              return (int)Math.Pow(2,c);
        }

        public float[] getASD(float[] samples)
        {   
            float[] fSamp=  new float[samples.Length];
            float[] window=  new float[samples.Length];
            float[] wSamp=  new float[samples.Length];
            float[] wSamp2=  new float[samples.Length*2];
            float wndAtt;
            //int L;
            //int NFFT;
            //L    = samples.Length;  // Length of signal
            //NFFT = nextpow2(L); // Next power of 2 from length of y

            window  = hann(L);
            wndAtt  = SumaElementos(window,0,(window.Length-1))/L;
            wSamp   = ProductoPunto(samples,window,1/wndAtt);//ventaneamos la señal(512)
            wSamp2  = IniFFT_Pin(wSamp);//duplicamos la ventana y completamos con ceros(1024)

            Fourier.FFT(wSamp2, NFFT,FourierDirection.Forward);
            fSamp   = ProductoEscalar(wSamp2,1/(float)L);
            afSamp = new float[NFFT/2];
            afSamp = moduloArrayComplejo(fSamp,NFFT);
            
            return afSamp;
        }

        float[] IniFFT_Pin(float[] wSamp)
        {  float[] wSamp2=new float[wSamp.Length*2];

            for (int x = 0; x < wSamp.Length*2; x = x + 2)
            {
                wSamp2[x] = wSamp[x / 2] ;
                wSamp2[x + 1] = 0;
            }

            return (wSamp2);
        }

        float[] moduloArrayComplejo( float[] Poli, int limite  )
        {   
            float modulo;
            float[] tmp = new float[limite/2];
            for (int i = 0; i < (limite-1); i++)
            {
                modulo = 2*(float)Math.Sqrt(Poli[i] * Poli[i] + Poli[i + 1] * Poli[i + 1]);
                tmp[i/2] = modulo;
            }
                return tmp;
        }
        
        float[] hann(int n)
        {   int c=0;
            float i;
            float[] tmp = new float[n];
            
            c=0;
            for(i=0 ; i<=n; i=i+1+(1)/(float)n)
            {
                tmp[c]=(float)(Math.Sin(2*Math.PI*i/(float)n - (float)Math.PI/2 )+1)/2.00f;
                c++;
            }
            return tmp;
        }
               
       /* public void deteccionRPM()
        {
            int Freq1Val=0,Freq2Val=0;
            float fDemon,df;
            int MM,Fs;
            BufferTime = Convert.ToDouble(this.txtBufferTime.Text);


            float[] AudioData = new float[(int)(Fs*BufferTime)];
            float[] filtData  = new float[(int)(Fs*BufferTime)];
       
            if (this.chkDetEngine.Checked==true)
        
            {
               Freq1Val = trackBar1Val;
               Freq2Val = trackBar2Val;
        
               //AudioData = bufferRpm.getFifoBuffer();//recojemos audio del buffer
               filtData  = filtAudioData(AudioData,Freq1Val, Freq2Val);

            //----- Part from pru14---/////
            MM  = Fs/(int)(2*fDemon);
            df  = (Fs/MM) / (2* /* AudioData???///  .Length);
        
            //----- Determinación de la velocidad de Rotación---
            Pxm = bufferObj.getMean;
            Pxm(1) = 0;
            //[ma, pos] = max(Pxm);  :
            maPxm  = Pxm.Max();
            posPxm = indexMax(Pxm);
            this.chkDetEngine.CheckedChanged;
            detRpm = NaN;
            if (maPxm > 1e-6) && (posPxm > 3)
            {
                frec = (posPxm-1)*df;
                detRpm = frec * 60;
            }
            
            if isnan(detRpm) 
                set(txtDetRpm,'String', '---');
            else if detRpm > 0
                set(txtDetRpm,'String', num2str(round(detRpm)));
         
        }
      }*/
        
        public float[] filtAudioData(float[] AudioData,float sldFreq1Val,float sldFreq2Val)
        { float[] filtAudioOut=new float[11025]; 
        


        return filtAudioOut; 
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sndIn.Stop();
           // tmr.Stop();
        }
        private void textPointerTime_TextChanged(object sender, EventArgs e)
        {

        }
        private void textPointerMag_TextChanged(object sender, EventArgs e)
        {

        }
        private void chkDetEngine_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void txtFreq2_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtFreq1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnAuto_Click(object sender, EventArgs e)
        {

        }
        private void sldFreq2_Scroll(object sender, ScrollEventArgs e)
        {
            AnchoPin = this.sliderAnchoPin.Value* 6;  //  AnchoPin : [0  -  600]
            this.textAnchoPin.Text = Convert.ToString(AnchoPin );
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

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
            UmbralPin = this.sliderUmbralPin.Value-100;  // UmbralPin : [-100 - 0]
            this.textUmbralPin.Text = Convert.ToString(UmbralPin);
        }

        private void sliderLowFrec_Scroll(object sender, ScrollEventArgs e)
        {
            LowFrec = this.sliderLowFrec.Value*15;   // LowFrec : [0  -  1500]
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
              Console.WriteLine(((widthDEP - 11) * trackBar2Val) / 11025 + 50);
           
        }

        private void txtDetRpm_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void glControlBar2_Load(object sender, EventArgs e)
        {
            GL2Barloaded = true;
            glControlBar2.MakeCurrent();
            GL.ClearColor(Color.Red);
            this.glControlBar2.Location = new Point(50, 95);
            
        }

        private void glControlBar1_Load(object sender, EventArgs e)
        {
            GL1Barloaded = true;
            glControlBar1.MakeCurrent();
            GL.ClearColor(Color.Red);
            this.glControlBar1.Location = new Point(50, 95);
            
        }

        private void glControlBar2_Paint(object sender, PaintEventArgs e)
        {   //Console.WriteLine("glControlBar2_Paint");
            if (!GL2Barloaded) // Play nice
                return;
            glControlBar2.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this.glControlBar2.Location = new Point(((widthDEP-11)*trackBar2Val)/11025 +50,100);
            glControlBar2.SwapBuffers();
            
            //Console.WriteLine(((widthDEP - 11) * trackBar2Val) / 11025 + 50);

             
        }

        private void glControlBar1_Paint(object sender, PaintEventArgs e)
        {
            if (!GL1Barloaded) // Play nice
                return;
            glControlBar1.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this.glControlBar1.Location = new Point(((widthDEP - 11) * trackBar1Val) / 11025 + 50, 100);
            glControlBar1.SwapBuffers();
        }

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
 
    }

}
