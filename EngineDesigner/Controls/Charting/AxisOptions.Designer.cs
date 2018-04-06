namespace EngineDesigner.Controls.Charting
{
    partial class AxisOptions
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_CycleElapse = new System.Windows.Forms.GroupBox();
            this.checkBox_ShowStrokeElapseCyclically = new System.Windows.Forms.CheckBox();
            this.checkBox_ShowElapsedStroke = new System.Windows.Forms.CheckBox();
            this.groupBox_Range = new System.Windows.Forms.GroupBox();
            this.checkBox_CustomRange = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel_Range = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_StartDegrees = new System.Windows.Forms.Label();
            this.numericUpDown_StartDegrees = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_EndDegrees = new System.Windows.Forms.Label();
            this.numericUpDown_EndDegrees = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox_Representation = new System.Windows.Forms.GroupBox();
            this.checkBox_ShowValuesInPi = new System.Windows.Forms.CheckBox();
            this.groupBox_CycleElapse.SuspendLayout();
            this.groupBox_Range.SuspendLayout();
            this.tableLayoutPanel_Range.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StartDegrees)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EndDegrees)).BeginInit();
            this.groupBox_Representation.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_CycleElapse
            // 
            this.groupBox_CycleElapse.Controls.Add(this.checkBox_ShowStrokeElapseCyclically);
            this.groupBox_CycleElapse.Controls.Add(this.checkBox_ShowElapsedStroke);
            this.groupBox_CycleElapse.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_CycleElapse.Location = new System.Drawing.Point(4, 103);
            this.groupBox_CycleElapse.Name = "groupBox_CycleElapse";
            this.groupBox_CycleElapse.Size = new System.Drawing.Size(327, 76);
            this.groupBox_CycleElapse.TabIndex = 3;
            this.groupBox_CycleElapse.TabStop = false;
            this.groupBox_CycleElapse.Text = "Cycle elapse";
            // 
            // checkBox_ShowStrokeElapseCyclically
            // 
            this.checkBox_ShowStrokeElapseCyclically.AutoSize = true;
            this.checkBox_ShowStrokeElapseCyclically.Enabled = false;
            this.checkBox_ShowStrokeElapseCyclically.Location = new System.Drawing.Point(35, 47);
            this.checkBox_ShowStrokeElapseCyclically.Name = "checkBox_ShowStrokeElapseCyclically";
            this.checkBox_ShowStrokeElapseCyclically.Size = new System.Drawing.Size(164, 17);
            this.checkBox_ShowStrokeElapseCyclically.TabIndex = 1;
            this.checkBox_ShowStrokeElapseCyclically.Text = "Show stroke elapse cyclically";
            this.checkBox_ShowStrokeElapseCyclically.UseVisualStyleBackColor = true;
            this.checkBox_ShowStrokeElapseCyclically.CheckedChanged += new System.EventHandler(this.checkBox_ShowStrokeElapseCyclically_CheckedChanged);
            // 
            // checkBox_ShowElapsedStroke
            // 
            this.checkBox_ShowElapsedStroke.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ShowElapsedStroke.AutoSize = true;
            this.checkBox_ShowElapsedStroke.Location = new System.Drawing.Point(17, 24);
            this.checkBox_ShowElapsedStroke.Name = "checkBox_ShowElapsedStroke";
            this.checkBox_ShowElapsedStroke.Size = new System.Drawing.Size(125, 17);
            this.checkBox_ShowElapsedStroke.TabIndex = 0;
            this.checkBox_ShowElapsedStroke.Text = "Show elapsed stroke";
            this.checkBox_ShowElapsedStroke.UseVisualStyleBackColor = true;
            this.checkBox_ShowElapsedStroke.CheckedChanged += new System.EventHandler(this.checkBox_ShowElapsedStroke_CheckedChanged);
            // 
            // groupBox_Range
            // 
            this.groupBox_Range.Controls.Add(this.checkBox_CustomRange);
            this.groupBox_Range.Controls.Add(this.tableLayoutPanel_Range);
            this.groupBox_Range.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Range.Location = new System.Drawing.Point(4, 4);
            this.groupBox_Range.Name = "groupBox_Range";
            this.groupBox_Range.Size = new System.Drawing.Size(327, 83);
            this.groupBox_Range.TabIndex = 2;
            this.groupBox_Range.TabStop = false;
            this.groupBox_Range.Text = "Range";
            // 
            // checkBox_CustomRange
            // 
            this.checkBox_CustomRange.AutoSize = true;
            this.checkBox_CustomRange.Location = new System.Drawing.Point(17, 24);
            this.checkBox_CustomRange.Name = "checkBox_CustomRange";
            this.checkBox_CustomRange.Size = new System.Drawing.Size(61, 17);
            this.checkBox_CustomRange.TabIndex = 1;
            this.checkBox_CustomRange.Text = "Custom";
            this.checkBox_CustomRange.UseVisualStyleBackColor = true;
            this.checkBox_CustomRange.CheckedChanged += new System.EventHandler(this.checkBox_CustomRange_CheckedChanged);
            // 
            // tableLayoutPanel_Range
            // 
            this.tableLayoutPanel_Range.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_Range.ColumnCount = 3;
            this.tableLayoutPanel_Range.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Range.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel_Range.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Range.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel_Range.Controls.Add(this.panel2, 2, 0);
            this.tableLayoutPanel_Range.Enabled = false;
            this.tableLayoutPanel_Range.Location = new System.Drawing.Point(27, 44);
            this.tableLayoutPanel_Range.Name = "tableLayoutPanel_Range";
            this.tableLayoutPanel_Range.RowCount = 1;
            this.tableLayoutPanel_Range.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Range.Size = new System.Drawing.Size(294, 32);
            this.tableLayoutPanel_Range.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_StartDegrees);
            this.panel1.Controls.Add(this.numericUpDown_StartDegrees);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(125, 26);
            this.panel1.TabIndex = 4;
            // 
            // label_StartDegrees
            // 
            this.label_StartDegrees.AutoSize = true;
            this.label_StartDegrees.Location = new System.Drawing.Point(5, 5);
            this.label_StartDegrees.Name = "label_StartDegrees";
            this.label_StartDegrees.Size = new System.Drawing.Size(70, 13);
            this.label_StartDegrees.TabIndex = 0;
            this.label_StartDegrees.Text = "Start degrees";
            // 
            // numericUpDown_StartDegrees
            // 
            this.numericUpDown_StartDegrees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_StartDegrees.Location = new System.Drawing.Point(81, 3);
            this.numericUpDown_StartDegrees.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.numericUpDown_StartDegrees.Minimum = new decimal(new int[] {
            7200,
            0,
            0,
            -2147483648});
            this.numericUpDown_StartDegrees.Name = "numericUpDown_StartDegrees";
            this.numericUpDown_StartDegrees.Size = new System.Drawing.Size(41, 20);
            this.numericUpDown_StartDegrees.TabIndex = 1;
            this.numericUpDown_StartDegrees.ValueChanged += new System.EventHandler(this.numericUpDown_StartDegrees_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_EndDegrees);
            this.panel2.Controls.Add(this.numericUpDown_EndDegrees);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(166, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(125, 26);
            this.panel2.TabIndex = 5;
            // 
            // label_EndDegrees
            // 
            this.label_EndDegrees.AutoSize = true;
            this.label_EndDegrees.Location = new System.Drawing.Point(3, 5);
            this.label_EndDegrees.Name = "label_EndDegrees";
            this.label_EndDegrees.Size = new System.Drawing.Size(67, 13);
            this.label_EndDegrees.TabIndex = 0;
            this.label_EndDegrees.Text = "End degrees";
            // 
            // numericUpDown_EndDegrees
            // 
            this.numericUpDown_EndDegrees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_EndDegrees.Location = new System.Drawing.Point(76, 3);
            this.numericUpDown_EndDegrees.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.numericUpDown_EndDegrees.Minimum = new decimal(new int[] {
            7200,
            0,
            0,
            -2147483648});
            this.numericUpDown_EndDegrees.Name = "numericUpDown_EndDegrees";
            this.numericUpDown_EndDegrees.Size = new System.Drawing.Size(46, 20);
            this.numericUpDown_EndDegrees.TabIndex = 1;
            this.numericUpDown_EndDegrees.ValueChanged += new System.EventHandler(this.numericUpDown_EndDegrees_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(327, 16);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 179);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(327, 16);
            this.panel4.TabIndex = 5;
            // 
            // groupBox_Representation
            // 
            this.groupBox_Representation.Controls.Add(this.checkBox_ShowValuesInPi);
            this.groupBox_Representation.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_Representation.Location = new System.Drawing.Point(4, 195);
            this.groupBox_Representation.Name = "groupBox_Representation";
            this.groupBox_Representation.Size = new System.Drawing.Size(327, 52);
            this.groupBox_Representation.TabIndex = 6;
            this.groupBox_Representation.TabStop = false;
            this.groupBox_Representation.Text = "Representation";
            // 
            // checkBox_ShowValuesInPi
            // 
            this.checkBox_ShowValuesInPi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ShowValuesInPi.AutoSize = true;
            this.checkBox_ShowValuesInPi.Location = new System.Drawing.Point(17, 24);
            this.checkBox_ShowValuesInPi.Name = "checkBox_ShowValuesInPi";
            this.checkBox_ShowValuesInPi.Size = new System.Drawing.Size(111, 17);
            this.checkBox_ShowValuesInPi.TabIndex = 0;
            this.checkBox_ShowValuesInPi.Text = "Show values in PI";
            this.checkBox_ShowValuesInPi.UseVisualStyleBackColor = true;
            this.checkBox_ShowValuesInPi.CheckedChanged += new System.EventHandler(this.checkBox_ShowValuesInPi_CheckedChanged);
            // 
            // AxisOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_Representation);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.groupBox_CycleElapse);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox_Range);
            this.Name = "AxisOptions";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(335, 254);
            this.groupBox_CycleElapse.ResumeLayout(false);
            this.groupBox_CycleElapse.PerformLayout();
            this.groupBox_Range.ResumeLayout(false);
            this.groupBox_Range.PerformLayout();
            this.tableLayoutPanel_Range.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StartDegrees)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_EndDegrees)).EndInit();
            this.groupBox_Representation.ResumeLayout(false);
            this.groupBox_Representation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_CycleElapse;
        private System.Windows.Forms.CheckBox checkBox_ShowStrokeElapseCyclically;
        private System.Windows.Forms.CheckBox checkBox_ShowElapsedStroke;
        private System.Windows.Forms.GroupBox groupBox_Range;
        private System.Windows.Forms.CheckBox checkBox_CustomRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Range;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_StartDegrees;
        private System.Windows.Forms.NumericUpDown numericUpDown_StartDegrees;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_EndDegrees;
        private System.Windows.Forms.NumericUpDown numericUpDown_EndDegrees;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox_Representation;
        private System.Windows.Forms.CheckBox checkBox_ShowValuesInPi;
    }
}
