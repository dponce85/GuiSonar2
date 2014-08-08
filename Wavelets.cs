using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        void freqban(WaveletPacket wp)
        {
            /*

            // function [F51,F52,F53,F54,F55,F56,F57,F58,f,delta]=freqban(B51,B52,B53,B54,B55,B56,B57,B58,fsb)

            // Pack vars
            var B5x = new float[][]{
                wp.B51, wp.B52, 
                wp.B53, wp.B54, 
                wp.B55, wp.B56, 
                wp.B57, wp.B58
            };


            // Create array in memory to perform complex FFT
            int sLength = wp.B51.Length;
            int tLength = nextpow2(sLength);
            var tSignal = new float[tLength];

            // For every band
            for (int j = 0; j < 8; j++)
            {
                // Copy signal coefficients to array
                for (int i = 0; i < sLength; i++)
                    tSignal[i].Re = B5x[j][i];


                // Recommended: hann window over signal coeff!

                
                // Perform FFT
                Fourier.FFT(tSignal, FourierDirection.Forward);

                
                // Recommended: compensate hann window attn! (x2)


                // Clear first 10 coeff (some kind of highpass)
                for (int i = 0; i < 10; i++)
                    tSignal[i] = new ComplexF(0, 0);

                // Normalize
                B5x[j] = normalize(tSignal);
            }


            // Save vars
            wp.B51 = B5x[0];
            wp.B52 = B5x[1];
            wp.B53 = B5x[2];
            wp.B54 = B5x[3];
            wp.B55 = B5x[4];
            wp.B56 = B5x[5];
            wp.B57 = B5x[6];
            wp.B58 = B5x[7];

            */
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



        /*float[] normalize(float[] data)
        {
            float[] ret = new float[data.Length / 2];            

            // Get Abs. value
            for (int i = 0; i < ret.Length; i++)
                ret[i] = data[i].GetModulus();

            // Get Max value
            float maxRet = max(ret);

            // Normalize
            for (int i = 0; i < ret.Length; i++)
                ret[i] = ret[i] / maxRet;

            return ret;
        }*/

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

    public class WaveletPacket
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
