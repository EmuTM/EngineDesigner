namespace EngineDesigner.FloatingForms.EngineMonitors
{
    partial class Form_CrankshaftDiagram
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
            this.crankshaftDiagram1 = new EngineDesigner.Controls.Charting.CrankshaftDiagram();
            this.SuspendLayout();
            // 
            // crankshaftDiagram1
            // 
            this.crankshaftDiagram1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crankshaftDiagram1.Location = new System.Drawing.Point(0, 0);
            this.crankshaftDiagram1.Name = "crankshaftDiagram1";
            this.crankshaftDiagram1.Size = new System.Drawing.Size(284, 262);
            this.crankshaftDiagram1.TabIndex = 0;
            // 
            // Form_CrankshaftDiagram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.crankshaftDiagram1);
            this.Name = "Form_CrankshaftDiagram";
            this.Text = "Crankshaft diagram";
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Controls.Charting.CrankshaftDiagram crankshaftDiagram1;
    }
}