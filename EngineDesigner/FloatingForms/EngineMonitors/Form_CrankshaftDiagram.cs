using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;

namespace EngineDesigner.FloatingForms.EngineMonitors
{
    internal partial class Form_CrankshaftDiagram : Form_EngineMonitorBase
    {
        public Form_CrankshaftDiagram()
            : this(null, null)
        {
        }
        public Form_CrankshaftDiagram(Form _owner, Form_EngineControl _engineControl)
            : base(_owner, _engineControl)
        {
            InitializeComponent();
        }



        protected override void OnEngineChanged(Engine _engine)
        {
            this.crankshaftDiagram1.Engine = _engine;
        }
        protected override void OnCrankshaftAngleChanged(double _newAngle_deg)
        {
            this.crankshaftDiagram1.CrankshaftRotation_deg = _newAngle_deg;
        }

    }
}
