using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Common;
using EngineDesigner.Environment;
using EngineDesigner.Machine;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_Finish : Form_NewEngineWizardBase
    {
        public Form_NewEngineWizard_Finish()
            : this(null)
        {
        }
        public Form_NewEngineWizard_Finish(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();


            base.FinishEnabled = false;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[8];

            base.CloseEnabled = false;
            this.backgroundWorker1.RunWorkerAsync(base.State);
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _backgroundWorker = (BackgroundWorker)sender;
            NewEngineWizardState _newEngineWizardState = (NewEngineWizardState)e.Argument;

            _backgroundWorker.ReportProgress(10);


            try
            {
                Engine _engine = null;


                List<PositionedCylinder> _positionedCylinders = new List<PositionedCylinder>();

                foreach (NewEngineWizardState.CylinderLyout _cylinderLayout in _newEngineWizardState.CylinderLyouts)
                {
                    #region "Piston"
                    Piston _piston = Piston.FromParameters(
                        _newEngineWizardState.Bore,
                        _newEngineWizardState.PistonMass);
                    #endregion "Piston"


                    #region "ConnectingRod"
                    double _rotatingDistributionPercentage = (100d - _newEngineWizardState.ConnectingRodMassAndDistanceDistributionPercentage) / 100d;
                    double _reciprocatingDistributionPercentage = _newEngineWizardState.ConnectingRodMassAndDistanceDistributionPercentage / 100d;

                    ConnectingRod _connectingRod = new ConnectingRod(
                        _newEngineWizardState.ConnectingRodMass * _rotatingDistributionPercentage,
                        _newEngineWizardState.ConnectingRodLength * _reciprocatingDistributionPercentage,
                        _newEngineWizardState.ConnectingRodMass * _reciprocatingDistributionPercentage,
                        _newEngineWizardState.ConnectingRodLength * _rotatingDistributionPercentage);
                    #endregion "ConnectingRod"


                    #region "CrankThrow"
                    CrankThrow _crankThrow;
                    if (_newEngineWizardState.BalancerMass > 0)
                    {
                        _crankThrow = CrankThrow.FromParameters(
                            _newEngineWizardState.Stroke / 2d,
                            _newEngineWizardState.BalancerMass,
                            _newEngineWizardState.BalancerRotationRadius);
                    }
                    else
                    {
                        _crankThrow = CrankThrow.FromParameters(_newEngineWizardState.Stroke / 2d);
                    }
                    #endregion "CrankThrow"


                    #region "Cylinder"
                    Cylinder _cylinder = new Cylinder(
                        _newEngineWizardState.Cycle,
                        _piston,
                        _connectingRod,
                        _crankThrow);
                    #endregion "Cylinder"


                    #region "PositionedCylinder"
                    double _offset = 0;
                    double _tilt = 0;

                    if (this.GetLayoutOffset(_newEngineWizardState) == 0d) //motor je inline
                    {
                        _offset = (_cylinderLayout.CylinderPosition - 1)
                            * (_newEngineWizardState.Bore + (_newEngineWizardState.Bore / EngineDesigner.Machine.Properties.Settings.Default.DefaultOffsetDivisor));
                    }
                    else
                    {
                        _tilt = _cylinderLayout.Tilt;

                        if (Mathematics.IsOdd(_cylinderLayout.CylinderPosition))
                        {
                            if (_positionedCylinders.Count > 0)
                            {
                                _offset = (_positionedCylinders[_positionedCylinders.Count - 1].Offset_mm
                                    + (_newEngineWizardState.Bore + (_newEngineWizardState.Bore / EngineDesigner.Machine.Properties.Settings.Default.DefaultOffsetDivisor)));
                            }
                        }
                        else
                        {
                            _offset = _positionedCylinders[_positionedCylinders.Count - 1].Offset_mm;
                            _offset += _crankThrow.CrankPinWidth_mm;
                        }
                    }

                    PositionedCylinder _positionedCylinder = new PositionedCylinder(
                        _cylinder,
                        _cylinderLayout.CylinderPosition,
                        _offset,
                        _tilt,
                        _cylinderLayout.FiringAngle);
                    _positionedCylinders.Add(_positionedCylinder);


                    //javimo progress (60% gre za ustvarjanje cilindrov, 10 je bilo že od prej)
                    double _progressPercentage = 10d + (_cylinderLayout.CylinderPosition * 60d / _newEngineWizardState.CylinderLyouts.Length);
                    _backgroundWorker.ReportProgress((int)_progressPercentage);
                    #endregion "PositionedCylinder"
                }


                _engine = new Engine(_positionedCylinders.ToArray());
                _engine.Flywheel.Mass_g = _newEngineWizardState.FlywheelMass;
                _engine.Flywheel.Diameter_mm = _newEngineWizardState.FlywheelDiameter;


                e.Result = _engine;
            }
            catch (Exception _exception)
            {
                e.Result = _exception;
            }
            finally
            {
                _backgroundWorker.ReportProgress(100);
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                Exception _exception = (Exception)e.Result;


                this.label_Info.Text = "Failed!";


                StringBuilder _stringBuilder = new StringBuilder();
                _stringBuilder.AppendLine("An engine with this parameters cannot be created!");
                _stringBuilder.AppendLine();
                _stringBuilder.AppendLine("Error while checking paremeters:");
                _stringBuilder.Append("'");
                _stringBuilder.Append(_exception.Message);
                _stringBuilder.AppendLine("'");
                _stringBuilder.AppendLine();
                _stringBuilder.AppendLine();
                _stringBuilder.AppendLine("Please, go back and correct the parameters in order to make the creation of the engine possible.");

                this.label_Result.Text = _stringBuilder.ToString();
                this.label_Result.Visible = true;
            }
            else if (e.Result is Engine)
            {
                Engine _engine = (Engine)e.Result;


                this.label_Info.Text = "Done!";


                StringBuilder _stringBuilder = new StringBuilder();
                _stringBuilder.AppendLine("Your engine is ready to use:");
                _stringBuilder.AppendLine();

                if (_engine.NumberOfCylinders > 1)
                {
                    _stringBuilder.Append(string.Format(
                        "{0} {1}",
                        ((NewEngineWizardState)base.State).EngineType,
                        _engine.NumberOfCylinders));
                }
                else
                {
                    _stringBuilder.Append("Single cylinder");
                }

                if ((Math.Abs(_engine.PositionedCylinders[0].Tilt_deg) > 0d)
                    && (Math.Abs(_engine.PositionedCylinders[0].Tilt_deg) < 90d))
                {
                    _stringBuilder.AppendLine(string.Format(
                        ", {0}°, {1}",
                        Math.Abs(_engine.PositionedCylinders[0].Tilt_deg * 2d),
                        _engine.PositionedCylinders[0].Cycle.CycleId));

                    if (_engine.NumberOfCylinders > 1)
                    {
                        //če se ročice dotikajo
                        if (_engine.PositionedCylinders[0].IsMatedWith(_engine.PositionedCylinders[1]))
                        {
                            //in so na istem mestu
                            if (_engine.CrankThrows_deg[0] == _engine.CrankThrows_deg[1])
                            {
                                //potem so "shared crankpins"
                                _stringBuilder.AppendLine("Shared crank pins");
                            }
                            else
                            {
                                //potem so "split crankpins"
                                _stringBuilder.AppendLine("Split crank pins");
                            }
                        }
                    }
                }
                else
                {
                    _stringBuilder.AppendLine(string.Format(
                        ", {0}",
                        _engine.PositionedCylinders[0].Cycle.CycleId));
                }

                if (!string.IsNullOrEmpty(((NewEngineWizardState)base.State).EngineParticularity))
                {
                    _stringBuilder.AppendLine(((NewEngineWizardState)base.State).EngineParticularity);
                }

                _stringBuilder.AppendLine();
                _stringBuilder.AppendLine();
                _stringBuilder.AppendLine("Click Close to start using your engine.");

                this.label_Result.Text = _stringBuilder.ToString();
                this.label_Result.Visible = true;


                base.Result = _engine;
                this.CloseEnabled = true;
            }
            else
            {
                throw new NotSupportedException();
            }
        }



        private double GetLayoutOffset(NewEngineWizardState _newEngineWizardState)
        {
            double _offset_deg = 0d;

            foreach (NewEngineWizardState.CylinderLyout _cylinderLayout in _newEngineWizardState.CylinderLyouts)
            {
                if (_cylinderLayout.Tilt != 0)
                {
                    _offset_deg = Math.Abs(_cylinderLayout.Tilt);
                    _offset_deg *= 2;

                    break;
                }
            }

            return _offset_deg;
        }

    }
}
