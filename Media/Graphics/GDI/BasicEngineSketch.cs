using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

using EngineDesigner.Machine;
using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Media;

using EngineDesigner.Media.Graphics.DX;

using System.Windows.Forms;


namespace EngineDesigner.Media.Graphics.GDI
{
    public abstract partial class BasicEngineSketch : Panel
    {
        private double crankshaftRotation_deg = 0; //trenutno nastavljen zavrtljaj ročične gredi
        [DefaultValue(0d)]
        public double CrankshaftRotation_deg
        {
            get { return crankshaftRotation_deg; }

            set
            {
                crankshaftRotation_deg = value;
                this.Refresh();
            }
        }

        private Color cylinderColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CylinderColor
        {
            get { return cylinderColor; }

            set
            {
                cylinderColor = value;
                this.Refresh();
            }
        }

        private Color pistonColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color PistonColor
        {
            get { return pistonColor; }

            set
            {
                pistonColor = value;
                this.Refresh();
            }
        }

        private Color connectingRodColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color ConnectingRodColor
        {
            get { return connectingRodColor; }

            set
            {
                connectingRodColor = value;
                this.Refresh();
            }
        }

        private Color crankshaftColor = Common.Defaults.BlackColor;
        [DefaultValue(typeof(Color), Common.Defaults.BlackColorString)]
        public Color CrankshaftColor
        {
            get { return crankshaftColor; }

            set
            {
                crankshaftColor = value;
                this.Refresh();
            }
        }

        [DefaultValue(true)]
        public bool Animated
        {
            get { return this.rpmTimer1.Enabled; }
            set { this.rpmTimer1.Enabled = value; }
        }

        private Engine engine = null;
        public Engine @Engine
        {
            get
            {
                return engine;
            }

            set
            {
                engine = value;
                this.rpmTimer1.Engine = engine;
            }
        }

        private IPart[] selectedParts = null;
        [DefaultValue(null)]
        public IPart[] SelectedParts
        {
            get { return selectedParts; }

            set
            {
                selectedParts = value;
                this.Refresh();
            }
        }

        private Color selectedIPartColor = Common.Defaults.SelectedIPartColor;
        [DefaultValue(typeof(Color), Common.Defaults.SelectedIPartColorString)]
        public Color SelectedIPartColor
        {
            get { return selectedIPartColor; }
            set { selectedIPartColor = value; }
        }



        private double crankRotationInAnimation_deg = 0;

        private int centerX = 0;
        private int centerY = 0;



        public BasicEngineSketch()
        {
            InitializeComponent();

            this.Disposed
                += new EventHandler(BasicEngineSketch_Disposed);
        }
        private void BasicEngineSketch_Disposed(object sender, EventArgs e)
        {
            rpmTimer1.Dispose();
        }
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            this.centerX = this.Width / 2;
            this.centerY = this.Height / 2;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            if (!this.DesignMode)
            {
                this.Render(e.Graphics, this.centerX, this.centerY);
            }
        }



        private void Render(System.Drawing.Graphics _graphics, int _centerX, int _centerY)
        {
            if (!this.DesignMode)
            {
                if (this.engine != null)
                {
                    if (this.rpmTimer1.Enabled)
                    {
                        DrawEngine(_graphics, _centerX, _centerY, this.engine, this.crankRotationInAnimation_deg);
                    }
                    else
                    {
                        DrawEngine(_graphics, _centerX, _centerY, this.engine, this.crankshaftRotation_deg);
                    }
                }
            }
        }
        private void DrawEngine(System.Drawing.Graphics _graphics, int _centerX, int _centerY, Engine _engine, double _crankshaftRotation_deg)
        {
            foreach (PositionedCylinder _positionedCylinder in _engine.PositionedCylinders)
            {
                #region "Cylinder"
                Color _cylinderColor = this.CylinderColor;
                if (this.selectedParts != null)
                {
                    if (this.selectedParts.Contains(_positionedCylinder))
                    {
                        _cylinderColor = this.selectedIPartColor;
                    }
                }

                this.GetCylinderView(_positionedCylinder).Draw(_graphics, _cylinderColor, _centerX, _centerY, true);
                #endregion "Cylinder"

                #region "Piston"
                Color _pistonColor = this.pistonColor;
                if (this.selectedParts != null)
                {
                    if (this.selectedParts.Contains(_positionedCylinder))
                    {
                        _pistonColor = this.selectedIPartColor;
                    }
                }

                this.GetPistonView(_positionedCylinder, _crankshaftRotation_deg).Draw(_graphics, _pistonColor, _centerX, _centerY, true);
                #endregion "Piston"

                #region "ConnectingRod"
                Color _connectingRodColor = this.connectingRodColor;
                if (this.selectedParts != null)
                {
                    if (this.selectedParts.Contains(_positionedCylinder))
                    {
                        _connectingRodColor = this.selectedIPartColor;
                    }
                }

                this.GetConnectingRodView(_positionedCylinder, _crankshaftRotation_deg).Draw(_graphics, _connectingRodColor, _centerX, _centerY, true);
                #endregion "ConnectingRod"

                #region "CrankThrow"
                Color _crankThrowColor = this.crankshaftColor;
                if (this.selectedParts != null)
                {
                    if (this.selectedParts.Contains(_positionedCylinder))
                    {
                        _crankThrowColor = this.selectedIPartColor;
                    }
                }

                this.GetCrankThrowView(_positionedCylinder, _crankshaftRotation_deg).Draw(_graphics, _crankThrowColor, _centerX, _centerY, true);
                #endregion "CrankThrow"

                #region "Crankshaft"
                this.GetCrankshaftView(_positionedCylinder).Draw(_graphics, this.crankshaftColor, _centerX, _centerY, true);
                #endregion "Crankshaft"
            }
        }
        protected virtual Polygon GetCylinderView(PositionedCylinder _positionedCylinder)
        {
            throw new NotImplementedException();
        }
        protected virtual Polygon GetPistonView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            throw new NotImplementedException();
        }
        protected virtual Polygon GetConnectingRodView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            throw new NotImplementedException();
        }
        protected virtual Polygon GetCrankThrowView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            throw new NotImplementedException();
        }
        protected virtual Polygon GetCrankshaftView(PositionedCylinder _positionedCylinder)
        {
            throw new NotImplementedException();
        }



        private void rpmTimer1_CrankshaftAngleChanged(object sender, RPMTimerEventArgs e)
        {
            this.crankRotationInAnimation_deg = e.NewAngle_deg;
            this.Refresh();
        }

    }
}
