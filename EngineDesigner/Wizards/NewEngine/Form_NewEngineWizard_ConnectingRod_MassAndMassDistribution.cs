using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Common;
using EngineDesigner.Environment;
using System.Diagnostics;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution()
            : this(null)
        {
        }
        public Form_NewEngineWizard_ConnectingRod_MassAndMassDistribution(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[5].SubBookmarks[1];
            base.Next = new Form_NewEngineWizard_BalancerMassAndRotationRadius(this);
            base.Finish = new Form_NewEngineWizard_Finish(this);


            if (!double.IsNaN(((NewEngineWizardState)base.State).ConnectingRodMass))
            {
                this.numericUpDown_ConnectingRodMass.Value = (decimal)((NewEngineWizardState)base.State).ConnectingRodMass;
            }
            if (!double.IsNaN(((NewEngineWizardState)base.State).ConnectingRodMassAndDistanceDistributionPercentage))
            {
                this.trackBar_MassAndDistanceDistributionPercentage.Value = (int)((NewEngineWizardState)base.State).ConnectingRodMassAndDistanceDistributionPercentage;
            }
        }

        protected override bool OnNext()
        {
            this.SetConnectingRodMassAndMassDistributionToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetConnectingRodMassAndMassDistributionToState();

            return base.OnFinish();
        }



        private void numericUpDown_ConnectingRodMass_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown _numericUpDown = (NumericUpDown)sender;

            if (_numericUpDown.Value == 0)
            {
                this.trackBar_MassAndDistanceDistributionPercentage.Enabled = false;
            }
            else
            {
                this.trackBar_MassAndDistanceDistributionPercentage.Enabled = true;
            }
        }
        private void trackBar_MassAndDistanceDistributionPercentage_Scroll(object sender, EventArgs e)
        {
            TrackBar _trackBar = (TrackBar)sender;
            this.labe_MassAndDistanceDistributionPercentage.Text = Math.Abs(_trackBar.Value).ToString();
        }


        private void SetConnectingRodMassAndMassDistributionToState()
        {
            ((NewEngineWizardState)base.State).ConnectingRodMass = (double)this.numericUpDown_ConnectingRodMass.Value;

            //pretvorimo v format 0-100
            double _massDistribution = ((double)this.trackBar_MassAndDistanceDistributionPercentage.Value / 2d) + 50d;
            ((NewEngineWizardState)base.State).ConnectingRodMassAndDistanceDistributionPercentage = _massDistribution;
        }

    }
}
