namespace EngineDesigner.FloatingForms.EngineMonitors
{
    partial class Form_Statistics
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
            this.components = new System.ComponentModel.Container();
            this.report1 = new EngineDesigner.Environment.Controls.Report();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // report1
            // 
            this.report1.ColumnWidthKey = 350;
            this.report1.ColumnWidthValue = 98;
            this.report1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.report1.ImageList = this.imageList1;
            this.report1.Location = new System.Drawing.Point(0, 0);
            this.report1.Name = "report1";
            this.report1.ReportItems = new EngineDesigner.Environment.Controls.ReportItem[0];
            this.report1.Size = new System.Drawing.Size(484, 516);
            this.report1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Form_Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 516);
            this.Controls.Add(this.report1);
            this.Name = "Form_Statistics";
            this.Text = "Statistics";
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Environment.Controls.Report report1;
        private System.Windows.Forms.ImageList imageList1;
    }
}