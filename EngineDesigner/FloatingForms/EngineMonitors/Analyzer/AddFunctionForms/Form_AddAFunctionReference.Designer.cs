namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_AddAFunctionReference
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
            this.tabPage_Reference = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.report1 = new EngineDesigner.Environment.Controls.Report();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkLabel_ValidateExpression = new System.Windows.Forms.LinkLabel();
            this.customComboBox_Expression = new EngineDesigner.Environment.Controls.CustomComboBox();
            this.label_Expression = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage_Reference.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Reference);
            this.tabControl1.Size = new System.Drawing.Size(465, 152);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(489, 176);
            // 
            // tabPage_Reference
            // 
            this.tabPage_Reference.Controls.Add(this.panel1);
            this.tabPage_Reference.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Reference.Name = "tabPage_Reference";
            this.tabPage_Reference.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Reference.Size = new System.Drawing.Size(457, 126);
            this.tabPage_Reference.TabIndex = 0;
            this.tabPage_Reference.Text = "Reference";
            this.tabPage_Reference.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 120);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8);
            this.panel2.Size = new System.Drawing.Size(451, 73);
            this.panel2.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.report1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(435, 57);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tokens";
            // 
            // report1
            // 
            this.report1.Clickable = true;
            this.report1.ColumnHeaderTextKey = "Token";
            this.report1.ColumnHeaderTextValue = "Description";
            this.report1.ColumnWidthImage = 0;
            this.report1.ColumnWidthKey = 100;
            this.report1.ColumnWidthValue = 290;
            this.report1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.report1.Location = new System.Drawing.Point(8, 21);
            this.report1.Name = "report1";
            this.report1.ReportItems = new EngineDesigner.Environment.Controls.ReportItem[0];
            this.report1.Size = new System.Drawing.Size(419, 28);
            this.report1.TabIndex = 0;
            this.report1.ReportItemDoubleClicked += new System.EventHandler<EngineDesigner.Environment.Controls.ReportItemEventArgs>(this.report1_ReportItemDoubleClicked);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.linkLabel_ValidateExpression);
            this.panel3.Controls.Add(this.customComboBox_Expression);
            this.panel3.Controls.Add(this.label_Expression);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(451, 47);
            this.panel3.TabIndex = 13;
            // 
            // linkLabel_ValidateExpression
            // 
            this.linkLabel_ValidateExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_ValidateExpression.AutoSize = true;
            this.linkLabel_ValidateExpression.Location = new System.Drawing.Point(398, 15);
            this.linkLabel_ValidateExpression.Name = "linkLabel_ValidateExpression";
            this.linkLabel_ValidateExpression.Size = new System.Drawing.Size(45, 13);
            this.linkLabel_ValidateExpression.TabIndex = 12;
            this.linkLabel_ValidateExpression.TabStop = true;
            this.linkLabel_ValidateExpression.Text = "Validate";
            this.linkLabel_ValidateExpression.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel_ValidateExpression.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ValidateExpression_LinkClicked);
            // 
            // customComboBox_Expression
            // 
            this.customComboBox_Expression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customComboBox_Expression.FormattingEnabled = true;
            this.customComboBox_Expression.Location = new System.Drawing.Point(107, 12);
            this.customComboBox_Expression.Name = "customComboBox_Expression";
            this.customComboBox_Expression.Size = new System.Drawing.Size(285, 21);
            this.customComboBox_Expression.TabIndex = 10;
            this.customComboBox_Expression.Enter += new System.EventHandler(this.customComboBox_Expression_Enter);
            this.customComboBox_Expression.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_Expression_KeyPress);
            // 
            // label_Expression
            // 
            this.label_Expression.AutoSize = true;
            this.label_Expression.Location = new System.Drawing.Point(6, 15);
            this.label_Expression.Name = "label_Expression";
            this.label_Expression.Size = new System.Drawing.Size(58, 13);
            this.label_Expression.TabIndex = 9;
            this.label_Expression.Text = "Expression";
            // 
            // Form_AddAFunctionReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 409);
            this.Name = "Form_AddAFunctionReference";
            this.Load += new System.EventHandler(this.Form_AddAFunctionReference_Load);
            this.tabControl1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabPage_Reference.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Reference;
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Expression;
        private EngineDesigner.Environment.Controls.CustomComboBox customComboBox_Expression;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private EngineDesigner.Environment.Controls.Report report1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.LinkLabel linkLabel_ValidateExpression;

    }
}