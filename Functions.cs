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

    }
}
