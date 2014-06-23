using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiSonar2
{
    public partial class Form1
    {
        void filter()
        {
            int N = 4;
            float[] xx = new float[512];
            float[] NumCoeff = new float[5];
            float[] DenCoeff = new float[5];
            float[] Signal = new float[xx.Length];
            Signal = xx;
            float[] FilteredSignal = new float[xx.Length];
            float[] Reg = new float[100];
            int NumSigPts;
            //  FilteredSignal = zeros(size(x));
            //  Reg = zeros(size(x));


            for (int i = 0; i < xx.Length; i++)
            {

                FilteredSignal[i] = 0;

            }

            for (int i = 0; i < 100; i++)
            {
                Reg[i] = 0;
            }

            NumSigPts = Signal.Length;

            for (int j = 0; j < NumSigPts; j++)
            {
                for (int k = N; k >= 1; k--)
                {
                    Reg[k] = Reg[k - 1];

                }

                // El denominador
                Reg[0] = Signal[j];

                for (int k = 1; k <= N; k++)
                {
                    Reg[0] = Reg[0] - DenCoeff[k] * Reg[k];

                }

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

        }

        float mean(float[] xx)
        {
            float m = 0;

            for (int i = 0; i < xx.Length; i++)
            {
                m = m + xx[i];
            }

            return (m / (xx.Length));
        }


        float prueba1()
        {
            return 0;
         }

        float[][] envolvente1(float[] x, int N, float fc, float fs)
        {
            float[][] r = new float[2][];
            int M;
            bool[] ip;
            float[] y;
            float av;
            float[] hb = Fir1Coeff;

            M = (int)(fs / (2 * fc));
            // hb = Fir1(N, 2 * fc / fs);
            ip = find(x, 0, FindMethod.Less);
            //x(ip)=0  en matlab;
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = (ip[i]) ? 0 : x[i];
            }

            //mean:
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i] - mean(x);
            }

            //y = conv(x, hb);
            //av = y.Average();
            //for (int i = 0; i < y.Length; i++)
            {
               // y[i] = y[i] - av;
            }


            return null;
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
