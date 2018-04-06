namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class IndicatorFunction
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
            this.groupBox_IndicatorFunction = new System.Windows.Forms.GroupBox();
            this.comboBox_InterpolateMissingValues = new System.Windows.Forms.ComboBox();
            this.label_InterpolateMissingValues = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_SelectIndicatorFunctionFilename = new System.Windows.Forms.Button();
            this.textBox_IndicatorFunctionFilename = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox_IndicatorFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_IndicatorFunction
            // 
            this.groupBox_IndicatorFunction.Controls.Add(this.comboBox_InterpolateMissingValues);
            this.groupBox_IndicatorFunction.Controls.Add(this.label_InterpolateMissingValues);
            this.groupBox_IndicatorFunction.Controls.Add(this.label1);
            this.groupBox_IndicatorFunction.Controls.Add(this.button_SelectIndicatorFunctionFilename);
            this.groupBox_IndicatorFunction.Controls.Add(this.textBox_IndicatorFunctionFilename);
            this.groupBox_IndicatorFunction.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_IndicatorFunction.Location = new System.Drawing.Point(0, 0);
            this.groupBox_IndicatorFunction.Name = "groupBox_IndicatorFunction";
            this.groupBox_IndicatorFunction.Size = new System.Drawing.Size(302, 86);
            this.groupBox_IndicatorFunction.TabIndex = 16;
            this.groupBox_IndicatorFunction.TabStop = false;
            this.groupBox_IndicatorFunction.Text = "Indicator function";
            // 
            // comboBox_InterpolateMissingValues
            // 
            this.comboBox_InterpolateMissingValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_InterpolateMissingValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_InterpolateMissingValues.FormattingEnabled = true;
            this.comboBox_InterpolateMissingValues.Location = new System.Drawing.Point(165, 58);
            this.comboBox_InterpolateMissingValues.Name = "comboBox_InterpolateMissingValues";
            this.comboBox_InterpolateMissingValues.Size = new System.Drawing.Size(123, 21);
            this.comboBox_InterpolateMissingValues.TabIndex = 5;
            this.comboBox_InterpolateMissingValues.SelectedIndexChanged += new System.EventHandler(this.comboBox_InterpolateMissingValues_SelectedIndexChanged);
            // 
            // label_InterpolateMissingValues
            // 
            this.label_InterpolateMissingValues.AutoSize = true;
            this.label_InterpolateMissingValues.Location = new System.Drawing.Point(28, 61);
            this.label_InterpolateMissingValues.Name = "label_InterpolateMissingValues";
            this.label_InterpolateMissingValues.Size = new System.Drawing.Size(131, 13);
            this.label_InterpolateMissingValues.TabIndex = 4;
            this.label_InterpolateMissingValues.Text = "Interpolate missing values:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Function";
            // 
            // button_SelectIndicatorFunctionFilename
            // 
            this.button_SelectIndicatorFunctionFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SelectIndicatorFunctionFilename.Image = global::EngineDesigner.Properties.Resources.folder16x16;
            this.button_SelectIndicatorFunctionFilename.Location = new System.Drawing.Point(264, 26);
            this.button_SelectIndicatorFunctionFilename.Name = "button_SelectIndicatorFunctionFilename";
            this.button_SelectIndicatorFunctionFilename.Size = new System.Drawing.Size(24, 23);
            this.button_SelectIndicatorFunctionFilename.TabIndex = 1;
            this.button_SelectIndicatorFunctionFilename.UseVisualStyleBackColor = true;
            this.button_SelectIndicatorFunctionFilename.Click += new System.EventHandler(this.button_SelectIndicatorFunctionFilename_Click);
            // 
            // textBox_IndicatorFunctionFilename
            // 
            this.textBox_IndicatorFunctionFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_IndicatorFunctionFilename.Location = new System.Drawing.Point(69, 28);
            this.textBox_IndicatorFunctionFilename.Name = "textBox_IndicatorFunctionFilename";
            this.textBox_IndicatorFunctionFilename.ReadOnly = true;
            this.textBox_IndicatorFunctionFilename.Size = new System.Drawing.Size(189, 20);
            this.textBox_IndicatorFunctionFilename.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Engine designer Function files (*.xml)|*.xml";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // IndicatorFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_IndicatorFunction);
            this.Name = "IndicatorFunction";
            this.Size = new System.Drawing.Size(302, 87);
            this.groupBox_IndicatorFunction.ResumeLayout(false);
            this.groupBox_IndicatorFunction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_IndicatorFunction;
        private System.Windows.Forms.ComboBox comboBox_InterpolateMissingValues;
        private System.Windows.Forms.Label label_InterpolateMissingValues;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_SelectIndicatorFunctionFilename;
        private System.Windows.Forms.TextBox textBox_IndicatorFunctionFilename;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
