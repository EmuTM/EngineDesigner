namespace EngineDesigner.Controls.Viewers
{
    partial class EngineViewer
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
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Animated = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_View_Animated = new System.Windows.Forms.ToolStripMenuItem();
            this.iPartSketch1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // iPartSketch1
            // 
            // 
            // 
            // 
            this.iPartSketch1.Camera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.iPartSketch1.Controls.Add(this.menuStrip2);
            this.iPartSketch1.Controls.SetChildIndex(this.menuStrip2, 0);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItem_Animated});
            this.contextMenuStrip2.Name = "contextMenuStrip_EngineSketch";
            this.contextMenuStrip2.Size = new System.Drawing.Size(127, 32);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // toolStripMenuItem_Animated
            // 
            this.toolStripMenuItem_Animated.Checked = true;
            this.toolStripMenuItem_Animated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_Animated.Name = "toolStripMenuItem_Animated";
            this.toolStripMenuItem_Animated.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItem_Animated.Text = "Animated";
            this.toolStripMenuItem_Animated.Click += new System.EventHandler(this.toolStripMenuItem_Animated_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(150, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_View_Animated});
            this.viewToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolStripMenuItem_View_Animated
            // 
            this.toolStripMenuItem_View_Animated.Checked = true;
            this.toolStripMenuItem_View_Animated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_View_Animated.Name = "toolStripMenuItem_View_Animated";
            this.toolStripMenuItem_View_Animated.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_View_Animated.Text = "Animated";
            this.toolStripMenuItem_View_Animated.Click += new System.EventHandler(this.toolStripMenuItem_View_Animated_Click);
            // 
            // EngineViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "EngineViewer";
            this.Controls.SetChildIndex(this.iPartSketch1, 0);
            this.iPartSketch1.ResumeLayout(false);
            this.iPartSketch1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Animated;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_View_Animated;
    }
}
