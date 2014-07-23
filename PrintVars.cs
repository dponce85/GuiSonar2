using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuiSonar2
{
    public partial class PrintVars : Form
    {
        float[] data = null;

        public PrintVars(float[] data, string name)
        {
            InitializeComponent();
            this.Text = name;
            this.data = (float[])data.Clone();
        }

        private void PrintVars_Load(object sender, EventArgs e)
        {
            string VarDataTxt = "Longitud: " + data.Length + "\r\n\r\n";

            for (int i = 0; i < data.Length; i++)
                VarDataTxt += String.Format("{0:F8}, \r\n", data[i]);

            txtVarData.Text = VarDataTxt;
        }
    }
}
