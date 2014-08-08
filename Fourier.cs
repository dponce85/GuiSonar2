using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using FFTWSharp;

namespace GuiSonar2
{
    public static class Fourier
    {
        private static double[] b_inputReal = new double[44100];
        private static double[] b_outputReal = new double[44100];

        public enum FourierDirection : int { Forward, Backwards };

        public static void freqz(double[] inputReal, double[] outputReal)
        {
            // FFTW test
            int n_in = Math.Min(inputReal.Length, outputReal.Length);
            int n_out = 2 * outputReal.Length;


            // Check if tempSignal has enough space, grow if necessary
            if (b_outputReal.Length < n_out)
            {
                b_inputReal = new double[n_out];
                b_outputReal = new double[n_out];
            }
            else
            {
                // Block copy input signal                
                Buffer.BlockCopy(inputReal, 0, b_inputReal, 0, n_in * sizeof(double));
                Array.Clear(b_inputReal, n_in, n_out);
            }


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(b_inputReal, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(b_outputReal, GCHandleType.Pinned);


            // create a few test transforms
            IntPtr fplan6 = fftw.r2r_1d(n_out,
                hdin.AddrOfPinnedObject(),
                hdout.AddrOfPinnedObject(),
                fftw_kind.R2HC,
                fftw_flags.Estimate);


            // Tests a single plan, displaying results
            fftwf.execute(fplan6);


            // Free resources
            fftwf.destroy_plan(fplan6);
            hdin.Free();
            hdout.Free();


            // Copy valid samples
            Buffer.BlockCopy(b_outputReal, 0, outputReal, 0, n_in * sizeof(double));            
        }


        public static void freqc(double[] input_Real, double[] output_HalfComplex)
        {
            // FFTW test
            int n_in = Math.Min(input_Real.Length, output_HalfComplex.Length);
            int n_out = output_HalfComplex.Length;


            // Check if tempSignal has enough space, grow if necessary
            if (b_outputReal.Length < n_out)
            {
                b_inputReal = new double[n_out];
                b_outputReal = new double[n_out];
            }
            else
            {
                // Block copy input signal
                Array.Clear(b_inputReal, 0, n_out);
                Buffer.BlockCopy(input_Real, 0, b_inputReal, 0, n_in * sizeof(double));
            }


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(b_inputReal, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(b_outputReal, GCHandleType.Pinned);


            // create a few test transforms
            IntPtr fplan6 = fftw.r2r_1d(n_out,
                hdin.AddrOfPinnedObject(),
                hdout.AddrOfPinnedObject(),
                fftw_kind.R2HC,
                fftw_flags.Estimate);


            // Tests a single plan, displaying results
            fftwf.execute(fplan6);


            // Free resources
            fftwf.destroy_plan(fplan6);
            hdin.Free();
            hdout.Free();


            // Copy valid samples
            Buffer.BlockCopy(b_outputReal, 0, output_HalfComplex, 0, n_out * sizeof(double));
        }


        private static void genRFFT(double[] inputSignal, double[] outputSignal, FourierDirection fd)
        {
            // FFTW test
            int n = inputSignal.Length;


            // Check if outputSignal has enough space
            if (outputSignal.Length < n)
                throw new ArgumentException("Input and output lengths don't match");


            // Alias for double arrays
            var din = inputSignal;
            var dout = outputSignal;


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(din, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);


            // create a few test transforms
            IntPtr fplan6 = fftw.r2r_1d(n,
                hdin.AddrOfPinnedObject(),
                hdout.AddrOfPinnedObject(),
                (fd == FourierDirection.Forward) ? fftw_kind.R2HC : fftw_kind.HC2R,
                fftw_flags.Estimate);


            // Tests a single plan, displaying results
            fftwf.execute(fplan6);


            // Free resources
            fftwf.destroy_plan(fplan6);
            hdin.Free();
            hdout.Free();
        }

        public static void FFT(double[] input_Real, double[] output_HalfComplex)
        {
            genRFFT(input_Real, output_HalfComplex, FourierDirection.Forward);
        }

        public static void IFFT(double[] input_HalfComplex, double[] output_Real)
        {
            genRFFT(input_HalfComplex, output_Real, FourierDirection.Backwards);
        }
    }
}
