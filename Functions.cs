using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSonar2
{
    public partial class Form1
    {
        float[] filter(float[] B, float[] A, float[] xk)
        {
            int N = 4;
            float[] NumCoeff = B;
            float[] DenCoeff = A;
            float[] Signal = new float[xk.Length];
            Signal = xk;
            float[] FilteredSignal = new float[xk.Length];
            float[] Reg = new float[100];
            int NumSigPts;

            for (int i = 0; i < xk.Length; i++)
            { FilteredSignal[i] = 0; }

            for (int i = 0; i < 100; i++)
            { Reg[i] = 0; }

            NumSigPts = Signal.Length;

            for (int j = 0; j < NumSigPts; j++)
            {
                for (int k = N; k >= 1; k--)
                { Reg[k] = Reg[k - 1]; }

                // El denominador
                Reg[0] = Signal[j];

                for (int k = 1; k <= N; k++)
                { Reg[0] = Reg[0] - DenCoeff[k] * Reg[k]; }

                // El numerador
                float y = 0;
                for (int k = 0; k <= N; k++)
                {
                    y = y + NumCoeff[k] * Reg[k];
                    FilteredSignal[j] = y;
                }
            }

            /*
            ff1 = fft(Signal);
            figure, plot(abs(ff1(1:end/2)));
            ff2 = fft(FilteredSignal);
            figure, plot(abs(ff2(1:end/2)));
            */
            return FilteredSignal;
        }

        float[][] envolvente1(float[] x, int N, float fc, float fs)
        {
            float[][] r = new float[2][];
            int M;
            float[] y;
            float[] hb = Fir1Coeff;
            float av;
            M = (int)(fs / (2 * fc));
            //hb = Fir1(N, 2 * fc / fs);

            for (int i = 0; i < x.Length; i++)
                if (x[i] < 0) x[i] = 0;

            //mean:
            av = x.Average();
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] - av;
            }
            y = conv(x, hb, ConvMethod.full);
            float[] y2 = new float[y.Length % M];
            for (int i = 0; i < y.Length; i = i + M)
            {
                y2[i] = y[i];
            }

            av = y2.Average();
            for (int i = 0; i < y2.Length; i++)
            {
                y2[i] = y2[i] - av;
            }

            r[0] = y2;
            r[1][0] = fs / M;

            return r;
        }

        float[][] envolventeban(float[] B51k, float[] B52k, float[] B53k, float[] B54k, float[] B55k, float[] B56k, float[] B57k, float[] B58k, int ord, float fc1, float fs1)
        {
            float av;
            float[][] B5 = new float[9][];
            B5[0] = envolvente1(B51k, ord, fc1, fs1)[0];
            B5[1] = envolvente1(B52k, ord, fc1, fs1)[0];
            B5[2] = envolvente1(B53k, ord, fc1, fs1)[0];
            B5[3] = envolvente1(B54k, ord, fc1, fs1)[0];
            B5[4] = envolvente1(B55k, ord, fc1, fs1)[0];
            B5[5] = envolvente1(B56k, ord, fc1, fs1)[0];
            B5[6] = envolvente1(B57k, ord, fc1, fs1)[0];
            B5[7] = envolvente1(B58k, ord, fc1, fs1)[0];
            B5[8][0] = envolvente1(B58k, ord, fc1, fs1)[1][0];

            for (int j = 0; j < 8; j++)
            {
                av = B5[j].Average();
                for (int i = 0; i < B5[0].Length; i++)
                { B5[j][i] = B5[j][i] - av; }
            }

            return B5;
        }

        /*float[][] decima(float[][] BEN, float f0, float fsb, float sensi, float ct, float FAV, float boc, float tipve)
        {
            float[][] r = new float[6][];
            float[] B;
            float[] h;
            float fs1 = 2 * 10 * f0;
            int M = (int)Math.Round(fsb / fs1);
            bool[] ip;
            float k;
            if (M > 1)
                h = fir1_400(400, 1.0f / M);
            else
                fs1 = fsb;

            ip = find(ct, sensi, FindMethod.Less);

            k = 1;

            for (int u = 0; u < ip.Length; u++)
            {
                for (int f = 0; f < BEN.Size[0]; f++)
                {
                    B = BEN[f][ip[u]];
                }

                if (M > 1)
                {
                    B = filter(h, 1, B);
                }
            }

            return r;
        }
        */



        private bool[] find(float[] x, int p, FindMethod findMethod)
        {
            throw new NotImplementedException();
        }





        void dwt(float[] X, float[] hw0, float[] gw0, out float[] Xlp, out float[] Xhp)
        {
            Xlp = subSamp(conv(X, hw0), 2);
            Xhp = subSamp(conv(X, gw0), 2);
        }

        float[] subSamp(float[] X, int step)
        {
            int retLen = X.Length / step;
            float[] ret = new float[retLen];
            for (int i = 0, j = 0; i < X.Length; i += step)
                ret[j++] = X[i];

            return ret;
        }

        float[] double2float(double[] x)
        {
            float[] tmp = new float[x.Length];

            tmp = Array.ConvertAll(x, element => (float)element);

            return tmp;

        }

        float[][] detecarm33(float[] FV, float f0, float delta, float[] f, float sensar)
        {
            float[][] r = new float[7][];
            float[] FV2 = new float[FV.Length];
            float[] FV1;

            float fmin = 0;
            float ma;
            float ik1, ik2, k1, k2;
            float lim1;
            float[] k = new float[2];
            float[] mak = new float[2];
            float ban = 0;
            int p1, p2;
            int nar = 0;
            int L = 0;
            int pos = 0;
            int tol;
            int Na;
            int[] ivj = new int[2];

            nar = 0;
            fmin = 0.5f;
            k1 = (int)Math.Round(fmin / delta);
            FV1 = FV;
            //FV1(1:K1) = 0;
            for (int i = 0; i < k1; i++)
            {
                FV1[i] = 0;
            }

            L  = FV1.Length ;
            
            ma = FV1.Length ;
            pos = indexMax(FV1);
            ik1 = pico(FV1, pos, L)[0];
            ik2 = pico(FV1, pos, L)[1];
            k1  = pico(FV1, pos, L)[2];
            k2  = pico(FV1, pos, L)[3];

           tol = (int)Math.Round((ik2 - ik1 + 1)/2.0f);
           k[0] = k1;
           k[1] = k2;
           lim1 = k.Min();
           p1 = pos - tol;
           p2 = pos - tol;

           if (p1 < 0)
           { p1 = 0; }

           if (p2 > L)
           { p2 = L-1; }

           //FV1(p1:p2) = 0  
           for (int i = p1; i <= p2; i++)
           {
               FV1[i] = 0;
           }

           Na = 10;
           ivj[0] = pos;
           mak[0] = ma;

           ban = 0;
           cont = 1;

           List<double> abc = new List<double>();

            abc.Add(13.4);

            double[] empty;
            abc.CopyTo(empty, 0);
            

           while (ban == 0)
           { 
             ma = FV1.Max();    
             pos = indexMax(FV1); 
             
             ivj[0] = ivj[0];
             ivj[1] = pos;
             mak[0] = mak[0];
             mak[1] = ma;
             cont=cont+1;
      
              if( cont==Na)
              { ban=1;} 
                        
              p1=pos-tol;
              p2=pos+tol;
              if( p1<1)
              { p1=1; }
             
              if( p2>L)
              { p2=L;}
                 
              //FV1(p1:p2)=0;
               for( int i=p1;i<=p2;i++)
               {
                 FV1[i] = 0;
               }
               
              if(FV1.Sum()==0)
              { ban=1;}
           
            }


               return r;
        }

        float[] fir1_400(int N, float Wn)
        {

            const float PI = (float)Math.PI;
            int Pr_L = N + 1;
            /* FIR filter
            Return an array of 1 x 256
            */

            int odd, i, j, nhlf, i1;
            float f1, gain, c1;
            float[] wind = new float[Pr_L];
            float[] xn = new float[Pr_L / 2];
            float[] b = new float[Pr_L / 2];
            float[] c = new float[Pr_L / 2];
            float[] c3 = new float[Pr_L / 2];
            float[] bb = new float[Pr_L];

            //bb = (double *) malloc(sizeof(double) * Pr_L);

            gain = 0;
            N = N + 1;
            odd = N - (N / 2) * 2; /* odd = rem(N,2) */

            /*wind = hamming(N);*/
            for (i = 0; i < Pr_L; i++)
            {
                wind[i] = (float)(0.54 - (0.46 * Math.Cos((2 * PI * i) / (N - 1))));
            }

            f1 = Wn / 2.0f;
            c1 = f1;
            nhlf = (N + 1) / 2;
            nhlf = nhlf - 1;
            i1 = odd + 1;

            /* Lowpass */

            if (odd != 0)
                b[0] = 2 * c1;

            for (i = 0; i < nhlf; i++)
            {
                xn[i] = i + 0.5f * (1 - odd);
            }

            for (i = 0; i < nhlf; i++)
            {
                c[i] = PI * xn[i];
            }

            for (i = 0; i < nhlf; i++)
            {
                c3[i] = 2 * c1 * c[i];
            }

            /* b(i1:nhlf)=(sin(c3)./c) */
            for (i = 1; i < (nhlf); i++)
            {
                b[i] = (float)Math.Sin(c3[i]) / c[i];
            }

            /* bb = real([b(nhlf:-1:i1) b(1:nhlf)].*wind(:)') */
            for (i = 0, j = nhlf - 1; i < nhlf; i++, j--)
            {
                bb[i] = b[j];
            }
            for (i = nhlf, j = 0; i < Pr_L; i++, j++)
            {
                bb[i] = b[j];
            }
            for (i = 0; i < Pr_L; i++)
            {
                bb[i] = bb[i] * wind[i];
            }

            /* gain = abs(polyval(b,1)); */
            for (i = 0; i < Pr_L; i++)
            {
                gain += bb[i];
            }
            /* b = b / gain */
            for (i = 0; i < Pr_L; i++)
            {
                bb[i] = bb[i] / gain;
            }

            return bb;

        }



        float[] hw0 = new float[]{
            -0.00000000F,
             0.00000000F,
            -0.00000002F,
             0.00000000F,
             0.00000026F,
            -0.00000068F,
            -0.00000101F,
             0.00000724F,
            -0.00000438F,
            -0.00003711F,
             0.00006774F,
             0.00010153F,
            -0.00038510F,
            -0.00005350F,
             0.00139256F,
            -0.00083156F,
            -0.00358149F,
             0.00442054F,
             0.00672163F,
            -0.01381053F,
            -0.00878932F,
             0.03229430F,
             0.00587468F,
            -0.06172290F,
             0.00563225F,
             0.10229172F,
            -0.02471683F,
            -0.15545875F,
             0.03985025F,
             0.22829105F,
            -0.01672709F,
            -0.32678680F,
            -0.13921209F,
             0.36150230F,
             0.61049324F,
             0.47269619F,
             0.21994211F,
             0.06342378F,
             0.01054939F,
             0.00077995F
        };

        float[] gw0 = new float[] { 
            -0.00077995F,
             0.01054939F,
            -0.06342378F,
             0.21994211F,
            -0.47269619F,
             0.61049324F,
            -0.36150230F,
            -0.13921209F,
             0.32678680F,
            -0.01672709F,
            -0.22829105F,
             0.03985025F,
             0.15545875F,
            -0.02471683F,
            -0.10229172F,
             0.00563225F,
             0.06172290F,
             0.00587468F,
            -0.03229430F,
            -0.00878932F,
             0.01381053F,
             0.00672163F,
            -0.00442054F,
            -0.00358149F,
             0.00083156F,
             0.00139256F,
             0.00005350F,
            -0.00038510F,
            -0.00010153F,
             0.00006774F,
             0.00003711F,
            -0.00000438F,
            -0.00000724F,
            -0.00000101F,
             0.00000068F,
             0.00000026F,
            -0.00000000F,
            -0.00000002F,
            -0.00000000F,
            -0.00000000F
        };
    }
}
