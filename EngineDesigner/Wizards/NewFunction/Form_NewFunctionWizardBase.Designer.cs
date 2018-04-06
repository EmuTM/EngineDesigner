namespace EngineDesigner.Wizards.NewFunction
{
    partial class Form_NewFunctionWizardBase
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
            EngineDesigner.Environment.Controls.Bookmark bookmark1 = new EngineDesigner.Environment.Controls.Bookmark();
            EngineDesigner.Environment.Controls.Bookmark bookmark2 = new EngineDesigner.Environment.Controls.Bookmark();
            EngineDesigner.Environment.Controls.Bookmark bookmark3 = new EngineDesigner.Environment.Controls.Bookmark();
            EngineDesigner.Environment.Controls.Bookmark bookmark4 = new EngineDesigner.Environment.Controls.Bookmark();
            EngineDesigner.Environment.Controls.Bookmark bookmark5 = new EngineDesigner.Environment.Controls.Bookmark();
            EngineDesigner.Environment.Controls.Bookmark bookmark6 = new EngineDesigner.Environment.Controls.Bookmark();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_NewFunctionWizardBase));
            this.SuspendLayout();
            // 
            // tableOfContents1
            // 
            bookmark1.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark1.Title = "Start";
            bookmark2.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark2.Title = "Type of function";
            bookmark3.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark3.Title = "Min / Max values";
            bookmark4.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark4.Title = "Number of starting points";
            bookmark5.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark5.Title = "Alter function";
            bookmark6.SubBookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            bookmark6.Title = "Finish";
            this.tableOfContents1.Bookmarks = new EngineDesigner.Environment.Controls.Bookmark[] {
        bookmark1,
        bookmark2,
        bookmark3,
        bookmark4,
        bookmark5,
        bookmark6};
            this.tableOfContents1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableOfContents1.ItemHeight = 24;
            this.tableOfContents1.Size = new System.Drawing.Size(268, 416);
            this.tableOfContents1.TabStop = false;
            // 
            // Form_NewFunctionWizardBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackEnabled = true;
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Name = "Form_NewFunctionWizardBase";
            this.Text = "New function wizard";
            this.Title = "New function wizard";
            this.TotalSteps = 6;
            this.ResumeLayout(false);

        }

        #endregion

    }
}
