namespace EngineDesigner.Wizards.NewEngine
{
    partial class Form_NewEngineWizard_PistonMass
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
            this.numericUpDown_PistonMass = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PistonMass)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(182, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the mass of the pistons:";
            // 
            // numericUpDown_PistonMass
            // 
            this.numericUpDown_PistonMass.DecimalPlaces = 2;
            this.numericUpDown_PistonMass.Location = new System.Drawing.Point(411, 108);
            this.numericUpDown_PistonMass.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_PistonMass.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_PistonMass.Name = "numericUpDown_PistonMass";
            this.numericUpDown_PistonMass.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_PistonMass.TabIndex = 12;
            this.numericUpDown_PistonMass.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(339, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(314, 27);
            this.label4.TabIndex = 10;
            this.label4.Text = "The mass of the piston with accessories. This is the biggest contributor of recip" +
                "rocating forces.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Piston mass (g):";
            // 
            // Form_NewEngineWizard_PistonMass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.numericUpDown_PistonMass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.CurrentStep = 5;
            this.Name = "Form_NewEngineWizard_PistonMass";
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.numericUpDown_PistonMass, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_PistonMass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_PistonMass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}
