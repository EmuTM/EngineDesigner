namespace EngineDesigner.Media.Graphics.DX
{
    partial class Panel3DBase
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
            this.mouseControlledCamera = new EngineDesigner.Media.Graphics.DX.Cameras.MouseControlledCamera();
            this.SuspendLayout();
            // 
            // mouseControlledCamera
            // 
            this.mouseControlledCamera.Anchor = new EngineDesigner.Media.Graphics.DX.CustomVector3(0F, 0F, 0F);
            this.ResumeLayout(false);

        }

        #endregion

        private EngineDesigner.Media.Graphics.DX.Cameras.MouseControlledCamera mouseControlledCamera;

    }
}
