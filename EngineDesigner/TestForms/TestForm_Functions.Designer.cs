namespace EngineDesigner.TestForms
{
    partial class TestForm_Functions
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.functionChart1 = new EngineDesigner.Environment.Controls.Charting.FunctionChart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_Mass = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_RPM = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RPM)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.functionChart1);
            this.splitContainer1.Size = new System.Drawing.Size(928, 660);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 3;
            // 
            // functionChart1
            // 
            this.functionChart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.functionChart1.ChartCursorX = double.NaN;
            this.functionChart1.ChartCursorXEnabled = true;
            this.functionChart1.ChartCursorY = double.NaN;
            this.functionChart1.ChartCursorYEnabled = true;
            this.functionChart1.ChartGridColor = System.Drawing.Color.Transparent;
            this.functionChart1.ChartMarkersColor = System.Drawing.Color.Empty;
            this.functionChart1.ChartThickness = 5;
            this.functionChart1.ChartType = EngineDesigner.Environment.Controls.Charting.FunctionChartType.POINT;
            this.functionChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.functionChart1.Location = new System.Drawing.Point(0, 0);
            this.functionChart1.Name = "functionChart1";
            this.functionChart1.Size = new System.Drawing.Size(890, 660);
            this.functionChart1.TabIndex = 0;
            this.functionChart1.ZeroesCrossing = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(934, 698);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown_Mass);
            this.panel1.Controls.Add(this.numericUpDown_RPM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 666);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 32);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mass";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "RPM";
            // 
            // numericUpDown_Mass
            // 
            this.numericUpDown_Mass.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_Mass.Location = new System.Drawing.Point(273, 8);
            this.numericUpDown_Mass.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_Mass.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Mass.Name = "numericUpDown_Mass";
            this.numericUpDown_Mass.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Mass.TabIndex = 6;
            this.numericUpDown_Mass.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_Mass.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_RPM
            // 
            this.numericUpDown_RPM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_RPM.Location = new System.Drawing.Point(49, 8);
            this.numericUpDown_RPM.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown_RPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_RPM.Name = "numericUpDown_RPM";
            this.numericUpDown_RPM.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_RPM.TabIndex = 6;
            this.numericUpDown_RPM.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_RPM.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // TestForm_Functions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 698);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestForm_Functions";
            this.Text = "Test_Window";
            this.Load += new System.EventHandler(this.TestForm_Functions_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RPM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Environment.Controls.Charting.FunctionChart functionChart1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericUpDown_RPM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_Mass;






















    }
}