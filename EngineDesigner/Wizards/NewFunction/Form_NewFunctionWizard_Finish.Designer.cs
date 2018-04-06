namespace EngineDesigner.Wizards.NewFunction
{
    partial class Form_NewFunctionWizard_Finish
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
            this.label_Result = new System.Windows.Forms.Label();
            this.label_Info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Result
            // 
            this.label_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Result.Location = new System.Drawing.Point(311, 111);
            this.label_Result.Name = "label_Result";
            this.label_Result.Size = new System.Drawing.Size(514, 324);
            this.label_Result.TabIndex = 5;
            this.label_Result.Text = "_RESULT_";
            // 
            // label_Info
            // 
            this.label_Info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Info.AutoSize = true;
            this.label_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_Info.Location = new System.Drawing.Point(280, 68);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(41, 13);
            this.label_Info.TabIndex = 6;
            this.label_Info.Text = "Done!";
            // 
            // Form_NewFunctionWizard_Finish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.label_Info);
            this.Controls.Add(this.label_Result);
            this.CurrentStep = 6;
            this.Name = "Form_NewFunctionWizard_Finish";
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            this.Controls.SetChildIndex(this.label_Result, 0);
            this.Controls.SetChildIndex(this.label_Info, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Result;
        private System.Windows.Forms.Label label_Info;

    }
}
