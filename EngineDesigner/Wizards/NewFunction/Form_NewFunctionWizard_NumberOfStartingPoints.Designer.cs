namespace EngineDesigner.Wizards.NewFunction
{
    partial class Form_NewFunctionWizard_NumberOfStartingPoints
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_NumberOfStartingPoints = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumberOfStartingPoints)).BeginInit();
            this.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Select the number of starting points:";
            // 
            // numericUpDown_NumberOfStartingPoints
            // 
            this.numericUpDown_NumberOfStartingPoints.Location = new System.Drawing.Point(453, 108);
            this.numericUpDown_NumberOfStartingPoints.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_NumberOfStartingPoints.Name = "numericUpDown_NumberOfStartingPoints";
            this.numericUpDown_NumberOfStartingPoints.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_NumberOfStartingPoints.TabIndex = 30;
            this.numericUpDown_NumberOfStartingPoints.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Number of starting points:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(339, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(314, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "The entered nuber of points will be generated automatically.";
            // 
            // Form_NewFunctionWizard_NumberOfStartingPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.numericUpDown_NumberOfStartingPoints);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.CurrentStep = 4;
            this.Name = "Form_NewFunctionWizard_NumberOfStartingPoints";
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.numericUpDown_NumberOfStartingPoints, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumberOfStartingPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_NumberOfStartingPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
    }
}
