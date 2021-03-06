﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                        Epor = SumaElementosf(Poli, ij1, ij2);            //-->Calculo de la Potencia del PIN
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

                        por1 = SumaElementosf(Poli, kp1, ij1 - 1) * 100 / Epor;  //-->Porcentaje de Potencia de banda adyacente inferior
                        por2 = SumaElementosf(Poli, ij2 + 1, kp2) * 100 / Epor;  //-->Porcentaje de Potencia de banda adyacente superior

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
            mayor = array[0]; /* asumimos ultimo es mayor */

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

        int indexMax(int[] array)
        {
            int mayor;
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

        float SumaElementosf(float[] Poli, int m, int n)
        {
            float s = 0;
            for (int i = m; i <= n; i++)
            {
                s = s + Poli[i];
            }

            return s;
        }
        double SumaElementos(float[] Poli, int m, int n)
        {
            double s = 0;
            for (int i = m; i <= n; i++)
            {
                s = s + Poli[i];
            }

            return s;
        }

        double[] ProductoPunto(float[] Poli, float[] Poli2, double factor)
        {
            double[] Poli3 = new double[Poli.Length];

            for (int i = 0; i < Poli.Length; i++)
            {
                Poli3[i] = Poli[i] * Poli2[i] * factor;
            }

            return Poli3;
        }

        double[] ProductoEscalar(double[] Poli, float factor)
        {
            double[] Poli2 = new double[Poli.Length];

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
            double[] fSamp = new double[samples.Length];
            float[] afSamp = new float[NFFT/2];
            float[] window = new float[samples.Length];
            double[] wSamp = new double[samples.Length];
            float[] wSamp2 = new float[samples.Length * 2];
            double wndAtt;
            //int L;
            //int NFFT;
            //L    = samples.Length;  // Length of signal
            //NFFT = nextpow2(L); // Next power of 2 from length of y

            window = hann(L);
            wndAtt = SumaElementos(window, 0, (window.Length - 1)) / L;
            wSamp = ProductoPunto(samples, window, 1 / wndAtt);//ventaneamos la señal(512)
            //wSamp2 = IniFFT_Pin(wSamp);//duplicamos la ventana y completamos con ceros(1024)
            double[] wSampFFT= new double[NFFT];
            Fourier.FFT(wSamp, wSampFFT);
            // Fourier.FFT(wSamp2, NFFT, FourierDirection.Forward);
            wSampFFT = ProductoEscalar(wSampFFT, 1 / (float)L);
            
            afSamp = moduloArrayComplejo(wSampFFT, NFFT);

            return afSamp;
        }

        /*float[] IniFFT_Pin(float[] wSamp)
        {
            float[] wSamp2 = new float[wSamp.Length * 2];

            for (int x = 0; x < wSamp.Length * 2; x = x + 2)
            {
                wSamp2[x] = wSamp[x / 2];
                wSamp2[x + 1] = 0;
            }

            return (wSamp2);
        }*/

        float[] moduloArrayComplejo(double[] Poli, int size_in)
        {
            float modulo;
            float[] tmp = new float[size_in/2 ];
            for (int i = 0; i < tmp.Length; i=i+1)
            {
                modulo = (float)Math.Sqrt(Poli[i] * Poli[i] + Poli[size_in - 1 - i] * Poli[size_in - 1 - i]);
                tmp[i] = modulo;
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

        float[] blackman(int n)
        {

            int c = 0;
            float i;
            float[] tmp = new float[n];
            float PI = (float)Math.PI;
            c = 0;
            for (i = 0; i <= n; i = i + 1 + (1) / (float)n)
            {
                tmp[c] = (float)(0.42 + 0.5 * Math.Sin(2*PI*i / (float)n - PI/ 2) - 0.08*Math.Sin(4 * PI * i / (float)n - PI / 2));
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



        float[] conv(float[] A, float[] B, ConvMethod cm)
        {
            switch (cm)
            { 
                case ConvMethod.full :
                    return conv(A, B);
                case ConvMethod.valid : 
                    return conv(A, B);
                default :
                    return conv(A, B);
            }
        }

        private enum ConvMethod
        {
            full,
            same,
            valid
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
                    if (i1 < 0)
                        tmp = tmp + (A[-i1] * B[j]);
                    else if (i1 >= lenA)
                        tmp = tmp + (A[2 * lenA - i1 - 1] * B[j]);
                    else // if (i1 >= 0 && i1 < lenA)
                        tmp = tmp + (A[i1] * B[j]);

                    i1 = i1 - 1;
                    C[i] = tmp;
                }
            }

            return C;
        }

        float[] conv2(float[] B, float[] H)
        {
            int nconv, xconv, hconv;
            int i, j, i1;
            float tmp;
            float[] C;
            int lenA = B.Length;
            int lenB = H.Length;

            //allocated convolution array   
            nconv = lenA + lenB - 1;
            xconv = (lenA > lenB) ? lenA : lenB;
            hconv = (lenA < lenB) ? lenA : lenB;

            C = new float[xconv];



            //convolution process
            for (i = 0; i < nconv; i++)
            {
                i1 = i;
                tmp = 0.0f;
                for (j = 0; j < lenB; j++)
                {
                    if (i1 < 0)
                        tmp = tmp + (B[-i1] * H[j]);
                    else if (i1 >= lenA)
                        tmp = tmp + (B[2 * lenA - i1 - 1] * H[j]);
                    else // if (i1 >= 0 && i1 < lenA)
                        tmp = tmp + (B[i1] * H[j]);

                    i1 = i1 - 1;

                    int dc_ini = (int)(hconv / 2);
                    int dc_end = hconv - (int)(hconv / 2) - 1;

                    if ((i >= dc_ini) && (i <= xconv - dc_end + dc_ini))
                        C[i - dc_ini] = tmp;
                }
            }
            return C;
        }

        float[] filter3(float[] B, float[] H)
        {
            //int nconv, xconv, hconv;
            int k, j, min, max;
            float tmp;
            float[] C;
            float[] tmp2;
            int lenB= B.Length;
            int lenH = H.Length;

            //allocated convolution array   
            
            C = new float[lenB];

            tmp2 = new float[2 * H.Length];

            for (int c = 0; c < lenH; c++)
            {
                tmp2[c] = H[c];
            }

            for (int c = lenH; c < 2 * lenH; c++)
            {
                tmp2[c] = 0.0f; 
            }
            
            //convolution process
            for (k = 0; k <lenB ; k++)
            {
                tmp = 0.0f;
                min = (1 > (k - lenH)) ? 1 : (k - lenH);
                max = (k < lenB) ? k : lenB;
                for (j = min; j < max; j++)
                { 
                   tmp = tmp +B[j]*tmp2[k-j];
                }
            C[k] = tmp;
            }
            return C;
        }

        float[] filter2(float[] B, float[] H)
        {
            int nconv, xconv, hconv;
            int i, j, i1;
            float tmp;
            float[] C;
            int lenB = B.Length;
            int lenH = H.Length * 2;

            //allocated convolution array   
            nconv = lenB + lenH - 1;
            xconv = (lenB > lenH) ? lenB : lenH;
            hconv = (lenB < lenH) ? lenB : lenH;

            C = new float[xconv];



            //convolution process
            for (i = 0; i < nconv; i++)
            {
                i1 = i;
                tmp = 0.0f;
                for (j = 0; j < lenH; j++)
                {
                    int k = j - lenH;
                    float Hk = (k >= 0 ? H[j] : 0);

                    if (i1 < 0)
                        tmp = tmp + (B[-i1] * Hk);
                    else if (i1 >= lenB)
                        tmp = tmp + (B[2 * lenB - i1 - 1] * Hk);
                    else // if (i1 >= 0 && i1 < lenA)
                        tmp = tmp + (B[i1] * Hk);

                    i1 = i1 - 1;

                    int dc_ini = (int)(hconv / 2);
                    int dc_end = hconv - (int)(hconv / 2) - 1;

                    if ((i >= dc_ini) && (i <= xconv - dc_end + dc_ini))
                        C[i - dc_ini] = tmp;
                }
            }
            return C;
        }

        float[] conv(float[] A, float[] B, int offset, int count, int step)
        {
            int i, j, i1;
            float tmp;
            float[] C;
            int lenA = A.Length;
            int lenB = B.Length;
            int red = lenA - count - offset;
            int nconv = (lenA + lenB - 1 - red)/step;

            //allocated convolution array   
            C = new float[nconv];

            //convolution process
            for (i = 0; i < nconv; i++)
            {
                i1 = i*step + offset;
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

        int[] find(float[] A, float b, FindMethod fMeth)
        {
            int Alen = A.Length;
            List<int> R = new List<int>();

                        
            switch (fMeth)
            {
                case FindMethod.Greater:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] > b)
                            R.Add(i); break;
                case FindMethod.GreaterEqual:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] >= b)
                            R.Add(i); break;
                case FindMethod.Less:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] < b)
                            R.Add(i); break;
                case FindMethod.Equal:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] == b)
                            R.Add(i); break;
                default:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] <= b)
                            R.Add(i); break;
            };




            int[] ret = new int[R.Count];
            R.CopyTo(ret);


            return ret;
        }


        int[] find(int[] A, int b, FindMethod fMeth)
        {
            int Alen = A.Length;
            List<int> R = new List<int>();


            switch (fMeth)
            {
                case FindMethod.Greater:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] > b)
                            R.Add(i); break;
                case FindMethod.GreaterEqual:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] >= b)
                            R.Add(i); break;
                case FindMethod.Less:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] < b)
                            R.Add(i); break;
                case FindMethod.Equal:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] == b)
                            R.Add(i); break;
                default:
                    for (int i = 0; i < Alen; i++)
                        if (A[i] <= b)
                            R.Add(i); break;
            };




            int[] ret = new int[R.Count];
            R.CopyTo(ret);


            return ret;
        }

        private enum FindMethod
        {
            Greater,
            GreaterEqual,
            Less,
            LessEqual,
            Equal
        }

        
    }
}
