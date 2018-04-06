namespace EngineDesigner.Wizards.NewEngine
{
    partial class Form_NewEngineWizard_ConnectingRod_Length
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
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_ConnectingRodLength = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConnectingRodLength)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(206, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the connecting rod\'s length:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(339, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(314, 27);
            this.label4.TabIndex = 5;
            this.label4.Text = "The length of the connecting rod significantly alters the characteristics of an e" +
                "ngine.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(311, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Connecting rod length (mm):";
            // 
            // numericUpDown_ConnectingRodLength
            // 
            this.numericUpDown_ConnectingRodLength.DecimalPlaces = 3;
            this.numericUpDown_ConnectingRodLength.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_ConnectingRodLength.Location = new System.Drawing.Point(456, 108);
            this.numericUpDown_ConnectingRodLength.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_ConnectingRodLength.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_ConnectingRodLength.Name = "numericUpDown_ConnectingRodLength";
            this.numericUpDown_ConnectingRodLength.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_ConnectingRodLength.TabIndex = 6;
            this.numericUpDown_ConnectingRodLength.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // Form_NewEngineWizard_ConnectingRod_Length
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown_ConnectingRodLength);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.CurrentStep = 6;
            this.Name = "Form_NewEngineWizard_ConnectingRod_Length";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.numericUpDown_ConnectingRodLength, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ConnectingRodLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_ConnectingRodLength;
    }
}
