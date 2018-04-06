namespace EngineDesigner.Wizards.NewEngine
{
    partial class Form_NewEngineWizard_BoreAndStroke
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
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_Bore = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_Stroke = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown_Displacement = new System.Windows.Forms.NumericUpDown();
            this.checkBox_BoreFixed = new System.Windows.Forms.CheckBox();
            this.checkBox_StrokeFixed = new System.Windows.Forms.CheckBox();
            this.shape1 = new EngineDesigner.Environment.Controls.Shape();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Bore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Stroke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Displacement)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(348, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the engine\'s bore and stroke, or simply displacement:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bore (mm):";
            // 
            // numericUpDown_Bore
            // 
            this.numericUpDown_Bore.DecimalPlaces = 2;
            this.numericUpDown_Bore.Location = new System.Drawing.Point(383, 108);
            this.numericUpDown_Bore.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_Bore.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Bore.Name = "numericUpDown_Bore";
            this.numericUpDown_Bore.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_Bore.TabIndex = 6;
            this.numericUpDown_Bore.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Bore.ValueChanged += new System.EventHandler(this.numericUpDown_Bore_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(339, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Bore is the diameter of the cylinder.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(311, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Stroke (mm):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(310, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Stroke is the reciprocating travel of the piston within the cylinder.";
            // 
            // numericUpDown_Stroke
            // 
            this.numericUpDown_Stroke.DecimalPlaces = 2;
            this.numericUpDown_Stroke.Location = new System.Drawing.Point(383, 173);
            this.numericUpDown_Stroke.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_Stroke.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_Stroke.Name = "numericUpDown_Stroke";
            this.numericUpDown_Stroke.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_Stroke.TabIndex = 6;
            this.numericUpDown_Stroke.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_Stroke.ValueChanged += new System.EventHandler(this.numericUpDown_Stroke_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(311, 273);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Displacement (cm3):";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(339, 299);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(314, 27);
            this.label11.TabIndex = 5;
            this.label11.Text = "Displacement is the value computed from bore, stroke and the nuber of cylinders i" +
                "n an engine.";
            // 
            // numericUpDown_Displacement
            // 
            this.numericUpDown_Displacement.DecimalPlaces = 3;
            this.numericUpDown_Displacement.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Displacement.Location = new System.Drawing.Point(420, 271);
            this.numericUpDown_Displacement.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numericUpDown_Displacement.Minimum = new decimal(new int[] {
            393,
            0,
            0,
            196608});
            this.numericUpDown_Displacement.Name = "numericUpDown_Displacement";
            this.numericUpDown_Displacement.Size = new System.Drawing.Size(144, 20);
            this.numericUpDown_Displacement.TabIndex = 6;
            this.numericUpDown_Displacement.Value = new decimal(new int[] {
            393,
            0,
            0,
            196608});
            this.numericUpDown_Displacement.ValueChanged += new System.EventHandler(this.numericUpDown_Displacement_ValueChanged);
            // 
            // checkBox_BoreFixed
            // 
            this.checkBox_BoreFixed.AutoSize = true;
            this.checkBox_BoreFixed.Location = new System.Drawing.Point(569, 109);
            this.checkBox_BoreFixed.Name = "checkBox_BoreFixed";
            this.checkBox_BoreFixed.Size = new System.Drawing.Size(51, 17);
            this.checkBox_BoreFixed.TabIndex = 11;
            this.checkBox_BoreFixed.Text = "Fixed";
            this.checkBox_BoreFixed.UseVisualStyleBackColor = true;
            this.checkBox_BoreFixed.CheckedChanged += new System.EventHandler(this.checkBox_BoreFixed_CheckedChanged);
            // 
            // checkBox_StrokeFixed
            // 
            this.checkBox_StrokeFixed.AutoSize = true;
            this.checkBox_StrokeFixed.Location = new System.Drawing.Point(569, 174);
            this.checkBox_StrokeFixed.Name = "checkBox_StrokeFixed";
            this.checkBox_StrokeFixed.Size = new System.Drawing.Size(51, 17);
            this.checkBox_StrokeFixed.TabIndex = 11;
            this.checkBox_StrokeFixed.Text = "Fixed";
            this.checkBox_StrokeFixed.UseVisualStyleBackColor = true;
            this.checkBox_StrokeFixed.CheckedChanged += new System.EventHandler(this.checkBox_StrokeFixed_CheckedChanged);
            // 
            // shape1
            // 
            this.shape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shape1.Location = new System.Drawing.Point(283, 243);
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = EngineDesigner.Environment.Controls.ShapeType.LINE;
            this.shape1.Size = new System.Drawing.Size(573, 1);
            this.shape1.TabIndex = 7;
            // 
            // Form_NewEngineWizard_BoreAndStroke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.checkBox_StrokeFixed);
            this.Controls.Add(this.checkBox_BoreFixed);
            this.Controls.Add(this.shape1);
            this.Controls.Add(this.numericUpDown_Displacement);
            this.Controls.Add(this.numericUpDown_Stroke);
            this.Controls.Add(this.numericUpDown_Bore);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.CurrentStep = 4;
            this.Name = "Form_NewEngineWizard_BoreAndStroke";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.numericUpDown_Bore, 0);
            this.Controls.SetChildIndex(this.numericUpDown_Stroke, 0);
            this.Controls.SetChildIndex(this.numericUpDown_Displacement, 0);
            this.Controls.SetChildIndex(this.shape1, 0);
            this.Controls.SetChildIndex(this.checkBox_BoreFixed, 0);
            this.Controls.SetChildIndex(this.checkBox_StrokeFixed, 0);
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Bore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Stroke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Displacement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_Bore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_Stroke;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown_Displacement;
        private System.Windows.Forms.CheckBox checkBox_BoreFixed;
        private System.Windows.Forms.CheckBox checkBox_StrokeFixed;
        private EngineDesigner.Environment.Controls.Shape shape1;
    }
}
