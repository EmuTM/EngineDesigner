using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;
using EngineDesigner.Environment.Controls.Charting;
using System.Windows.Forms.DataVisualization.Charting;
using EngineDesigner.FloatingForms.EngineMonitors.Analyzer;
using System.IO;
using System.Runtime.Serialization;
using EngineDesigner.Common.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class Form_Analyzer : Form_EngineMonitorBase
    {
        [DataContract]
        private class AnalyzerSerializer : Serializable<AnalyzerSerializer>
        {
            [DataMember]
            private FunctionInfoBase[] functionInfos;
            public FunctionInfoBase[] FunctionInfos
            {
                get { return functionInfos; }
                set { functionInfos = value; }
            }


            protected override Type[] GetKnownTypes()
            {
                return new Type[]
                {
                    typeof(System.Drawing.FontStyle),
                    typeof(System.Drawing.GraphicsUnit),

                    typeof(FunctionInfoBase),
                    typeof(FunctionInfoReference),
                    typeof(FunctionInfoKinematic),
                    typeof(FunctionInfoForce),
                    typeof(FunctionInfoTorque),
                    typeof(FunctionInfoSuperposition),

                };
            }

        }



        private const double RESOLUTION = 1d;
        private const double TRUNCATE_VALUE = 0.00001d;



        //tle damo na čakanje, če je trenutno backgroundWorkerZaseden; ko konča svoje opravilo, pogleda še tle
        private FunctionInfoBase[] pendingBackgroundWorkerArgument = null;

        //loaded cylinderPressureVsCrankAngleIndicatorFunction
        private FileInfo cylinderPressureVsCrankAngleIndicatorFunctionFile = null;
        private Function cylinderPressureVsCrankAngleIndicatorFunction = null;
        private InterpolationMethodInfo cylinderPressureVsCrankAngleInterpolationMethod = null;



        public Form_Analyzer()
            : this(null, null)
        {
        }
        public Form_Analyzer(Form _owner, Form_EngineControl _engineControl)
            : base(_owner, _engineControl)
        {
            InitializeComponent();


            this.toolStripStatusLabel_RPM.Text = string.Format(
                "{0} RPM",
                base.RPM);

            this.SetReady();
        }



        private double From
        {
            get
            {
                if (this.axisOptions1.CustomRange)
                {
                    return this.axisOptions1.StartDegrees;
                }
                else
                {
                    return 0d;
                }
            }
        }
        private double To
        {
            get
            {
                if (this.axisOptions1.CustomRange)
                {
                    return this.axisOptions1.EndDegrees;
                }
                else
                {
                    if (base.Engine != null)
                    {
                        if (base.Engine.CombinedCycleDuration_deg > 0)
                        {
                            return base.Engine.CombinedCycleDuration_deg;
                        }
                    }

                    return EngineDesigner.Common.Defaults.DefaultCycle_deg;
                }
            }
        }



        private Function ComputeFunction(FunctionInfoBase _functionInfoBase, double _from, double _to, double _resolution, double _rpm, Engine _engine, ref List<PositionedCylinder> _cylinderInconsistencies)
        {
            Function _function = null;


            PositionedCylinder _positionedCylinder = null;
            #region "po potrebi najdemo positioned cylinder, ali ga vpišemo med _cylinderInconsistencies"
            //tukaj preverimo, če imamo še vedno na voljo naš cilinder (FunctionInfoKinematics je najbolj base funkcija s cilindrom, zato preverimo kar na tej) in ga povežemo po referenci, da odseva trenutno stanje v engineu
            if (_functionInfoBase is FunctionInfoKinematic)
            {
                FunctionInfoKinematic _functionInfoKinematics = (FunctionInfoKinematic)_functionInfoBase;

                if (base.Engine != null)
                {
                    foreach (PositionedCylinder _positionedCylinderTmp in base.Engine.PositionedCylinders)
                    {
                        if (_positionedCylinderTmp.Equals(_functionInfoKinematics.PositionedCylinder))
                        {
                            _positionedCylinder = _positionedCylinderTmp;
                        }
                    }
                }

                if (_positionedCylinder != null)
                {
                    _functionInfoKinematics.PositionedCylinder = _positionedCylinder;
                }
                else
                {
                    _cylinderInconsistencies.Add(_functionInfoKinematics.PositionedCylinder);
                }
            }
            #endregion "po potrebi najdemo positioned cylinder, ali ga vpišemo med _cylinderInconsistencies"


            bool _truncateFunction = true;

            if (_functionInfoBase is FunctionInfoTorque)
            {
                #region "FunctionInfoTorque"
                FunctionInfoTorque _functionInfoTorque = (FunctionInfoTorque)_functionInfoBase;

                if (_functionInfoTorque.Equals(FunctionInfoTorque.CylinderGasPressureTorque_Nm))
                {
                    #region
                    Function _cylinderPressureVsCrankAngleIndicatorFunction = this.GetInterpolatedFunction(
                        _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction,
                        _from, _to, _resolution,
                        _functionInfoTorque.InterpolationMethod.InterpolationMethod);


                    if (_functionInfoTorque.CylinderRelative)
                    {
                        #region
                        if (_functionInfoTorque.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoTorque.PositionedCylinder.GetGasPressureTorque_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoTorque.PositionedCylinder.GetGasPressureTorqueAproximation_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg),
                                        _functionInfoTorque.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoTorque.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoTorque.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoTorque.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoTorque.PositionedCylinder.GetGasPressureTorque_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoTorque.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoTorque.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoTorque.PositionedCylinder.GetGasPressureTorqueAproximation_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg),
                                        _functionInfoTorque.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.CylinderInertiaTorque_Nm))
                {
                    #region
                    if (_functionInfoTorque.CylinderRelative)
                    {
                        #region
                        if (_functionInfoTorque.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoTorque.PositionedCylinder.GetInertiaTorque_Nm(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoTorque.PositionedCylinder.GetInertiaTorqueAproximation_Nm(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoTorque.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoTorque.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoTorque.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoTorque.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoTorque.PositionedCylinder.GetInertiaTorque_Nm(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoTorque.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoTorque.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoTorque.PositionedCylinder.GetInertiaTorqueAproximation_Nm(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoTorque.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.CylinderTotalTorque_Nm))
                {
                    #region
                    Function _cylinderPressureVsCrankAngleIndicatorFunction = this.GetInterpolatedFunction(
                        _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction,
                        _from, _to, _resolution,
                        _functionInfoTorque.InterpolationMethod.InterpolationMethod);


                    if (_functionInfoTorque.CylinderRelative)
                    {
                        #region
                        if (_functionInfoTorque.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoTorque.PositionedCylinder.GetTotalTorque_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg),
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoTorque.PositionedCylinder.GetTotalTorqueAproximation_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg),
                                        _rpm,
                                        _functionInfoTorque.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoTorque.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoTorque.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoTorque.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoTorque.PositionedCylinder.GetTotalTorque_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg),
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoTorque.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoTorque.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoTorque.PositionedCylinder.GetTotalTorqueAproximation_Nm(
                                        _crankThrowRotation_deg,
                                        _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg),
                                        _rpm,
                                        _functionInfoTorque.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelSmoothedCylinderGasPressureTorque_Nm))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderGasPressureTorque_Nm = FunctionInfoTorque.CylinderGasPressureTorque_Nm;
                    _cylinderGasPressureTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderGasPressureTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderGasPressureTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderGasPressureTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderGasPressureTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderGasPressureTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderGasPressureTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _gasPressureTorque_Nm = this.ComputeFunction(
                        _cylinderGasPressureTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);
                    double _gasPressureTorqueAverage_Nm = _gasPressureTorque_Nm.GetAverageY();
                    double _coefficientOfSpeedFluctuation = _engine.Flywheel.GetCoefficientOfSpeedFluctuation(
                        _rpm, _gasPressureTorque_Nm, _resolution);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetSmoothedTorque_Nm(_rpm, _gasPressureTorque_Nm.GetFx(_x), _gasPressureTorqueAverage_Nm, _coefficientOfSpeedFluctuation);
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelSmoothedCylinderInertiaTorque_Nm))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderInertiaTorque_Nm = FunctionInfoTorque.CylinderInertiaTorque_Nm;
                    _cylinderInertiaTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderInertiaTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderInertiaTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderInertiaTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderInertiaTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderInertiaTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderInertiaTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _inertiaTorque_Nm = this.ComputeFunction(
                        _cylinderInertiaTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);
                    double _inertiaTorqueAverage_Nm = _inertiaTorque_Nm.GetAverageY();
                    double _coefficientOfSpeedFluctuation = _engine.Flywheel.GetCoefficientOfSpeedFluctuation(
                        _rpm, _inertiaTorque_Nm, _resolution);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetSmoothedTorque_Nm(_rpm, _inertiaTorque_Nm.GetFx(_x), _inertiaTorqueAverage_Nm, _coefficientOfSpeedFluctuation);
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelSmoothedCylinderTotalTorque_Nm))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderTotalTorque_Nm = FunctionInfoTorque.CylinderTotalTorque_Nm;
                    _cylinderTotalTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderTotalTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderTotalTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderTotalTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderTotalTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderTotalTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderTotalTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _totalTorque_Nm = this.ComputeFunction(
                        _cylinderTotalTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);
                    double _totalTorqueAverage_Nm = _totalTorque_Nm.GetAverageY();
                    double _coefficientOfSpeedFluctuation = _engine.Flywheel.GetCoefficientOfSpeedFluctuation(
                        _rpm, _totalTorque_Nm, _resolution);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetSmoothedTorque_Nm(_rpm, _totalTorque_Nm.GetFx(_x), _totalTorqueAverage_Nm, _coefficientOfSpeedFluctuation);
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelAngularVelocityBecauseOfCylinderGasPressureTorque_degps))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderGasPressureTorque_Nm = FunctionInfoTorque.CylinderGasPressureTorque_Nm;
                    _cylinderGasPressureTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderGasPressureTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderGasPressureTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderGasPressureTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderGasPressureTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderGasPressureTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderGasPressureTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _gasPressureTorque_Nm = this.ComputeFunction(
                        _cylinderGasPressureTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetAngularVelocity_degps(_rpm, _gasPressureTorque_Nm, _x, _resolution);
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelAngularVelocityBecauseOfCylinderInertiaTorque_degps))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderInertiaTorque_Nm = FunctionInfoTorque.CylinderInertiaTorque_Nm;
                    _cylinderInertiaTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderInertiaTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderInertiaTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderInertiaTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderInertiaTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderInertiaTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderInertiaTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _inertiaTorque_Nm = this.ComputeFunction(
                        _cylinderInertiaTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetAngularVelocity_degps(_rpm, _inertiaTorque_Nm, _x, _resolution);
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelAngularVelocityBecauseOfCylinderTotalTorque_degps))
                {
                    #region
                    FunctionInfoTorque _cylinderTotalTorque_Nm = FunctionInfoTorque.CylinderTotalTorque_Nm;
                    _cylinderTotalTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderTotalTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderTotalTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderTotalTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderTotalTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderTotalTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderTotalTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _totalTorque_Nm = this.ComputeFunction(
                        _cylinderTotalTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetAngularAcceleration_degps2(_totalTorque_Nm.GetFx(_x));
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelAngularAccelerationBecauseOfCylinderGasPressureTorque_degpsmpdeg2))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderGasPressureTorque_Nm = FunctionInfoTorque.CylinderGasPressureTorque_Nm;
                    _cylinderGasPressureTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderGasPressureTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderGasPressureTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderGasPressureTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderGasPressureTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderGasPressureTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderGasPressureTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _gasPressureTorque_Nm = this.ComputeFunction(
                        _cylinderGasPressureTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetAngularAcceleration_degps2(_gasPressureTorque_Nm.GetFx(_x));
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelAngularAccelerationBecauseOfCylinderInertiaTorque_degpsmpdeg2))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderInertiaTorque_Nm = FunctionInfoTorque.CylinderInertiaTorque_Nm;
                    _cylinderInertiaTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderInertiaTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderInertiaTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderInertiaTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderInertiaTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderInertiaTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderInertiaTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _inertiaTorque_Nm = this.ComputeFunction(
                        _cylinderInertiaTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetAngularAcceleration_degps2(_inertiaTorque_Nm.GetFx(_x));
                            return _torque_Nm;
                        });
                    #endregion
                }
                else if (_functionInfoTorque.Equals(FunctionInfoTorque.FlywheelAngularAccelerationBecauseOfCylinderTotalTorque_degpsmpdeg2))
                {
                    #region
                    //dobimo najprej funkcijo navora
                    FunctionInfoTorque _cylinderTotalTorque_Nm = FunctionInfoTorque.CylinderTotalTorque_Nm;
                    _cylinderTotalTorque_Nm.ConvertYToPercents = _functionInfoTorque.ConvertYToPercents;
                    _cylinderTotalTorque_Nm.MinIsNegative100 = _functionInfoTorque.MinIsNegative100;
                    _cylinderTotalTorque_Nm.PositionedCylinder = _functionInfoTorque.PositionedCylinder;
                    _cylinderTotalTorque_Nm.CylinderRelative = _functionInfoTorque.CylinderRelative;
                    _cylinderTotalTorque_Nm.HarmonicOrder = _functionInfoTorque.HarmonicOrder;
                    _cylinderTotalTorque_Nm.CylinderPressureVsCrankAngleIndicatorFunction = _functionInfoTorque.CylinderPressureVsCrankAngleIndicatorFunction;
                    _cylinderTotalTorque_Nm.InterpolationMethod = _functionInfoTorque.InterpolationMethod;

                    Function _totalTorque_Nm = this.ComputeFunction(
                        _cylinderTotalTorque_Nm,
                        _from,
                        _to,
                        _resolution,
                        _rpm,
                        _engine,
                        ref _cylinderInconsistencies);

                    _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                        delegate(double _x)
                        {
                            double _torque_Nm = _engine.Flywheel.GetAngularAcceleration_degps2(_totalTorque_Nm.GetFx(_x));
                            return _torque_Nm;
                        });
                    #endregion
                }
                #endregion "FunctionInfoTorque"
            }
            else if (_functionInfoBase is FunctionInfoForce)
            {
                #region "FunctionInfoForce"
                FunctionInfoForce _functionInfoForce = (FunctionInfoForce)_functionInfoBase;

                if (_functionInfoForce.Equals(FunctionInfoForce.CylinderGasPressureForceAxial_N))
                {
                    #region
                    Function _cylinderPressureVsCrankAngleIndicatorFunction = this.GetInterpolatedFunction(
                        _functionInfoForce.CylinderPressureVsCrankAngleIndicatorFunction,
                        _from, _to, _resolution,
                        _functionInfoForce.InterpolationMethod.InterpolationMethod);


                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankThrowRotation_deg)
                            {
                                return _functionInfoForce.PositionedCylinder.GetGasPressureForce_N(
                                    _crankThrowRotation_deg,
                                    _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                            });
                        #endregion
                    }
                    else
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankshaftRotation_deg)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                    _from, _to,
                                    _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);
                                return _functionInfoForce.PositionedCylinder.GetGasPressureForce_N(
                                    _crankThrowRotation_deg,
                                    _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                            });
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderGasPressureForceX_N))
                {
                    #region
                    Function _cylinderPressureVsCrankAngleIndicatorFunction = this.GetInterpolatedFunction(
                        _functionInfoForce.CylinderPressureVsCrankAngleIndicatorFunction,
                        _from, _to, _resolution,
                        _functionInfoForce.InterpolationMethod.InterpolationMethod);


                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankThrowRotation_deg)
                            {
                                return _functionInfoForce.PositionedCylinder.GetGasPressureForceX_N(
                                    _crankThrowRotation_deg,
                                    _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                            });
                        #endregion
                    }
                    else
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankshaftRotation_deg)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                    _from, _to,
                                    _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                return _functionInfoForce.PositionedCylinder.GetGasPressureForceX_N(
                                    _crankThrowRotation_deg,
                                    _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                            });
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderGasPressureForceY_N))
                {
                    #region
                    Function _cylinderPressureVsCrankAngleIndicatorFunction = this.GetInterpolatedFunction(
                        _functionInfoForce.CylinderPressureVsCrankAngleIndicatorFunction,
                        _from, _to, _resolution,
                        _functionInfoForce.InterpolationMethod.InterpolationMethod);


                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankThrowRotation_deg)
                            {
                                return _functionInfoForce.PositionedCylinder.GetGasPressureForceY_N(
                                    _crankThrowRotation_deg,
                                    _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                            });
                        #endregion
                    }
                    else
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankshaftRotation_deg)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                    _from, _to,
                                    _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                return _functionInfoForce.PositionedCylinder.GetGasPressureForceY_N(
                                    _crankThrowRotation_deg,
                                    _cylinderPressureVsCrankAngleIndicatorFunction.GetFx(_crankThrowRotation_deg));
                            });
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceReciprocatingAxial_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocating_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximation_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocating_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximation_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceReciprocatingX_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingX_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximationX_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingX_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximationX_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceReciprocatingY_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingY_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximationY_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingY_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximationY_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceRotating_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankThrowRotation_deg)
                            {
                                return _functionInfoForce.PositionedCylinder.GetInertiaForceRotating_N(
                                    _crankThrowRotation_deg,
                                    _rpm);
                            });
                        #endregion
                    }
                    else
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankshaftRotation_deg)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                    _from, _to,
                                    _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                return _functionInfoForce.PositionedCylinder.GetInertiaForceRotating_N(
                                    _crankThrowRotation_deg,
                                    _rpm);
                            });
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceTotalAxial_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForce_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceAproximation_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForce_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceAproximation_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceTotalX_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceX_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceAproximationX_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceX_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceAproximationX_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceTotalY_N))
                {
                    #region
                    if (_functionInfoForce.CylinderRelative)
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceY_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceAproximationY_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceY_N(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoForce.PositionedCylinder.GetInertiaForceAproximationY_N(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoForce.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceReciprocatingVsRotating_N))
                {
                    //NOTE: ta zaenkrat ni v threadpoolu

                    #region
                    //to ni nikoli več kot 360
                    _to = Mathematics.GetCyclicAngle(_to, 0d, 360d, 360d);

                    List<XY> _points = new List<XY>();

                    if (_functionInfoForce.CylinderRelative)
                    {
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            for (double _angle_deg = _from; _angle_deg < _to; _angle_deg += _resolution)
                            {
                                _points.Add(new XY(
                                    _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocating_N(_angle_deg, _rpm),
                                    -_functionInfoForce.PositionedCylinder.GetInertiaForceRotating_N(_angle_deg, _rpm)));
                            }
                        }
                        else
                        {
                            for (double _angle_deg = _from; _angle_deg < _to; _angle_deg += _resolution)
                            {
                                _points.Add(new XY(
                                    _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximation_N(
                                        _angle_deg, _rpm, _functionInfoForce.HarmonicOrder.HarmonicOrders),
                                    -_functionInfoForce.PositionedCylinder.GetInertiaForceRotating_N(_angle_deg, _rpm)));
                            }
                        }
                    }
                    else
                    {
                        if (_functionInfoForce.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            for (double _angle_deg = _from; _angle_deg < _to; _angle_deg += _resolution)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_angle_deg),
                                    _from, _to,
                                    _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                _points.Add(new XY(
                                    _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm),
                                    -_functionInfoForce.PositionedCylinder.GetInertiaForceRotating_N(_crankThrowRotation_deg, _rpm)));
                            }
                        }
                        else
                        {
                            for (double _angle_deg = _from; _angle_deg < _to; _angle_deg += _resolution)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoForce.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_angle_deg),
                                    _from, _to,
                                    _functionInfoForce.PositionedCylinder.Cycle.Duration_deg);

                                _points.Add(new XY(
                                    _functionInfoForce.PositionedCylinder.GetInertiaForceReciprocatingAproximation_N(
                                    _crankThrowRotation_deg, _rpm, _functionInfoForce.HarmonicOrder.HarmonicOrders),
                                    -_functionInfoForce.PositionedCylinder.GetInertiaForceRotating_N(_crankThrowRotation_deg, _rpm)));
                            }
                        }
                    }

                    _function = Function.FromPoints(_points.ToArray(), false);
                    #endregion

                    _truncateFunction = false;
                }
                #endregion "FunctionInfoForce"
            }
            else if (_functionInfoBase is FunctionInfoMoment)
            {
                #region "FunctionInfoMoment"
                FunctionInfoMoment _functionInfoMoment = (FunctionInfoMoment)_functionInfoBase;

                if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentReciprocatingAxial_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocating_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingAproximation_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocating_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingAproximation_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentReciprocatingX_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingAproximationX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingAproximationX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentReciprocatingY_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingAproximationY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentReciprocatingAproximationY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentRotating_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankThrowRotation_deg)
                            {
                                return _functionInfoMoment.PositionedCylinder.GetInertiaMomentRotating_N(
                                    _engine.Length_mm,
                                    _crankThrowRotation_deg,
                                    _rpm);
                            });
                        #endregion
                    }
                    else
                    {
                        #region
                        _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                            delegate(double _crankshaftRotation_deg)
                            {
                                double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                    _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                    _from, _to,
                                    _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                return _functionInfoMoment.PositionedCylinder.GetInertiaMomentRotating_N(
                                    _engine.Length_mm,
                                    _crankThrowRotation_deg,
                                    _rpm);
                            });
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentTotalAxial_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMoment_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentAproximation_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMoment_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentAproximation_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentTotalX_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentAproximationX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentAproximationX_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoMoment.Equals(FunctionInfoMoment.CylinderInertiaMomentTotalY_N))
                {
                    #region
                    if (_functionInfoMoment.CylinderRelative)
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentAproximationY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoMoment.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoMoment.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoMoment.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoMoment.PositionedCylinder.GetInertiaMomentAproximationY_N(
                                        _engine.Length_mm,
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoMoment.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion "FunctionInfoMoment"
            }
            else if (_functionInfoBase is FunctionInfoKinematic)
            {
                #region "FunctionInfoKinematic"
                FunctionInfoKinematic _functionInfoKinematic = (FunctionInfoKinematic)_functionInfoBase;

                if (_functionInfoKinematic.Equals(FunctionInfoKinematic.PistonTravelFromCrankCenter_mm))
                {
                    #region
                    if (_functionInfoKinematic.CylinderRelative)
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonTravelFromCrankCenter_mm(
                                        _crankThrowRotation_deg);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonTravelFromCrankCenterAproximation_mm(
                                        _crankThrowRotation_deg,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonTravelFromCrankCenter_mm(
                                        _crankThrowRotation_deg);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonTravelFromCrankCenterAproximation_mm(
                                        _crankThrowRotation_deg,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoKinematic.Equals(FunctionInfoKinematic.PistonVelocity_mpdeg))
                {
                    #region
                    if (_functionInfoKinematic.CylinderRelative)
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocity_mpdeg(
                                        _crankThrowRotation_deg);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocityAproximation_mpdeg(
                                        _crankThrowRotation_deg,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocity_mpdeg(
                                        _crankThrowRotation_deg);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocityAproximation_mpdeg(
                                        _crankThrowRotation_deg,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoKinematic.Equals(FunctionInfoKinematic.PistonVelocity_mps))
                {
                    #region
                    if (_functionInfoKinematic.CylinderRelative)
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocity_mps(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocityAproximation_mps(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocity_mps(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonVelocityAproximation_mps(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoKinematic.Equals(FunctionInfoKinematic.PistonAcceleration_mpdeg2))
                {
                    #region
                    if (_functionInfoKinematic.CylinderRelative)
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAcceleration_mpdeg2(
                                        _crankThrowRotation_deg);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAccelerationAproximation_mpdeg2(
                                        _crankThrowRotation_deg,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAcceleration_mpdeg2(
                                        _crankThrowRotation_deg);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAccelerationAproximation_mpdeg2(
                                        _crankThrowRotation_deg,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (_functionInfoKinematic.Equals(FunctionInfoKinematic.PistonAcceleration_mps2))
                {
                    #region
                    if (_functionInfoKinematic.CylinderRelative)
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAcceleration_mps2(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankThrowRotation_deg)
                                {
                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAccelerationAproximation_mps2(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    else
                    {
                        #region
                        if (_functionInfoKinematic.HarmonicOrder.Equals(HarmonicOrderInfo.Full))
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAcceleration_mps2(
                                        _crankThrowRotation_deg,
                                        _rpm);
                                });
                        }
                        else
                        {
                            _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                                delegate(double _crankshaftRotation_deg)
                                {
                                    double _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                                        _functionInfoKinematic.PositionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                                        _from, _to,
                                        _functionInfoKinematic.PositionedCylinder.Cycle.Duration_deg);

                                    return _functionInfoKinematic.PositionedCylinder.GetPistonAccelerationAproximation_mps2(
                                        _crankThrowRotation_deg,
                                        _rpm,
                                        _functionInfoKinematic.HarmonicOrder.HarmonicOrders);
                                });
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion "FunctionInfoKinematic"
            }
            else if (_functionInfoBase is FunctionInfoReference)
            {
                #region "FunctionInfoReference"
                FunctionInfoReference _functionInfoReference = (FunctionInfoReference)_functionInfoBase;


                _function = Utility.ComputeInThreadPool(_from, _to, _resolution,
                    delegate(double _x)
                    {
                        Mathematics.MathParser _mathParser = new Mathematics.MathParser(new KeyValuePair<string, double>("x", _x));
                        return _mathParser.Compute(_functionInfoReference.Expression);
                    });
                #endregion "FunctionInfoReference"
            }
            else if (_functionInfoBase is FunctionInfoSuperposition)
            {
                #region "FunctionInfoSuperposition"
                FunctionInfoSuperposition _functionInfoSuperposition = (FunctionInfoSuperposition)_functionInfoBase;

                List<Function> _computedFunctions = new List<Function>();
                foreach (FunctionInfoBase _functionInfoBaseTmp in _functionInfoSuperposition.Functions)
                {
                    Function _computedFunction = this.ComputeFunction(_functionInfoBaseTmp, _from, _to, _resolution, _rpm, _engine, ref _cylinderInconsistencies);
                    if (_computedFunction != null)
                    {
                        _computedFunctions.Add(_computedFunction);
                    }
                }

                if (_computedFunctions.Count > 1)
                {
                    _function = Function.SuperpositonY(_computedFunctions.ToArray());
                }
                #endregion "FunctionInfoSuperposition"
            }
            else if (_functionInfoBase is FunctionInfoAverage)
            {
                #region "FunctionInfoAverage"
                FunctionInfoAverage _functionInfoAverage = (FunctionInfoAverage)_functionInfoBase;

                List<Function> _computedFunctions = new List<Function>();
                foreach (FunctionInfoBase _functionInfoBaseTmp in _functionInfoAverage.Functions)
                {
                    Function _computedFunction = this.ComputeFunction(_functionInfoBaseTmp, _from, _to, _resolution, _rpm, _engine, ref _cylinderInconsistencies);
                    if (_computedFunction != null)
                    {
                        _computedFunctions.Add(_computedFunction);
                    }
                }

                if (_computedFunctions.Count > 0)
                {
                    _function = Function.AverageY(_computedFunctions.ToArray());
                }
                #endregion "FunctionInfoAverage"
            }


            if (_function != null)
            {
                if (_functionInfoBase.ConvertYToPercents)
                {
                    _function.ConvertYToPercents(_function.GetMinY(), _function.GetMaxY(), _functionInfoBase.MinIsNegative100);
                }
            }
            else
            {
                throw new NotImplementedException(
                    string.Format(
                        "Function '{0}' is not implemented!",
                        _functionInfoBase.ToLongString()));
            }


            if (_truncateFunction)
            {
                return this.TruncateFunctionY(_function, TRUNCATE_VALUE);
            }
            else
            {
                return _function;
            }
        }
        //če so številke blizu 0, jih zaokroži na 0 (da ni problemov z grafom)
        //NOTE: ni idealno, ma zaenkrat edina rešitev
        private Function TruncateFunctionY(Function _function, double _minimumValue)
        {
            XY[] _xys = new XY[_function.Length];
            for (int a = 0; a < _function.Length; a++)
            {
                _xys[a] = _function[a];
            }


            for (int a = 0; a < _xys.Length; a++)
            {
                if (_xys[a].Y < 0)
                {
                    if (_xys[a].Y > -_minimumValue)
                    {
                        _xys[a].Y = 0;
                    }
                }
                else if (_xys[a].Y > 0)
                {
                    if (_xys[a].Y < _minimumValue)
                    {
                        _xys[a].Y = 0;
                    }
                }
            }


            return Function.FromPoints(_xys);
        }
        private Function GetInterpolatedFunction(Function _function, double _from, double _to, double _resolution, InterpolationMethod _interpolationMethod)
        {
            List<double> _desiredXValues = new List<double>();
            for (double _angle_deg = _from; _angle_deg < _to; _angle_deg += _resolution)
            {
                _desiredXValues.Add(_angle_deg);
            }

            Function _interpolatedFunction = _function.Interpolate(_interpolationMethod, _desiredXValues.ToArray());
            return _interpolatedFunction;
        }



        private ChartAreaInfo[] GetAvailableChartAreas()
        {
            ChartArea[][] _availableChartAreas = this.multiFunctionChart1.GetAvailableChartAreas();

            List<ChartAreaInfo> _differentChartAreas = new List<ChartAreaInfo>();
            foreach (ChartArea[] _chartAreas in _availableChartAreas)
            {
                _differentChartAreas.Add(new ChartAreaInfo(
                    _chartAreas[0], //_double
                    _chartAreas[1], //y
                    _chartAreas[2])); //chart
            }

            return _differentChartAreas.ToArray();
        }

        //zriše funkcije še enkrat
        private void SetAndRedrawChart()
        {
            this.SetBusy();

            //si zapomnemo funkcije, ki so trenutno na chartu
            List<FunctionInfoBase> _list = new List<FunctionInfoBase>();
            foreach (Function _function in this.multiFunctionChart1.FunctionsOnChart)
            {
                _list.Add((FunctionInfoBase)_function._Tag);
            }


            if (!this.backgroundWorker1.IsBusy)
            {
                this.backgroundWorker1.RunWorkerAsync(_list.ToArray());
            }
            else
            {
                this.pendingBackgroundWorkerArgument = _list.ToArray();
                this.backgroundWorker1.CancelAsync();
            }
        }
        private void SetAndRedrawCrankshaftAngle()
        {
            if (base.Engine != null)
            {
                if (this.axisOptions1.ShowElapsedStroke)
                {
                    if (!this.multiFunctionChart1.ChartCursorXEnabled)
                    {
                        this.multiFunctionChart1.ChartCursorXEnabled = true;
                    }


                    double _startDegrees = 0d;
                    double _endDegrees = base.Engine.CombinedCycleDuration_deg;
                    if (_endDegrees <= 0)
                    {
                        _endDegrees = EngineDesigner.Common.Defaults.DefaultCycle_deg;
                    }
                    double _cycleDegrees = _endDegrees;

                    if (this.axisOptions1.CustomRange)
                    {
                        _startDegrees = (double)this.axisOptions1.StartDegrees;
                        _endDegrees = (double)this.axisOptions1.EndDegrees;
                    }

                    if ((this.axisOptions1.ShowStrokeElapseCyclically)
                        || (!this.axisOptions1.CustomRange))
                    {
                        this.multiFunctionChart1.ChartCursorX = EngineDesigner.Common.Mathematics.GetCyclicAngle(
                            base.CrankshaftRotation_deg,
                            _startDegrees, _endDegrees,
                            _cycleDegrees);
                    }
                    else
                    {
                        this.multiFunctionChart1.ChartCursorX = base.CrankshaftRotation_deg;
                    }
                }
                else
                {
                    if (this.multiFunctionChart1.ChartCursorXEnabled)
                    {
                        this.multiFunctionChart1.ChartCursorXEnabled = false;
                    }
                }
            }
            else
            {
                if (this.multiFunctionChart1.ChartCursorXEnabled)
                {
                    this.multiFunctionChart1.ChartCursorXEnabled = false;
                }
            }
        }


        private void AddFuctionToChart(FunctionInfoBase _functionInfoBase, Function _function)
        {
            try
            {
                //shranimo v tag functionInfo
                _function._Tag = _functionInfoBase;

                this.multiFunctionChart1.CursorWindowMinX = this.From;
                this.multiFunctionChart1.CursorWindowMaxX = this.To;


                if (_functionInfoBase.ChartArea == null)
                {
                    //in jo narišemo
                    this.multiFunctionChart1.AddFunction(
                        _function,
                        _functionInfoBase.ToLongString(),
                        _functionInfoBase.Color);
                }
                else
                {
                    if (_functionInfoBase.ChartArea.ForceNewChartArea)
                    {
                        //in jo narišemo
                        this.multiFunctionChart1.AddFunction(
                            _function,
                            _functionInfoBase.ToLongString(),
                            _functionInfoBase.Color,
                            true);
                    }
                    else
                    {
                        //in jo narišemo
                        this.multiFunctionChart1.AddFunction(
                            _functionInfoBase.ChartArea.ChartAreaX,
                            _functionInfoBase.ChartArea.ChartAreaY,
                            _functionInfoBase.ChartArea.ChartAreaChart,
                            _function,
                            _functionInfoBase.ToLongString(),
                            _functionInfoBase.Color);
                    }
                }
            }
            catch (Exception _exception)
            {
                Utility.WarningMessage(
                    this,
                    this.Text,
                    "Error while adding function",
                    string.Format(
                        "Function '{0}' could not be added:{1}'{2}'",
                        _functionInfoBase.ToString(),
                        System.Environment.NewLine,
                        _exception.Message));
            }


            this.SetReady();
        }
        private void CheckInconsistency(IList<PositionedCylinder> _cylinderInconsistencies)
        {
            if (_cylinderInconsistencies.Count > 0)
            {
                StringBuilder _stringBuilder = new StringBuilder();
                foreach (PositionedCylinder _positionedCylinder in _cylinderInconsistencies)
                {
                    string _string = _positionedCylinder.ToString();

                    if (!_stringBuilder.ToString().Contains(_string))
                    {
                        _stringBuilder.AppendLine(string.Format(
                            "- {0}",
                            _string));
                    }
                }

                Utility.WarningMessage(
                    null, //ne moremo imeti ownerja, ker zgleda da priletimo iz drugega threada
                    this.Text,
                    "Inconsistency warning",
                    string.Format(
                        "The following cylinder(s) no longer exist or may not be the same:{0}{1}",
                        System.Environment.NewLine,
                        _stringBuilder.ToString()));
            }
        }


        private void toolStripMenuItem_CreateAverage_Click(object sender, EventArgs e)
        {
            this.CreateAverage();
        }
        private void toolStripMenuItem_CreateSuperpositon_Click(object sender, EventArgs e)
        {
            this.CreateSuperposition();
        }
        private void toolStripMenuItem_CreateAverage2_Click(object sender, EventArgs e)
        {
            this.CreateAverage();
        }
        private void toolStripMenuItem_CreateSuperpositon2_Click(object sender, EventArgs e)
        {
            this.CreateSuperposition();
        }
        private void CreateAverage()
        {
            List<FunctionInfoBase> _functionsToAverage;
            List<FunctionInfoBase> _unsortedFunctionsFound;

            this.BuildFunctionsTables(out _functionsToAverage, out _unsortedFunctionsFound);

            #region "obvestimo, če imamo takšne, kjer average ni mogoč"
            if (_unsortedFunctionsFound.Count > 0)
            {
                StringBuilder _stringBuilder = new StringBuilder();
                foreach (FunctionInfoBase _functionInfoBase in _unsortedFunctionsFound)
                {
                    _stringBuilder.Append("    - ");
                    _stringBuilder.AppendLine(_functionInfoBase.Name);
                }

                Utility.WarningMessage(
                    this,
                    this.Text,
                    "Error creating average",
                    string.Format(
                        "The following functions cannot be averaged and will be void:{0}{1}",
                        System.Environment.NewLine,
                        _stringBuilder.ToString()));
            }
            #endregion "obvestimo, če imamo takšne, kjer average ni mogoč"

            if (_functionsToAverage.Count > 0)
            {
                Form_AddAFunctionAverage _form_AddAFunctionAverage = new Form_AddAFunctionAverage(
                        this.GetAvailableChartAreas(), _functionsToAverage.ToArray());
                _form_AddAFunctionAverage.Owner = this;

                _form_AddAFunctionAverage.ShowDialog();
                if (_form_AddAFunctionAverage.SelectedFunction != null)
                {
                    this.SetBusy();

                    List<PositionedCylinder> _cylinderInconsistencies = new List<PositionedCylinder>();
                    Function _function = this.ComputeFunction(
                        _form_AddAFunctionAverage.SelectedFunction,
                        this.From, this.To, RESOLUTION, base.RPM, base.Engine, ref _cylinderInconsistencies);
                    this.CheckInconsistency(_cylinderInconsistencies);

                    #region "dodamo funkcijo na graf"
                    if (_function != null)
                    {
                        //shranimo v tag functionInfo
                        _function._Tag = _form_AddAFunctionAverage.SelectedFunction;

                        this.multiFunctionChart1.CursorWindowMinX = this.From;
                        this.multiFunctionChart1.CursorWindowMaxX = this.To;


                        if (_form_AddAFunctionAverage.SelectedFunction.ChartArea == null)
                        {
                            //in jo narišemo
                            this.multiFunctionChart1.AddFunction(
                                _function,
                                _form_AddAFunctionAverage.SelectedFunction.ToLongString(),
                                _form_AddAFunctionAverage.SelectedFunction.Color);
                        }
                        else
                        {
                            //in jo narišemo
                            this.multiFunctionChart1.AddFunction(
                                _form_AddAFunctionAverage.SelectedFunction.ChartArea.ChartAreaX,
                                _form_AddAFunctionAverage.SelectedFunction.ChartArea.ChartAreaY,
                                _form_AddAFunctionAverage.SelectedFunction.ChartArea.ChartAreaChart,
                                _function,
                                _form_AddAFunctionAverage.SelectedFunction.ToLongString(),
                                _form_AddAFunctionAverage.SelectedFunction.Color);
                        }
                    }
                    #endregion "dodamo funkcijo na graf"

                    this.SetReady();
                }
            }
        }
        private void CreateSuperposition()
        {
            List<FunctionInfoBase> _functionsToSuperpose;
            List<FunctionInfoBase> _unsortedFunctionsFound;

            this.BuildFunctionsTables(out _functionsToSuperpose, out _unsortedFunctionsFound);

            #region "obvestimo, če imamo takšne, kjer superpozicija ni mogoča"
            if (_unsortedFunctionsFound.Count > 0)
            {
                StringBuilder _stringBuilder = new StringBuilder();
                foreach (FunctionInfoBase _functionInfoBase in _unsortedFunctionsFound)
                {
                    _stringBuilder.Append("    - ");
                    _stringBuilder.AppendLine(_functionInfoBase.Name);
                }

                Utility.WarningMessage(
                    this,
                    this.Text,
                    "Error creating average",
                    string.Format(
                        "The following functions cannot be superposed and will be void:{0}{1}",
                        System.Environment.NewLine,
                        _stringBuilder.ToString()));
            }
            #endregion "obvestimo, če imamo takšne, kjer superpozicija ni mogoča"

            if (_functionsToSuperpose.Count > 1)
            {
                Form_AddAFunctionSuperposition _form_AddAFunctionSuperposition = new Form_AddAFunctionSuperposition(
                        this.GetAvailableChartAreas(), _functionsToSuperpose.ToArray());
                _form_AddAFunctionSuperposition.Owner = this;

                _form_AddAFunctionSuperposition.ShowDialog();
                if (_form_AddAFunctionSuperposition.SelectedFunction != null)
                {
                    this.SetBusy();

                    List<PositionedCylinder> _cylinderInconsistencies = new List<PositionedCylinder>();
                    Function _function = this.ComputeFunction(
                        _form_AddAFunctionSuperposition.SelectedFunction,
                        this.From, this.To, RESOLUTION, base.RPM, base.Engine, ref _cylinderInconsistencies);
                    this.CheckInconsistency(_cylinderInconsistencies);

                    #region "dodamo funkcijo na graf"
                    if (_function != null)
                    {
                        //shranimo v tag functionInfo
                        _function._Tag = _form_AddAFunctionSuperposition.SelectedFunction;

                        this.multiFunctionChart1.CursorWindowMinX = this.From;
                        this.multiFunctionChart1.CursorWindowMaxX = this.To;


                        if (_form_AddAFunctionSuperposition.SelectedFunction.ChartArea == null)
                        {
                            //in jo narišemo
                            this.multiFunctionChart1.AddFunction(
                                _function,
                                _form_AddAFunctionSuperposition.SelectedFunction.ToLongString(),
                                _form_AddAFunctionSuperposition.SelectedFunction.Color);
                        }
                        else
                        {
                            //in jo narišemo
                            this.multiFunctionChart1.AddFunction(
                                _form_AddAFunctionSuperposition.SelectedFunction.ChartArea.ChartAreaX,
                                _form_AddAFunctionSuperposition.SelectedFunction.ChartArea.ChartAreaY,
                                _form_AddAFunctionSuperposition.SelectedFunction.ChartArea.ChartAreaChart,
                                _function,
                                _form_AddAFunctionSuperposition.SelectedFunction.ToLongString(),
                                _form_AddAFunctionSuperposition.SelectedFunction.Color);
                        }
                    }
                    #endregion "dodamo funkcijo na graf"

                    this.SetReady();
                }
            }
        }
        private void BuildFunctionsTables(out List<FunctionInfoBase> _sortedFunctionsFound, out List<FunctionInfoBase> _unsortedFunctionsFound)
        {
            _sortedFunctionsFound = new List<FunctionInfoBase>();
            _unsortedFunctionsFound = new List<FunctionInfoBase>();

            foreach (Function _functionTmp in this.multiFunctionChart1.SelectedFunctions)
            {
                if (_functionTmp.Unsorted)
                {
                    _unsortedFunctionsFound.Add((FunctionInfoBase)_functionTmp._Tag);
                }
                else
                {
                    _sortedFunctionsFound.Add((FunctionInfoBase)_functionTmp._Tag);
                }
            }
        }

        private void multiFunctionChart1_LegendItemToolTipShowing(object sender, CustomLegendItemEventArgs e)
        {
            e.ShowOnlyWhenTextDoesNotFitScreen = false;


            Function _function = (Function)e.Series.Tag;


            StringBuilder _stringBuilder = new StringBuilder();

            _stringBuilder.AppendLine(string.Format(
                "X:   {0} - {1}",
                _function.GetMinX().ToString(Defaults.ROUNDING),
                _function.GetMaxX().ToString(Defaults.ROUNDING)));

            _stringBuilder.AppendLine(string.Format(
                "Y:   Min: {0}",
                _function.GetMinY().ToString(Defaults.ROUNDING)));
            _stringBuilder.AppendLine(string.Format(
                "      Avg: {0}",
                _function.GetAverageY().ToString(Defaults.ROUNDING)));
            _stringBuilder.AppendLine(string.Format(
                "      Max: {0}",
                _function.GetMaxY().ToString(Defaults.ROUNDING)));

            e.AlternativeText = _stringBuilder.ToString();
        }

        private void toolStripSplitButton_AddAFunction_ButtonClick(object sender, EventArgs e)
        {
            Form_AddAFunctionBase _form_AddAFunctionBase = null;
            if (base.Engine != null)
            {
                //najbolj derivan klas!
                _form_AddAFunctionBase = new Form_AddAFunctionTorque(
                    this.GetAvailableChartAreas(),
                    base.Engine.PositionedCylinders.ToArray(),
                    this.cylinderPressureVsCrankAngleIndicatorFunction,
                    this.cylinderPressureVsCrankAngleIndicatorFunctionFile,
                    this.cylinderPressureVsCrankAngleInterpolationMethod);
            }
            else
            {
                _form_AddAFunctionBase = new Form_AddAFunctionReference(
                    this.GetAvailableChartAreas());
            }
            _form_AddAFunctionBase.Owner = this;
            _form_AddAFunctionBase.ShowDialog();


            if (_form_AddAFunctionBase.SelectedFunction != null)
            {
                this.SetBusy();

                List<PositionedCylinder> _cylinderInconsistencies = new List<PositionedCylinder>();
                Function _function = this.ComputeFunction(
                    _form_AddAFunctionBase.SelectedFunction,
                    this.From, this.To, RESOLUTION, base.RPM, base.Engine, ref _cylinderInconsistencies);
                this.CheckInconsistency(_cylinderInconsistencies);

                //dodamo funkcijo na graf
                if (_function != null)
                {
                    this.AddFuctionToChart(_form_AddAFunctionBase.SelectedFunction, _function);
                }
            }
        }
        private void toolStripDropDownButton_RemoveSelectedFunctions_Click(object sender, EventArgs e)
        {
            DialogResult _dialogResult = MessageBox.Show(
                this,
                "Are you sure, you want to remove the selected function(s)?",
                "Confirm function(s) remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (_dialogResult == DialogResult.Yes)
            {
                List<Function> _functions = new List<Function>();
                _functions.AddRange(this.multiFunctionChart1.SelectedFunctions);

                foreach (Function _function in _functions)
                {
                    this.multiFunctionChart1.RemoveFunction(_function);
                }
            }
        }
        private void toolStripDropDownButton_Redraw_Click(object sender, EventArgs e)
        {
            this.SetAndRedrawChart();

            this.toolStripDropDownButton_Redraw.ForeColor = SystemColors.ControlText;
        }
        private void toolStripDropDownButton_ClearChart_Click(object sender, EventArgs e)
        {
            DialogResult _dialogResult = MessageBox.Show(
                this,
                "Are you sure, you want to clear the chart?",
                "Confirm clear chart",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (_dialogResult == DialogResult.Yes)
            {
                this.multiFunctionChart1.ClearChart();
            }
        }

        private void toolStripMenuItem_Load_Click(object sender, EventArgs e)
        {
            DialogResult _dialogResult = openFileDialog1.ShowDialog();

            if (_dialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    FileInfo _fileInfo = new FileInfo(openFileDialog1.FileName);

                    if (_fileInfo.Exists)
                    {
                        try
                        {
                            AnalyzerSerializer _analyzerSerializer = AnalyzerSerializer.From(_fileInfo.FullName);

                            this.SetBusy();
                            if (!this.backgroundWorker1.IsBusy)
                            {
                                this.backgroundWorker1.RunWorkerAsync(_analyzerSerializer.FunctionInfos);
                            }
                            else
                            {
                                this.pendingBackgroundWorkerArgument = _analyzerSerializer.FunctionInfos;
                                this.backgroundWorker1.CancelAsync();
                            }
                        }
                        catch (Exception _exception)
                        {
                            Utility.WarningMessage(
                                this,
                                this.Text,
                                "Error while loading file",
                                string.Format(
                                    "File '{0}' could not be loaded:{1}'{2}'",
                                    openFileDialog1.FileName,
                                    System.Environment.NewLine,
                                    _exception.Message));
                        }
                    }
                }
            }
        }
        private void toolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            DialogResult _dialogResult = saveFileDialog1.ShowDialog();

            if (_dialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    List<FunctionInfoBase> _list = new List<FunctionInfoBase>();
                    foreach (Function _function in this.multiFunctionChart1.FunctionsOnChart)
                    {
                        FunctionInfoBase _functionInfoBase = (FunctionInfoBase)_function._Tag;
                        _list.Add(_functionInfoBase);
                    }

                    AnalyzerSerializer _analyzerSerializer = new AnalyzerSerializer();
                    _analyzerSerializer.FunctionInfos = _list.ToArray();
                    _analyzerSerializer.Save(saveFileDialog1.FileName);
                }
            }
        }

        private void multiFunctionChart1_FunctionAdded(object sender, MultiFunctionChartEventArgs e)
        {
            FunctionInfoBase _functionInfoBase = (FunctionInfoBase)e.Function._Tag;
            //tukaj dobimo točne podatke, na katero chart areo smo dali graf, zato jih updatamo
            _functionInfoBase.ChartArea = new ChartAreaInfo(e.ChartAreaAxisX, e.ChartAreaAxisY, e.ChartAreaChart);
        }
        private void multiFunctionChart1_SelectedFunctionsChanged(object sender, EventArgs e)
        {
            this.toolStripMenuItem_CreateAverage.Enabled = false;
            this.toolStripMenuItem_CreateAverage2.Enabled = false;

            this.toolStripMenuItem_CreateSuperpositon.Enabled = false;
            this.toolStripMenuItem_CreateSuperpositon2.Enabled = false;

            this.toolStripDropDownButton_RemoveSelectedFunctions.Enabled = false;


            if (this.multiFunctionChart1.SelectedFunctions.Length > 1)
            {
                this.toolStripMenuItem_CreateSuperpositon.Enabled = true;
                this.toolStripMenuItem_CreateSuperpositon2.Enabled = true;
            }

            if (this.multiFunctionChart1.SelectedFunctions.Length > 0)
            {
                this.toolStripMenuItem_CreateAverage.Enabled = true;
                this.toolStripMenuItem_CreateAverage2.Enabled = true;

                this.toolStripDropDownButton_RemoveSelectedFunctions.Enabled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _backgroundWorker = (BackgroundWorker)sender;
            FunctionInfoBase[] _functions = (FunctionInfoBase[])e.Argument;


            List<Function> _recomputedFunctions = new List<Function>();
            List<PositionedCylinder> _cylinderInconsistencies = new List<PositionedCylinder>();

            foreach (FunctionInfoBase _functionInfoBase in _functions)
            {
                #region "Cancelled?"
                if (_backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                #endregion "Cancelled?"


                Function _function = this.ComputeFunction(
                    _functionInfoBase,
                    this.From, this.To, RESOLUTION, base.RPM, base.Engine, ref _cylinderInconsistencies);

                if (_function != null)
                {
                    _function._Tag = _functionInfoBase;

                    _recomputedFunctions.Add(_function);
                }
            }


            e.Result = new Tuple<Function[], IList<PositionedCylinder>>(
                _recomputedFunctions.ToArray(),
                _cylinderInconsistencies);
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                this.multiFunctionChart1.ClearChart();

                this.multiFunctionChart1.CursorWindowMinX = this.From;
                this.multiFunctionChart1.CursorWindowMaxX = this.To;
                this.multiFunctionChart1.ShowAxisXInPiValues = this.axisOptions1.ShowValuesInPI;


                Tuple<Function[], IList<PositionedCylinder>> _tuple = (Tuple<Function[], IList<PositionedCylinder>>)e.Result;
                this.CheckInconsistency(_tuple.Item2);

                foreach (Function _function in _tuple.Item1)
                {
                    FunctionInfoBase _functionInfoBase = (FunctionInfoBase)_function._Tag;

                    this.AddFuctionToChart(_functionInfoBase, _function);

                    //OBSOLETE:
                    //if (_functionInfoBase.ChartArea == null)
                    //{
                    //    //in jo narišemo
                    //    this.multiFunctionChart1.AddFunction(
                    //        _function,
                    //        _functionInfoBase.ToString(),
                    //        _functionInfoBase.Color);
                    //}
                    //else
                    //{
                    //    //in jo narišemo
                    //    this.multiFunctionChart1.AddFunction(
                    //        _functionInfoBase.ChartArea.ChartAreaX,
                    //        _functionInfoBase.ChartArea.ChartAreaY,
                    //        _functionInfoBase.ChartArea.ChartAreaChart,
                    //        _function,
                    //        _functionInfoBase.ToString(),
                    //        _functionInfoBase.Color);
                    //}
                }


                this.SetReady();
            }
            else //če je bil cancellan, imamo v morda pendingu
            {
                if (this.pendingBackgroundWorkerArgument != null)
                {
                    this.backgroundWorker1.RunWorkerAsync(this.pendingBackgroundWorkerArgument);
                    this.pendingBackgroundWorkerArgument = null;
                }
                else
                {
                    this.SetReady();
                }
            }
        }

        private void axisOptions1_CustomRangeChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawChart();
        }
        private void axisOptions1_StartDegreesChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawChart();
        }
        private void axisOptions1_EndDegreesChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawChart();
        }
        private void axisOptions1_ShowElapsedStrokeChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawCrankshaftAngle();
        }
        private void axisOptions1_ShowStrokeElapseCyclicallyChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawCrankshaftAngle();
        }
        private void axisOptions1_ShowValuesInPIChanged(object sender, EventArgs e)
        {
            this.SetAndRedrawChart();
        }

        #region "Fast functions"
        private ToolStripMenuItem[] AddEngineItems(ToolStripMenuItem _toolStripMenuItem, Engine _engine)
        {
            List<ToolStripMenuItem> _addedItems = new List<ToolStripMenuItem>();


            if (base.Engine.NumberOfCylinders > 0)
            {
                foreach (PositionedCylinder _positionedCylinder in _engine.PositionedCylinders)
                {
                    ToolStripMenuItem _toolStripMenuItem_PositionedCylinder = new ToolStripMenuItem();
                    _toolStripMenuItem_PositionedCylinder.Tag = _positionedCylinder;
                    _toolStripMenuItem_PositionedCylinder.Text = _positionedCylinder.ToString();

                    _toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem_PositionedCylinder);
                    _addedItems.Add(_toolStripMenuItem_PositionedCylinder);
                }


                _toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());


                ToolStripMenuItem _toolStripMenuItem_Cylinders = new ToolStripMenuItem();
                _toolStripMenuItem_Cylinders.Tag = _engine.PositionedCylinders.ToArray();
                _toolStripMenuItem_Cylinders.Text = "By cylinder";

                _toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem_Cylinders);
                _addedItems.Add(_toolStripMenuItem_Cylinders);


                _toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());


                ToolStripMenuItem _toolStripMenuItem_Engine = new ToolStripMenuItem();
                _toolStripMenuItem_Engine.Tag = _engine;
                _toolStripMenuItem_Engine.Text = _engine.ToString();

                _toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem_Engine);
                _addedItems.Add(_toolStripMenuItem_Engine);
            }


            return _addedItems.ToArray();
        }
        private ToolStripMenuItem[] AddFunctionInfoItems(ToolStripMenuItem _toolStripMenuItem, EventHandler _eventHandler, params FunctionInfoBase[] _functionInfosBase)
        {
            List<ToolStripMenuItem> _addedItems = new List<ToolStripMenuItem>();


            foreach (FunctionInfoBase _functionInfoBase in _functionInfosBase)
            {
                ToolStripMenuItem _toolStripMenuItem_FunctionInfoBase = new ToolStripMenuItem();
                _toolStripMenuItem_FunctionInfoBase.Tag = _functionInfoBase;
                _toolStripMenuItem_FunctionInfoBase.Text = _functionInfoBase.Name;
                _toolStripMenuItem_FunctionInfoBase.Click += _eventHandler;

                _toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem_FunctionInfoBase);
                _addedItems.Add(_toolStripMenuItem_FunctionInfoBase);
            }


            return _addedItems.ToArray();
        }
        private ToolStripMenuItem AddFunctionInfoItemsCombined(ToolStripMenuItem _toolStripMenuItem, EventHandler _eventHandler, string _text, params FunctionInfoBase[] _functionInfosBase)
        {
            ToolStripMenuItem _toolStripMenuItem_FunctionInfoBase = new ToolStripMenuItem();
            _toolStripMenuItem_FunctionInfoBase.Tag = _functionInfosBase;
            _toolStripMenuItem_FunctionInfoBase.Text = _text;
            _toolStripMenuItem_FunctionInfoBase.Click += _eventHandler;
            _toolStripMenuItem.DropDownItems.Add(_toolStripMenuItem_FunctionInfoBase);

            return _toolStripMenuItem_FunctionInfoBase;
        }

        private void toolStripSplitButton_AddAFunction_DropDownOpening(object sender, EventArgs e)
        {
            this.toolStripSplitButton_AddAFunction.DropDownItems.Clear();

            EventHandler _eventHandler = new EventHandler(ToolStripMenuItem_FastFunction_Click);

            ToolStripMenuItem _toolStripMenuItem_Reference = new ToolStripMenuItem();
            _toolStripMenuItem_Reference.Text = "Reference";
            this.AddFunctionInfoItems(_toolStripMenuItem_Reference, _eventHandler, FunctionInfoReference.GetAvailableFunctions());
            this.toolStripSplitButton_AddAFunction.DropDownItems.Add(_toolStripMenuItem_Reference);


            if (base.Engine != null)
            {
                this.toolStripSplitButton_AddAFunction.DropDownItems.Add(new ToolStripSeparator());

                #region "Kinematics"
                {
                    ToolStripMenuItem _toolStripMenuItem_Kinematics = new ToolStripMenuItem();
                    _toolStripMenuItem_Kinematics.Text = "Kinematics";

                    ToolStripMenuItem[] _engineItems = this.AddEngineItems(_toolStripMenuItem_Kinematics, base.Engine);
                    foreach (ToolStripMenuItem _toolStripMenuItem in _engineItems)
                    {
                        ToolStripMenuItem[] _functionInfoItems = this.AddFunctionInfoItems(_toolStripMenuItem, _eventHandler, FunctionInfoKinematic.GetAvailableFunctions());

                        //to ni za engine!
                        if (_toolStripMenuItem.Tag is PositionedCylinder)
                        {
                            _toolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());


                            #region "Piston travel, velocity and acceleration (cylinder relative) - percentages"
                            FunctionInfoKinematic _pistonTravelFromCrankCenter_mm = FunctionInfoKinematic.PistonTravelFromCrankCenter_mm;
                            _pistonTravelFromCrankCenter_mm.CylinderRelative = true;
                            _pistonTravelFromCrankCenter_mm.ConvertYToPercents = true;
                            _pistonTravelFromCrankCenter_mm.MinIsNegative100 = false;

                            FunctionInfoKinematic _pistonVelocity_mpdeg = FunctionInfoKinematic.PistonVelocity_mpdeg;
                            _pistonVelocity_mpdeg.CylinderRelative = true;
                            _pistonVelocity_mpdeg.ConvertYToPercents = true;
                            _pistonVelocity_mpdeg.MinIsNegative100 = true;

                            FunctionInfoKinematic _pistonAcceleration_mpdeg2 = FunctionInfoKinematic.PistonAcceleration_mpdeg2;
                            _pistonAcceleration_mpdeg2.CylinderRelative = true;
                            _pistonAcceleration_mpdeg2.ConvertYToPercents = true;
                            _pistonAcceleration_mpdeg2.MinIsNegative100 = true;

                            this.AddFunctionInfoItemsCombined(_toolStripMenuItem, _eventHandler,
                                "Piston travel, velocity and acceleration (cylinder relative)",
                                _pistonTravelFromCrankCenter_mm,
                                _pistonVelocity_mpdeg,
                                _pistonAcceleration_mpdeg2);
                            #endregion "Piston travel, velocity and acceleration (cylinder relative) - percentages"

                            #region "Piston acceleration components (cylinder relative)"
                            FunctionInfoKinematic _pistonAcceleration_mpdeg2_FirstApproximation = FunctionInfoKinematic.PistonAcceleration_mpdeg2;
                            _pistonAcceleration_mpdeg2_FirstApproximation.CylinderRelative = true;
                            _pistonAcceleration_mpdeg2_FirstApproximation.HarmonicOrder = HarmonicOrderInfo.FirstApproximation;

                            FunctionInfoKinematic _pistonAcceleration_mpdeg2_SecondApproximation = FunctionInfoKinematic.PistonAcceleration_mpdeg2;
                            _pistonAcceleration_mpdeg2_SecondApproximation.CylinderRelative = true;
                            _pistonAcceleration_mpdeg2_SecondApproximation.HarmonicOrder = HarmonicOrderInfo.SecondApproximation;

                            FunctionInfoKinematic _pistonAcceleration_mpdeg2_Full = FunctionInfoKinematic.PistonAcceleration_mpdeg2;
                            _pistonAcceleration_mpdeg2_Full.CylinderRelative = true;
                            _pistonAcceleration_mpdeg2_Full.HarmonicOrder = HarmonicOrderInfo.Full;

                            this.AddFunctionInfoItemsCombined(_toolStripMenuItem, _eventHandler,
                                "Piston acceleration components (cylinder relative)",
                                _pistonAcceleration_mpdeg2_FirstApproximation,
                                _pistonAcceleration_mpdeg2_SecondApproximation,
                                _pistonAcceleration_mpdeg2_Full);
                            #endregion "Piston acceleration components (cylinder relative)"
                        }
                    }

                    this.toolStripSplitButton_AddAFunction.DropDownItems.Add(_toolStripMenuItem_Kinematics);
                }
                #endregion "Kinematics"

                #region "Moments"
                {
                    ToolStripMenuItem _toolStripMenuItem_Moments = new ToolStripMenuItem();
                    _toolStripMenuItem_Moments.Text = "Moments";

                    ToolStripMenuItem[] _engineItems = this.AddEngineItems(_toolStripMenuItem_Moments, base.Engine);
                    foreach (ToolStripMenuItem _toolStripMenuItem in _engineItems)
                    {
                        this.AddFunctionInfoItems(_toolStripMenuItem, _eventHandler, FunctionInfoMoment.GetAvailableFunctions());
                    }

                    this.toolStripSplitButton_AddAFunction.DropDownItems.Add(_toolStripMenuItem_Moments);
                }
                #endregion "Moments"

                #region "Forces"
                {
                    ToolStripMenuItem _toolStripMenuItem_Forces = new ToolStripMenuItem();
                    _toolStripMenuItem_Forces.Text = "Forces";

                    ToolStripMenuItem[] _engineItems = this.AddEngineItems(_toolStripMenuItem_Forces, base.Engine);
                    foreach (ToolStripMenuItem _toolStripMenuItem in _engineItems)
                    {
                        List<FunctionInfoForce> _list = new List<FunctionInfoForce>();
                        foreach (FunctionInfoForce _functionInfoForce in FunctionInfoForce.GetAvailableFunctions())
                        {
                            //v engine ne moremo dati nesortirane funkcije, ker ne sme iti v superpozicijo!
                            if (_toolStripMenuItem.Tag is Engine)
                            {
                                if (!_functionInfoForce.Equals(FunctionInfoForce.CylinderInertiaForceReciprocatingVsRotating_N))
                                {
                                    _list.Add(_functionInfoForce);
                                }
                            }
                            else
                            {
                                _list.Add(_functionInfoForce);
                            }
                        }

                        this.AddFunctionInfoItems(_toolStripMenuItem, _eventHandler, _list.ToArray());
                    }

                    this.toolStripSplitButton_AddAFunction.DropDownItems.Add(_toolStripMenuItem_Forces);
                }
                #endregion "Forces"

                #region "Torques"
                {
                    ToolStripMenuItem _toolStripMenuItem_Torques = new ToolStripMenuItem();
                    _toolStripMenuItem_Torques.Text = "Torques";

                    ToolStripMenuItem[] _engineItems = this.AddEngineItems(_toolStripMenuItem_Torques, base.Engine);
                    foreach (ToolStripMenuItem _toolStripMenuItem in _engineItems)
                    {
                        this.AddFunctionInfoItems(_toolStripMenuItem, _eventHandler, FunctionInfoTorque.GetAvailableFunctions());
                    }

                    this.toolStripSplitButton_AddAFunction.DropDownItems.Add(_toolStripMenuItem_Torques);
                }
                #endregion "Torques"

                this.toolStripSplitButton_AddAFunction.DropDownItems.Add(new ToolStripSeparator());

                #region "CylinderPressureVsCrankAngleIndicatorFunction"
                ToolStripMenuItem _toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction = new ToolStripMenuItem();
                _toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction.Click += new EventHandler(toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction_Click);
                _toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction.Text = "Load Cylinder pressure vs. Crank angle indicator function";
                if (this.cylinderPressureVsCrankAngleIndicatorFunction != null)
                {
                    _toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction.Checked = true;
                }
                else
                {
                    _toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction.Checked = false;
                }
                this.toolStripSplitButton_AddAFunction.DropDownItems.Add(_toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction);
                #endregion "CylinderPressureVsCrankAngleIndicatorFunction"
            }
        }


        private void ToolStripMenuItem_FastFunction_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem_FunctionInfoBase = (ToolStripMenuItem)sender;


            FunctionInfoBase[] _functionInfosBase;
            #region "dobimo funkcije"
            if (_toolStripMenuItem_FunctionInfoBase.Tag is FunctionInfoBase)
            {
                _functionInfosBase = new FunctionInfoBase[1] 
                { 
                    (FunctionInfoBase)_toolStripMenuItem_FunctionInfoBase.Tag 
                };
            }
            else if (_toolStripMenuItem_FunctionInfoBase.Tag is FunctionInfoBase[])
            {
                _functionInfosBase = (FunctionInfoBase[])_toolStripMenuItem_FunctionInfoBase.Tag;
            }
            else
            {
                throw new NotSupportedException();
            }
            #endregion "dobimo funkcije"

            #region "poštimamo fukncije"
            foreach (FunctionInfoBase _functionInfoBase in _functionInfosBase)
            {
                if (_functionInfoBase is FunctionInfoKinematic)
                {
                    FunctionInfoKinematic _functionInfoKinematics = (FunctionInfoKinematic)_functionInfoBase;

                    //če ni drugače nastavljeno v meniju, izberemo full
                    if (_functionInfoKinematics.HarmonicOrder == null)
                    {
                        _functionInfoKinematics.HarmonicOrder = HarmonicOrderInfo.Full;
                    }
                }

                if (_functionInfoBase is FunctionInfoForce)
                {
                    FunctionInfoForce _functionInfoForce = (FunctionInfoForce)_functionInfoBase;

                    //če ni drugače nastavljeno v meniju, izberemo polinomsko
                    if (_functionInfoForce.InterpolationMethod == null)
                    {
                        _functionInfoForce.InterpolationMethod = InterpolationMethodInfo.Polynomially;
                    }
                }
            }
            #endregion "poštimamo fukncije"


            //pohendlamo ustrezno za parenta
            if (_toolStripMenuItem_FunctionInfoBase.OwnerItem.Tag is Engine)
            {
                #region
                Engine _engine = (Engine)_toolStripMenuItem_FunctionInfoBase.OwnerItem.Tag;


                //od vsake funkcije v meniju naredimo superpozicijo funkcij za spodaj ležeče cilindre
                List<FunctionInfoSuperposition> _list = new List<FunctionInfoSuperposition>();

                foreach (FunctionInfoKinematic _functionInfoKinematics in _functionInfosBase) //nižje od kinematics ni za Engine
                {
                    List<FunctionInfoBase> _functionsByCilinders = new List<FunctionInfoBase>();

                    //za vsak cilinder naredimo klon izbrane funkcije
                    foreach (PositionedCylinder _positionedCylinder in _engine.PositionedCylinders)
                    {
                        FunctionInfoKinematic _functionInfoKinematics_Cylinder = Utility.CopyObject<FunctionInfoKinematic>(_functionInfoKinematics);
                        _functionInfoKinematics_Cylinder.PositionedCylinder = _positionedCylinder;
                        _functionsByCilinders.Add(_functionInfoKinematics_Cylinder);
                    }

                    //iz cilindrov potem zložimo superpozicijo za cel motor
                    FunctionInfoSuperposition _functionInfoSuperposition = new FunctionInfoSuperposition(
                         string.Format(
                         "{0}; {1}",
                         _functionInfoKinematics.ToLongString(),
                         _engine.ToString()),
                        _functionsByCilinders.ToArray());

                    //in jo shranimo med superpozicije
                    _list.Add(_functionInfoSuperposition);
                }


                //izbrane funkcije so po novem superpozicije za spodaj ležeče cilindre
                _functionInfosBase = _list.ToArray();
                #endregion
            }
            else if (_toolStripMenuItem_FunctionInfoBase.OwnerItem.Tag is PositionedCylinder[])
            {
                #region
                PositionedCylinder[] _positionedCylinders = (PositionedCylinder[])_toolStripMenuItem_FunctionInfoBase.OwnerItem.Tag;


                List<FunctionInfoBase> _functionsByCilinders = new List<FunctionInfoBase>();

                foreach (FunctionInfoKinematic _functionInfoKinematics in _functionInfosBase) //nižje od kinematics ni za Engine
                {
                    //za vsak cilinder naredimo klon izbrane funkcije
                    foreach (PositionedCylinder _positionedCylinder in _positionedCylinders)
                    {
                        FunctionInfoKinematic _functionInfoKinematics_Cylinder = Utility.CopyObject<FunctionInfoKinematic>(_functionInfoKinematics);
                        _functionInfoKinematics_Cylinder.PositionedCylinder = _positionedCylinder;
                        _functionsByCilinders.Add(_functionInfoKinematics_Cylinder);
                    }
                }

                _functionInfosBase = _functionsByCilinders.ToArray();
                #endregion
            }
            else if (_toolStripMenuItem_FunctionInfoBase.OwnerItem.Tag is PositionedCylinder)
            {
                #region
                PositionedCylinder _positionedCylinder = (PositionedCylinder)_toolStripMenuItem_FunctionInfoBase.OwnerItem.Tag;

                //dodamo cilinder
                foreach (FunctionInfoKinematic _functionInfoKinematics in _functionInfosBase) //nižje od kinematics ni za PositionedCylinder
                {
                    _functionInfoKinematics.PositionedCylinder = _positionedCylinder;
                }
                #endregion
            }


            #region "damo barvo"
            foreach (FunctionInfoBase _functionInfoBase in _functionInfosBase)
            {
                _functionInfoBase.Color = this.colorPicker1.GetRandomColor();
            }
            #endregion "damo barvo"


            #region "preračunamo in damo na chart"
            this.SetBusy();

            foreach (FunctionInfoBase _functionInfoBase in _functionInfosBase)
            {
                #region "po potrebi zahtevamo nalaganje indikator funkcije"
                if (this.FunctionRequiresIndicatorFunction(_functionInfoBase))
                {
                    if (this.cylinderPressureVsCrankAngleIndicatorFunction == null)
                    {
                        this.LoadCylinderPressureVsCrankAngleIndicatorFunction();
                    }

                    //če nismo zloadali, je ta funkcija zavržena
                    if (this.cylinderPressureVsCrankAngleIndicatorFunction == null)
                    {
                        Utility.WarningMessage(
                            this,
                            this.Text,
                            "Error creating function",
                            string.Format(
                                "No indicator function 'Cylinder pressure vs. crank angle' loaded.{0}Function '{1}' will not be computed.",
                                System.Environment.NewLine,
                                _functionInfoBase.Name));

                        continue;
                    }
                    else
                    {
                        this.ApplyIndicatorFunction(_functionInfoBase, this.cylinderPressureVsCrankAngleIndicatorFunction, this.cylinderPressureVsCrankAngleInterpolationMethod);
                    }
                }
                #endregion "po potrebi zahtevamo nalaganje indikator funkcije"


                //NOTE: to mora bit tukaj, da vedno znova preverjam, katere chart aree so na voljo!
                #region
                ChartAreaInfo[] _availableChartAreas = this.GetAvailableChartAreas();

                //za procentne funkcije ok, če uporabimo kar y od 0-100 oz -100-100, ker itak vemo, da bo v tem razponu
                if (_functionInfoBase.ConvertYToPercents)
                {
                    foreach (ChartAreaInfo _chartAreaInfoTmp in _availableChartAreas)
                    {
                        if (((_chartAreaInfoTmp.ChartAreaChart.AxisY.Minimum == 0d) && (_chartAreaInfoTmp.ChartAreaChart.AxisY.Maximum == 100d))
                            || ((_chartAreaInfoTmp.ChartAreaChart.AxisY.Minimum == -100d) && (_chartAreaInfoTmp.ChartAreaChart.AxisY.Maximum == 100d)))
                        {
                            _functionInfoBase.ChartArea = _chartAreaInfoTmp;
                            break;
                        }
                    }
                }

                //če je chart area še vedno null, vseeno forcam vse funkcije na eno samo chart areo (razen za ta tip funkcije!)
                //NOTE: tle se poraja vprašanje, ali imeti vse grafe na eni chart areii ali ne?
                if (!_functionInfoBase.Equals(FunctionInfoForce.CylinderInertiaForceReciprocatingVsRotating_N))
                {
                    if (_functionInfoBase.ChartArea == null)
                    {
                        if (_availableChartAreas.Length > 0)
                        {
                            _functionInfoBase.ChartArea = _availableChartAreas[_availableChartAreas.Length - 1];
                        }
                    }
                }
                #endregion


                List<PositionedCylinder> _cylinderInconsistencies = new List<PositionedCylinder>();
                Function _function = this.ComputeFunction(
                    _functionInfoBase,
                    this.From, this.To, RESOLUTION, base.RPM, base.Engine, ref _cylinderInconsistencies);
                this.CheckInconsistency(_cylinderInconsistencies);

                if (_function != null)
                {
                    this.AddFuctionToChart(_functionInfoBase, _function);
                }
            }
            #endregion "preračunamo in damo na chart"
        }

        //rekurzije zaradi FunctionInfoSuperposition
        private void ApplyIndicatorFunction(FunctionInfoBase _functionInfoBase, Function _cylinderPressureVsCrankAngleIndicatorFunction, InterpolationMethodInfo _interpolationMethodInfo)
        {
            if ((_functionInfoBase.Equals(FunctionInfoForce.CylinderGasPressureForceAxial_N))
                || (_functionInfoBase.Equals(FunctionInfoForce.CylinderGasPressureForceX_N))
                || (_functionInfoBase.Equals(FunctionInfoForce.CylinderGasPressureForceY_N))
                || (_functionInfoBase.Equals(FunctionInfoTorque.CylinderGasPressureTorque_Nm))
                || (_functionInfoBase.Equals(FunctionInfoTorque.CylinderTotalTorque_Nm))
                || (_functionInfoBase.Equals(FunctionInfoTorque.FlywheelSmoothedCylinderGasPressureTorque_Nm))
                || (_functionInfoBase.Equals(FunctionInfoTorque.FlywheelSmoothedCylinderTotalTorque_Nm)))
            {
                //damo v funkcijo (zadosti, če kastamo v FunctionInfoForce
                ((FunctionInfoForce)_functionInfoBase).CylinderPressureVsCrankAngleIndicatorFunction = _cylinderPressureVsCrankAngleIndicatorFunction;
                ((FunctionInfoForce)_functionInfoBase).InterpolationMethod = _interpolationMethodInfo;
            }
            else if (_functionInfoBase is FunctionInfoSuperposition)
            {
                List<FunctionInfoBase> _functionInfosBaseInSuperposition = new List<FunctionInfoBase>();
                this.GetFunctionInfos((FunctionInfoSuperposition)_functionInfoBase, ref _functionInfosBaseInSuperposition);

                foreach (FunctionInfoBase _functionInfoBaseInSuperposition in _functionInfosBaseInSuperposition)
                {
                    this.ApplyIndicatorFunction(_functionInfoBaseInSuperposition, _cylinderPressureVsCrankAngleIndicatorFunction, _interpolationMethodInfo);
                }
            }
        }
        private bool FunctionRequiresIndicatorFunction(FunctionInfoBase _functionInfoBase)
        {
            if (_functionInfoBase is FunctionInfoForce)
            {
                FunctionInfoForce _functionInfoForce = (FunctionInfoForce)_functionInfoBase;
                return _functionInfoForce.RequiresIndicatorFunction;
            }
            else if (_functionInfoBase is FunctionInfoSuperposition)
            {
                List<FunctionInfoBase> _functionInfosBaseInSuperposition = new List<FunctionInfoBase>();
                this.GetFunctionInfos((FunctionInfoSuperposition)_functionInfoBase, ref _functionInfosBaseInSuperposition);

                foreach (FunctionInfoBase _functionInfoBaseInSuperposition in _functionInfosBaseInSuperposition)
                {
                    if (this.FunctionRequiresIndicatorFunction(_functionInfoBaseInSuperposition))
                    {
                        return true;
                    }
                }
            }


            return false;
        }
        private void GetFunctionInfos(FunctionInfoSuperposition _functionInfoSuperposition, ref List<FunctionInfoBase> _foundFunctionInfosBase)
        {
            foreach (FunctionInfoBase _functionInfoBase in _functionInfoSuperposition.Functions)
            {
                if (_functionInfoBase is FunctionInfoSuperposition)
                {
                    this.GetFunctionInfos((FunctionInfoSuperposition)_functionInfoBase, ref _foundFunctionInfosBase);
                }
                else
                {
                    _foundFunctionInfosBase.Add(_functionInfoBase);
                }
            }
        }


        private void toolStripMenuItem_CylinderPressureVsCrankAngleIndicatorFunction_Click(object sender, EventArgs e)
        {
            this.LoadCylinderPressureVsCrankAngleIndicatorFunction();
        }
        private void LoadCylinderPressureVsCrankAngleIndicatorFunction()
        {
            Form_LoadIndicatorFunction _form_LoadIndicatorFunction = new Form_LoadIndicatorFunction();
            _form_LoadIndicatorFunction.IndicatorFunctionName = "Cylinder pressure vs. crank angle";
            if (this.cylinderPressureVsCrankAngleInterpolationMethod != null)
            {
                _form_LoadIndicatorFunction.SelectedInterpolationMethod = this.cylinderPressureVsCrankAngleInterpolationMethod;
            }
            if (this.cylinderPressureVsCrankAngleIndicatorFunctionFile != null)
            {
                _form_LoadIndicatorFunction.SelectedIndicatorFunctionFile = this.cylinderPressureVsCrankAngleIndicatorFunctionFile;
            }

            DialogResult _dialogResult = _form_LoadIndicatorFunction.ShowDialog();

            if (_dialogResult == DialogResult.OK)
            {
                this.cylinderPressureVsCrankAngleIndicatorFunction = _form_LoadIndicatorFunction.SelectedIndicatorFunction;
                this.cylinderPressureVsCrankAngleInterpolationMethod = _form_LoadIndicatorFunction.SelectedInterpolationMethod;
                this.cylinderPressureVsCrankAngleIndicatorFunctionFile = _form_LoadIndicatorFunction.SelectedIndicatorFunctionFile;
            }
        }
        #endregion "Fast functions"


        private void SetBusy()
        {
            this.toolStripStatusLabel_Status.Text = "Computing...";
            this.Cursor = Cursors.AppStarting;
        }
        private void SetReady()
        {
            this.toolStripStatusLabel_Status.Text = "Ready";
            this.Cursor = Cursors.Default;
        }


        protected override void OnEngineChanged(Engine _engine)
        {
            this.SetAndRedrawChart();
        }
        protected override void OnRPMChanged(int _newRPM)
        {
            this.toolStripStatusLabel_RPM.Text = string.Format(
                "{0} RPM",
                base.RPM);


            if (this.multiFunctionChart1.FunctionsOnChart.Length > 0)
            {
                this.toolStripDropDownButton_Redraw.ForeColor = Defaults.RedColor;
            }
        }
        protected override void OnCrankshaftAngleChanged(double _newAngle_deg)
        {
            this.SetAndRedrawCrankshaftAngle();
        }

    }
}
