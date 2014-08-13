using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using FFTWSharp;
using System.Runtime.InteropServices;

namespace GuiSonar2
{
    partial class Form1
    {
        void test1_dwt()
        {
            float[] xx = LoadTxtVar("xx");

            // Testbench
            float[] B11, B12;
            dwt(xx, hw0, gw0, out B11, out B12);

            float[] xx_ref = LoadTxtVar("xx");
            float[] B11_ref = LoadTxtVar("B11");
            float[] B12_ref = LoadTxtVar("B12");

            PlotVar(B11, "B11");
            PlotVar(B11_ref, "B11_ref");


            // Calculate SNR
            double snr_B11 = getSNR(B11, B11_ref);
            double snr_B12 = getSNR(B12, B12_ref);

            Console.WriteLine("SNR B11 = " + snr_B11);
            Console.WriteLine("SNR B12 = " + snr_B12);
        }

        void test2_dwt()
        {
            var xx = new float[] { 1, 2, 3, 4, 5 };
            var hw0 = new float[] { 1 };
            var gw0 = new float[] { -1 };            

            float[] B11, B12;
            dwt(xx, hw0, gw0, out B11, out B12);

            // B11 = { 2,  4};
            // B12 = {-2, -4};
        }

        void test_descompowav44()
        {
            float[] xx = LoadTxtVar("xx");

            float fs = 22050;
            WaveletPacket wp = descompowav44(xx, hw0, gw0, fs);

            var B5x = new float[][]{
                wp.B51, wp.B52, 
                wp.B53, wp.B54, 
                wp.B55, wp.B56, 
                wp.B57, wp.B58
            };

            var B5x_ref = new float[][]{
                LoadTxtVar("B51"),
                LoadTxtVar("B52"),
                LoadTxtVar("B53"),
                LoadTxtVar("B54"),
                LoadTxtVar("B55"),
                LoadTxtVar("B56"),
                LoadTxtVar("B57"),
                LoadTxtVar("B58")
            };


            PlotVar(xx, "xx");

            double snr_B5x = 0;
            for (int i = 0; i < 8; i++)
            {
                snr_B5x = getSNRdB(B5x[i], B5x_ref[i]);
                Console.WriteLine("SNR(dB) B5" + i + " = " + snr_B5x);
                
                PlotVar(B5x[i], "B5" + i);
                PlotVar(B5x_ref[i], "B5" + i + "_ref");
            }

            // Observations:
            // Low SNRs (< 10 dB) at B51 band
        }

        void test_envolventeban()
        {
            // TODO:
        }

        void test_freqban()
        {

            // Load inputs
            WaveletPacket wp = new WaveletPacket();
            wp.B51 = LoadTxtVar("B51");
            wp.B52 = LoadTxtVar("B52");
            wp.B53 = LoadTxtVar("B53");
            wp.B54 = LoadTxtVar("B54");
            wp.B55 = LoadTxtVar("B55");
            wp.B56 = LoadTxtVar("B56");
            wp.B57 = LoadTxtVar("B57");
            wp.B58 = LoadTxtVar("B58");
            wp.fs1 = 229.6875f;


            // Do process
            freqban(wp);
            float delta = wp.fs1 / (2 * wp.B51.Length);


            // Pack vars
            var F5x = new float[][]{
                wp.B51, wp.B52, 
                wp.B53, wp.B54, 
                wp.B55, wp.B56, 
                wp.B57, wp.B58
            };


            // Load expected outputs
            var F5x_ref = new float[][]{
                LoadTxtVar("F51"),
                LoadTxtVar("F52"),
                LoadTxtVar("F53"),
                LoadTxtVar("F54"),
                LoadTxtVar("F55"),
                LoadTxtVar("F56"),
                LoadTxtVar("F57"),
                LoadTxtVar("F58")
            };

            // Compare
            double snr_F5x = 0;
            for (int i = 0; i < 8; i++)
            {
                snr_F5x = getSNRdB(F5x[i], F5x_ref[i]);
                Console.WriteLine("SNR(dB) F5" + (i + 1) + " = " + snr_F5x);

                PlotVar(F5x[i], "F5" + (i + 1));
                PlotVar(F5x_ref[i], "F5" + (i + 1) + "_ref");
            }
        }

        

