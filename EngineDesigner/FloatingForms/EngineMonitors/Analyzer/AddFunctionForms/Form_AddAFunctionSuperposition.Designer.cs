namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_AddAFunctionSuperposition
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
            this.tabPage_Superposition = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Function = new System.Windows.Forms.TextBox();
            this.label_Function_Reference = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage_Superposition.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Superposition);
            this.tabControl1.Size = new System.Drawing.Size(465, 73);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(489, 97);
            // 
            // tabPage_Superposition
            // 
            this.tabPage_Superposition.Controls.Add(this.panel1);
            this.tabPage_Superposition.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Superposition.Name = "tabPage_Superposition";
            this.tabPage_Superposition.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Superposition.Size = new System.Drawing.Size(457, 47);
            this.tabPage_Superposition.TabIndex = 0;
            this.tabPage_Superposition.Text = "Superposition";
            this.tabPage_Superposition.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_Function);
            this.panel1.Controls.Add(this.label_Function_Reference);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 41);
            this.panel1.TabIndex = 8;
            // 
            // textBox_Function
            // 
            this.textBox_Function.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Function.Location = new System.Drawing.Point(107, 12);
            this.textBox_Function.Name = "textBox_Function";
            this.textBox_Function.Size = new System.Drawing.Size(336, 20);
            this.textBox_Function.TabIndex = 6;
            // 
            // label_Function_Reference
            // 
            this.label_Function_Reference.AutoSize = true;
            this.label_Function_Reference.Location = new System.Drawing.Point(6, 15);
            this.label_Function_Reference.Name = "label_Function_Reference";
            this.label_Function_Reference.Size = new System.Drawing.Size(48, 13);
            this.label_Function_Reference.TabIndex = 5;
            this.label_Function_Reference.Text = "Function";
            // 
            // Form_AddAFunctionSuperposition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 330);
            this.Name = "Form_AddAFunctionSuperposition";
            this.Load += new System.EventHandler(this.Form_AddAFunctionSuperposition_Load);
            this.tabControl1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabPage_Superposition.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Superposition;
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Function_Reference;
        private System.Windows.Forms.TextBox textBox_Function;

    }
}