using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;
using EngineDesigner.Machine;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizardBase : Form_WizardBase
    {
        public Form_NewEngineWizardBase()
            : this(null)
        {
        }
        public Form_NewEngineWizardBase(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }

    }


    internal class NewEngineWizardState : WizardStateBase
    {
        internal class CylinderLyout
        {
            public CylinderLyout(int _cylinderPosition, double _firingAngle, double _tilt)
            {
                this.cylinderPosition = _cylinderPosition;
                this.firingAngle = _firingAngle;
                this.tilt = _tilt;
            }


            private int cylinderPosition;
            public int CylinderPosition
            {
                get { return cylinderPosition; }
            }

            private double firingAngle;
            public double FiringAngle
            {
                get { return firingAngle; }
            }

            private double tilt;
            public double Tilt
            {
                get { return tilt; }
            }
        }



        private Cycle cycle = Cycle.FourStroke;
        public Cycle @Cycle
        {
            get { return cycle; }
            set { cycle = value; }
        }

        private string selectedEngineLayoutNode = null;
        public string SelectedEngineLayoutNode
        {
            get { return selectedEngineLayoutNode; }
            set { selectedEngineLayoutNode = value; }
        }
        private CylinderLyout[] cylinderLyouts;
        public CylinderLyout[] CylinderLyouts
        {
            get { return cylinderLyouts; }
            set { cylinderLyouts = value; }
        }
        private string engineType = null;
        public string EngineType
        {
            get { return engineType; }
            set { engineType = value; }
        }
        private string engineParticularity = null;
        public string EngineParticularity
        {
            get { return engineParticularity; }
            set { engineParticularity = value; }
        }

        private double bore = double.NaN;
        public double Bore
        {
            get { return bore; }
            set { bore = value; }
        }

        private double stroke = double.NaN;
        public double Stroke
        {
            get { return stroke; }
            set { stroke = value; }
        }

        private double connectingRodLength = double.NaN;
        public double ConnectingRodLength
        {
            get { return connectingRodLength; }
            set { connectingRodLength = value; }
        }

        private double pistonMass = double.NaN;
        public double PistonMass
        {
            get { return pistonMass; }
            set { pistonMass = value; }
        }

        private double connectingRodMass = double.NaN;
        public double ConnectingRodMass
        {
            get { return connectingRodMass; }
            set { connectingRodMass = value; }
        }

        private double connectingRodMassAndDistanceDistributionPercentage = double.NaN;
        public double ConnectingRodMassAndDistanceDistributionPercentage
        {
            get { return connectingRodMassAndDistanceDistributionPercentage; }
            set { connectingRodMassAndDistanceDistributionPercentage = value; }
        }

        private double balancerMass = double.NaN;
        public double BalancerMass
        {
            get { return balancerMass; }
            set { balancerMass = value; }
        }

        private double balancerRotationRadius = double.NaN;
        public double BalancerRotationRadius
        {
            get { return balancerRotationRadius; }
            set { balancerRotationRadius = value; }
        }

        private double flywheelMass = double.NaN;
        public double FlywheelMass
        {
            get { return flywheelMass; }
            set { flywheelMass = value; }
        }

        private double flywheelDiameter = double.NaN;
        public double FlywheelDiameter
        {
            get { return flywheelDiameter; }
            set { flywheelDiameter = value; }
        }



        #region IWizardState Members
        protected override void SetDefaults()
        {
            this.bore = 10d;
            this.stroke = 5d;

            this.pistonMass = 1d;

            this.connectingRodLength = 20d;

            this.connectingRodMass = 1d;
            this.connectingRodMassAndDistanceDistributionPercentage = 33.5d;

            this.balancerMass = 0d;
            this.balancerRotationRadius = 1d;

            this.flywheelMass = 0d;
            this.flywheelDiameter = 10d;
        }
        #endregion
    }

}