        public void test_FFT1(int n)
        {
            // FFTW test
            // int n = inputSignal.Length;

            // and two more for double FFTW            
            var din = new double[n * 2];
            var dout = new double[n * 2];

            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(din, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);

            // create a few test transforms
            IntPtr fplan5 = fftw.dft_1d(n, hdin.AddrOfPinnedObject(), hdout.AddrOfPinnedObject(),
                fftw_direction.Forward, fftw_flags.Estimate);

            // Fill with sawtooth-like signal
            for (int i = 0; i < n * 2; i = i + 2)
            {
                din[i] = i % 50;
                din[i + 1] = 0;
            }

            // Tests a single plan, displaying results
            fftwf.execute(fplan5);

            // Free resources
            fftwf.destroy_plan(fplan5);
            hdin.Free();
            hdout.Free();

            // System.Console.WriteLine("Testing managed interface:\n");
            PlotVar(din, "Input, before FFT");
            PlotVar(dout, "Output, before FFT");

            // mplan.Execute();

            PlotVar(din, "Input, after FFT");
            PlotVar(dout, "Output, after FFT");
        }

        public void test_FFT2(int n)
        {
            // FFTW test
            // int n = inputSignal.Length;

            // and two more for double FFTW            
            var din = new double[n];
            var dout = new double[n];


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(din, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);


            // create a few test transforms
            IntPtr fplan6 = fftw.r2r_1d(n,
                hdin.AddrOfPinnedObject(),
                hdout.AddrOfPinnedObject(),
                fftw_kind.R2HC,
                fftw_flags.Estimate);


            // Fill with sawtooth-like signal
            for (int i = 0; i < n; i++)
                din[i] = i % 50;


            // System.Console.WriteLine("Testing managed interface:\n");
            PlotVar(din, "Input, before FFT");
            PlotVar(dout, "Output, before FFT");


            // Tests a single plan, displaying results
            fftwf.execute(fplan6);


            // System.Console.WriteLine("Testing managed interface:\n");
            PlotVar(din, "Input, after FFT");
            PlotVar(dout, "Output, after FFT");



            // create a few test transforms
            IntPtr fplan7 = fftw.r2r_1d(n,
                hdout.AddrOfPinnedObject(),
                hdin.AddrOfPinnedObject(),
                fftw_kind.HC2R,
                fftw_flags.Estimate);


            // Tests a single plan, displaying results
            fftwf.execute(fplan7);


            // Free resources
            fftwf.destroy_plan(fplan6);
            hdin.Free();
            hdout.Free();


            // mplan.Execute();
            PlotVar(dout, "Input, after iFFT");
            PlotVar(din, "Output, after iFFT");
        }


        public void test_FFT3(int n)
        {
            // FFTW test
            // int n = inputSignal.Length;

            // and two more for double FFTW            
            var din = new double[n];
            var dout = new double[2*n];


            // get handles and pin arrays so the GC doesn't move them
            GCHandle hdin = GCHandle.Alloc(din, GCHandleType.Pinned);
            GCHandle hdout = GCHandle.Alloc(dout, GCHandleType.Pinned);


            // create a few test transforms
            IntPtr fplan6 = fftw.dft_r2c_1d(n,
                hdin.AddrOfPinnedObject(),
                hdout.AddrOfPinnedObject(),                
                fftw_flags.Estimate);


            // Fill with sawtooth-like signal
            for (int i = 0; i < n; i++)
                din[i] = i % 50;


            // System.Console.WriteLine("Testing managed interface:\n");
            PlotVar(din, "Input, before FFT");
            PlotVar(dout, "Output, before FFT");


            // Tests a single plan, displaying results
            fftwf.execute(fplan6);


            // System.Console.WriteLine("Testing managed interface:\n");
            PlotVar(din, "Input, after FFT");
            PlotVar(dout, "Output, after FFT");



            // create a few test transforms
            IntPtr fplan7 = fftw.dft_c2r_1d(n,
                hdout.AddrOfPinnedObject(),
                hdin.AddrOfPinnedObject(),                
                fftw_flags.Estimate);


            // Tests a single plan, displaying results
            fftwf.execute(fplan7);


            // Free resources
            fftwf.destroy_plan(fplan6);
            fftwf.destroy_plan(fplan7);
            hdin.Free();
            hdout.Free();


            // mplan.Execute();
            PlotVar(dout, "Input, after iFFT");
            PlotVar(din, "Output, after iFFT");
        }


