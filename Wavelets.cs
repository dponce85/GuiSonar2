using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exocortex.DSP;

namespace GuiSonar2
{
    public partial class Form1
    {
        WaveletPacket descompowav44(float[] xx, float[] hw0, float[] gw0, float fs)
        {
            WaveletPacket wp = new WaveletPacket();
            wp.fs1 = fs / 32;

            float[] B1, B2;
            float[] B41, B42, B43, B44;

            dwt(xx,  hw0, gw0, out B1,  out B2); // Level 1
            dwt(B1,  hw0, gw0, out B1,  out B2); // Level 2
            dwt(B1,  hw0, gw0, out B1,  out B2); // Level 3
            dwt(B1,  hw0, gw0, out B41, out B42); // Level 4
            dwt(B2,  hw0, gw0, out B43, out B44); // Level 4
            dwt(B41, hw0, gw0, out wp.B51, out wp.B52); // Level 5
            dwt(B42, hw0, gw0, out wp.B53, out wp.B54); // Level 5
            dwt(B43, hw0, gw0, out wp.B55, out wp.B56); // Level 5
            dwt(B44, hw0, gw0, out wp.B57, out wp.B58); // Level 5

            return wp;
        }

        float freqban(WaveletPacket wp)
        {
            // function [F51,F52,F53,F54,F55,F56,F57,F58,f,delta]=freqban(B51,B52,B53,B54,B55,B56,B57,B58,fsb)

            float fsb = wp.fs1;

            Fourier.RFFT(wp.B51, FourierDirection.Forward);
            Fourier.RFFT(wp.B52, FourierDirection.Forward);
            Fourier.RFFT(wp.B53, FourierDirection.Forward);
            Fourier.RFFT(wp.B54, FourierDirection.Forward);
            Fourier.RFFT(wp.B55, FourierDirection.Forward);
            Fourier.RFFT(wp.B56, FourierDirection.Forward);
            Fourier.RFFT(wp.B57, FourierDirection.Forward);
            Fourier.RFFT(wp.B58, FourierDirection.Forward);

            float[][] B5x = new float[][] { wp.B51, wp.B52, wp.B53, wp.B54, wp.B55, wp.B56, wp.B57, wp.B58 };
            for (int ax = 0; ax < B5x.Length; ax++)
            {
                for (int i = 0; i < 10; i++)
                    B5x[ax][i] = 0;

                abs(B5x[ax]);
                divide(B5x[ax], max(B5x[ax]));
            }

            float delta = fsb / (2 * wp.B51.Length);
            return delta;
        }


        void detectonban(WaveletPacket wp)
        { 
            // function [mav,ct,posva1]=detectonban(F51,F52,F53,F54,F55,F56,F57,F58);

            float pg = 0.5f;
            int ct1 = 0, ct2 = 0, ct3 = 0, ct4 = 0, ct5 = 0, ct6 = 0, ct7 = 0, ct8 = 0;

            while (pg<1)
            {
                find(wp.B51, pg, FindMethod.Greater);

                ct1 += length(find(wp.B51, pg, FindMethod.Greater));
                ct2 += length(find(wp.B52, pg, FindMethod.Greater));
                ct3 += length(find(wp.B53, pg, FindMethod.Greater));
                ct4 += length(find(wp.B54, pg, FindMethod.Greater));
                ct5 += length(find(wp.B55, pg, FindMethod.Greater));
                ct6 += length(find(wp.B56, pg, FindMethod.Greater));
                ct7 += length(find(wp.B57, pg, FindMethod.Greater));
                ct8 += length(find(wp.B58, pg, FindMethod.Greater));
                pg += 0.15f;
            }

            int[] ct = new int[]{ct1, ct2, ct3, ct4, ct5, ct6, ct7, ct8};
            // [mav,posva1] = min(ct);
        }



        void abs(float[] data)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] = Math.Abs(data[i]);
        }

        float max(float[] data)
        {
            return data.Max();
        }

        void divide(float[] data, float divisor)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] /= divisor;
        }

        int length(float[] data)
        {
            return data.Length;
        }

        int length(int[] data)
        {
            return data.Length;
        }

        void divide(float[] data, float[] divisor)
        {
            if (data.Length != divisor.Length)
                throw new ArgumentException("Dimensions do not match.");

            for (int i = 0; i < data.Length; i++)
                data[i] /= divisor[i];
        }
    }

    public struct WaveletPacket
    {
        public float[] B51;
        public float[] B52;
        public float[] B53;
        public float[] B54;
        public float[] B55;
        public float[] B56;
        public float[] B57;
        public float[] B58;

        public float fs1;
    }
}
