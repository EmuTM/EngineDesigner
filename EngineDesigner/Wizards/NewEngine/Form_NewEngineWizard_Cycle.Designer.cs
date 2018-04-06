namespace EngineDesigner.Wizards.NewEngine
{
    partial class Form_NewEngineWizard_Cycle
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
            this.radioButton_TwoStroke = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton_FourStroke = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButton_TwoStroke
            // 
            this.radioButton_TwoStroke.AutoSize = true;
            this.radioButton_TwoStroke.Location = new System.Drawing.Point(311, 110);
            this.radioButton_TwoStroke.Name = "radioButton_TwoStroke";
            this.radioButton_TwoStroke.Size = new System.Drawing.Size(78, 17);
            this.radioButton_TwoStroke.TabIndex = 1;
            this.radioButton_TwoStroke.Text = "Two stroke";
            this.radioButton_TwoStroke.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(339, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(314, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "This type of engine completes a power cycle every crankshaft revolution and with " +
                "two strokes.";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(280, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the engine\'s cycle:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(339, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(314, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "This type of engine completes a power cycle every second crankshaft revolution an" +
                "d with four strokes.";
            // 
            // radioButton_FourStroke
            // 
            this.radioButton_FourStroke.AutoSize = true;
            this.radioButton_FourStroke.Checked = true;
            this.radioButton_FourStroke.Location = new System.Drawing.Point(311, 199);
            this.radioButton_FourStroke.Name = "radioButton_FourStroke";
            this.radioButton_FourStroke.Size = new System.Drawing.Size(78, 17);
            this.radioButton_FourStroke.TabIndex = 3;
            this.radioButton_FourStroke.TabStop = true;
            this.radioButton_FourStroke.Text = "Four stroke";
            this.radioButton_FourStroke.UseVisualStyleBackColor = true;
            // 
            // Form_NewEngineWizard_Cycle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.radioButton_FourStroke);
            this.Controls.Add(this.radioButton_TwoStroke);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.CurrentStep = 2;
            this.Name = "Form_NewEngineWizard_Cycle";
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.radioButton_TwoStroke, 0);
            this.Controls.SetChildIndex(this.radioButton_FourStroke, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_TwoStroke;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_FourStroke;
    }
}
