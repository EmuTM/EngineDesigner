using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;

using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Machine.Definitions;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents a flywheel unit for the engine.
    /// </summary>
    [TypeConverter(typeof(FlywheelConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Flywheel : IPart
    {
        private const int INTEGRATION_EVALUATIONS = 5;
        private const InterpolationMethod INTEGRATION_INTERPOLATION = InterpolationMethod.Linear;



        public Flywheel()
            : this(DefaultFlywheel.diameter_mm, DefaultFlywheel.mass_g)
        {

        }
        /// <param name="_diameter_mm">The flywheel's diameter, in milimeters.</param>
        /// <param name="_mass_g">The total mass of the flywheel, in grams.</param>
        public Flywheel(double _diameter_mm, double _mass_g)
            : this(false, _diameter_mm, _mass_g)
        {
        }
        private Flywheel(bool _skipValidation, double _diameter_mm, double _mass_g)
        {
            this.diameter_mm = _diameter_mm;
            this.mass_g = _mass_g;


            if (!_skipValidation)
            {
                this.Validate();
            }
#if IPART_ALWAYS_VALIDATE
            this.Validate();
#endif
        }
        public override bool Equals(object obj)
        {
            if (obj is Flywheel)
            {
                Flywheel _flywheel = (Flywheel)obj;

                if (this.guid.ToString() == _flywheel.guid.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format(
                "{0}g / {1}mm",
                mass_g.ToString(),
                diameter_mm.ToString());
        }



        #region "System-defined pistons"
        /// <summary>
        /// Gets the default flywheel (for UI integration).
        /// </summary>
        public static Flywheel DefaultFlywheel
        {
            get
            {
                return new Flywheel(
                    true,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultFlywheelDiameter_mm,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultFlywheelMass_g);
            }
        }
        #endregion "System-defined pistons"



        [DataMember]
        private double diameter_mm;
        [DataMember]
        private double mass_g;



        #region "Public properties"
        /// <summary>
        /// Defines the flywheel's diameter, in milimeters.
        /// </summary>
        [DisplayName("Diameter")]
        [Description("Defines the flywheel's diameter, in milimeters")]
        public double Diameter_mm
        {
            get { return this.diameter_mm; }

            set
            {
                this.diameter_mm = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the total mass of the flywheel, in grams.
        /// </summary>
        [DisplayName("Mass")]
        [Description("Defines the total mass of the flywheel, in grams.")]
        public double Mass_g
        {
            get { return this.mass_g; }

            set
            {
                this.mass_g = value;
                this.Validate();
            }
        }
        #endregion "Public properties"



        public double GetMomentOfInertia_Kgm2()
        {
            double _r_m = Conversions.MmToM(this.diameter_mm / 2d);
            double _double = 0.5d * Conversions.GToKg(this.mass_g) * Math.Pow(_r_m, 2);

            return _double;
        }
        public double GetKineticEnergy_J(double _rpm)
        {
            double _momentOfInertia_Kgm2 = this.GetMomentOfInertia_Kgm2();
            double _angularVelocity_degps = Conversions.RpmToDegps(_rpm);
            double _double = 0.5d * _momentOfInertia_Kgm2 * Math.Pow(_angularVelocity_degps, 2);

            return _double;
        }
        public double GetAngularAcceleration_degps2(double _instantaneousTorque_Nm)
        {
            double _momentOfInertia_Kgm2 = this.GetMomentOfInertia_Kgm2();
            double _angularAcceleration_degps2 = Conversions.RadToDeg(_instantaneousTorque_Nm / _momentOfInertia_Kgm2);

            return _angularAcceleration_degps2;
        }
        //integrira pospešek, da dobi hitrost
        public double GetAngularVelocity_degps(double _rpm, Function _angularAccelerationFunction_degps2, double _angle_deg, double _resolution_deg)
        {
            Function _subFunction = _angularAccelerationFunction_degps2.SubFunction(0d, _angle_deg + _resolution_deg);
            double _flywheelAngularVelocity = _subFunction.IntegrateY(INTEGRATION_INTERPOLATION, INTEGRATION_EVALUATIONS);

            return _rpm + Conversions.DegspsToRpm(_flywheelAngularVelocity);
        }

        //OBSOLETE: GetCoefficientOfSpeedFluctuation(double _rpm, double _kineticEnergyChange_Nm)
        //public double GetCoefficientOfSpeedFluctuation(double _rpm, double _kineticEnergyChange_Nm)
        //{
        //    double _radps = Conversions.DegToRad(Conversions.RpmToDegps(_rpm));
        //    double _coefficient = _kineticEnergyChange_Nm / (this.GetMomentOfInertia_Kgm2() * Math.Pow(_radps, 2));

        //    return _coefficient;
        //}
        //public double GetEnergyFluctuation_Nm(Function _torqueFunction)
        //{
        //    List<Function> _spikes = new List<Function>();

        //    _torqueFunction = _torqueFunction.Normalize();


        //    Function _positiveSpikes = this.FindPositiveSpikes(_torqueFunction);
        //    Function[] _splitPositiveSpikes = this.SplitByPositiveSpikes(_positiveSpikes);
        //    _spikes.AddRange(_splitPositiveSpikes);


        //    Function _negativeSpikes = this.FindNegativeSpikes(_torqueFunction);
        //    Function[] _splitNegativeSpikes = this.SplitByNegativeSpikes(_negativeSpikes);
        //    _spikes.AddRange(_splitNegativeSpikes);


        //    Function[] _sortedSpikes = this.SortSpikes(_spikes);


        //    double _suma = 0;
        //    double _minEnergy = double.MaxValue;
        //    double _maxEnergy = double.MinValue;
        //    foreach (Function _spike in _sortedSpikes)
        //    {
        //        //če nima vsaj dveh točk je to nek nepomemben ostanek
        //        if (_spike.Length > 1)
        //        {
        //            //NOTE: zgleda, da morajo bit radiani za dobit Nm
        //            double _kineticEnergy = Conversions.DegToRad(_spike.IntegrateY(INTEGRATION_INTERPOLATION, INTEGRATION));
        //            _suma += _kineticEnergy;

        //            //Console.WriteLine(string.Format(
        //            //    "{0}   /   {1}",
        //            //    _kineticnaEnergija,
        //            //    _suma));

        //            if (_suma < _minEnergy)
        //            {
        //                _minEnergy = _suma;
        //            }
        //            if (_suma > _maxEnergy)
        //            {
        //                _maxEnergy = _suma;
        //            }
        //        }
        //    }


        //    double _fluctuationOfEnergy = _maxEnergy - _minEnergy;
        //    return _fluctuationOfEnergy;
        //}
        //private Function FindPositiveSpikes(Function _function)
        //{
        //    List<XY> _points = new List<XY>();

        //    foreach (XY _xyTmp in _function)
        //    {
        //        XY _xy = new XY();
        //        _xy.X = _xyTmp.X;

        //        _xy.Y = _function.GetFx(_xyTmp.X);

        //        if (_xy.Y >= 0d)
        //        {
        //            _points.Add(_xy);
        //        }
        //    }

        //    return Function.FromPoints(_points);
        //}
        //private Function FindNegativeSpikes(Function _function)
        //{
        //    List<XY> _points = new List<XY>();

        //    foreach (XY _xyTmp in _function)
        //    {
        //        XY _xy = new XY();
        //        _xy.X = _xyTmp.X;

        //        _xy.Y = _function.GetFx(_xyTmp.X);

        //        if (_xy.Y <= 0d)
        //        {
        //            _points.Add(_xy);
        //        }
        //    }

        //    return Function.FromPoints(_points);
        //}
        //private Function[] SplitByPositiveSpikes(Function _function)
        //{
        //    List<Function> _functions = new List<Function>();


        //    List<XY> _points = new List<XY>();
        //    double _previousX = double.NaN;
        //    double _previousY = double.NaN;
        //    bool _hasRaised = false;
        //    bool _hasDropped = false;
        //    //načeloma je to 2
        //    double _maxApart = 2;

        //    foreach (XY _xy in _function)
        //    {
        //        //dokler nimamo točk za nazaj, nimamo kaj
        //        if ((!double.IsNaN(_previousY))
        //            && (!double.IsNaN(_previousX)))
        //        {
        //            if (((_hasRaised) && (_hasDropped) && (_xy.Y > _previousY))
        //                || (_xy.X > _previousX + _maxApart))
        //            {
        //                _functions.Add(Function.FromPoints(_points));

        //                _points.Clear();
        //                _hasDropped = false;
        //                _hasRaised = false;
        //            }
        //            else if ((_hasRaised)
        //                && (_xy.Y < _previousY))
        //            {
        //                _hasDropped = true;
        //            }
        //            else if (_xy.Y > _previousY)
        //            {
        //                _hasRaised = true;
        //            }
        //        }

        //        _points.Add(_xy);

        //        _previousX = _xy.X;
        //        _previousY = _xy.Y;
        //    }

        //    //dodamo še tisto, kar je ostalo
        //    if (_points.Count > 0)
        //    {
        //        _functions.Add(Function.FromPoints(_points));
        //    }


        //    return _functions.ToArray();
        //}
        //private Function[] SplitByNegativeSpikes(Function _function)
        //{
        //    List<Function> _functions = new List<Function>();


        //    List<XY> _points = new List<XY>();
        //    double _previousX = double.NaN;
        //    double _previousY = double.NaN;
        //    bool _hasDropped = false;
        //    bool _hasRaised = false;
        //    //načeloma je to 2
        //    double _maxApart = 2;

        //    foreach (XY _xy in _function)
        //    {
        //        //dokler nimamo točk za nazaj, nimamo kaj
        //        if ((!double.IsNaN(_previousY))
        //            && (!double.IsNaN(_previousX)))
        //        {
        //            if (((_hasDropped) && (_hasRaised) && (_xy.Y < _previousY))
        //                || (_xy.X > _previousX + _maxApart))
        //            {
        //                _functions.Add(Function.FromPoints(_points));

        //                _points.Clear();
        //                _hasDropped = false;
        //                _hasRaised = false;
        //            }
        //            else if ((_hasDropped) && (_xy.Y > _previousY))
        //            {
        //                _hasRaised = true;
        //            }
        //            else if (_xy.Y < _previousY)
        //            {
        //                _hasDropped = true;
        //            }
        //        }

        //        _points.Add(_xy);

        //        _previousX = _xy.X;
        //        _previousY = _xy.Y;
        //    }

        //    //dodamo še tisto, kar je ostalo
        //    if (_points.Count > 0)
        //    {
        //        _functions.Add(Function.FromPoints(_points));
        //    }


        //    return _functions.ToArray();
        //}
        //private Function[] SortSpikes(IEnumerable<Function> _spikes)
        //{
        //    List<Function> _sortedSpikes = new List<Function>();


        //    while (true)
        //    {
        //        Function _minXFunction = null;

        //        foreach (Function _function in _spikes)
        //        {
        //            //ta je že bila dodana
        //            if (_sortedSpikes.Contains(_function))
        //            {
        //                continue;
        //            }

        //            if (_minXFunction != null)
        //            {
        //                if (_function.GetMinX() < _minXFunction.GetMinX())
        //                {
        //                    _minXFunction = _function;
        //                }
        //            }
        //            else
        //            {
        //                _minXFunction = _function;
        //            }
        //        }

        //        if (_minXFunction != null)
        //        {
        //            _sortedSpikes.Add(_minXFunction);
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }


        //    return _sortedSpikes.ToArray();
        //}

        public double GetCoefficientOfSpeedFluctuation(double _rpm, Function _totalTorqueFunction_Nm, double _resolution_deg)
        {
            double _xFrom = _totalTorqueFunction_Nm.GetMinX();
            double _xTo = _totalTorqueFunction_Nm.GetMaxX();


            double _averageTorque_Nm = _totalTorqueFunction_Nm.GetAverageY();

            Function _excessiveTorqueFunction_Nm = Function.Compute(_xFrom, _xTo, _resolution_deg,
                delegate(double _x)
                {
                    double _torque = _totalTorqueFunction_Nm.GetFx(_x) - _averageTorque_Nm;
                    return _torque;
                });

            Function _flywheelAngularAccelerationFunction_degps2 = Function.Compute(_xFrom, _xTo, _resolution_deg,
                delegate(double _x)
                {
                    double _excessiveTorque_Nm = _excessiveTorqueFunction_Nm.GetFx(_x);
                    double _angularAcceleration_degps2 = this.GetAngularAcceleration_degps2(_excessiveTorque_Nm);
                    return _angularAcceleration_degps2;
                });

            Function _flywheelAngularVelocityFunction_degps = Utility.ComputeInThreadPool(_xFrom, _xTo, _resolution_deg,
                delegate(double _x)
                {
                    double _flywheelAngularVelocity_degp = this.GetAngularVelocity_degps(_rpm, _flywheelAngularAccelerationFunction_degps2, _x, _resolution_deg);
                    return _flywheelAngularVelocity_degp;
                });

            double _flywheelAngularVelocityAverage_degps = _flywheelAngularVelocityFunction_degps.GetAverageY();


            double _coefficientOfSpeedFluctuation = 1d - (_rpm / _flywheelAngularVelocityAverage_degps);
            return _coefficientOfSpeedFluctuation;

        }
        public double GetSmoothedTorque_Nm(double _rpm, double _instantaneousTorque_Nm, double _averageTorque_Nm, double _coefficientOfSpeedFluctuation)
        {
            double _excessiveTorque_Nm = _instantaneousTorque_Nm - _averageTorque_Nm;
            double _smoothedTorque_Nm = _averageTorque_Nm + (_excessiveTorque_Nm * _coefficientOfSpeedFluctuation);

            return _smoothedTorque_Nm;
        }



        #region IPart Members
        [DataMember]
        private Guid guid = Guid.NewGuid();
        [Browsable(false)]
        public Guid Guid
        {
            get { return guid; }
        }

        [Browsable(false)]
        public double Length
        {
            get
            {
                double _minLength = Bound_Z_Min; //najmanjša dolžina (če je negativna, se upošteva)
                double _maxLength = Bound_Z_Max; //največja dolžina

                if (_minLength < 0)
                {
                    return Math.Abs(_maxLength + Math.Abs(_minLength));
                }
                else
                {
                    return Math.Abs(_maxLength);
                }
            }
        }

        [Browsable(false)]
        public double Width
        {
            get
            {
                double _minWidth = Bound_X_Min; //najmanjša širina (če je negativno, potem je to širina "v levo" in se upošteva)
                double _maxWidth = Bound_X_Max; //največja širina (širina "v desno")

                if (_minWidth < 0)
                {
                    return Math.Abs(_maxWidth + Math.Abs(_minWidth));
                }
                else
                {
                    return Math.Abs(_maxWidth);
                }
            }
        }

        [Browsable(false)]
        public double Height
        {
            get
            {
                double _minHeight = Bound_Y_Min; //najmanjša višina (če je negativno, potem je to največja globina in se upošteva)
                double _maxHeight = Bound_Y_Max; //največja višina

                if (_minHeight < 0)
                {
                    return Math.Abs(_maxHeight + Math.Abs(_minHeight));
                }
                else
                {
                    return Math.Abs(_maxHeight);
                }
            }
        }

        [Browsable(false)]
        public double Bound_X_Min
        {
            get
            {
                return
                    -(this.diameter_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Y_Min
        {
            get
            {
                return
                    -(this.diameter_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Z_Min
        {
            get
            {
                return
                    -((this.diameter_mm / EngineDesigner.Machine.Properties.Settings.Default.FlywheelDiameterVsFlywheelWidth) / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_X_Max
        {
            get
            {
                return
                    (this.diameter_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Y_Max
        {
            get
            {
                return
                    (this.diameter_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Z_Max
        {
            get
            {
                return
                    ((this.diameter_mm / EngineDesigner.Machine.Properties.Settings.Default.FlywheelDiameterVsFlywheelWidth) / 2d);
            }
        }


        public void Validate()
        {
            if (this.diameter_mm < 0d)
            {
                throw new ValidationException("this.diameter_mm < 0d");
            }
            if (this.mass_g < 0d)
            {
                throw new ValidationException("this.mass_g < 0d");
            }


            this.OnValidated();
        }


        private event IPartDelegate validated;
        public event IPartDelegate Validated
        {
            add { this.validated += value; }
            remove { this.validated += value; }
        }
        protected void OnValidated()
        {
            if (this.validated != null)
            {
                this.validated(this);
            }
        }
        #endregion

    }


    /// <summary>
    /// Provides UI integration for Flywheel class.
    /// </summary>
    class FlywheelConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Piston))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Flywheel))
            {
                Flywheel _flywheel = (Flywheel)value;
                return _flywheel.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
