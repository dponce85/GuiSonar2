namespace GuiSonar2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkDetEngine = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textPointerMag = new System.Windows.Forms.TextBox();
            this.textPointerTime = new System.Windows.Forms.TextBox();
            this.textPointerFrec = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelHertz = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDemon = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.txtDetRpm = new System.Windows.Forms.TextBox();
            this.txtFstop = new System.Windows.Forms.TextBox();
            this.txtForder = new System.Windows.Forms.TextBox();
            this.txtFreq2 = new System.Windows.Forms.TextBox();
            this.txtFreq1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBufferTime = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pushbuttonBufferFall = new System.Windows.Forms.Button();
            this.pushbuttonBufferRise = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textLowFrec = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textUmbralPin = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textAnchoPin = new System.Windows.Forms.TextBox();
            this.pushbuttonReportPin = new System.Windows.Forms.Button();
            this.sliderLowFrec = new System.Windows.Forms.HScrollBar();
            this.sliderUmbralPin = new System.Windows.Forms.HScrollBar();
            this.sliderAnchoPin = new System.Windows.Forms.HScrollBar();
            this.chkDetPin = new System.Windows.Forms.CheckBox();
            this.textFrecPin = new System.Windows.Forms.TextBox();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.glControl1 = new OpenTK.GLControl();
            this.glControl2 = new OpenTK.GLControl();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.panel6 = new System.Windows.Forms.Panel();
            this.textBox9K = new System.Windows.Forms.TextBox();
            this.textBox7K = new System.Windows.Forms.TextBox();
            this.textBox5K = new System.Windows.Forms.TextBox();
            this.textBox3K = new System.Windows.Forms.TextBox();
            this.textBox11K = new System.Windows.Forms.TextBox();
            this.textBox2K = new System.Windows.Forms.TextBox();
            this.textBox10K = new System.Windows.Forms.TextBox();
            this.textBox8K = new System.Windows.Forms.TextBox();
            this.textBox6K = new System.Windows.Forms.TextBox();
            this.textBox4K = new System.Windows.Forms.TextBox();
            this.textBox1K = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.glControlTicks = new OpenTK.GLControl();
            this.glControlBar1 = new OpenTK.GLControl();
            this.glControlBar2 = new OpenTK.GLControl();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkDetEngine
            // 
            this.chkDetEngine.AutoSize = true;
            this.chkDetEngine.Checked = true;
            this.chkDetEngine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDetEngine.Location = new System.Drawing.Point(3, 3);
            this.chkDetEngine.Name = "chkDetEngine";
            this.chkDetEngine.Size = new System.Drawing.Size(105, 17);
            this.chkDetEngine.TabIndex = 2;
            this.chkDetEngine.Text = "Detectar Hélices";
            this.chkDetEngine.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "T";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "F";
            // 
            // textPointerMag
            // 
            this.textPointerMag.Location = new System.Drawing.Point(55, 75);
            this.textPointerMag.Name = "textPointerMag";
            this.textPointerMag.Size = new System.Drawing.Size(42, 20);
            this.textPointerMag.TabIndex = 8;
            // 
            // textPointerTime
            // 
            this.textPointerTime.Location = new System.Drawing.Point(55, 42);
            this.textPointerTime.Name = "textPointerTime";
            this.textPointerTime.Size = new System.Drawing.Size(42, 20);
            this.textPointerTime.TabIndex = 7;
            // 
            // textPointerFrec
            // 
            this.textPointerFrec.Location = new System.Drawing.Point(55, 11);
            this.textPointerFrec.Name = "textPointerFrec";
            this.textPointerFrec.Size = new System.Drawing.Size(42, 20);
            this.textPointerFrec.TabIndex = 6;
            this.textPointerFrec.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "W/Hz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "mm:ss";
            // 
            // labelHertz
            // 
            this.labelHertz.AutoSize = true;
            this.labelHertz.Location = new System.Drawing.Point(103, 15);
            this.labelHertz.Name = "labelHertz";
            this.labelHertz.Size = new System.Drawing.Size(20, 13);
            this.labelHertz.TabIndex = 3;
            this.labelHertz.Text = "Hz";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "RPM";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(67, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "RPMm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Orden";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(68, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Fmax";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Fmin";
            // 
            // btnDemon
            // 
            this.btnDemon.Location = new System.Drawing.Point(82, 135);
            this.btnDemon.Name = "btnDemon";
            this.btnDemon.Size = new System.Drawing.Size(62, 23);
            this.btnDemon.TabIndex = 18;
            this.btnDemon.Text = "DEMON";
            this.btnDemon.UseVisualStyleBackColor = true;
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(10, 135);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(57, 23);
            this.btnAuto.TabIndex = 17;
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            // 
            // txtDetRpm
            // 
            this.txtDetRpm.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtDetRpm.Location = new System.Drawing.Point(54, 100);
            this.txtDetRpm.Multiline = true;
            this.txtDetRpm.Name = "txtDetRpm";
            this.txtDetRpm.Size = new System.Drawing.Size(63, 27);
            this.txtDetRpm.TabIndex = 16;
            // 
            // txtFstop
            // 
            this.txtFstop.Location = new System.Drawing.Point(98, 68);
            this.txtFstop.Name = "txtFstop";
            this.txtFstop.Size = new System.Drawing.Size(42, 20);
            this.txtFstop.TabIndex = 15;
            // 
            // txtForder
            // 
            this.txtForder.Location = new System.Drawing.Point(20, 68);
            this.txtForder.Name = "txtForder";
            this.txtForder.Size = new System.Drawing.Size(42, 20);
            this.txtForder.TabIndex = 14;
            // 
            // txtFreq2
            // 
            this.txtFreq2.BackColor = System.Drawing.SystemColors.Menu;
            this.txtFreq2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFreq2.Location = new System.Drawing.Point(98, 39);
            this.txtFreq2.Name = "txtFreq2";
            this.txtFreq2.Size = new System.Drawing.Size(42, 13);
            this.txtFreq2.TabIndex = 12;
            this.txtFreq2.Text = "---";
            // 
            // txtFreq1
            // 
            this.txtFreq1.BackColor = System.Drawing.SystemColors.Menu;
            this.txtFreq1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFreq1.Location = new System.Drawing.Point(20, 39);
            this.txtFreq1.Name = "txtFreq1";
            this.txtFreq1.Size = new System.Drawing.Size(42, 13);
            this.txtFreq1.TabIndex = 13;
            this.txtFreq1.Text = "---";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(135, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(12, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "s";
            // 
            // txtBufferTime
            // 
            this.txtBufferTime.Location = new System.Drawing.Point(94, 30);
            this.txtBufferTime.Name = "txtBufferTime";
            this.txtBufferTime.Size = new System.Drawing.Size(35, 20);
            this.txtBufferTime.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Conf. Bloque";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(74, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "T";
            // 
            // pushbuttonBufferFall
            // 
            this.pushbuttonBufferFall.Image = ((System.Drawing.Image)(resources.GetObject("pushbuttonBufferFall.Image")));
            this.pushbuttonBufferFall.Location = new System.Drawing.Point(27, 27);
            this.pushbuttonBufferFall.Name = "pushbuttonBufferFall";
            this.pushbuttonBufferFall.Size = new System.Drawing.Size(25, 23);
            this.pushbuttonBufferFall.TabIndex = 1;
            this.pushbuttonBufferFall.UseVisualStyleBackColor = true;
            // 
            // pushbuttonBufferRise
            // 
            this.pushbuttonBufferRise.Image = ((System.Drawing.Image)(resources.GetObject("pushbuttonBufferRise.Image")));
            this.pushbuttonBufferRise.Location = new System.Drawing.Point(3, 27);
            this.pushbuttonBufferRise.Name = "pushbuttonBufferRise";
            this.pushbuttonBufferRise.Size = new System.Drawing.Size(25, 23);
            this.pushbuttonBufferRise.TabIndex = 0;
            this.pushbuttonBufferRise.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(105, 153);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(20, 13);
            this.label21.TabIndex = 32;
            this.label21.Text = "Hz";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 154);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 31;
            this.label20.Text = "PIN :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(114, 111);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(20, 13);
            this.label18.TabIndex = 30;
            this.label18.Text = "Hz";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 111);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 13);
            this.label19.TabIndex = 28;
            this.label19.Text = "Detectar desde:";
            // 
            // textLowFrec
            // 
            this.textLowFrec.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textLowFrec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textLowFrec.Location = new System.Drawing.Point(89, 111);
            this.textLowFrec.Name = "textLowFrec";
            this.textLowFrec.Size = new System.Drawing.Size(26, 13);
            this.textLowFrec.TabIndex = 29;
            this.textLowFrec.Text = "750";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(95, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "dB";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(22, 68);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "umbral:";
            // 
            // textUmbralPin
            // 
            this.textUmbralPin.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textUmbralPin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textUmbralPin.Location = new System.Drawing.Point(68, 68);
            this.textUmbralPin.Name = "textUmbralPin";
            this.textUmbralPin.Size = new System.Drawing.Size(30, 13);
            this.textUmbralPin.TabIndex = 26;
            this.textUmbralPin.Text = "-100";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(95, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 24;
            this.label15.Text = "Hz";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "ancho:";
            // 
            // textAnchoPin
            // 
            this.textAnchoPin.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textAnchoPin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textAnchoPin.Location = new System.Drawing.Point(69, 23);
            this.textAnchoPin.Name = "textAnchoPin";
            this.textAnchoPin.Size = new System.Drawing.Size(29, 13);
            this.textAnchoPin.TabIndex = 13;
            this.textAnchoPin.Text = "300";
            // 
            // pushbuttonReportPin
            // 
            this.pushbuttonReportPin.Location = new System.Drawing.Point(31, 179);
            this.pushbuttonReportPin.Name = "pushbuttonReportPin";
            this.pushbuttonReportPin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pushbuttonReportPin.Size = new System.Drawing.Size(75, 23);
            this.pushbuttonReportPin.TabIndex = 12;
            this.pushbuttonReportPin.Text = "Reporte";
            this.pushbuttonReportPin.UseVisualStyleBackColor = true;
            // 
            // sliderLowFrec
            // 
            this.sliderLowFrec.LargeChange = 1;
            this.sliderLowFrec.Location = new System.Drawing.Point(26, 124);
            this.sliderLowFrec.Name = "sliderLowFrec";
            this.sliderLowFrec.Size = new System.Drawing.Size(80, 17);
            this.sliderLowFrec.TabIndex = 11;
            this.sliderLowFrec.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderLowFrec_Scroll);
            // 
            // sliderUmbralPin
            // 
            this.sliderUmbralPin.LargeChange = 1;
            this.sliderUmbralPin.Location = new System.Drawing.Point(25, 84);
            this.sliderUmbralPin.Name = "sliderUmbralPin";
            this.sliderUmbralPin.Size = new System.Drawing.Size(80, 17);
            this.sliderUmbralPin.TabIndex = 10;
            this.sliderUmbralPin.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sliderUmbralPin_Scroll);
            // 
            // sliderAnchoPin
            // 
            this.sliderAnchoPin.LargeChange = 1;
            this.sliderAnchoPin.Location = new System.Drawing.Point(26, 39);
            this.sliderAnchoPin.Name = "sliderAnchoPin";
            this.sliderAnchoPin.Size = new System.Drawing.Size(80, 17);
            this.sliderAnchoPin.TabIndex = 9;
            this.sliderAnchoPin.Scroll += new System.Windows.Forms.ScrollEventHandler(this.sldFreq2_Scroll);
            // 
            // chkDetPin
            // 
            this.chkDetPin.AutoSize = true;
            this.chkDetPin.Location = new System.Drawing.Point(3, 3);
            this.chkDetPin.Name = "chkDetPin";
            this.chkDetPin.Size = new System.Drawing.Size(85, 17);
            this.chkDetPin.TabIndex = 0;
            this.chkDetPin.Text = "Detectar Pin";
            this.chkDetPin.UseVisualStyleBackColor = true;
            this.chkDetPin.CheckedChanged += new System.EventHandler(this.chkDetPin_CheckedChanged);
            // 
            // textFrecPin
            // 
            this.textFrecPin.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textFrecPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFrecPin.ForeColor = System.Drawing.Color.Green;
            this.textFrecPin.Location = new System.Drawing.Point(49, 148);
            this.textFrecPin.Multiline = true;
            this.textFrecPin.Name = "textFrecPin";
            this.textFrecPin.Size = new System.Drawing.Size(56, 25);
            this.textFrecPin.TabIndex = 23;
            this.textFrecPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayPause.Image")));
            this.btnPlayPause.Location = new System.Drawing.Point(9, 566);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPlayPause.Size = new System.Drawing.Size(26, 23);
            this.btnPlayPause.TabIndex = 9;
            this.btnPlayPause.UseVisualStyleBackColor = true;
            // 
            // btnRecord
            // 
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.Location = new System.Drawing.Point(59, 566);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRecord.Size = new System.Drawing.Size(26, 23);
            this.btnRecord.TabIndex = 11;
            this.btnRecord.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(34, 566);
            this.btnStop.Name = "btnStop";
            this.btnStop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnStop.Size = new System.Drawing.Size(26, 23);
            this.btnStop.TabIndex = 12;
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnPlayPause);
            this.panel1.Controls.Add(this.btnRecord);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(560, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 613);
            this.panel1.TabIndex = 16;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.pushbuttonReportPin);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.chkDetPin);
            this.panel5.Controls.Add(this.label20);
            this.panel5.Controls.Add(this.textFrecPin);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.sliderLowFrec);
            this.panel5.Controls.Add(this.textAnchoPin);
            this.panel5.Controls.Add(this.textLowFrec);
            this.panel5.Controls.Add(this.label19);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.sliderAnchoPin);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Controls.Add(this.sliderUmbralPin);
            this.panel5.Controls.Add(this.textUmbralPin);
            this.panel5.Location = new System.Drawing.Point(9, 352);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(154, 209);
            this.panel5.TabIndex = 26;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pushbuttonBufferRise);
            this.panel4.Controls.Add(this.pushbuttonBufferFall);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.txtBufferTime);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Location = new System.Drawing.Point(9, 286);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(154, 60);
            this.panel4.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.btnAuto);
            this.panel3.Controls.Add(this.btnDemon);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.chkDetEngine);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtDetRpm);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtFreq1);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtFstop);
            this.panel3.Controls.Add(this.txtFreq2);
            this.panel3.Controls.Add(this.txtForder);
            this.panel3.Location = new System.Drawing.Point(9, 117);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(154, 163);
            this.panel3.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textPointerFrec);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.labelHertz);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textPointerTime);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textPointerMag);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(9, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 108);
            this.panel2.TabIndex = 23;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(45, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 426);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.glControl1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.glControl2, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.trackBar2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.glControlTicks, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 613);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(45, 33);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(512, 46);
            this.glControl1.TabIndex = 20;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // glControl2
            // 
            this.glControl2.BackColor = System.Drawing.Color.Black;
            this.glControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.glControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.glControl2.Location = new System.Drawing.Point(45, 548);
            this.glControl2.Name = "glControl2";
            this.glControl2.Size = new System.Drawing.Size(512, 62);
            this.glControl2.TabIndex = 21;
            this.glControl2.VSync = false;
            this.glControl2.Load += new System.EventHandler(this.glControl2_Load);
            this.glControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl2_Paint);
            this.glControl2.Resize += new System.EventHandler(this.glControl2_Resize);
            // 
            // trackBar2
            // 
            this.trackBar2.AutoSize = false;
            this.trackBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(42, 530);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar2.Maximum = 11025;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(518, 15);
            this.trackBar2.TabIndex = 23;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.textBox9K);
            this.panel6.Controls.Add(this.textBox7K);
            this.panel6.Controls.Add(this.textBox5K);
            this.panel6.Controls.Add(this.textBox3K);
            this.panel6.Controls.Add(this.textBox11K);
            this.panel6.Controls.Add(this.textBox2K);
            this.panel6.Controls.Add(this.textBox10K);
            this.panel6.Controls.Add(this.textBox8K);
            this.panel6.Controls.Add(this.textBox6K);
            this.panel6.Controls.Add(this.textBox4K);
            this.panel6.Controls.Add(this.textBox1K);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(45, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(512, 24);
            this.panel6.TabIndex = 24;
            // 
            // textBox9K
            // 
            this.textBox9K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox9K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox9K.Location = new System.Drawing.Point(178, 11);
            this.textBox9K.Name = "textBox9K";
            this.textBox9K.Size = new System.Drawing.Size(21, 11);
            this.textBox9K.TabIndex = 10;
            this.textBox9K.Text = "9kHz";
            // 
            // textBox7K
            // 
            this.textBox7K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox7K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox7K.Location = new System.Drawing.Point(133, 11);
            this.textBox7K.Name = "textBox7K";
            this.textBox7K.Size = new System.Drawing.Size(21, 11);
            this.textBox7K.TabIndex = 9;
            this.textBox7K.Text = "7kHz";
            // 
            // textBox5K
            // 
            this.textBox5K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox5K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox5K.Location = new System.Drawing.Point(87, 11);
            this.textBox5K.Name = "textBox5K";
            this.textBox5K.Size = new System.Drawing.Size(21, 11);
            this.textBox5K.TabIndex = 8;
            this.textBox5K.Text = "5kHz";
            // 
            // textBox3K
            // 
            this.textBox3K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox3K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox3K.Location = new System.Drawing.Point(43, 11);
            this.textBox3K.Name = "textBox3K";
            this.textBox3K.Size = new System.Drawing.Size(21, 11);
            this.textBox3K.TabIndex = 7;
            this.textBox3K.Text = "3kHz";
            // 
            // textBox11K
            // 
            this.textBox11K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox11K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox11K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox11K.Location = new System.Drawing.Point(227, 11);
            this.textBox11K.Name = "textBox11K";
            this.textBox11K.Size = new System.Drawing.Size(26, 11);
            this.textBox11K.TabIndex = 6;
            this.textBox11K.Text = "11kHz";
            // 
            // textBox2K
            // 
            this.textBox2K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox2K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox2K.Location = new System.Drawing.Point(24, 11);
            this.textBox2K.Name = "textBox2K";
            this.textBox2K.Size = new System.Drawing.Size(21, 11);
            this.textBox2K.TabIndex = 5;
            this.textBox2K.Text = "2kHz";
            // 
            // textBox10K
            // 
            this.textBox10K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox10K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox10K.Location = new System.Drawing.Point(200, 11);
            this.textBox10K.Name = "textBox10K";
            this.textBox10K.Size = new System.Drawing.Size(26, 11);
            this.textBox10K.TabIndex = 4;
            this.textBox10K.Text = "10kHz";
            // 
            // textBox8K
            // 
            this.textBox8K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox8K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox8K.Location = new System.Drawing.Point(155, 11);
            this.textBox8K.Name = "textBox8K";
            this.textBox8K.Size = new System.Drawing.Size(21, 11);
            this.textBox8K.TabIndex = 3;
            this.textBox8K.Text = "8kHz";
            // 
            // textBox6K
            // 
            this.textBox6K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox6K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox6K.Location = new System.Drawing.Point(110, 11);
            this.textBox6K.Name = "textBox6K";
            this.textBox6K.Size = new System.Drawing.Size(21, 11);
            this.textBox6K.TabIndex = 2;
            this.textBox6K.Text = "6kHz";
            // 
            // textBox4K
            // 
            this.textBox4K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox4K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox4K.Location = new System.Drawing.Point(65, 11);
            this.textBox4K.Name = "textBox4K";
            this.textBox4K.Size = new System.Drawing.Size(21, 11);
            this.textBox4K.TabIndex = 1;
            this.textBox4K.Text = "4kHz";
            // 
            // textBox1K
            // 
            this.textBox1K.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox1K.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1K.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1K.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1K.Location = new System.Drawing.Point(1, 11);
            this.textBox1K.Name = "textBox1K";
            this.textBox1K.Size = new System.Drawing.Size(21, 11);
            this.textBox1K.TabIndex = 0;
            this.textBox1K.Text = "1kHz";
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(42, 514);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.trackBar1.Maximum = 11025;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(518, 16);
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // glControlTicks
            // 
            this.glControlTicks.BackColor = System.Drawing.Color.Black;
            this.glControlTicks.Location = new System.Drawing.Point(3, 85);
            this.glControlTicks.Name = "glControlTicks";
            this.glControlTicks.Size = new System.Drawing.Size(36, 416);
            this.glControlTicks.TabIndex = 25;
            this.glControlTicks.VSync = false;
            this.glControlTicks.Load += new System.EventHandler(this.glControlTicks_Load);
            this.glControlTicks.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlTicks_Paint);
            // 
            // glControlBar1
            // 
            this.glControlBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.glControlBar1.BackColor = System.Drawing.Color.Red;
            this.glControlBar1.Location = new System.Drawing.Point(422, 105);
            this.glControlBar1.Name = "glControlBar1";
            this.glControlBar1.Size = new System.Drawing.Size(1, 400);
            this.glControlBar1.TabIndex = 19;
            this.glControlBar1.VSync = false;
            this.glControlBar1.Load += new System.EventHandler(this.glControlBar1_Load);
            this.glControlBar1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlBar1_Paint);
            // 
            // glControlBar2
            // 
            this.glControlBar2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.glControlBar2.BackColor = System.Drawing.Color.Red;
            this.glControlBar2.ForeColor = System.Drawing.Color.Red;
            this.glControlBar2.Location = new System.Drawing.Point(395, 105);
            this.glControlBar2.Name = "glControlBar2";
            this.glControlBar2.Size = new System.Drawing.Size(1, 400);
            this.glControlBar2.TabIndex = 20;
            this.glControlBar2.VSync = false;
            this.glControlBar2.Load += new System.EventHandler(this.glControlBar2_Load);
            this.glControlBar2.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlBar2_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(732, 613);
            this.Controls.Add(this.glControlBar2);
            this.Controls.Add(this.glControlBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(190, 640);
            this.Name = "Form1";
            this.Text = "Procesador de señales de sonar";
            this.MaximumSizeChanged += new System.EventHandler(this.Form1_MaximumSizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDetEngine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelHertz;
        private System.Windows.Forms.TextBox textPointerMag;
        private System.Windows.Forms.TextBox textPointerTime;
        private System.Windows.Forms.TextBox textPointerFrec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDemon;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.TextBox txtDetRpm;
        private System.Windows.Forms.TextBox txtFstop;
        private System.Windows.Forms.TextBox txtForder;
        private System.Windows.Forms.TextBox txtFreq2;
        private System.Windows.Forms.TextBox txtFreq1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBufferTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button pushbuttonBufferFall;
        private System.Windows.Forms.Button pushbuttonBufferRise;
        private System.Windows.Forms.CheckBox chkDetPin;
        private System.Windows.Forms.Button pushbuttonReportPin;
        private System.Windows.Forms.HScrollBar sliderLowFrec;
        private System.Windows.Forms.HScrollBar sliderUmbralPin;
        private System.Windows.Forms.HScrollBar sliderAnchoPin;
        private System.Windows.Forms.TextBox textFrecPin;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox textAnchoPin;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textLowFrec;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textUmbralPin;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox textBox9K;
        private System.Windows.Forms.TextBox textBox7K;
        private System.Windows.Forms.TextBox textBox5K;
        private System.Windows.Forms.TextBox textBox3K;
        private System.Windows.Forms.TextBox textBox11K;
        private System.Windows.Forms.TextBox textBox2K;
        private System.Windows.Forms.TextBox textBox10K;
        private System.Windows.Forms.TextBox textBox8K;
        private System.Windows.Forms.TextBox textBox6K;
        private System.Windows.Forms.TextBox textBox4K;
        private System.Windows.Forms.TextBox textBox1K;
        private OpenTK.GLControl glControl2;
        private OpenTK.GLControl glControlBar1;
        private OpenTK.GLControl glControlBar2;
        private OpenTK.GLControl glControlTicks;
    }
}

