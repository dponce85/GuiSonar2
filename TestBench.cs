using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

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

            // Do process and pack vars
            float delta = freqban(wp);
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
