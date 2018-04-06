namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_LoadIndicatorFunction
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
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Panel panel2;
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.indicatorFunction1 = new EngineDesigner.FloatingForms.EngineMonitors.Analyzer.IndicatorFunction();
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 113);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(461, 43);
            panel1.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.Controls.Add(this.button_Cancel);
            panel2.Controls.Add(this.button_OK);
            panel2.Dock = System.Windows.Forms.DockStyle.Right;
            panel2.Location = new System.Drawing.Point(286, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(175, 43);
            panel2.TabIndex = 4;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(88, 8);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(8, 8);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            // 
            // indicatorFunction1
            // 
            this.indicatorFunction1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.indicatorFunction1.Location = new System.Drawing.Point(12, 12);
            this.indicatorFunction1.Name = "indicatorFunction1";
            this.indicatorFunction1.Size = new System.Drawing.Size(437, 87);
            this.indicatorFunction1.TabIndex = 9;
            this.indicatorFunction1.IndicatorFunctionChanged += new System.EventHandler<EngineDesigner.FloatingForms.EngineMonitors.Analyzer.IndicatorFunctionEventArgs>(this.indicatorFunction1_IndicatorFunctionChanged);
            // 
            // Form_LoadIndicatorFunction
            // 
            this.AcceptButton = this.button_Cancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(461, 156);
            this.Controls.Add(this.indicatorFunction1);
            this.Controls.Add(panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(65535, 194);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(348, 194);
            this.Name = "Form_LoadIndicatorFunction";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load indicator function";
            this.Load += new System.EventHandler(this.Form_LoadIndicatorFunction_Load);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private IndicatorFunction indicatorFunction1;
    }
}