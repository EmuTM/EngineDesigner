namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_AddAFunctionTorque
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
            this.tabPage_Torques = new System.Windows.Forms.TabPage();
            this.cylinderFunctionWithGasPressure_Torque = new EngineDesigner.FloatingForms.EngineMonitors.Analyzer.CylinderFunctionWithGasPressure();
            this.tabControl1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage_Torques.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(451, 232);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Torques);
            this.tabControl1.Controls.SetChildIndex(this.tabPage_Torques, 0);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(489, 347);
            this.panel5.Controls.SetChildIndex(this.tabControl1, 0);
            // 
            // tabPage_Torques
            // 
            this.tabPage_Torques.Controls.Add(this.cylinderFunctionWithGasPressure_Torque);
            this.tabPage_Torques.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Torques.Name = "tabPage_Torques";
            this.tabPage_Torques.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Torques.Size = new System.Drawing.Size(457, 238);
            this.tabPage_Torques.TabIndex = 2;
            this.tabPage_Torques.Text = "Torques";
            this.tabPage_Torques.UseVisualStyleBackColor = true;
            // 
            // cylinderFunctionWithGasPressure_Torque
            // 
            this.cylinderFunctionWithGasPressure_Torque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderFunctionWithGasPressure_Torque.Location = new System.Drawing.Point(3, 3);
            this.cylinderFunctionWithGasPressure_Torque.MaximumSize = new System.Drawing.Size(32767, 234);
            this.cylinderFunctionWithGasPressure_Torque.MinimumSize = new System.Drawing.Size(0, 234);
            this.cylinderFunctionWithGasPressure_Torque.Name = "cylinderFunctionWithGasPressure_Torque";
            this.cylinderFunctionWithGasPressure_Torque.Size = new System.Drawing.Size(451, 234);
            this.cylinderFunctionWithGasPressure_Torque.TabIndex = 0;
            this.cylinderFunctionWithGasPressure_Torque.CylinderPressureVsCrankAngleIndicatorFunctionChanged += new System.EventHandler<EngineDesigner.FloatingForms.EngineMonitors.Analyzer.IndicatorFunctionEventArgs>(this.cylinderFunctionWithGasPressure_Torque_CylinderPressureVsCrankAngleIndicatorFunctionChanged);
            this.cylinderFunctionWithGasPressure_Torque.FunctionChanged += new System.EventHandler<EngineDesigner.FloatingForms.EngineMonitors.Analyzer.FunctionEventArgs>(this.cylinderFunctionWithGasPressure_Torque_FunctionChanged);
            // 
            // Form_AddAFunctionTorque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 580);
            this.Name = "Form_AddAFunctionTorque";
            this.tabControl1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabPage_Torques.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Torques;
        private CylinderFunctionWithGasPressure cylinderFunctionWithGasPressure_Torque;

    }
}