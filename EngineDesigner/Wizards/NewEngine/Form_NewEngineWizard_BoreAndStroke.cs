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
    internal partial class Form_NewEngineWizard_BoreAndStroke : Form_NewEngineWizardBase
    {
        private bool dontHandleEvent = false;



        public Form_NewEngineWizard_BoreAndStroke()
            : this(null)
        {
        }
        public Form_NewEngineWizard_BoreAndStroke(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[3];
            base.Next = new Form_NewEngineWizard_PistonMass(this);
            base.Finish = new Form_NewEngineWizard_Finish(this);


            if (!double.IsNaN(((NewEngineWizardState)base.State).Bore))
            {
                this.numericUpDown_Bore.Value = Math.Round((decimal)((NewEngineWizardState)base.State).Bore, 2);
            }
            if (!double.IsNaN(((NewEngineWizardState)base.State).Stroke))
            {
                this.numericUpDown_Stroke.Value = Math.Round((decimal)((NewEngineWizardState)base.State).Stroke, 2);
            }


            this.boreOldValue = this.numericUpDown_Bore.Value;
            this.strokeOldValue = this.numericUpDown_Stroke.Value;

            this.SetDisplacement();
            this.displacementOldValue = this.numericUpDown_Displacement.Value;
        }

        protected override bool OnNext()
        {
            this.SetBoreAndStrokeToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetBoreAndStrokeToState();

            return base.OnFinish();
        }



        private decimal boreOldValue;
        private void numericUpDown_Bore_ValueChanged(object sender, EventArgs e)
        {
            if (this.dontHandleEvent)
            {
                return;
            }


            NumericUpDown _numericUpDown = (NumericUpDown)sender;
            if (!this.SetDisplacement())
            {
                this.dontHandleEvent = true;
                _numericUpDown.Value = boreOldValue;
                this.dontHandleEvent = false;
            }
            else
            {
                this.dontHandleEvent = true;
                boreOldValue = _numericUpDown.Value;
                this.dontHandleEvent = false;
            }
        }
        private decimal strokeOldValue;
        private void numericUpDown_Stroke_ValueChanged(object sender, EventArgs e)
        {
            if (this.dontHandleEvent)
            {
                return;
            }


            NumericUpDown _numericUpDown = (NumericUpDown)sender;
            if (!this.SetDisplacement())
            {
                this.dontHandleEvent = true;
                _numericUpDown.Value = strokeOldValue;
                this.dontHandleEvent = false;
            }
            else
            {
                this.dontHandleEvent = true;
                strokeOldValue = _numericUpDown.Value;
                this.dontHandleEvent = false;
            }
        }
        [DebuggerStepThrough()]
        private bool SetDisplacement()
        {
            try
            {
                this.dontHandleEvent = true;

                double _pi4 = Math.PI / 4d;
                double _bore2 = Math.Pow((double)this.numericUpDown_Bore.Value, 2d);

                double _displacement = _pi4 * _bore2 * (double)this.numericUpDown_Stroke.Value * (double)((NewEngineWizardState)base.State).CylinderLyouts.Length;
                this.numericUpDown_Displacement.Value = (decimal)Math.Round(Conversions.Mm3ToCm3(_displacement), 3);

                return true;
            }
            catch (Exception _exception)
            {
                Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
            finally
            {
                this.dontHandleEvent = false;
            }
        }

        private decimal displacementOldValue;
        private void numericUpDown_Displacement_ValueChanged(object sender, EventArgs e)
        {
            if (this.dontHandleEvent)
            {
                return;
            }


            if (this.checkBox_BoreFixed.Checked)
            {
                NumericUpDown _numericUpDown = (NumericUpDown)sender;
                if (!this.SetStroke())
                {
                    this.dontHandleEvent = true;
                    _numericUpDown.Value = displacementOldValue;
                    this.dontHandleEvent = false;
                }
                else
                {
                    this.dontHandleEvent = true;
                    displacementOldValue = _numericUpDown.Value;
                    this.dontHandleEvent = false;
                }
            }
            else if (this.checkBox_StrokeFixed.Checked)
            {
                NumericUpDown _numericUpDown = (NumericUpDown)sender;
                if (!this.SetBore())
                {
                    this.dontHandleEvent = true;
                    _numericUpDown.Value = displacementOldValue;
                    this.dontHandleEvent = false;
                }
                else
                {
                    this.dontHandleEvent = true;
                    displacementOldValue = _numericUpDown.Value;
                    this.dontHandleEvent = false;
                }
            }
            else //nobeden ni čekiran
            {
                NumericUpDown _numericUpDown = (NumericUpDown)sender;
                if (!this.SetBoreAndStroke())
                {
                    this.dontHandleEvent = true;
                    _numericUpDown.Value = displacementOldValue;
                    this.dontHandleEvent = false;
                }
                else
                {
                    this.dontHandleEvent = true;
                    _numericUpDown.Value = _numericUpDown.Value;
                    this.dontHandleEvent = false;
                }
            }
        }
        [DebuggerStepThrough()]
        private bool SetBore()
        {
            try
            {
                this.dontHandleEvent = true;

                double _displacement_mm3 = Conversions.Cm3ToMm3((double)this.numericUpDown_Displacement.Value);
                double _pi4 = Math.PI / 4d;

                double _bore = Math.Sqrt(_displacement_mm3 / (_pi4 * (double)this.numericUpDown_Stroke.Value * (double)((NewEngineWizardState)base.State).CylinderLyouts.Length));
                this.numericUpDown_Bore.Value = (decimal)_bore;

                return true;
            }
            catch (Exception _exception)
            {
                Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
            finally
            {
                this.dontHandleEvent = false;
            }
        }
        [DebuggerStepThrough()]
        private bool SetStroke()
        {
            try
            {
                this.dontHandleEvent = true;

                double _displacement_mm3 = Conversions.Cm3ToMm3((double)this.numericUpDown_Displacement.Value);
                double _pi4 = Math.PI / 4d;
                double _bore2 = Math.Pow((double)this.numericUpDown_Bore.Value, 2d);

                double _stroke = _displacement_mm3 / (_pi4 * _bore2 * (double)((NewEngineWizardState)base.State).CylinderLyouts.Length);
                this.numericUpDown_Stroke.Value = (decimal)_stroke;

                return true;
            }
            catch (Exception _exception)
            {
                Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
            finally
            {
                this.dontHandleEvent = false;
            }
        }
        [DebuggerStepThrough()]
        private bool SetBoreAndStroke()
        {
            try
            {
                this.dontHandleEvent = true;

                double _displacement_mm3 = Conversions.Cm3ToMm3((double)this.numericUpDown_Displacement.Value);
                double _pi4 = Math.PI / 4d;

                double _double = _displacement_mm3 / (_pi4 * (double)((NewEngineWizardState)base.State).CylinderLyouts.Length);
                decimal _boreStroke = (decimal)Math.Pow(_double, 1d / 3d);
                this.numericUpDown_Bore.Value = _boreStroke;
                this.numericUpDown_Stroke.Value = _boreStroke;

                return true;
            }
            catch (Exception _exception)
            {
                Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                return false;
            }
            finally
            {
                this.dontHandleEvent = false;
            }
        }

        private void checkBox_BoreFixed_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericUpDown_Displacement();
        }
        private void checkBox_StrokeFixed_CheckedChanged(object sender, EventArgs e)
        {
            this.SetNumericUpDown_Displacement();
        }
        private void SetNumericUpDown_Displacement()
        {
            if ((this.checkBox_BoreFixed.Checked)
                && (this.checkBox_StrokeFixed.Checked))
            {
                this.numericUpDown_Displacement.Enabled = false;
            }
            else
            {
                this.numericUpDown_Displacement.Enabled = true;
            }
        }


        private void SetBoreAndStrokeToState()
        {
            ((NewEngineWizardState)base.State).Bore = (double)this.numericUpDown_Bore.Value;
            ((NewEngineWizardState)base.State).Stroke = (double)this.numericUpDown_Stroke.Value;
        }

    }
}
