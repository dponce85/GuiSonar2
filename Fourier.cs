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
        private static double[] tempInputSignal = new double[44100];
        private static double[] tempOutputSignal = new double[44100];

        public enum FourierDirection : int { Forward, Backwards };

        public static void freqz(double[] inputSignal, double[] outputSignal)
        {
            // FFTW test
            int n_in = Math.Min(inputSignal.Length, outputSignal.Length);
            int n_out = 2 * outputSignal.Length;


            // Check if tempSignal has enough space, grow if necessary
            if (tempOutputSignal.Length < n_out)
            {
                tempInputSignal = new double[n_out];
                tempOutputSignal = new double[n_out];
            }
            else
            {
                // Block copy input signal
                Array.Clear(tempInputSignal, 0, n_out);
                Buffer.BlockCopy(inputSignal, 0, tempInputSignal, 0, n_in * sizeof(double));
            }


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(tempInputSignal, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(tempOutputSignal, GCHandleType.Pinned);


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
            Buffer.BlockCopy(tempOutputSignal, 0, outputSignal, 0, n_in * sizeof(double));            
        }


        public static void freqc(double[] inputSignal, double[] outputSignal)
        {
            // FFTW test
            int n_in = Math.Min(inputSignal.Length, outputSignal.Length);
            int n_out = outputSignal.Length;


            // Check if tempSignal has enough space, grow if necessary
            if (tempOutputSignal.Length < n_out)
            {
                tempInputSignal = new double[n_out];
                tempOutputSignal = new double[n_out];
            }
            else
            {
                // Block copy input signal
                Array.Clear(tempInputSignal, 0, n_out);
                Buffer.BlockCopy(inputSignal, 0, tempInputSignal, 0, n_in * sizeof(double));
            }


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(tempInputSignal, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(tempOutputSignal, GCHandleType.Pinned);


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
            Buffer.BlockCopy(tempOutputSignal, 0, outputSignal, 0, n_out * sizeof(double));
        }


        private static void genRFFT(double[] inputSignal, double[] outputSignal, FourierDirection fd)
        {
            // FFTW test
            int n = inputSignal.Length;


            // Check if tempSignal has enough space, grow if necessary
            if (tempOutputSignal.Length < n)
                tempOutputSignal = new double[n];


            // Check if outputSignal has enough space
            if (outputSignal.Length < n)
                throw new ArgumentException("Output array doesn't have enough space");


            // Alias for double arrays
            var din = inputSignal;
            var dtmp = tempOutputSignal;
            var dout = outputSignal;


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(din, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(dtmp, GCHandleType.Pinned);


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


            // Copy valid samples
            Buffer.BlockCopy(dtmp, 0, dout, 0, n * sizeof(double));
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
