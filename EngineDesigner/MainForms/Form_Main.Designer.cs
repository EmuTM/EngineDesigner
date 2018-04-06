namespace EngineDesigner.MainForms
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_New_Engine_Wizard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_New_Engine_Empty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_New_Function_Wizard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_New_Function_Empty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton_New = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem_New_Engine_Wizard2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_New_Function_Wizard2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File});
            this.menuStrip2.Location = new System.Drawing.Point(0, 49);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(784, 24);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File_New});
            this.toolStripMenuItem_File.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem_File.Text = "&File";
            // 
            // toolStripMenuItem_File_New
            // 
            this.toolStripMenuItem_File_New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_New_Engine_Wizard,
            this.toolStripMenuItem_New_Engine_Empty,
            this.toolStripSeparator1,
            this.toolStripMenuItem_New_Function_Wizard,
            this.toolStripMenuItem_New_Function_Empty});
            this.toolStripMenuItem_File_New.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripMenuItem_File_New.Name = "toolStripMenuItem_File_New";
            this.toolStripMenuItem_File_New.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem_File_New.Text = "New";
            // 
            // toolStripMenuItem_New_Engine_Wizard
            // 
            this.toolStripMenuItem_New_Engine_Wizard.Image = global::EngineDesigner.Properties.Resources.wizard16x16;
            this.toolStripMenuItem_New_Engine_Wizard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_New_Engine_Wizard.Name = "toolStripMenuItem_New_Engine_Wizard";
            this.toolStripMenuItem_New_Engine_Wizard.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem_New_Engine_Wizard.Text = "Engine (wizard)";
            this.toolStripMenuItem_New_Engine_Wizard.Click += new System.EventHandler(this.toolStripMenuItem_New_Engine_Wizard_Click);
            // 
            // toolStripMenuItem_New_Engine_Empty
            // 
            this.toolStripMenuItem_New_Engine_Empty.Image = global::EngineDesigner.Properties.Resources.document_plain_new16x16;
            this.toolStripMenuItem_New_Engine_Empty.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_New_Engine_Empty.Name = "toolStripMenuItem_New_Engine_Empty";
            this.toolStripMenuItem_New_Engine_Empty.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem_New_Engine_Empty.Text = "Engine (empty)";
            this.toolStripMenuItem_New_Engine_Empty.Click += new System.EventHandler(this.toolStripMenuItem_New_Engine_Empty_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // toolStripMenuItem_New_Function_Wizard
            // 
            this.toolStripMenuItem_New_Function_Wizard.Image = global::EngineDesigner.Properties.Resources.wizard16x16;
            this.toolStripMenuItem_New_Function_Wizard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_New_Function_Wizard.Name = "toolStripMenuItem_New_Function_Wizard";
            this.toolStripMenuItem_New_Function_Wizard.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem_New_Function_Wizard.Text = "Function (wizard)";
            this.toolStripMenuItem_New_Function_Wizard.Click += new System.EventHandler(this.toolStripMenuItem_New_Function_Wizard_Click);
            // 
            // toolStripMenuItem_New_Function_Empty
            // 
            this.toolStripMenuItem_New_Function_Empty.Image = global::EngineDesigner.Properties.Resources.document_plain_new16x16;
            this.toolStripMenuItem_New_Function_Empty.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_New_Function_Empty.Name = "toolStripMenuItem_New_Function_Empty";
            this.toolStripMenuItem_New_Function_Empty.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem_New_Function_Empty.Text = "Function (empty)";
            this.toolStripMenuItem_New_Function_Empty.Click += new System.EventHandler(this.toolStripMenuItem_New_Function_Empty_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton_New});
            this.toolStrip2.Location = new System.Drawing.Point(0, 73);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(784, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripDropDownButton_New
            // 
            this.toolStripDropDownButton_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton_New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_New_Engine_Wizard2,
            this.toolStripSeparator2,
            this.toolStripMenuItem_New_Function_Wizard2});
            this.toolStripDropDownButton_New.Image = global::EngineDesigner.Properties.Resources.document_plain_new16x16;
            this.toolStripDropDownButton_New.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton_New.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripDropDownButton_New.Name = "toolStripDropDownButton_New";
            this.toolStripDropDownButton_New.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton_New.Text = "New";
            // 
            // toolStripMenuItem_New_Engine_Wizard2
            // 
            this.toolStripMenuItem_New_Engine_Wizard2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_New_Engine_Wizard2.Image")));
            this.toolStripMenuItem_New_Engine_Wizard2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_New_Engine_Wizard2.Name = "toolStripMenuItem_New_Engine_Wizard2";
            this.toolStripMenuItem_New_Engine_Wizard2.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem_New_Engine_Wizard2.Text = "Engine (wizard)";
            this.toolStripMenuItem_New_Engine_Wizard2.Click += new System.EventHandler(this.toolStripMenuItem_New_Engine_Wizard2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(163, 6);
            // 
            // toolStripMenuItem_New_Function_Wizard2
            // 
            this.toolStripMenuItem_New_Function_Wizard2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_New_Function_Wizard2.Image")));
            this.toolStripMenuItem_New_Function_Wizard2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_New_Function_Wizard2.Name = "toolStripMenuItem_New_Function_Wizard2";
            this.toolStripMenuItem_New_Function_Wizard2.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem_New_Function_Wizard2.Text = "Function (wizard)";
            this.toolStripMenuItem_New_Function_Wizard2.Click += new System.EventHandler(this.toolStripMenuItem_New_Function_Wizard2_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form_Main";
            this.Text = "Form_Main";
            this.Controls.SetChildIndex(this.menuStrip2, 0);
            this.Controls.SetChildIndex(this.toolStrip2, 0);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_New;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New_Engine_Wizard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New_Engine_Empty;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New_Function_Wizard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New_Function_Empty;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton_New;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New_Engine_Wizard2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_New_Function_Wizard2;
    }
}