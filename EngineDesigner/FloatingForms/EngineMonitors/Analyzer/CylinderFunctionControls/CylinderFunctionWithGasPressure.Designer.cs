namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class CylinderFunctionWithGasPressure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CylinderFunctionWithGasPressure));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.indicatorFunction_CylinderPressureVsCrankAngle = new EngineDesigner.FloatingForms.EngineMonitors.Analyzer.IndicatorFunction();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.indicatorFunction_CylinderPressureVsCrankAngle);
            this.panel1.Size = new System.Drawing.Size(343, 234);
            this.panel1.Controls.SetChildIndex(this.indicatorFunction_CylinderPressureVsCrankAngle, 0);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // indicatorFunction_CylinderPressureVsCrankAngle
            // 
            this.indicatorFunction_CylinderPressureVsCrankAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.indicatorFunction_CylinderPressureVsCrankAngle.IndicatorFunctionName = "Cylinder pressure vs. crank angle";
            this.indicatorFunction_CylinderPressureVsCrankAngle.Location = new System.Drawing.Point(9, 140);
            this.indicatorFunction_CylinderPressureVsCrankAngle.Name = "indicatorFunction_CylinderPressureVsCrankAngle";
            this.indicatorFunction_CylinderPressureVsCrankAngle.SelectedInterpolationMethod = ((EngineDesigner.FloatingForms.EngineMonitors.Analyzer.InterpolationMethodInfo)(resources.GetObject("indicatorFunction_CylinderPressureVsCrankAngle.SelectedInterpolationMethod")));
            this.indicatorFunction_CylinderPressureVsCrankAngle.Size = new System.Drawing.Size(326, 87);
            this.indicatorFunction_CylinderPressureVsCrankAngle.TabIndex = 0;
            this.indicatorFunction_CylinderPressureVsCrankAngle.IndicatorFunctionChanged += new System.EventHandler<EngineDesigner.FloatingForms.EngineMonitors.Analyzer.IndicatorFunctionEventArgs>(this.indicatorFunction_CylinderPressureVsCrankAngle_IndicatorFunctionChanged);
            // 
            // CylinderFunctionWithGasPressure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MaximumSize = new System.Drawing.Size(32767, 234);
            this.MinimumSize = new System.Drawing.Size(0, 234);
            this.Name = "CylinderFunctionWithGasPressure";
            this.Size = new System.Drawing.Size(343, 234);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private IndicatorFunction indicatorFunction_CylinderPressureVsCrankAngle;
    }
}
