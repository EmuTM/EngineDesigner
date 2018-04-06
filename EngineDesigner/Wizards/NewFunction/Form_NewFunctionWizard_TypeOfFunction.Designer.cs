namespace EngineDesigner.Wizards.NewFunction
{
    partial class Form_NewFunctionWizard_TypeOfFunction
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Two stroke");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Four stroke");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Gas pressure vs. firing angle", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Indicator diagram", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.label_SelectedNodeInfo = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_SelectedNodeInfo
            // 
            this.label_SelectedNodeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_SelectedNodeInfo.Location = new System.Drawing.Point(322, 401);
            this.label_SelectedNodeInfo.Name = "label_SelectedNodeInfo";
            this.label_SelectedNodeInfo.Size = new System.Drawing.Size(506, 44);
            this.label_SelectedNodeInfo.TabIndex = 25;
            this.label_SelectedNodeInfo.Text = "Tle se izpiše SelectedNode.TooltipText";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.HideSelection = false;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(311, 110);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node_IndicatorDiagram_GasPressureVsFiringAngle_TwoStroke";
            treeNode1.Text = "Two stroke";
            treeNode2.Name = "Node_IndicatorDiagram_GasPressureVsFiringAngle_FourStroke";
            treeNode2.Text = "Four stroke";
            treeNode3.Name = "Node_IndicatorDiagram_GasPressureVsFiringAngle";
            treeNode3.Text = "Gas pressure vs. firing angle";
            treeNode4.Name = "Node_IndicatorDiagram";
            treeNode4.Text = "Indicator diagram";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(531, 277);
            this.treeView1.TabIndex = 24;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
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
            this.label1.Size = new System.Drawing.Size(272, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Select the type of function you want to create:";
            // 
            // Form_NewFunctionWizard_TypeOfFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(868, 511);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_SelectedNodeInfo);
            this.Controls.Add(this.treeView1);
            this.CurrentStep = 2;
            this.Name = "Form_NewFunctionWizard_TypeOfFunction";
            this.Controls.SetChildIndex(this.tableOfContents1, 0);
            this.Controls.SetChildIndex(this.treeView1, 0);
            this.Controls.SetChildIndex(this.label_SelectedNodeInfo, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_SelectedNodeInfo;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
    }
}
