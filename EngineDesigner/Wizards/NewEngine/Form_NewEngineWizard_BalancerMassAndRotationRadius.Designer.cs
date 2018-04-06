namespace EngineDesigner.Wizards.NewEngine
{
    partial class Form_NewEngineWizard_BalancerMassAndRotationRadius
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
            this.numericUpDown_BalancerMass = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_BalancerRotationRadius = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BalancerMass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BalancerRotationRadius)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(362, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the masses of the balancers and their rotation radiuses:";
            // 
            // numericUpDown_BalancerMass
            // 
            this.numericUpDown_BalancerMass.DecimalPlaces = 2;
            this.numericUpDown_BalancerMass.Location = new System.Drawing.Point(463, 108);
            this.numericUpDown_BalancerMass.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BalancerMass.Name = "numericUpDown_BalancerMass";
            this.numericUpDown_BalancerMass.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_BalancerMass.TabIndex = 12;
            this.numericUpDown_BalancerMass.ValueChanged += new System.EventHandler(this.numericUpDown_BalancerMass_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Balancer mass (g):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(311, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Rotation radius (mm):";
            // 
            // numericUpDown_RotationRadius
            // 
            this.numericUpDown_BalancerRotationRadius.DecimalPlaces = 2;
            this.numericUpDown_BalancerRotationRadius.Enabled = false;
            this.numericUpDown_BalancerRotationRadius.Location = new System.Drawing.Point(463, 147);
            this.numericUpDown_BalancerRotationRadius.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_BalancerRotationRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_BalancerRotationRadius.Name = "numericUpDown_RotationRadius";
            this.numericUpDown_BalancerRotationRadius.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_BalancerRotationRadius.TabIndex = 12;
            this.numericUpDown_BalancerRotationRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(339, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 44);
            this.label3.TabIndex = 10;
            this.label3.Text = "The crank balancer is used to counteract the reciprocating forces. The forces and" +
                " torques that are generated by the crank balancer depend on its mass and rotatio" +
                "n radius.";
            // 
            // Form_NewEngineWizard_BalancerMass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.numericUpDown_BalancerMass);
            this.Controls.Add(this.numericUpDown_BalancerRotationRadius);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.CurrentStep = 8;
            this.Name = "Form_NewEngineWizard_BalancerMass";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.numericUpDown_BalancerRotationRadius, 0);
            this.Controls.SetChildIndex(this.numericUpDown_BalancerMass, 0);
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BalancerMass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BalancerRotationRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown_BalancerMass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_BalancerRotationRadius;
        private System.Windows.Forms.Label label3;
    }
}
