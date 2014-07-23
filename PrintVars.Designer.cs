namespace GuiSonar2
{
    partial class PrintVars
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtVarData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtVarData
            // 
            this.txtVarData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVarData.Location = new System.Drawing.Point(0, 0);
            this.txtVarData.Multiline = true;
            this.txtVarData.Name = "txtVarData";
            this.txtVarData.Size = new System.Drawing.Size(284, 408);
            this.txtVarData.TabIndex = 0;
            // 
            // PrintVars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 408);
            this.Controls.Add(this.txtVarData);
            this.Name = "PrintVars";
            this.Text = "PrintVars";
            this.Load += new System.EventHandler(this.PrintVars_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVarData;
    }
}