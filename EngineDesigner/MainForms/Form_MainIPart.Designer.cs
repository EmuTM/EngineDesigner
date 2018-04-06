namespace EngineDesigner.MainForms
{
    partial class Form_MainIPart
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupPanel1 = new EngineDesigner.Environment.Controls.GroupPanel();
            this.iPartEditor1 = new EngineDesigner.Controls.Editors.IPartEditor();
            this.iPartViewer1 = new EngineDesigner.Controls.Viewers.IPartViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.iPartViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 491);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 6;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupPanel1.Controls.Add(this.iPartEditor1);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(257, 491);
            this.groupPanel1.TabIndex = 1;
            this.groupPanel1.Text = "Properties";
            // 
            // iPartEditor1
            // 
            this.iPartEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iPartEditor1.EditedPart = null;
            this.iPartEditor1.Location = new System.Drawing.Point(0, 20);
            this.iPartEditor1.Name = "iPartEditor1";
            this.iPartEditor1.Size = new System.Drawing.Size(255, 489);
            this.iPartEditor1.TabIndex = 0;
            this.iPartEditor1.EditedPartChanged += new System.EventHandler<EngineDesigner.Controls.Editors.EditedPartChangedEventArgs>(this.iPartEditor1_EditedPartChanged);
            // 
            // iPartViewer1
            // 
            this.iPartViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iPartViewer1.Location = new System.Drawing.Point(0, 0);
            this.iPartViewer1.Name = "iPartViewer1";
            this.iPartViewer1.Size = new System.Drawing.Size(523, 491);
            this.iPartViewer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form_MainIPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_MainIPart";
            this.Load += new System.EventHandler(this.Form_MainIPart_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private EngineDesigner.Environment.Controls.GroupPanel groupPanel1;
        protected EngineDesigner.Controls.Editors.IPartEditor iPartEditor1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected EngineDesigner.Controls.Viewers.IPartViewer iPartViewer1;


    }
}
