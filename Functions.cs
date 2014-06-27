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

        float[][] envolvente1(Matrix x, int N, float fc, float fs)
        {
            float[][] r = new float[2][];
            int M;
            float[] y;
            float[] hb = Fir1Coeff;

            M = (int)(fs / (2 * fc));
            //hb = Fir1(N, 2 * fc / fs);

            for (int i = 0; i < x.Length; i++)
                if (x[i] < 0) x[i] = 0;

            //mean:
            x = x - mean(x);

            y = conv(double2float(x), hb, ConvMethod.full);
            Matrix y2 = new double[y.Length % M];
            for (int i = 0; i < y.Length; i = i + M)
            {
                y2[i] = y[i];
            }

            y2 = y2 - mean(y2);

            r[0] = double2float(y2);
            r[1][0] = fs / M;

            return r;
        }

        float[][] envolventeban(Matrix B51k, Matrix B52k, Matrix B53k, Matrix B54k, Matrix B55k, Matrix B56k, Matrix B57k, Matrix B58k, int ord, float fc1, float fs1)
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



        float[][] decima(Matrix BEN, float f0, float fsb, float sensi, float ct, float FAV, float boc, float tipve)
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



        private float[] double2float(Matrix x)
        {
            throw new NotImplementedException();
        }

        private float[] mean(float[] y)
        {
            throw new NotImplementedException();
        }

        private bool[] find(Matrix x, int p, FindMethod findMethod)
        {
            throw new NotImplementedException();
        }

        private Matrix mean(Matrix x)
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

        double[] my_fir1(int N, float Wn)
{

const float PI =(float)Math.PI;
    int Pr_L = N+1;         
/* FIR filter
Return an array of 1 X 256
*/
   
int odd, i, j, nhlf, i1;
float f1, gain, c1;
float[] wind = new float[Pr_L/2];
float[] xn = new float[Pr_L/2];
float[] b  = new float[Pr_L/2];
float[] c  = new float[Pr_L/2];
float[] c3 = new float[Pr_L/2];
float[] bb = new float[Pr_L];

//bb = (double *) malloc(sizeof(double) * Pr_L);

gain = 0;
N = N+1;
odd = N - (N/2)*2; /* odd = rem(N,2) */

/*wind = hamming(N);*/
for (i=0; i < Pr_L; i++)
{
wind[i] = (float)(0.54 -(0.46 * Math.Cos ((2 *PI* i) / (N-1))));
}

f1 = Wn / 2.0f;
c1 = f1;
nhlf = (N+1) / 2;
i1 = odd + 1;

/* Lowpass */

if(odd!=0)
b[0] = 2 * c1;

for (i=0; i < nhlf; i++)
{
xn[i] = i + 0.5f * (1 - odd);
}

for (i=0; i < nhlf; i++)
{
c[i] = PI*xn[i];
}

for (i=0; i < nhlf; i++)
{
c3[i] = 2 * c1 * c[i];
}

/* b(i1:nhlf)=(sin(c3)./c) */
for (i=0; i < nhlf; i++)
{
b[i] = (float)Math.Sin(c3[i]) / c[i];
}

/* bb = real([b(nhlf:-1:i1) b(1:nhlf)].*wind(:)') */
for (i=0,j=nhlf-1; i < nhlf; i++, j--)
{
bb[i] = b[j];
}
for (i=nhlf,j=0; i < Pr_L; i++,j++)
{
bb[i] = b[j];
}
for (i=0; i < Pr_L; i++)
{
bb[i] = bb[i] * wind[i];
}

/* gain = abs(polyval(b,1)); */
for (i=0; i < Pr_L; i++)
{
gain += bb[i];
}
/* b = b / gain */
for (i=0; i < Pr_L; i++)
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