        void test_detecarm()
        {
            /////  test de detecarm33  ////
            float[] FJA = new float[1024];
            float[] fre = new float[1024];
            float f01 = 1.4274f;
            float delta1 = 0.0150f;
            int   sensar = 25;
            float[] f00 =new float[1];
             FJA = PruebaDetecarm;
            //for (int i = 0; i < FJA.Length; i++)
            //{   if (i >= FJA.Length / 2) FJA[i] = ((FJA.Length - i) * 0.9f) / (float)(FJA.Length/2);
            //    else FJA[i] = (i * 0.9f) / (float)(FJA.Length/2);
            //}

            for (int i = 0; i < fre.Length; i++)
            {  fre[i] = i * 0.15f; }

            f00[0] = detecarm33(FJA, f01, delta1, fre, sensar)[1][0];
            PrintVar(f00, "f00");
        }


        void test_decima()
        {
            ////// test  de  Decima   /////
            float[][] pruBEN = new float[8][];
            float[][] OutDec = new float[6][];
            float f0 = 7.0335f;
            float fsb = 229.6875f;
            float sensi = 30;
            float[] ct = new float[] { 257f, 89f, 18f, 203f, 7f, 8f, 8f, 7f };
            float boc = 1;
            float tipve = 3;

            pruBEN[0] = PruebaBEN0;
            pruBEN[1] = PruebaBEN1;
            pruBEN[2] = PruebaBEN2;
            pruBEN[3] = PruebaBEN3;
            pruBEN[4] = PruebaBEN4;
            pruBEN[5] = PruebaBEN5;
            pruBEN[6] = PruebaBEN6;
            pruBEN[7] = PruebaBEN7;

            OutDec[0] = decima(pruBEN, f0, fsb, sensi, ct, pruebaFJA, boc, tipve)[0];
            PrintVar(OutDec[0], "FJA");
            ////FIN DECIMA
        }


        void test_filter3()
        {
            float[] h = new float[] { 4, 3, 2, 1, 0 };
            float[] B = new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            float[] R;            

            R = filter3(B, h);
            PrintVar(R, "prup");
        }


        void test_envolventeban2()
        {
            //// test de envolventeban /////

            float[][] prup = new float[9][];
            float[] pru8;
            float[] B51k = new float[12440];//{ 0.1320f ,  0.9421f ,   0.9561f ,   0.5752f ,   0.0598f };
            for (int i = 0; i < B51k.Length; i++)
                B51k[i] = (i + 1) / (float)B51k.Length;

            int ord = 100;
            float fc1 = 100;
            float fs1 = 1378.1f;
            pru8 = envolventeban(B51k, B51k, B51k, B51k, B51k, B51k, B51k, B51k, ord, fc1, fs1)[0];
            PrintVar(pru8, "pru8");
        }



        float[] LoadTxtVar(string strVarName)
        {
            string FileName = strVarName;

            string cd = @"C:\Users\DIDT-AETA\Documents\INICTEL\2013\MATLAB\freqCatchGui\";
            FileName = cd + FileName + ".txt";

            var listLine = new List<string>();
            var sr = new StreamReader(FileName);

            string line = null;
            while (true)
            {
                line = sr.ReadLine();
                if (line != null)
                    listLine.Add(line);
                else
                    break;
            }

            sr.Close();

            float[] xx = new float[listLine.Count];
            for (int i = 0; i < xx.Length; i++)
                xx[i] = (float)Double.Parse(listLine[i]);

            return xx;
        }

        void PlotVar(float[] xx, string name, params bool[] isModal)
        {
            var pVars = new PlotVars(xx, name);
            if (isModal.Length == 1)
            {
                if (isModal[0])
                    pVars.ShowDialog();
                else
                    pVars.Show();
            }
            else
                pVars.Show();
        }
        void PlotVar(double[] xx, string name, params bool[] isModal)
        {
            var pVars = new PlotVars(xx, name);
            if (isModal.Length == 1)
            {
                if (isModal[0])
                    pVars.ShowDialog();
                else
                    pVars.Show();
            }
            else
                pVars.Show();
        }

        void PrintVar(float[] xx, string name)
        {
            var pVars = new PrintVars(xx, "B11");
            pVars.Show();
        }

        double getSNR(float[] signal, float[] reference)
        {
            if (signal.Length != reference.Length)
                return Double.NaN;

            double se = Double.Epsilon;
            double ee = Double.Epsilon;

            for (int i = 0; i < signal.Length; i++)
            {
                se += Math.Pow(reference[i], 2);
                ee += Math.Pow(signal[i] - reference[i], 2);
            }

            return se/ee;
        }

        double getSNRdB(float[] signal, float[] reference)
        {
            double snr = getSNR(signal, reference);
            return 10.0 * Math.Log10(snr);
        }
    }
}
