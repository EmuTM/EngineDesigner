namespace EngineDesigner.Wizards.NewFunction
{
    partial class Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle
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
            EngineDesigner.Environment.Controls.Bookmark bookmark1 = new EngineDesigner.Environment.Controls.Bookmark();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_HighestPressureOnPowerStroke = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_LowestPressureOnIntakeStroke = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_AverageAtmosphericPressure = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HighestPressureOnPowerStroke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LowestPressureOnIntakeStroke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AverageAtmosphericPressure)).BeginInit();
            this.SuspendLayout();
            // 
            // tableOfContents1
            // 
            bookmark1.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark1.Title = "Min / Max values";
            this.tableOfContents1.SelectedBookmark = bookmark1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(280, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Select the minimum and maximum values:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(311, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Highest pressure on power stroke:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(339, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 21);
            this.label3.TabIndex = 29;
            this.label3.Text = "This values will be used for the creation of starting points.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Lowest pressure on intake stroke:";
            // 
            // numericUpDown_HighestPressureOnPowerStroke
            // 
            this.numericUpDown_HighestPressureOnPowerStroke.DecimalPlaces = 2;
            this.numericUpDown_HighestPressureOnPowerStroke.Location = new System.Drawing.Point(494, 147);
            this.numericUpDown_HighestPressureOnPowerStroke.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_HighestPressureOnPowerStroke.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_HighestPressureOnPowerStroke.Name = "numericUpDown_HighestPressureOnPowerStroke";
            this.numericUpDown_HighestPressureOnPowerStroke.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_HighestPressureOnPowerStroke.TabIndex = 31;
            this.numericUpDown_HighestPressureOnPowerStroke.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_HighestPressureOnPowerStroke.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_LowestPressureOnIntakeStroke
            // 
            this.numericUpDown_LowestPressureOnIntakeStroke.DecimalPlaces = 2;
            this.numericUpDown_LowestPressureOnIntakeStroke.Location = new System.Drawing.Point(494, 108);
            this.numericUpDown_LowestPressureOnIntakeStroke.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_LowestPressureOnIntakeStroke.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_LowestPressureOnIntakeStroke.Name = "numericUpDown_LowestPressureOnIntakeStroke";
            this.numericUpDown_LowestPressureOnIntakeStroke.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_LowestPressureOnIntakeStroke.TabIndex = 30;
            this.numericUpDown_LowestPressureOnIntakeStroke.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown_LowestPressureOnIntakeStroke.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(311, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Average atmospheric pressure:";
            // 
            // numericUpDown_AverageAtmosphericPressure
            // 
            this.numericUpDown_AverageAtmosphericPressure.DecimalPlaces = 2;
            this.numericUpDown_AverageAtmosphericPressure.Location = new System.Drawing.Point(494, 186);
            this.numericUpDown_AverageAtmosphericPressure.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numericUpDown_AverageAtmosphericPressure.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_AverageAtmosphericPressure.Name = "numericUpDown_AverageAtmosphericPressure";
            this.numericUpDown_AverageAtmosphericPressure.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_AverageAtmosphericPressure.TabIndex = 31;
            this.numericUpDown_AverageAtmosphericPressure.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_AverageAtmosphericPressure.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(659, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "(Bar)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(659, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "(Bar)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(659, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "(Bar)";
            // 
            // Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDown_LowestPressureOnIntakeStroke);
            this.Controls.Add(this.numericUpDown_HighestPressureOnPowerStroke);
            this.Controls.Add(this.numericUpDown_AverageAtmosphericPressure);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Name = "Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.numericUpDown_AverageAtmosphericPressure, 0);
            this.Controls.SetChildIndex(this.numericUpDown_HighestPressureOnPowerStroke, 0);
            this.Controls.SetChildIndex(this.numericUpDown_LowestPressureOnIntakeStroke, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HighestPressureOnPowerStroke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LowestPressureOnIntakeStroke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AverageAtmosphericPressure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_HighestPressureOnPowerStroke;
        private System.Windows.Forms.NumericUpDown numericUpDown_LowestPressureOnIntakeStroke;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_AverageAtmosphericPressure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
    }
}
