using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exocortex.DSP;

namespace GuiSonar2
{
    public partial class Form1
    {

        int detectFlagPin(float[] Poli, int Fs, int NFFT, float ancho, float umbral)
        {
            int pos = 0, an, kp1, kancho;
            float maxFreqLoc, delta, madb, ma, Epor, por1, por2;
            int N, NF, kad, kp2;
            int ij1 = 0, ij2 = 0, ban = 0;

            N = Fs / frameRate;                   //---> tamaño de sub-bloque
            NF = NFFT / 2;                        //---> tamaño de la transformada de [0-Fs/2]
            delta = Fs / (float)(2 * NF);
            kancho = (int)(ancho / delta);
            kad = (int)(100 / delta);           //---> adband=100;
            ma = Poli.Max();
            madb = 10 * (float)Math.Log10(ma);
            maxFreqLoc = 0;

            if (madb > umbral)
            {
                //ma = Poli.Max();//al parecer no se usa
                detecPic(Poli, out ij1, out ij2, out pos, out ban);
                if (ban == 0)
                {
                    an = (ij2 - ij1) * (int)delta;
                    if (an <= (kancho * delta))
                    {
                        Epor = SumaElementos(Poli, ij1, ij2);            //-->Calculo de la Potencia del PIN
                        kp1 = ij1 - kad;
                        if (kp1 < 1)
                        {
                            kp1 = 1;
                        }
                        kp2 = ij2 + kad;
                        if (kp2 > NF)
                        {
                            kp2 = NF;
                        }

                        por1 = SumaElementos(Poli, kp1, ij1 - 1) * 100 / Epor;  //-->Porcentaje de Potencia de banda adyacente inferior
                        por2 = SumaElementos(Poli, ij2 + 1, kp2) * 100 / Epor;  //-->Porcentaje de Potencia de banda adyacente superior

                        if (por1 < 25 && por2 < 25)         //--->Validación de existencia de portadora fuerte entre 2 bandas asyacentes
                        {
                            maxFreqLoc = (pos * delta);
                        }
                    }
                    else
                    {
                        maxFreqLoc = 0;
                    }
                }
                else
                {
                    maxFreqLoc = 0;
                }
            }

            return (int)maxFreqLoc;
        }

        void detecPic(float[] Poli, out int ij1, out int ij2, out int pos, out int ban)
        {
            int ed, ij;
            pos = indexMax(Poli);
            ed = Poli.Length;
            ban = 0;
            ij = pos;
            ij2 = ij;
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
            ij1 = ij;
        }

