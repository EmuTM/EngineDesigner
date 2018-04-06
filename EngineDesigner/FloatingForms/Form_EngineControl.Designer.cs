namespace EngineDesigner.FloatingForms
{
    partial class Form_EngineControl
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
            this.components = new System.ComponentModel.Container();
            this.inputAngleChart_CrankshaftAngle = new EngineDesigner.Environment.Controls.Charting.InputAngleChart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_CrankshaftAngle = new System.Windows.Forms.TabPage();
            this.numericUpDown_CrankshaftAngle = new System.Windows.Forms.NumericUpDown();
            this.label_CurrentAngle = new System.Windows.Forms.Label();
            this.tabPage_RPM = new System.Windows.Forms.TabPage();
            this.shape1 = new EngineDesigner.Environment.Controls.Shape();
            this.label_CurrentAccuracy = new System.Windows.Forms.Label();
            this.label_CurrentAccuracyText = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Minimum = new System.Windows.Forms.Label();
            this.numericUpDown_MinimumRPM = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_Current = new System.Windows.Forms.Label();
            this.numericUpDown_RPM = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label_Maximum = new System.Windows.Forms.Label();
            this.numericUpDown_MaximumRPM = new System.Windows.Forms.NumericUpDown();
            this.label_RPM = new System.Windows.Forms.Label();
            this.trackBar_RPM = new System.Windows.Forms.TrackBar();
            this.rpmTimer1 = new EngineDesigner.Media.RPMTimer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage_CrankshaftAngle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CrankshaftAngle)).BeginInit();
            this.tabPage_RPM.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinimumRPM)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RPM)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumRPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RPM)).BeginInit();
            this.SuspendLayout();
            // 
            // inputAngleChart_CrankshaftAngle
            // 
            this.inputAngleChart_CrankshaftAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.inputAngleChart_CrankshaftAngle.Location = new System.Drawing.Point(3, 3);
            this.inputAngleChart_CrankshaftAngle.Name = "inputAngleChart_CrankshaftAngle";
            this.inputAngleChart_CrankshaftAngle.Rounding = 2;
            this.inputAngleChart_CrankshaftAngle.Size = new System.Drawing.Size(241, 175);
            this.inputAngleChart_CrankshaftAngle.TabIndex = 0;
            this.inputAngleChart_CrankshaftAngle.AngleChanged += new System.EventHandler<EngineDesigner.Environment.Controls.Charting.AngleChangedEventArgs>(this.inputAngleChart_CrankshaftAngle_AngleChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_CrankshaftAngle);
            this.tabControl1.Controls.Add(this.tabPage_RPM);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 232);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage_CrankshaftAngle
            // 
            this.tabPage_CrankshaftAngle.Controls.Add(this.numericUpDown_CrankshaftAngle);
            this.tabPage_CrankshaftAngle.Controls.Add(this.label_CurrentAngle);
            this.tabPage_CrankshaftAngle.Controls.Add(this.inputAngleChart_CrankshaftAngle);
            this.tabPage_CrankshaftAngle.Location = new System.Drawing.Point(4, 22);
            this.tabPage_CrankshaftAngle.Name = "tabPage_CrankshaftAngle";
            this.tabPage_CrankshaftAngle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CrankshaftAngle.Size = new System.Drawing.Size(247, 206);
            this.tabPage_CrankshaftAngle.TabIndex = 0;
            this.tabPage_CrankshaftAngle.Text = "Crankshaft angle";
            this.tabPage_CrankshaftAngle.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_CrankshaftAngle
            // 
            this.numericUpDown_CrankshaftAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_CrankshaftAngle.DecimalPlaces = 2;
            this.numericUpDown_CrankshaftAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_CrankshaftAngle.Location = new System.Drawing.Point(87, 183);
            this.numericUpDown_CrankshaftAngle.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numericUpDown_CrankshaftAngle.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numericUpDown_CrankshaftAngle.Name = "numericUpDown_CrankshaftAngle";
            this.numericUpDown_CrankshaftAngle.Size = new System.Drawing.Size(157, 20);
            this.numericUpDown_CrankshaftAngle.TabIndex = 2;
            this.numericUpDown_CrankshaftAngle.ThousandsSeparator = true;
            this.numericUpDown_CrankshaftAngle.ValueChanged += new System.EventHandler(this.numericUpDown_CrankshaftAngle_ValueChanged);
            // 
            // label_CurrentAngle
            // 
            this.label_CurrentAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_CurrentAngle.AutoEllipsis = true;
            this.label_CurrentAngle.AutoSize = true;
            this.label_CurrentAngle.Location = new System.Drawing.Point(8, 185);
            this.label_CurrentAngle.Name = "label_CurrentAngle";
            this.label_CurrentAngle.Size = new System.Drawing.Size(73, 13);
            this.label_CurrentAngle.TabIndex = 1;
            this.label_CurrentAngle.Text = "Current angle:";
            // 
            // tabPage_RPM
            // 
            this.tabPage_RPM.Controls.Add(this.shape1);
            this.tabPage_RPM.Controls.Add(this.label_CurrentAccuracy);
            this.tabPage_RPM.Controls.Add(this.label_CurrentAccuracyText);
            this.tabPage_RPM.Controls.Add(this.tableLayoutPanel1);
            this.tabPage_RPM.Controls.Add(this.label_RPM);
            this.tabPage_RPM.Controls.Add(this.trackBar_RPM);
            this.tabPage_RPM.Location = new System.Drawing.Point(4, 22);
            this.tabPage_RPM.Name = "tabPage_RPM";
            this.tabPage_RPM.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_RPM.Size = new System.Drawing.Size(247, 206);
            this.tabPage_RPM.TabIndex = 1;
            this.tabPage_RPM.Text = "RPM";
            this.tabPage_RPM.UseVisualStyleBackColor = true;
            // 
            // shape1
            // 
            this.shape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shape1.Location = new System.Drawing.Point(3, 177);
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = EngineDesigner.Environment.Controls.ShapeType.LINE;
            this.shape1.Size = new System.Drawing.Size(237, 1);
            this.shape1.TabIndex = 10;
            // 
            // label_CurrentAccuracy
            // 
            this.label_CurrentAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_CurrentAccuracy.AutoEllipsis = true;
            this.label_CurrentAccuracy.Location = new System.Drawing.Point(118, 185);
            this.label_CurrentAccuracy.Name = "label_CurrentAccuracy";
            this.label_CurrentAccuracy.Size = new System.Drawing.Size(126, 18);
            this.label_CurrentAccuracy.TabIndex = 9;
            this.label_CurrentAccuracy.Text = "1";
            // 
            // label_CurrentAccuracyText
            // 
            this.label_CurrentAccuracyText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_CurrentAccuracyText.AutoEllipsis = true;
            this.label_CurrentAccuracyText.AutoSize = true;
            this.label_CurrentAccuracyText.Location = new System.Drawing.Point(8, 185);
            this.label_CurrentAccuracyText.Name = "label_CurrentAccuracyText";
            this.label_CurrentAccuracyText.Size = new System.Drawing.Size(104, 13);
            this.label_CurrentAccuracyText.TabIndex = 9;
            this.label_CurrentAccuracyText.Text = "Current accuracy (°):";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(78, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(163, 164);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_Minimum);
            this.panel1.Controls.Add(this.numericUpDown_MinimumRPM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 50);
            this.panel1.TabIndex = 2;
            // 
            // label_Minimum
            // 
            this.label_Minimum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Minimum.AutoSize = true;
            this.label_Minimum.Location = new System.Drawing.Point(0, 14);
            this.label_Minimum.Name = "label_Minimum";
            this.label_Minimum.Size = new System.Drawing.Size(48, 13);
            this.label_Minimum.TabIndex = 0;
            this.label_Minimum.Text = "Minimum";
            // 
            // numericUpDown_MinimumRPM
            // 
            this.numericUpDown_MinimumRPM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numericUpDown_MinimumRPM.Location = new System.Drawing.Point(0, 30);
            this.numericUpDown_MinimumRPM.Maximum = new decimal(new int[] {
            29999,
            0,
            0,
            0});
            this.numericUpDown_MinimumRPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_MinimumRPM.Name = "numericUpDown_MinimumRPM";
            this.numericUpDown_MinimumRPM.Size = new System.Drawing.Size(157, 20);
            this.numericUpDown_MinimumRPM.TabIndex = 1;
            this.numericUpDown_MinimumRPM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_MinimumRPM.ValueChanged += new System.EventHandler(this.numericUpDown_MinimumRPM_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_Current);
            this.panel2.Controls.Add(this.numericUpDown_RPM);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 48);
            this.panel2.TabIndex = 1;
            // 
            // label_Current
            // 
            this.label_Current.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Current.AutoSize = true;
            this.label_Current.Location = new System.Drawing.Point(0, 12);
            this.label_Current.Name = "label_Current";
            this.label_Current.Size = new System.Drawing.Size(41, 13);
            this.label_Current.TabIndex = 0;
            this.label_Current.Text = "Current";
            // 
            // numericUpDown_RPM
            // 
            this.numericUpDown_RPM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numericUpDown_RPM.Location = new System.Drawing.Point(0, 28);
            this.numericUpDown_RPM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_RPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_RPM.Name = "numericUpDown_RPM";
            this.numericUpDown_RPM.Size = new System.Drawing.Size(157, 20);
            this.numericUpDown_RPM.TabIndex = 1;
            this.numericUpDown_RPM.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_RPM.ValueChanged += new System.EventHandler(this.numericUpDown_RPM_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label_Maximum);
            this.panel3.Controls.Add(this.numericUpDown_MaximumRPM);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(157, 48);
            this.panel3.TabIndex = 0;
            // 
            // label_Maximum
            // 
            this.label_Maximum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Maximum.AutoSize = true;
            this.label_Maximum.Location = new System.Drawing.Point(0, 12);
            this.label_Maximum.Name = "label_Maximum";
            this.label_Maximum.Size = new System.Drawing.Size(51, 13);
            this.label_Maximum.TabIndex = 0;
            this.label_Maximum.Text = "Maximum";
            // 
            // numericUpDown_MaximumRPM
            // 
            this.numericUpDown_MaximumRPM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.numericUpDown_MaximumRPM.Location = new System.Drawing.Point(0, 28);
            this.numericUpDown_MaximumRPM.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numericUpDown_MaximumRPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_MaximumRPM.Name = "numericUpDown_MaximumRPM";
            this.numericUpDown_MaximumRPM.Size = new System.Drawing.Size(157, 20);
            this.numericUpDown_MaximumRPM.TabIndex = 1;
            this.numericUpDown_MaximumRPM.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_MaximumRPM.ValueChanged += new System.EventHandler(this.numericUpDown_MaximumRPM_ValueChanged);
            // 
            // label_RPM
            // 
            this.label_RPM.AutoSize = true;
            this.label_RPM.Location = new System.Drawing.Point(6, 9);
            this.label_RPM.Name = "label_RPM";
            this.label_RPM.Size = new System.Drawing.Size(34, 13);
            this.label_RPM.TabIndex = 0;
            this.label_RPM.Text = "RPM:";
            // 
            // trackBar_RPM
            // 
            this.trackBar_RPM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBar_RPM.LargeChange = 100;
            this.trackBar_RPM.Location = new System.Drawing.Point(6, 25);
            this.trackBar_RPM.Maximum = 1000;
            this.trackBar_RPM.Minimum = 1;
            this.trackBar_RPM.Name = "trackBar_RPM";
            this.trackBar_RPM.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_RPM.Size = new System.Drawing.Size(45, 145);
            this.trackBar_RPM.SmallChange = 10;
            this.trackBar_RPM.TabIndex = 1;
            this.trackBar_RPM.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar_RPM.Value = 25;
            this.trackBar_RPM.ValueChanged += new System.EventHandler(this.trackBar_RPM_ValueChanged);
            // 
            // rpmTimer1
            // 
            this.rpmTimer1.Enabled = false;
            this.rpmTimer1.CrankshaftAngleChanged += new System.EventHandler<EngineDesigner.Media.RPMTimerEventArgs>(this.rpmTimer1_CrankshaftAngleChanged);
            // 
            // Form_EngineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 232);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(220, 224);
            this.Name = "Form_EngineControl";
            this.Text = "Engine control";
            this.Load += new System.EventHandler(this.Form_EngineControl_Load);
            this.Resize += new System.EventHandler(this.Form_EngineControl_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_CrankshaftAngle.ResumeLayout(false);
            this.tabPage_CrankshaftAngle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CrankshaftAngle)).EndInit();
            this.tabPage_RPM.ResumeLayout(false);
            this.tabPage_RPM.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinimumRPM)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RPM)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaximumRPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RPM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Environment.Controls.Charting.InputAngleChart inputAngleChart_CrankshaftAngle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_CrankshaftAngle;
        private System.Windows.Forms.TabPage tabPage_RPM;
        private System.Windows.Forms.Label label_CurrentAngle;
        private System.Windows.Forms.TrackBar trackBar_RPM;
        private System.Windows.Forms.Label label_RPM;
        private System.Windows.Forms.NumericUpDown numericUpDown_MinimumRPM;
        private System.Windows.Forms.Label label_Minimum;
        private System.Windows.Forms.NumericUpDown numericUpDown_RPM;
        private System.Windows.Forms.Label label_Current;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaximumRPM;
        private System.Windows.Forms.Label label_Maximum;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown numericUpDown_CrankshaftAngle;
        private EngineDesigner.Media.RPMTimer rpmTimer1;
        private System.Windows.Forms.Label label_CurrentAccuracy;
        private System.Windows.Forms.Label label_CurrentAccuracyText;
        private EngineDesigner.Environment.Controls.Shape shape1;
    }
}