namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_AddAFunctionMoment
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
            this.tabPage_Moments = new System.Windows.Forms.TabPage();
            this.cylinderFunction_Moments = new EngineDesigner.FloatingForms.EngineMonitors.Analyzer.CylinderFunction();
            this.tabControl1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage_Moments.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(451, 120);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Moments);
            this.tabControl1.Controls.SetChildIndex(this.tabPage_Moments, 0);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(489, 233);
            this.panel5.Controls.SetChildIndex(this.tabControl1, 0);
            // 
            // tabPage_Moments
            // 
            this.tabPage_Moments.Controls.Add(this.cylinderFunction_Moments);
            this.tabPage_Moments.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Moments.Name = "tabPage_Moments";
            this.tabPage_Moments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Moments.Size = new System.Drawing.Size(457, 126);
            this.tabPage_Moments.TabIndex = 2;
            this.tabPage_Moments.Text = "Moments";
            this.tabPage_Moments.UseVisualStyleBackColor = true;
            // 
            // cylinderFunction_Moments
            // 
            this.cylinderFunction_Moments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderFunction_Moments.Location = new System.Drawing.Point(3, 3);
            this.cylinderFunction_Moments.MaximumSize = new System.Drawing.Size(32767, 120);
            this.cylinderFunction_Moments.MinimumSize = new System.Drawing.Size(0, 120);
            this.cylinderFunction_Moments.Name = "cylinderFunction1";
            this.cylinderFunction_Moments.Size = new System.Drawing.Size(451, 120);
            this.cylinderFunction_Moments.TabIndex = 0;
            // 
            // Form_AddAFunctionMoment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(489, 466);
            this.Name = "Form_AddAFunctionMoment";
            this.tabControl1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabPage_Moments.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Moments;
        private CylinderFunction cylinderFunction_Moments;
    }
}