        int indexMax(float[] array)
        {
            float mayor;
            int i;
            int index = 0;
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

        float[] ProductoPunto(float[] Poli, float[] Poli2, float factor)
        {
            float[] Poli3 = new float[Poli.Length];

            for (int i = 0; i < Poli.Length; i++)
            {
                Poli3[i] = Poli[i] * Poli2[i] * factor;
            }

            return Poli3;
        }

        float[] ProductoEscalar(float[] Poli, float factor)
        {
            float[] Poli2 = new float[Poli.Length];

            for (int i = 0; i < Poli.Length; i++)
            {
                Poli2[i] = Poli[i] * factor;
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
                { flag = 1; }

                n = n / 2;
                c++;
            }
            if (flag == 1)
                return (int)Math.Pow(2, c + 1);
            else
                return (int)Math.Pow(2, c);
        }

        float[] getASD(float[] samples)
        {
            float[] fSamp = new float[samples.Length];
            float[] window = new float[samples.Length];
            float[] wSamp = new float[samples.Length];
            float[] wSamp2 = new float[samples.Length * 2];
            float wndAtt;
            //int L;
            //int NFFT;
            //L    = samples.Length;  // Length of signal
            //NFFT = nextpow2(L); // Next power of 2 from length of y

            window = hann(L);
            wndAtt = SumaElementos(window, 0, (window.Length - 1)) / L;
            wSamp = ProductoPunto(samples, window, 1 / wndAtt);//ventaneamos la señal(512)
            wSamp2 = IniFFT_Pin(wSamp);//duplicamos la ventana y completamos con ceros(1024)

            Fourier.FFT(wSamp2, NFFT, FourierDirection.Forward);
            fSamp = ProductoEscalar(wSamp2, 1 / (float)L);
            afSamp = new float[NFFT / 2];
            afSamp = moduloArrayComplejo(fSamp, NFFT);

            return afSamp;
        }

        float[] IniFFT_Pin(float[] wSamp)
        {
            float[] wSamp2 = new float[wSamp.Length * 2];

            for (int x = 0; x < wSamp.Length * 2; x = x + 2)
            {
                wSamp2[x] = wSamp[x / 2];
                wSamp2[x + 1] = 0;
            }

            return (wSamp2);
        }

        float[] moduloArrayComplejo(float[] Poli, int limite)
        {
            float modulo;
            float[] tmp = new float[limite / 2];
            for (int i = 0; i < (limite - 1); i++)
            {
                modulo = 2 * (float)Math.Sqrt(Poli[i] * Poli[i] + Poli[i + 1] * Poli[i + 1]);
                tmp[i / 2] = modulo;
            }
            return tmp;
        }

        float[] hann(int n)
        {
            int c = 0;
            float i;
            float[] tmp = new float[n];

            c = 0;
            for (i = 0; i <= n; i = i + 1 + (1) / (float)n)
            {
                tmp[c] = (float)(Math.Sin(2 * Math.PI * i / (float)n - (float)Math.PI / 2) + 1) / 2.00f;
                c++;
            }
            return tmp;
        }

        /* void deteccionRPM()
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

        float[] filtAudioData(float[] AudioData, float sldFreq1Val, float sldFreq2Val)
        {
            float[] filtAudioOut = new float[11025];
            return filtAudioOut;
        }


        float[] conv(float[] A, float[] B)
        {
            int nconv;
            int i, j, i1;
            float tmp;
            float[] C;
            int lenA = A.Length;
            int lenB = B.Length;

            //allocated convolution array   
            nconv = lenA + lenB - 1;
            C = new float[nconv];

            //convolution process
            for (i = 0; i < nconv; i++)
            {
                i1 = i;
                tmp = 0.0f;
                for (j = 0; j < lenB; j++)
                {
                    if (i1 >= 0 && i1 < lenA)
                        tmp = tmp + (A[i1] * B[j]);

                    i1 = i1 - 1;
                    C[i] = tmp;
                }
            }

            return C;
        }

        private void testConv()
        {
            float[] x = new float[] { 1, 2, 3, 4, 5 };
            float[] h = new float[] { 0, 1, 0 };
            float[] z = conv(x, h);

            string s = "";
            for (int i = 0; i < z.Length; i++)
                s += z[i] + ", ";

            Console.WriteLine(s);

            // Resultado: 0, 1, 2, 3, 4, 5, 0
        }



        bool[] find(float[] A, float b, FindMethod fMeth)
        {
            int Alen = A.Length;
            bool[] R = new bool[Alen];
            
            for (int i = 0; i < Alen; i++)
            {
                switch (fMeth)
                {
                    case FindMethod.Greater:
                        R[i] = A[i] > b; break;
                    case FindMethod.GreaterEqual:
                        R[i] = A[i] >= b; break;
                    case FindMethod.Less:
                        R[i] = A[i] < b; break;
                    case FindMethod.LessEqual:
                        R[i] = A[i] <= b; break;
                }
            }

            return R;
        }

        private void testFind()
        {
            float[] x = new float[] { 1, 2, 3, 4, 5 };
            float h = 3;
            bool[] z = find(x, h, FindMethod.Greater);

            string s = "";
            for (int i = 0; i < z.Length; i++)
                s += z[i] + ", ";

            Console.WriteLine(s);
            
            // False, False, False, True, True, 
        }

        private enum FindMethod
        {
            Greater,
            GreaterEqual,
            Less,
            LessEqual
        }
    }
}
