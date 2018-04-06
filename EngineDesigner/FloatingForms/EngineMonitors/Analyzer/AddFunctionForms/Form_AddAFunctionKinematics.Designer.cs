namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    partial class Form_AddAFunctionKinematics
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
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Panel panel3;
            this.groupBox_WithRespectTo = new System.Windows.Forms.GroupBox();
            this.radioButton_WithRespectTo_CrankThrow = new System.Windows.Forms.RadioButton();
            this.radioButton_WithRespectTo_Crankshaft = new System.Windows.Forms.RadioButton();
            this.tabPage_Kinematics = new System.Windows.Forms.TabPage();
            this.cylinderFunction_Kinematics = new EngineDesigner.FloatingForms.EngineMonitors.Analyzer.CylinderFunction();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.panel5.SuspendLayout();
            panel3.SuspendLayout();
            this.groupBox_WithRespectTo.SuspendLayout();
            this.tabPage_Kinematics.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(451, 116);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Kinematics);
            this.tabControl1.Size = new System.Drawing.Size(465, 148);
            this.tabControl1.Controls.SetChildIndex(this.tabPage_Kinematics, 0);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(panel3);
            this.panel5.Size = new System.Drawing.Size(489, 233);
            this.panel5.Controls.SetChildIndex(panel3, 0);
            this.panel5.Controls.SetChildIndex(this.tabControl1, 0);
            // 
            // panel2
            // 
            panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel2.Location = new System.Drawing.Point(0, 170);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(489, 63);
            panel2.TabIndex = 8;
            // 
            // panel3
            // 
            panel3.Controls.Add(this.groupBox_WithRespectTo);
            panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel3.Location = new System.Drawing.Point(12, 160);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(465, 61);
            panel3.TabIndex = 8;
            // 
            // groupBox_WithRespectTo
            // 
            this.groupBox_WithRespectTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_WithRespectTo.Controls.Add(this.radioButton_WithRespectTo_CrankThrow);
            this.groupBox_WithRespectTo.Controls.Add(this.radioButton_WithRespectTo_Crankshaft);
            this.groupBox_WithRespectTo.Enabled = false;
            this.groupBox_WithRespectTo.Location = new System.Drawing.Point(12, 5);
            this.groupBox_WithRespectTo.Name = "groupBox_WithRespectTo";
            this.groupBox_WithRespectTo.Size = new System.Drawing.Size(441, 44);
            this.groupBox_WithRespectTo.TabIndex = 7;
            this.groupBox_WithRespectTo.TabStop = false;
            this.groupBox_WithRespectTo.Text = "With respect to ";
            // 
            // radioButton_WithRespectTo_CrankThrow
            // 
            this.radioButton_WithRespectTo_CrankThrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_WithRespectTo_CrankThrow.AutoSize = true;
            this.radioButton_WithRespectTo_CrankThrow.Location = new System.Drawing.Point(264, 18);
            this.radioButton_WithRespectTo_CrankThrow.Name = "radioButton_WithRespectTo_CrankThrow";
            this.radioButton_WithRespectTo_CrankThrow.Size = new System.Drawing.Size(164, 17);
            this.radioButton_WithRespectTo_CrankThrow.TabIndex = 1;
            this.radioButton_WithRespectTo_CrankThrow.Text = "Crank throw (cylinder relative)";
            this.radioButton_WithRespectTo_CrankThrow.UseVisualStyleBackColor = true;
            // 
            // radioButton_WithRespectTo_Crankshaft
            // 
            this.radioButton_WithRespectTo_Crankshaft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton_WithRespectTo_Crankshaft.AutoSize = true;
            this.radioButton_WithRespectTo_Crankshaft.Checked = true;
            this.radioButton_WithRespectTo_Crankshaft.Location = new System.Drawing.Point(148, 18);
            this.radioButton_WithRespectTo_Crankshaft.Name = "radioButton_WithRespectTo_Crankshaft";
            this.radioButton_WithRespectTo_Crankshaft.Size = new System.Drawing.Size(76, 17);
            this.radioButton_WithRespectTo_Crankshaft.TabIndex = 0;
            this.radioButton_WithRespectTo_Crankshaft.TabStop = true;
            this.radioButton_WithRespectTo_Crankshaft.Text = "Crankshaft";
            this.radioButton_WithRespectTo_Crankshaft.UseVisualStyleBackColor = true;
            // 
            // tabPage_Kinematics
            // 
            this.tabPage_Kinematics.Controls.Add(this.cylinderFunction_Kinematics);
            this.tabPage_Kinematics.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Kinematics.Name = "tabPage_Kinematics";
            this.tabPage_Kinematics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Kinematics.Size = new System.Drawing.Size(457, 122);
            this.tabPage_Kinematics.TabIndex = 1;
            this.tabPage_Kinematics.Text = "Kinematics";
            this.tabPage_Kinematics.UseVisualStyleBackColor = true;
            // 
            // cylinderFunction_Kinematics
            // 
            this.cylinderFunction_Kinematics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderFunction_Kinematics.Location = new System.Drawing.Point(3, 3);
            this.cylinderFunction_Kinematics.MaximumSize = new System.Drawing.Size(32767, 234);
            this.cylinderFunction_Kinematics.MinimumSize = new System.Drawing.Size(0, 234);
            this.cylinderFunction_Kinematics.Name = "cylinderFunction_Kinematics";
            this.cylinderFunction_Kinematics.Size = new System.Drawing.Size(451, 234);
            this.cylinderFunction_Kinematics.TabIndex = 0;
            // 
            // Form_AddAFunctionKinematics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 466);
            this.Name = "Form_AddAFunctionKinematics";
            this.tabControl1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            this.groupBox_WithRespectTo.ResumeLayout(false);
            this.groupBox_WithRespectTo.PerformLayout();
            this.tabPage_Kinematics.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Kinematics;
        private CylinderFunction cylinderFunction_Kinematics;
        private System.Windows.Forms.RadioButton radioButton_WithRespectTo_Crankshaft;
        private System.Windows.Forms.RadioButton radioButton_WithRespectTo_CrankThrow;
        private System.Windows.Forms.GroupBox groupBox_WithRespectTo;
    }
}