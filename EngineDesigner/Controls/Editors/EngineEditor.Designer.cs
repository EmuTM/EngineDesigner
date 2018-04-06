namespace EngineDesigner.Controls.Editors
{
    partial class EngineEditor
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_CopyAsTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_PasteFromTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit_CopyAsTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit_PasteFromTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_CopyAsTemplate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_PastFromTemplate = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.ContextMenuStrip = this.contextMenuStrip1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_CopyAsTemplate,
            this.toolStripMenuItem_PasteFromTemplate});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(182, 48);
            // 
            // toolStripMenuItem_CopyAsTemplate
            // 
            this.toolStripMenuItem_CopyAsTemplate.Enabled = false;
            this.toolStripMenuItem_CopyAsTemplate.Image = global::EngineDesigner.Properties.Resources.copy16x16;
            this.toolStripMenuItem_CopyAsTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_CopyAsTemplate.Name = "toolStripMenuItem_CopyAsTemplate";
            this.toolStripMenuItem_CopyAsTemplate.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem_CopyAsTemplate.Text = "Copy as template";
            this.toolStripMenuItem_CopyAsTemplate.Click += new System.EventHandler(this.toolStripMenuItem_CopyAsTemplate_Click);
            // 
            // toolStripMenuItem_PasteFromTemplate
            // 
            this.toolStripMenuItem_PasteFromTemplate.Enabled = false;
            this.toolStripMenuItem_PasteFromTemplate.Image = global::EngineDesigner.Properties.Resources.paste16x16;
            this.toolStripMenuItem_PasteFromTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_PasteFromTemplate.Name = "toolStripMenuItem_PasteFromTemplate";
            this.toolStripMenuItem_PasteFromTemplate.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem_PasteFromTemplate.Text = "Paste from template";
            this.toolStripMenuItem_PasteFromTemplate.Click += new System.EventHandler(this.toolStripMenuItem_PasteFromTemplate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Edit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 25);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(200, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip2";
            // 
            // toolStripMenuItem_Edit
            // 
            this.toolStripMenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Edit_CopyAsTemplate,
            this.toolStripMenuItem_Edit_PasteFromTemplate});
            this.toolStripMenuItem_Edit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripMenuItem_Edit.MergeIndex = 1;
            this.toolStripMenuItem_Edit.Name = "toolStripMenuItem_Edit";
            this.toolStripMenuItem_Edit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem_Edit.Text = "&Edit";
            // 
            // toolStripMenuItem_Edit_CopyAsTemplate
            // 
            this.toolStripMenuItem_Edit_CopyAsTemplate.Enabled = false;
            this.toolStripMenuItem_Edit_CopyAsTemplate.Image = global::EngineDesigner.Properties.Resources.copy16x16;
            this.toolStripMenuItem_Edit_CopyAsTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Edit_CopyAsTemplate.Name = "toolStripMenuItem_Edit_CopyAsTemplate";
            this.toolStripMenuItem_Edit_CopyAsTemplate.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem_Edit_CopyAsTemplate.Text = "Copy as template";
            this.toolStripMenuItem_Edit_CopyAsTemplate.Click += new System.EventHandler(this.toolStripMenuItem_Edit_CopyAsTemplate_Click);
            // 
            // toolStripMenuItem_Edit_PasteFromTemplate
            // 
            this.toolStripMenuItem_Edit_PasteFromTemplate.Enabled = false;
            this.toolStripMenuItem_Edit_PasteFromTemplate.Image = global::EngineDesigner.Properties.Resources.paste16x16;
            this.toolStripMenuItem_Edit_PasteFromTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Edit_PasteFromTemplate.Name = "toolStripMenuItem_Edit_PasteFromTemplate";
            this.toolStripMenuItem_Edit_PasteFromTemplate.Size = new System.Drawing.Size(181, 22);
            this.toolStripMenuItem_Edit_PasteFromTemplate.Text = "Paste from template";
            this.toolStripMenuItem_Edit_PasteFromTemplate.Click += new System.EventHandler(this.toolStripMenuItem_Edit_PasteFromTemplate_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButton_CopyAsTemplate,
            this.toolStripButton_PastFromTemplate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(200, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_CopyAsTemplate
            // 
            this.toolStripButton_CopyAsTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_CopyAsTemplate.Enabled = false;
            this.toolStripButton_CopyAsTemplate.Image = global::EngineDesigner.Properties.Resources.copy16x16;
            this.toolStripButton_CopyAsTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_CopyAsTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CopyAsTemplate.Name = "toolStripButton_CopyAsTemplate";
            this.toolStripButton_CopyAsTemplate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_CopyAsTemplate.Text = "Copy as template";
            this.toolStripButton_CopyAsTemplate.Click += new System.EventHandler(this.toolStripButton_CopyAsTemplate_Click);
            // 
            // toolStripButton_PastFromTemplate
            // 
            this.toolStripButton_PastFromTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_PastFromTemplate.Enabled = false;
            this.toolStripButton_PastFromTemplate.Image = global::EngineDesigner.Properties.Resources.paste16x16;
            this.toolStripButton_PastFromTemplate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_PastFromTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_PastFromTemplate.Name = "toolStripButton_PastFromTemplate";
            this.toolStripButton_PastFromTemplate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_PastFromTemplate.Text = "Past from template";
            this.toolStripButton_PastFromTemplate.Click += new System.EventHandler(this.toolStripButton_PastFromTemplate_Click);
            // 
            // EngineEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EngineEditor";
            this.Controls.SetChildIndex(this.propertyGrid1, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CopyAsTemplate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_PasteFromTemplate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit_CopyAsTemplate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit_PasteFromTemplate;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_CopyAsTemplate;
        private System.Windows.Forms.ToolStripButton toolStripButton_PastFromTemplate;
    }
}
