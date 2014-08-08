using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization;

namespace GuiSonar2
{
    public partial class PlotVars : Form
    {
        double[] data;

        public PlotVars(float[] data, string name)
        {
            InitializeComponent();

            this.data = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
                this.data[i] = data[i];
            this.Text = name + "   numel: " + data.Length;
        }

        public PlotVars(double[] data, string name)
        {
            InitializeComponent();
            this.data = data;
            this.Text = name + "   numel: " + data.Length;
        }

        private void PlotVars_Load(object sender, EventArgs e)
        {
            int targetLength = 1920*16;
            chart1.Series[0].Points.Clear();

            if (data.Length < targetLength)
            {
                for (int i = 0; i < data.Length; i++)
                    chart1.Series[0].Points.AddY(data[i]);
            }
            else
            {
                double step = 1.0 * data.Length / targetLength;

                for (double i = 0; i < targetLength; i += step)
                    chart1.Series[0].Points.AddY(data[(int)i]);
            }

            chart1.Update();
        }
    }
}