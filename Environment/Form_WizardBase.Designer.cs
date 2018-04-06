namespace EngineDesigner.Environment
{
    partial class Form_WizardBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_WizardBase));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Finish = new System.Windows.Forms.Button();
            this.button_Next = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Step = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_Title = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableOfContents1 = new EngineDesigner.Environment.Controls.TableOfContents();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button_Cancel);
            this.flowLayoutPanel1.Controls.Add(this.button_Close);
            this.flowLayoutPanel1.Controls.Add(this.button_Finish);
            this.flowLayoutPanel1.Controls.Add(this.button_Next);
            this.flowLayoutPanel1.Controls.Add(this.button_Back);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(150, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(424, 38);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(338, 7);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Close
            // 
            this.button_Close.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_Close.Location = new System.Drawing.Point(257, 7);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 2;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Visible = false;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Finish
            // 
            this.button_Finish.Location = new System.Drawing.Point(176, 7);
            this.button_Finish.Name = "button_Finish";
            this.button_Finish.Size = new System.Drawing.Size(75, 23);
            this.button_Finish.TabIndex = 1;
            this.button_Finish.Text = "Finish";
            this.button_Finish.UseVisualStyleBackColor = true;
            this.button_Finish.Visible = false;
            this.button_Finish.Click += new System.EventHandler(this.button_Finish_Click);
            // 
            // button_Next
            // 
            this.button_Next.Location = new System.Drawing.Point(95, 7);
            this.button_Next.Name = "button_Next";
            this.button_Next.Size = new System.Drawing.Size(75, 23);
            this.button_Next.TabIndex = 0;
            this.button_Next.Text = "Next";
            this.button_Next.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button_Next.UseVisualStyleBackColor = true;
            this.button_Next.Visible = false;
            this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // button_Back
            // 
            this.button_Back.Enabled = false;
            this.button_Back.Location = new System.Drawing.Point(14, 7);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(75, 23);
            this.button_Back.TabIndex = 3;
            this.button_Back.Text = "Back";
            this.button_Back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label_Step);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 355);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 40);
            this.panel1.TabIndex = 1;
            // 
            // label_Step
            // 
            this.label_Step.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Step.Location = new System.Drawing.Point(0, 0);
            this.label_Step.Name = "label_Step";
            this.label_Step.Padding = new System.Windows.Forms.Padding(4);
            this.label_Step.Size = new System.Drawing.Size(150, 38);
            this.label_Step.TabIndex = 0;
            this.label_Step.Text = "Step: 0/0";
            this.label_Step.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label_Title
            // 
            this.label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_Title.Location = new System.Drawing.Point(62, 0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(512, 53);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "Title";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label_Title);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(576, 55);
            this.panel2.TabIndex = 2;
            // 
            // tableOfContents1
            // 
            this.tableOfContents1.Bookmarks = new EngineDesigner.Environment.Controls.Bookmark[0];
            this.tableOfContents1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableOfContents1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tableOfContents1.Location = new System.Drawing.Point(0, 55);
            this.tableOfContents1.Name = "tableOfContents1";
            this.tableOfContents1.SelectedBookmark = null;
            this.tableOfContents1.Size = new System.Drawing.Size(192, 300);
            this.tableOfContents1.TabIndex = 3;
            // 
            // Form_WizardBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(576, 395);
            this.Controls.Add(this.tableOfContents1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 300);
            this.Name = "Form_WizardBase";
            this.Text = "Form_WizardBase";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Finish;
        private System.Windows.Forms.Button button_Next;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Label label_Step;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Panel panel2;
        protected EngineDesigner.Environment.Controls.TableOfContents tableOfContents1;
        private System.Windows.Forms.Button button_Close;



    }
}