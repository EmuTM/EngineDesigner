namespace EngineDesigner.MainForms
{
    partial class Form_MainEngine
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tools_EngineControl = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Tools_Analyzer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Tools_CycleDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tools_CrankshaftDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tools_ExhaustNote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Tools_Statistics = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // iPartEditor1
            // 
            this.iPartEditor1.EditedPartChanged += new System.EventHandler<EngineDesigner.Controls.Editors.EditedPartChangedEventArgs>(this.iPartEditor1_EditedPartChanged);
            this.iPartEditor1.EditedItemSelected += new System.EventHandler<EngineDesigner.Controls.Editors.EditedItemSelectedEventArgs>(this.iPartEditor1_EditedItemSelected);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Tools});
            this.menuStrip2.Location = new System.Drawing.Point(0, 49);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(784, 24);
            this.menuStrip2.TabIndex = 7;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem_Tools
            // 
            this.toolStripMenuItem_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Tools_EngineControl,
            this.toolStripSeparator1,
            this.toolStripMenuItem_Tools_Analyzer,
            this.toolStripSeparator2,
            this.toolStripMenuItem_Tools_CycleDiagram,
            this.toolStripMenuItem_Tools_CrankshaftDiagram,
            this.toolStripMenuItem_Tools_ExhaustNote,
            this.toolStripMenuItem_Tools_Statistics});
            this.toolStripMenuItem_Tools.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripMenuItem_Tools.MergeIndex = 3;
            this.toolStripMenuItem_Tools.Name = "toolStripMenuItem_Tools";
            this.toolStripMenuItem_Tools.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem_Tools.Text = "&Tools";
            // 
            // toolStripMenuItem_Tools_EngineControl
            // 
            this.toolStripMenuItem_Tools_EngineControl.Name = "toolStripMenuItem_Tools_EngineControl";
            this.toolStripMenuItem_Tools_EngineControl.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Tools_EngineControl.Text = "Engine control";
            this.toolStripMenuItem_Tools_EngineControl.Click += new System.EventHandler(this.toolStripMenuItem_Tools_EngineControl_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // toolStripMenuItem_Tools_Analyzer
            // 
            this.toolStripMenuItem_Tools_Analyzer.Name = "toolStripMenuItem_Tools_Analyzer";
            this.toolStripMenuItem_Tools_Analyzer.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Tools_Analyzer.Text = "Analyzer";
            this.toolStripMenuItem_Tools_Analyzer.Click += new System.EventHandler(this.toolStripMenuItem_Tools_Analyzer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(175, 6);
            // 
            // toolStripMenuItem_Tools_CycleDiagram
            // 
            this.toolStripMenuItem_Tools_CycleDiagram.Name = "toolStripMenuItem_Tools_CycleDiagram";
            this.toolStripMenuItem_Tools_CycleDiagram.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Tools_CycleDiagram.Text = "Cycle diagram";
            this.toolStripMenuItem_Tools_CycleDiagram.Click += new System.EventHandler(this.toolStripMenuItem_Tools_CycleDiagram_Click);
            // 
            // toolStripMenuItem_Tools_CrankshaftDiagram
            // 
            this.toolStripMenuItem_Tools_CrankshaftDiagram.Name = "toolStripMenuItem_Tools_CrankshaftDiagram";
            this.toolStripMenuItem_Tools_CrankshaftDiagram.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Tools_CrankshaftDiagram.Text = "Crankshaft diagram";
            this.toolStripMenuItem_Tools_CrankshaftDiagram.Click += new System.EventHandler(this.toolStripMenuItem_Tools_CrankshaftDiagram_Click);
            // 
            // toolStripMenuItem_Tools_ExhaustNote
            // 
            this.toolStripMenuItem_Tools_ExhaustNote.Name = "toolStripMenuItem_Tools_ExhaustNote";
            this.toolStripMenuItem_Tools_ExhaustNote.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Tools_ExhaustNote.Text = "Exhaust note";
            this.toolStripMenuItem_Tools_ExhaustNote.Click += new System.EventHandler(this.toolStripMenuItem_Tools_ExhaustNote_Click);
            // 
            // toolStripMenuItem_Tools_Statistics
            // 
            this.toolStripMenuItem_Tools_Statistics.Name = "toolStripMenuItem_Tools_Statistics";
            this.toolStripMenuItem_Tools_Statistics.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem_Tools_Statistics.Text = "Statistics";
            this.toolStripMenuItem_Tools_Statistics.Click += new System.EventHandler(this.toolStripMenuItem_Statistics_Click);
            // 
            // Form_MainEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form_MainEngine";
            this.Controls.SetChildIndex(this.menuStrip2, 0);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools_CycleDiagram;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools_CrankshaftDiagram;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools_EngineControl;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools_ExhaustNote;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools_Analyzer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Tools_Statistics;
    }
}
