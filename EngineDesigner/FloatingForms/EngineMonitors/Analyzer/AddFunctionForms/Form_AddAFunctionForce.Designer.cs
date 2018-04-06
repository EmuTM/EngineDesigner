namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_AddAFunctionForce
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
            this.tabPage_Forces = new System.Windows.Forms.TabPage();
            this.cylinderFunctionWithGasPressure_Force = new EngineDesigner.FloatingForms.EngineMonitors.Analyzer.CylinderFunctionWithGasPressure();
            this.tabControl1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage_Forces.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(451, 230);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Forces);
            this.tabControl1.Size = new System.Drawing.Size(465, 262);
            this.tabControl1.Controls.SetChildIndex(this.tabPage_Forces, 0);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(489, 347);
            this.panel5.Controls.SetChildIndex(this.tabControl1, 0);
            // 
            // tabPage_Forces
            // 
            this.tabPage_Forces.Controls.Add(this.cylinderFunctionWithGasPressure_Force);
            this.tabPage_Forces.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Forces.Name = "tabPage_Forces";
            this.tabPage_Forces.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Forces.Size = new System.Drawing.Size(457, 236);
            this.tabPage_Forces.TabIndex = 2;
            this.tabPage_Forces.Text = "Forces";
            this.tabPage_Forces.UseVisualStyleBackColor = true;
            // 
            // cylinderFunctionWithGasPressure_Force
            // 
            this.cylinderFunctionWithGasPressure_Force.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderFunctionWithGasPressure_Force.Location = new System.Drawing.Point(3, 3);
            this.cylinderFunctionWithGasPressure_Force.MaximumSize = new System.Drawing.Size(32767, 234);
            this.cylinderFunctionWithGasPressure_Force.MinimumSize = new System.Drawing.Size(0, 234);
            this.cylinderFunctionWithGasPressure_Force.Name = "cylinderFunctionWithGasPressure_Force";
            this.cylinderFunctionWithGasPressure_Force.Size = new System.Drawing.Size(451, 234);
            this.cylinderFunctionWithGasPressure_Force.TabIndex = 0;
            this.cylinderFunctionWithGasPressure_Force.CylinderPressureVsCrankAngleIndicatorFunctionChanged += new System.EventHandler<EngineDesigner.FloatingForms.EngineMonitors.Analyzer.IndicatorFunctionEventArgs>(this.cylinderFunctionWithGasPressure_Force_CylinderPressureVsCrankAngleIndicatorFunctionChanged);
            this.cylinderFunctionWithGasPressure_Force.FunctionChanged += new System.EventHandler<EngineDesigner.FloatingForms.EngineMonitors.Analyzer.FunctionEventArgs>(this.cylinderFunctionWithGasPressure_Force_FunctionChanged);
            // 
            // Form_AddAFunctionForce
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(489, 580);
            this.Name = "Form_AddAFunctionForce";
            this.tabControl1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabPage_Forces.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Forces;
        private CylinderFunctionWithGasPressure cylinderFunctionWithGasPressure_Force;

    }
}