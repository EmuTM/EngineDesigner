namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class CylinderFunction
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_Function = new System.Windows.Forms.ComboBox();
            this.label_HarmonicOrder = new System.Windows.Forms.Label();
            this.label_Cylinder = new System.Windows.Forms.Label();
            this.comboBox_HarmonicOrder = new System.Windows.Forms.ComboBox();
            this.label_Function = new System.Windows.Forms.Label();
            this.comboBox_Cylinder = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_Function);
            this.panel1.Controls.Add(this.label_HarmonicOrder);
            this.panel1.Controls.Add(this.label_Cylinder);
            this.panel1.Controls.Add(this.comboBox_HarmonicOrder);
            this.panel1.Controls.Add(this.label_Function);
            this.panel1.Controls.Add(this.comboBox_Cylinder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 120);
            this.panel1.TabIndex = 15;
            // 
            // comboBox_Function
            // 
            this.comboBox_Function.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Function.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Function.FormattingEnabled = true;
            this.comboBox_Function.Location = new System.Drawing.Point(107, 12);
            this.comboBox_Function.Name = "comboBox_Function";
            this.comboBox_Function.Size = new System.Drawing.Size(228, 21);
            this.comboBox_Function.TabIndex = 12;
            this.comboBox_Function.SelectedIndexChanged += new System.EventHandler(this.comboBox_Function_SelectedIndexChanged);
            // 
            // label_HarmonicOrder
            // 
            this.label_HarmonicOrder.AutoSize = true;
            this.label_HarmonicOrder.Location = new System.Drawing.Point(6, 95);
            this.label_HarmonicOrder.Name = "label_HarmonicOrder";
            this.label_HarmonicOrder.Size = new System.Drawing.Size(79, 13);
            this.label_HarmonicOrder.TabIndex = 13;
            this.label_HarmonicOrder.Text = "Harmonic order";
            // 
            // label_Cylinder
            // 
            this.label_Cylinder.AutoSize = true;
            this.label_Cylinder.Location = new System.Drawing.Point(6, 55);
            this.label_Cylinder.Name = "label_Cylinder";
            this.label_Cylinder.Size = new System.Drawing.Size(44, 13);
            this.label_Cylinder.TabIndex = 13;
            this.label_Cylinder.Text = "Cylinder";
            // 
            // comboBox_HarmonicOrder
            // 
            this.comboBox_HarmonicOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_HarmonicOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_HarmonicOrder.FormattingEnabled = true;
            this.comboBox_HarmonicOrder.Location = new System.Drawing.Point(107, 92);
            this.comboBox_HarmonicOrder.Name = "comboBox_HarmonicOrder";
            this.comboBox_HarmonicOrder.Size = new System.Drawing.Size(228, 21);
            this.comboBox_HarmonicOrder.TabIndex = 12;
            this.comboBox_HarmonicOrder.SelectedIndexChanged += new System.EventHandler(this.comboBox_HarmonicOrder_SelectedIndexChanged);
            // 
            // label_Function
            // 
            this.label_Function.AutoSize = true;
            this.label_Function.Location = new System.Drawing.Point(6, 15);
            this.label_Function.Name = "label_Function";
            this.label_Function.Size = new System.Drawing.Size(48, 13);
            this.label_Function.TabIndex = 13;
            this.label_Function.Text = "Function";
            // 
            // comboBox_Cylinder
            // 
            this.comboBox_Cylinder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Cylinder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Cylinder.FormattingEnabled = true;
            this.comboBox_Cylinder.Location = new System.Drawing.Point(107, 52);
            this.comboBox_Cylinder.Name = "comboBox_Cylinder";
            this.comboBox_Cylinder.Size = new System.Drawing.Size(228, 21);
            this.comboBox_Cylinder.TabIndex = 12;
            this.comboBox_Cylinder.SelectedIndexChanged += new System.EventHandler(this.comboBox_Cylinder_SelectedIndexChanged);
            // 
            // CylinderFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(32767, 120);
            this.MinimumSize = new System.Drawing.Size(0, 120);
            this.Name = "CylinderFunction";
            this.Size = new System.Drawing.Size(343, 120);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_Function;
        private System.Windows.Forms.Label label_HarmonicOrder;
        private System.Windows.Forms.Label label_Cylinder;
        private System.Windows.Forms.ComboBox comboBox_HarmonicOrder;
        private System.Windows.Forms.Label label_Function;
        private System.Windows.Forms.ComboBox comboBox_Cylinder;
    }
}
