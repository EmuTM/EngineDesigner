namespace EngineDesigner.Media.Graphics.GDI
{
    partial class BasicEngineSketch
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
            this.rpmTimer1 = new EngineDesigner.Media.RPMTimer(this.components);
            this.SuspendLayout();
            // 
            // rpmTimer1
            // 
            this.rpmTimer1.CrankshaftAngleChanged += new System.EventHandler<EngineDesigner.Media.RPMTimerEventArgs>(this.rpmTimer1_CrankshaftAngleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Media.RPMTimer rpmTimer1;
    }
}
