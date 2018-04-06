namespace EngineDesigner.TestForms
{
    partial class TestForm_IPart
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cylinderSketch1 = new EngineDesigner.TestForms.CylinderSketch();
            this.connectingRodSketch1 = new EngineDesigner.TestForms.ConnectingRodSketch();
            this.pistonSketch1 = new EngineDesigner.TestForms.PistonSketch();
            this.crankThrowSketch1 = new EngineDesigner.TestForms.CrankThrowSketch();
            this.positionedCylinderSketch1 = new EngineDesigner.TestForms.PositionedCylinderSketch();
            this.flywheelSketch1 = new EngineDesigner.TestForms.FlywheelSketch();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(12, 12);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(200, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // cylinderSketch1
            // 
            // 
            // 
            // 
            this.cylinderSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.cylinderSketch1.Location = new System.Drawing.Point(218, 38);
            this.cylinderSketch1.Name = "cylinderSketch1";
            this.cylinderSketch1.PistonColor = System.Drawing.Color.Red;
            this.cylinderSketch1.Size = new System.Drawing.Size(200, 100);
            this.cylinderSketch1.TabIndex = 3;
            // 
            // connectingRodSketch1
            // 
            // 
            // 
            // 
            this.connectingRodSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.connectingRodSketch1.Location = new System.Drawing.Point(12, 144);
            this.connectingRodSketch1.Name = "connectingRodSketch1";
            this.connectingRodSketch1.Size = new System.Drawing.Size(200, 100);
            this.connectingRodSketch1.TabIndex = 2;
            // 
            // pistonSketch1
            // 
            // 
            // 
            // 
            this.pistonSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.pistonSketch1.Location = new System.Drawing.Point(12, 38);
            this.pistonSketch1.Name = "pistonSketch1";
            this.pistonSketch1.Size = new System.Drawing.Size(200, 100);
            this.pistonSketch1.TabIndex = 1;
            // 
            // crankThrowSketch1
            // 
            // 
            // 
            // 
            this.crankThrowSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.crankThrowSketch1.Location = new System.Drawing.Point(12, 250);
            this.crankThrowSketch1.Name = "crankThrowSketch1";
            this.crankThrowSketch1.Size = new System.Drawing.Size(200, 100);
            this.crankThrowSketch1.TabIndex = 0;
            // 
            // positionedCylinderSketch1
            // 
            // 
            // 
            // 
            this.positionedCylinderSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.positionedCylinderSketch1.Location = new System.Drawing.Point(218, 144);
            this.positionedCylinderSketch1.Name = "positionedCylinderSketch1";
            this.positionedCylinderSketch1.PistonColor = System.Drawing.Color.Red;
            this.positionedCylinderSketch1.Size = new System.Drawing.Size(200, 100);
            this.positionedCylinderSketch1.TabIndex = 5;
            // 
            // flywheelSketch1
            // 
            // 
            // 
            // 
            this.flywheelSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.flywheelSketch1.Location = new System.Drawing.Point(218, 250);
            this.flywheelSketch1.Name = "flywheelSketch1";
            this.flywheelSketch1.Size = new System.Drawing.Size(200, 100);
            this.flywheelSketch1.TabIndex = 6;
            // 
            // TestForm_IPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 554);
            this.Controls.Add(this.flywheelSketch1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.positionedCylinderSketch1);
            this.Controls.Add(this.cylinderSketch1);
            this.Controls.Add(this.connectingRodSketch1);
            this.Controls.Add(this.pistonSketch1);
            this.Controls.Add(this.crankThrowSketch1);
            this.Name = "TestForm_IPart";
            this.Text = "TestForm_IPart";
            this.Load += new System.EventHandler(this.TestForm_IPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrankThrowSketch crankThrowSketch1;
        private PistonSketch pistonSketch1;
        private ConnectingRodSketch connectingRodSketch1;
        private CylinderSketch cylinderSketch1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private PositionedCylinderSketch positionedCylinderSketch1;
        private FlywheelSketch flywheelSketch1;





    }
}