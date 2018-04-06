using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.FloatingForms;
using EngineDesigner.FloatingForms.EngineMonitors;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;
using EngineDesigner.Machine;
using System.Windows.Forms.DataVisualization.Charting;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;


namespace EngineDesigner.TestForms
{
    internal partial class TestForm_Functions : Form
    {
        private Engine engine;



        public TestForm_Functions()
        {
            InitializeComponent();






            //Form_EngineControl _form_EngineControl = new Form_EngineControl(this);
            //_form_EngineControl.Engine = this.engine;
            //_form_EngineControl.ShowDialog();


            //Form_CycleDiagram _form_CycleDiagram = new Form_CycleDiagram(this);
            //_form_CycleDiagram.Engine = this.engine;
            ////_form_CycleDiagram.EngineControl = _form_EngineControl;
            //_form_CycleDiagram.Show();


            //Form_CylinderDynamics _form = new Form_CylinderDynamics(this);
            ////_form.EngineControl = _form_EngineControl;
            //_form.Engine = this.engine;
            //_form.Show();
        }
        private void TestForm_Functions_Load(object sender, EventArgs e)
        {
            this.engine = Engine.From(@"..\..\..\Samples\FourStrokeEngine.xml");
            //this.engine = new Engine(new PositionedCylinder[] { 
            //    new PositionedCylinder(
            //        new Cylinder(
            //            Cycle.FourStroke, 
            //            Piston.FromParameters(100, 100), 
            //            new ConnectingRod(100, 100, 100, 200),
            //            CrankThrow.FromParameters(50, 220, 50)),
            //        1, 
            //        0, 0, 
            //        0)
            //});


            //this.engineSketch1.IPart = this.engine;
            //this.engineSketch1.View_Isometric();
            //this.engineSketch1.View_ZoomToFit();


            this.numericUpDown_ValueChanged(null, null);
        }



        private void ComputeFunctions(Engine _engine, double _rpm, int _xMax)
        {
            this.functionChart1.ClearChart();


            #region "Position"
            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonTravelFromCrankCenter_mm(_double); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetPistonTravelFromCrankCenterAproximation_mm(_double, 1); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[1].GetPistonTravelFromCrankCenterAproximation_mm(_double, 1, 2); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            #endregion "Position"

            #region "Velocity"
            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonVelocity_mpdeg(_double); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonVelocityAproximation_mpdeg(_double, 1); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonVelocityAproximation_mpdeg(_double, 1, 2); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });


            //this.DrawFunction(0, _xMax, Color.Green,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonVelocity_mps(_double, _rpm); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonVelocityAproximation_mps(_double, _rpm, 1); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetPistonVelocityAproximation_mps(_double, _rpm, 1, 2); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            #endregion "Velocity"

