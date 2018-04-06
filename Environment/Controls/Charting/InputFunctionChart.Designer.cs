namespace EngineDesigner.Environment.Controls.Charting
{
    partial class InputFunctionChart
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_AddPointHere = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DeletePoint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit_AddPointHere = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Edit_DeletePoint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_AddPointHere = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_DeletePoint = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Raised;
            this.chart1.Location = new System.Drawing.Point(0, 73);
            this.chart1.Size = new System.Drawing.Size(320, 127);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AddPointHere,
            this.toolStripMenuItem_DeletePoint,
            this.toolStripSeparator1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(154, 54);
            // 
            // toolStripMenuItem_AddPointHere
            // 
            this.toolStripMenuItem_AddPointHere.Enabled = false;
            this.toolStripMenuItem_AddPointHere.Image = global::EngineDesigner.Environment.Properties.Resources.add16x16;
            this.toolStripMenuItem_AddPointHere.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_AddPointHere.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_AddPointHere.MergeIndex = 0;
            this.toolStripMenuItem_AddPointHere.Name = "toolStripMenuItem_AddPointHere";
            this.toolStripMenuItem_AddPointHere.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItem_AddPointHere.Text = "Add point here";
            this.toolStripMenuItem_AddPointHere.Click += new System.EventHandler(this.toolStripMenuItem_AddPointHere_Click);
            // 
            // toolStripMenuItem_DeletePoint
            // 
            this.toolStripMenuItem_DeletePoint.Enabled = false;
            this.toolStripMenuItem_DeletePoint.Image = global::EngineDesigner.Environment.Properties.Resources.remove16x16;
            this.toolStripMenuItem_DeletePoint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_DeletePoint.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_DeletePoint.MergeIndex = 1;
            this.toolStripMenuItem_DeletePoint.Name = "toolStripMenuItem_DeletePoint";
            this.toolStripMenuItem_DeletePoint.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItem_DeletePoint.Text = "Delete point";
            this.toolStripMenuItem_DeletePoint.Click += new System.EventHandler(this.toolStripMenuItem_DeletePoint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 2;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Edit});
            this.menuStrip2.Location = new System.Drawing.Point(0, 49);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(320, 24);
            this.menuStrip2.TabIndex = 4;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem_Edit
            // 
            this.toolStripMenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Edit_AddPointHere,
            this.toolStripMenuItem_Edit_DeletePoint});
            this.toolStripMenuItem_Edit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolStripMenuItem_Edit.Name = "toolStripMenuItem_Edit";
            this.toolStripMenuItem_Edit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem_Edit.Text = "&Edit";
            // 
            // toolStripMenuItem_Edit_AddPointHere
            // 
            this.toolStripMenuItem_Edit_AddPointHere.Enabled = false;
            this.toolStripMenuItem_Edit_AddPointHere.Image = global::EngineDesigner.Environment.Properties.Resources.add16x16;
            this.toolStripMenuItem_Edit_AddPointHere.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Edit_AddPointHere.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_Edit_AddPointHere.MergeIndex = 0;
            this.toolStripMenuItem_Edit_AddPointHere.Name = "toolStripMenuItem_Edit_AddPointHere";
            this.toolStripMenuItem_Edit_AddPointHere.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItem_Edit_AddPointHere.Text = "Add point here";
            this.toolStripMenuItem_Edit_AddPointHere.Click += new System.EventHandler(this.toolStripMenuItem_Edit_AddPointHere_Click);
            // 
            // toolStripMenuItem_Edit_DeletePoint
            // 
            this.toolStripMenuItem_Edit_DeletePoint.Enabled = false;
            this.toolStripMenuItem_Edit_DeletePoint.Image = global::EngineDesigner.Environment.Properties.Resources.remove16x16;
            this.toolStripMenuItem_Edit_DeletePoint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem_Edit_DeletePoint.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_Edit_DeletePoint.MergeIndex = 1;
            this.toolStripMenuItem_Edit_DeletePoint.Name = "toolStripMenuItem_Edit_DeletePoint";
            this.toolStripMenuItem_Edit_DeletePoint.Size = new System.Drawing.Size(153, 22);
            this.toolStripMenuItem_Edit_DeletePoint.Text = "Delete point";
            this.toolStripMenuItem_Edit_DeletePoint.Click += new System.EventHandler(this.toolStripMenuItem_Edit_DeletePoint_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.toolStripButton_AddPointHere,
            this.toolStripButton_DeletePoint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(320, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_AddPointHere
            // 
            this.toolStripButton_AddPointHere.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_AddPointHere.Enabled = false;
            this.toolStripButton_AddPointHere.Image = global::EngineDesigner.Environment.Properties.Resources.add16x16;
            this.toolStripButton_AddPointHere.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AddPointHere.Name = "toolStripButton_AddPointHere";
            this.toolStripButton_AddPointHere.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_AddPointHere.Text = "Add point here";
            this.toolStripButton_AddPointHere.Click += new System.EventHandler(this.toolStripButton_AddPointHere_Click);
            // 
            // toolStripButton_DeletePoint
            // 
            this.toolStripButton_DeletePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_DeletePoint.Enabled = false;
            this.toolStripButton_DeletePoint.Image = global::EngineDesigner.Environment.Properties.Resources.remove16x16;
            this.toolStripButton_DeletePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_DeletePoint.Name = "toolStripButton_DeletePoint";
            this.toolStripButton_DeletePoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_DeletePoint.Text = "Delete point";
            this.toolStripButton_DeletePoint.Click += new System.EventHandler(this.toolStripButton_DeletePoint_Click);
            // 
            // InputFunctionChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "InputFunctionChart";
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.menuStrip2, 0);
            this.Controls.SetChildIndex(this.chart1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AddPointHere;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DeletePoint;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit_AddPointHere;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Edit_DeletePoint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_AddPointHere;
        private System.Windows.Forms.ToolStripButton toolStripButton_DeletePoint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}