            #region "Acceleration"
            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetPistonAcceleration_mpdeg2(_double); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetPistonAccelerationAproximation_mpdeg2(_double, 1); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[1].GetPistonAccelerationAproximation_mpdeg2(_double, 1, 2); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });


            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetPistonAcceleration_mps2(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetPistonAccelerationAproximation_mps2(_double, _rpm, 1); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[1].GetPistonAccelerationAproximation_mps2(_double, _rpm, 1, 2); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            #endregion "Acceleration"

            #region "Inertia force & torque"
            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetInertiaForceReciprocating_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Green,
            //    delegate(double _double) { return _engine[1].GetInertiaForceRotating_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetInertiaForce_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawInertia(0, 360, Color.Green, _rpm, _engine[1]);


            //this.DrawFunction(0, _xMax, Color.Green,
            //    delegate(double _double) { return _engine[1].GetInertiaTorque_Nm(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetInertiaTorqueAproximation_Nm(_double, _rpm, 1); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetInertiaTorqueAproximation_Nm(_double, _rpm, 2); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[1].GetInertiaTorqueAproximation_Nm(_double, _rpm, 3); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[1].GetInertiaTorqueAproximation_Nm(_double, _rpm, 1, 2, 3); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });


            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetTotalTorque_Nm(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            #endregion "Inertia force & torque"

            #region "Engine Inertia Reciprocating"
            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetInertiaForceReciprocatingX_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetInertiaForceReciprocatingY_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Violet,
            //    delegate(double _double) { return _engine[2].GetInertiaForceReciprocatingX_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[2].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            //this.DrawFunction(0, _xMax, Color.Pink,
            //    delegate(double _double) { return _engine[2].GetInertiaForceReciprocatingY_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[2].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[3].GetInertiaForceReciprocatingX_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[3].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Gray,
            //    delegate(double _double) { return _engine[3].GetInertiaForceReciprocatingY_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[3].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Yellow,
            //    delegate(double _double) { return _engine.GetInertiaForceReciprocatingX_N(_double, _rpm); });
            //this.DrawFunction(0, _xMax, Color.Green,
            //    delegate(double _double) { return _engine.GetInertiaForceReciprocatingY_N(_double, _rpm); });
            #endregion "Engine Inertia Reciprocating"

            #region "Engine Inertia Total"
            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetInertiaForceX_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetInertiaForceY_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Violet,
            //    delegate(double _double) { return _engine[2].GetInertiaForceX_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[2].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            //this.DrawFunction(0, _xMax, Color.Pink,
            //    delegate(double _double) { return _engine[2].GetInertiaForceY_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[2].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[3].GetInertiaForceX_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[3].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            //this.DrawFunction(0, _xMax, Color.Gray,
            //    delegate(double _double) { return _engine[3].GetInertiaForceY_N(_double, _rpm); },
            //    delegate(double _double) { return _engine[3].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Yellow,
            //    delegate(double _double) { return _engine.GetInertiaForceX_N(_double, _rpm); });
            //this.DrawFunction(0, _xMax, Color.Green,
            //    delegate(double _double) { return _engine.GetInertiaForceY_N(_double, _rpm); });
            #endregion "Engine Inertia Total"

            #region "Gas pressure force & torque"
            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].Cycle.DefaultGasPressureCurve_Percents.GetFx(_double); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Green,
            //    delegate(double _double) { return _engine[1].GetGasPressureForce_N(_double); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine[1].GetGasPressureTorqueAproximation_Nm(_double, 1); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine[1].GetGasPressureTorqueAproximation_Nm(_double, 2); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Black,
            //    delegate(double _double) { return _engine[1].GetGasPressureTorque_Nm(_double); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            #endregion "Gas pressure force & torque"

            #region "Total torque"
            //this.DrawFunction(0, _xMax, Color.Pink,
            //    delegate(double _double) { return _engine[1].GetGasPressureTorque_Nm(_double); },
            //    delegate(double _double) { return _engine[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Red,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetInertiaTorque_Nm(_double, _rpm); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });

            //this.DrawFunction(0, _xMax, Color.Blue,
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetTotalTorque_Nm(_double, _rpm); },
            //    delegate(double _double) { return _engine.PositionedCylinders[1].GetCylinderRelativeCrankThrowRotation_deg(_double); });
            #endregion "Total torque"

            #region "Flywheel"
            //rpm
            Function _rpmFunction = Function.Constant(0, _xMax, 1f,
                _rpm);
            this.functionChart1.DrawFunction(_rpmFunction, "RPM " + _rpm.ToString(Defaults.ROUNDING), Color.Gray);


            #region "torque"
            //total torque
            Function _totalTorqueFunction = Function.Compute(0, _xMax, 1f,
                delegate(double _x)
                {
                    return Math.Sin(Conversions.DegToRad(_x)) + 3d;
                });
            this.functionChart1.DrawFunction(_totalTorqueFunction, "Tot tq Nm " + _totalTorqueFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.Blue);


            //average torque
            double _averageTorque = _totalTorqueFunction.GetAverageY();
            Function _averageTorqueFunction = Function.Constant(0, _xMax, 1f,
                _averageTorque);
            this.functionChart1.DrawFunction(_averageTorqueFunction, "Tot tq Nm avg " + _averageTorque.ToString(Defaults.ROUNDING), Color.Cyan);


            //excessive torque
            Function _excessiveTorqueFunction = Function.Compute(0, _xMax, 1f,
                delegate(double _x)
                {
                    double _torque = _totalTorqueFunction.GetFx(_x) - _averageTorque;
                    return _torque;
                });
            this.functionChart1.DrawFunction(_excessiveTorqueFunction, "Excsv tq Nm " + _excessiveTorqueFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.Orange);


            //average excessive torque
            double _averageExcessiveTorque = _excessiveTorqueFunction.GetAverageY();
            Function _averageExcessiveTorqueFunction = Function.Constant(0, _xMax, 1f,
                _averageExcessiveTorque);
            this.functionChart1.DrawFunction(_averageExcessiveTorqueFunction, "Excsv tq Nm avg " + _averageExcessiveTorque.ToString(Defaults.ROUNDING), Color.Gold);
            #endregion "torque"


            #region "acceleration & velocity"
            //flywheel angular acceleration
            Function _flywheelAngularAccelerationFunction = Function.Compute(0, _xMax, 1f,
                delegate(double _x)
                {
                    double _excessiveTorque = _excessiveTorqueFunction.GetFx(_x);
                    double _angularAcceleration_degps2 = _engine.Flywheel.GetAngularAcceleration_degps2(_excessiveTorque);

                    return _angularAcceleration_degps2;
                });
            this.functionChart1.DrawFunction(_flywheelAngularAccelerationFunction, "Flwl acc degps^2 " + _flywheelAngularAccelerationFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.Green);


            //flywheel average angular acceleration
            double _averageFlywheelAngularAcceleration = _flywheelAngularAccelerationFunction.GetAverageY();
            Function _averageFlywheelAngularAccelerationFunction = Function.Constant(0, _xMax, 1,
                _averageFlywheelAngularAcceleration);
            this.functionChart1.DrawFunction(_averageFlywheelAngularAccelerationFunction, "Flwl acc degps^2 avg " + _averageFlywheelAngularAcceleration.ToString(Defaults.ROUNDING), Color.GreenYellow);


            //flywheel angular velocity
            Function _flywheelAngularVelocityFunction = Utility.ComputeInThreadPool(0, _xMax - 1f, 1f,
                delegate(double _x)
                {
                    double _flywheelAngularVelocity = _engine.Flywheel.GetAngularVelocity_degps(_rpm, _flywheelAngularAccelerationFunction, _x, 1d);
                    return _flywheelAngularVelocity;
                });
            this.functionChart1.DrawFunction(_flywheelAngularVelocityFunction, "Flwl vel rpm " + _flywheelAngularVelocityFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.Red);


            //flywheel average angular velocity
            double _averageFlywheelAngularVelocity = _flywheelAngularVelocityFunction.GetAverageY();
            Function _averageFlywheelAngularVelocityFunction = Function.Constant(0, _xMax, 1,
                _averageFlywheelAngularVelocity);
            this.functionChart1.DrawFunction(_averageFlywheelAngularVelocityFunction, "Flwl vel rpm avg " + _averageFlywheelAngularVelocity.ToString(Defaults.ROUNDING), Color.Pink);
            #endregion "acceleration & velocity"


            double _coefficientOfSpeedFluctuation = _engine.Flywheel.GetCoefficientOfSpeedFluctuation(_rpm, _totalTorqueFunction, 1d);


            #region "smoothed torque"
            Function _smoothedTorqueFunction = Function.Compute(0, _xMax, 1f,
            delegate(double _x)
            {
                double _torque = _engine.Flywheel.GetSmoothedTorque_Nm(_rpm, _totalTorqueFunction.GetFx(_x), _averageTorque, _coefficientOfSpeedFluctuation);
                return _torque;
            });
            this.functionChart1.DrawFunction(_smoothedTorqueFunction, "Smthd tq Nm " + _smoothedTorqueFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.DarkMagenta);


            //average smoothed torque
            double _averageSmoothedTorque = _smoothedTorqueFunction.GetAverageY();
            Function _averageSmoothedTorqueFunction = Function.Constant(0, _xMax, 1f,
                _averageSmoothedTorque);
            this.functionChart1.DrawFunction(_averageSmoothedTorqueFunction, "Smthd tq Nm avg " + _averageSmoothedTorque.ToString(Defaults.ROUNDING), Color.DeepPink);


            //smoothed excessive torque
            Function _smoothedExcessiveTorqueFunction = Function.Compute(0, _xMax, 1f,
                delegate(double _x)
                {
                    double _torque = _smoothedTorqueFunction.GetFx(_x) - _averageSmoothedTorque;
                    return _torque;
                });
            this.functionChart1.DrawFunction(_smoothedExcessiveTorqueFunction, "Smoothed excsv tq Nm " + _smoothedExcessiveTorqueFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.Brown);


            //smoothed average excessive torque
            double _averageSmoothedExcessiveTorque = _smoothedExcessiveTorqueFunction.GetAverageY();
            Function _averageSmoothedExcessiveTorqueFunction = Function.Constant(0, _xMax, 1f,
                _averageSmoothedExcessiveTorque);
            this.functionChart1.DrawFunction(_averageSmoothedExcessiveTorqueFunction, "Smoothed excsv tq Nm avg " + _averageSmoothedExcessiveTorque.ToString(Defaults.ROUNDING), Color.Peru);
            #endregion "smoothed torque"


            #region "smoothed acceleration & velocity"
            //smoothed flywheel angular acceleration
            Function _smoothedFlywheelAngularAccelerationFunction = Function.Compute(0, _xMax, 1f,
                delegate(double _x)
                {
                    double _excessiveTorque = _smoothedExcessiveTorqueFunction.GetFx(_x);
                    double _angularAcceleration_degps2 = _engine.Flywheel.GetAngularAcceleration_degps2(_excessiveTorque);

                    return _angularAcceleration_degps2;
                });
            this.functionChart1.DrawFunction(_smoothedFlywheelAngularAccelerationFunction, "Smoothed flwl acc degps^2 " + _smoothedFlywheelAngularAccelerationFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.SeaGreen);


            //smoothed flywheel average angular acceleration
            double _smoothedAverageFlywheelAngularAcceleration = _flywheelAngularAccelerationFunction.GetAverageY();
            Function _smoothedAverageFlywheelAngularAccelerationFunction = Function.Constant(0, _xMax, 1,
                _smoothedAverageFlywheelAngularAcceleration);
            this.functionChart1.DrawFunction(_smoothedAverageFlywheelAngularAccelerationFunction, "Smoothed flwl acc degps^2 avg " + _smoothedAverageFlywheelAngularAcceleration.ToString(Defaults.ROUNDING), Color.Lime);


            //smoothed flywheel angular velocity
            Function _smoothedFlywheelAngularVelocityFunction = Utility.ComputeInThreadPool(0, _xMax - 1f, 1f,
                delegate(double _x)
                {
                    double _flywheelAngularVelocity = _engine.Flywheel.GetAngularVelocity_degps(_rpm, _smoothedFlywheelAngularAccelerationFunction, _x, 1d);
                    return _flywheelAngularVelocity;
                });
            this.functionChart1.DrawFunction(_smoothedFlywheelAngularVelocityFunction, "Smoothed flwl vel rpm " + _smoothedFlywheelAngularVelocityFunction.GetMaxY().ToString(Defaults.ROUNDING), Color.Firebrick);


            //smoothed flywheel average angular velocity
            double _smoothedAverageFlywheelAngularVelocity = _smoothedFlywheelAngularVelocityFunction.GetAverageY();
            Function _smoothedAverageFlywheelAngularVelocityFunction = Function.Constant(0, _xMax, 1,
                _smoothedAverageFlywheelAngularVelocity);
            this.functionChart1.DrawFunction(_smoothedAverageFlywheelAngularVelocityFunction, "Smoothed flwl vel rpm avg " + _smoothedAverageFlywheelAngularVelocity.ToString(Defaults.ROUNDING), Color.Tomato);
            #endregion "smoothed acceleration & velocity"
            #endregion "Flywheel"


            //this.functionChart1.BaseAxisX.Minimum = -2000000;
            //this.functionChart1.BaseAxisX.Maximum = 2000000;
            //this.functionChart1.BaseAxisX.Interval = 10000;

            //this.functionChart1.BaseAxisY.Minimum = -2000000;
            //this.functionChart1.BaseAxisY.Maximum = 2000000;
            //this.functionChart1.BaseAxisY.Interval = 10000;

        }


        private void DrawCircle(Color _color, double _r)
        {
            List<XY> _list = new List<XY>();

            for (int a = 0; a < 360; a++)
            {
                double _x = _r * Math.Sin(Conversions.DegToRad(a));
                double _y = _r * Math.Cos(Conversions.DegToRad(a));

                _list.Add(new XY(_x, _y));
            }

            this.functionChart1.DrawFunction(Function.FromPoints(_list), null, _color);
        }
        private Function DrawFunction(double _from, double _to, Color _color, Func<double, double> _function)
        {
            Function _chart = this.GetFunction(_from, _to, _function);
            this.functionChart1.DrawFunction(_chart, null, _color);

            return _chart;
        }
        private Function GetFunction(double _from, double _to, Func<double, double> _function)
        {
            List<XY> _points = new List<XY>();

            for (double _x = _from; _x < _to; _x++)
            {
                double _y = _function(_x);
                _points.Add(new XY(_x, Math.Round(_y, EngineDesigner.Common.Defaults.RoundingDecimals)));

                //Console.WriteLine(string.Format(
                //    "X ({0});  Y ({1})",
                //    _double.ToString(EngineDesigner.Common.Defaults.ROUNDING),
                //    _y.ToString(EngineDesigner.Common.Defaults.ROUNDING)));
            }

            Function _chart = Function.FromPoints(_points);

            return _chart;
        }
        private Function DrawFunction(double _from, double _to, Color _color, Func<double, double> _function, Func<double, double> _alterX)
        {
            Function _chart = this.GetFunction(_from, _to, _function, _alterX);
            this.functionChart1.DrawFunction(_chart, null, _color);

            return _chart;
        }
        private Function GetFunction(double _from, double _to, Func<double, double> _function, Func<double, double> _alterX)
        {
            List<XY> _points = new List<XY>();

            for (double _x = _from; _x < _to; _x++)
            {
                double _alteredX = _alterX(_x);

                double _y = _function(_alteredX);
                _points.Add(new XY(_x, _y /*Math.Round(_y, EngineDesigner.Common.Defaults.ROUNDING_DECIMALS)*/));

                //Console.WriteLine(string.Format(
                //    "X ({0});  Y ({1})",
                //    _alteredX.ToString(/*EngineDesigner.Common.Defaults.ROUNDING*/),
                //    _y.ToString(/*EngineDesigner.Common.Defaults.ROUNDING*/)));
            }

            Function _chart = Function.FromPoints(_points);

            return _chart;
        }
        private void DrawInertia(double _from, double _to, Color _color, double _rpm, PositionedCylinder _positionedCylinder)
        {
            List<XY> _points = new List<XY>();

            for (double _a = _from; _a < _to; _a++)
            {
                double _alteredA = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_a);

                double _x = _positionedCylinder.GetInertiaForceReciprocating_N(_a, _rpm);
                double _y = _positionedCylinder.GetInertiaForceRotating_N(_a, _rpm);

                _points.Add(new XY(_x, _y));

                Console.WriteLine(string.Format(
                    "X ({0});  Y ({1})",
                    _x.ToString(),
                    _y.ToString()));
            }

            Function _chart = Function.FromPoints(_points);
            this.functionChart1.DrawFunction(_chart, null, _color);
        }


        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double _rpm = (double)this.numericUpDown_RPM.Value;
            double _mass = (double)this.numericUpDown_Mass.Value;

            this.engine.Flywheel.Mass_g = _mass;
            this.ComputeFunctions(this.engine, _rpm, 360);

        }
    }
}