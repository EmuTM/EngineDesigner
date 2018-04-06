using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;
using EngineDesigner.Machine.Definitions;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents a cylinder unit for the engine.
    /// </summary>
    [TypeConverter(typeof(CylinderConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Cylinder : IPart
    {
        private const double VALIDATION_INTERSECTION_ACCURACY_deg = 10d;



        public Cylinder()
            : this(DefaultCylinder.cycle, DefaultCylinder.piston, DefaultCylinder.connectingRod, DefaultCylinder.crankThrow)
        {

        }
        /// <param name="_cycle">The cylinder's working cycle.</param>
        /// <param name="_piston">The piston unit to be used with this cylinder.</param>
        /// <param name="_connectingRod">The connecting rod unit to be used with this cylinder.</param>
        /// <param name="_crankThrow">The crank throw unit to be used with this cylinder.</param>
        public Cylinder(Cycle _cycle, Piston _piston, ConnectingRod _connectingRod, CrankThrow _crankThrow)
            : this(false, _cycle, _piston, _connectingRod, _crankThrow)
        {
        }
        private Cylinder(bool _skipValidation, Cycle _cycle, Piston _piston, ConnectingRod _connectingRod, CrankThrow _crankThrow)
        {
            this.cycle = _cycle;
            this.piston = _piston;
            this.connectingRod = _connectingRod;
            this.crankThrow = _crankThrow;


            if (!_skipValidation)
            {
                this.Validate();
            }
#if IPART_ALWAYS_VALIDATE
            this.Validate();
#endif


            this.piston.Validated += new IPartDelegate(this.IPart_Validated);
            this.connectingRod.Validated += new IPartDelegate(this.IPart_Validated);
            this.crankThrow.Validated += new IPartDelegate(this.IPart_Validated);
        }
        public override bool Equals(object obj)
        {
            if (obj is Cylinder)
            {
                Cylinder _cylinder = (Cylinder)obj;

                if (this.guid.ToString() == _cylinder.guid.ToString())
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
                "{0}T / {1}cm3",
                this.cycle.Strokes.Length.ToString(),
                Math.Round(this.Displacement_cm3, 1).ToString());
        }



        #region "System-defined cylinders"
        /// <summary>
        /// Gets the default cylinder (for UI integration).
        /// </summary>
        public static Cylinder DefaultCylinder
        {
            get
            {
                return new Cylinder(
                     true,
                    Cycle.NaN,
                    Piston.DefaultPiston,
                    ConnectingRod.DefaultConnectingRod,
                    CrankThrow.DefaultCrankThrow);
            }
        }

        /// <summary>
        /// Gets a simple (super-square) cilinder based on given parameters. 
        /// </summary>
        /// <param name="_cycle">The type of the cycle for the cylinder.</param>
        /// <param name="_displacement_cm3">The displacement of the cylinder, in cubic centimeters.</param>
        public static Cylinder FromParameters(Cycle _cycle, double _displacement_cm3)
        {
            double _displacement_mm3 = Conversions.Cm3ToMm3(_displacement_cm3);
            double _boreStroke_mm = Math.Pow(((4 * _displacement_mm3) / Math.PI), 1d / 3d);

            return Cylinder.FromParameters(
                _cycle,
                _boreStroke_mm,
                _boreStroke_mm);
        }

        /// <summary>
        /// Gets a cilinder based on given parameters. 
        /// </summary>
        /// <param name="_cycle">The type of the cycle for the cylinder.</param>
        /// <param name="_bore_mm">The displacement of the cylinder, in cubic centimeters.</param>
        /// <param name="_stroke_mm">The displacement of the cylinder, in cubic centimeters.</param>
        public static Cylinder FromParameters(Cycle _cycle, double _bore_mm, double _stroke_mm)
        {
            return new Cylinder(
                _cycle,
                Piston.FromParameters(
                    _bore_mm,
                    Piston.DefaultPiston.Mass_g),
                ConnectingRod.FromParameters(
                    ConnectingRod.DefaultConnectingRod.Mass_g,
                    _stroke_mm * EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodLengthVsStroke), //to je ena taka smiselna dolžina ojnice
                CrankThrow.FromParameters(_stroke_mm / 2d));
        }
        #endregion "System-defined cylinders"



        [DataMember]
        private Cycle cycle;
        [DataMember]
        private Piston piston;
        [DataMember]
        private ConnectingRod connectingRod;
        [DataMember]
        private CrankThrow crankThrow;



        #region "Public properties"
        /// <summary>
        /// Defines the cylinder's working cycle.
        /// </summary>
        [DisplayName("Cycle")]
        [Description("Defines the cylinder's working cycle.")]
        public Cycle Cycle
        {
            get { return cycle; }
            set { cycle = value; }
        }

        /// <summary>
        /// Defines the piston unit to be used with this cylinder.
        /// </summary>
        [DisplayName("Piston")]
        [Description("Defines the piston unit to be used with this cylinder.")]
        public Piston @Piston
        {
            get { return piston; }

            //TODO: dokler se ne ve kako rešit evente, naj bo zakomentirano
            //set
            //{
            //    piston = value;
            //    Validate();
            //}
        }

        /// <summary>
        /// Defines the connecting rod unit to be used with this cylinder.
        /// </summary>
        [DisplayName("Connecting rod")]
        [Description("Defines the connecting rod unit to be used with this cylinder.")]
        public ConnectingRod @ConnectingRod
        {
            get { return connectingRod; }

            //TODO: dokler se ne ve kako rešit evente, naj bo zakomentirano
            //set
            //{
            //    connectingRod = value;
            //    Validate();
            //}
        }

        /// <summary>
        /// Defines the crank throw unit to be used with this cylinder.
        /// </summary>
        [DisplayName("Crank throw")]
        [Description("Defines the crank throw unit to be used with this cylinder.")]
        public CrankThrow @CrankThrow
        {
            get { return this.crankThrow; }

            //TODO: dokler se ne ve kako rešit evente, naj bo zakomentirano
            //set
            //{
            //    this.crankThrow = value;
            //    this.Validate();
            //}
        }



        /// <summary>
        /// Indicates the cylinder's bore (based on piston's diameter), in milimeters.
        /// </summary>
        [DisplayName("Bore")]
        [Description("Indicates the cylinder's bore (based on piston's diameter), in milimeters.")]
        public double Bore_mm
        {
            get { return this.piston.Diameter_mm; }
        }

        /// <summary>
        /// Indicates the cylinder's stroke (based on crank throw's crank radius), in milimeters.
        /// </summary>
        [DisplayName("Stroke")]
        [Description("Indicates the cylinder's stroke (based on crank throw's crank radius), in milimeters.")]
        public double Stroke_mm
        {
            get { return this.crankThrow.CrankRotationRadius_mm * 2; }
        }

        /// <summary>
        /// Indicates the cylinder's total displacement (resulting from defined bore and stroke), in cubic centimeters.
        /// </summary>
        [DisplayName("Displacement")]
        [Description("Indicates the cylinder's total displacement (resulting from defined bore and stroke), in cubic centimeters.")]
        public double Displacement_cm3
        {
            get
            {
                return Conversions.Mm3ToCm3(this.piston.Area_mm2 * Stroke_mm);
            }
        }

        /// <summary>
        /// Indicates the cylinder's stroke to bore ratio.
        /// </summary>
        [DisplayName("Stroke/Bore ratio")]
        [Description("Indicates the cylinder's stroke to bore ratio.")]
        public double StrokeToBoreRatio
        {
            get { return Stroke_mm / Bore_mm; }
        }

        /// <summary>
        /// Indicates the cylinder's bore to stroke ratio.
        /// </summary>
        [DisplayName("Bore/Stroke ratio")]
        [Description("Indicates the cylinder's bore to stroke ratio.")]
        public double BoreToStrokeRatio
        {
            get { return this.Bore_mm / this.Stroke_mm; }
        }

        /// <summary>
        /// Indicates the cylinder's crank radius vs. connecting rod length.
        /// </summary>
        [DisplayName("R/L ratio")]
        [Description("Indicates the crank throw's rotation radius vs. connecting rod length.")]
        public double RLRatio
        {
            get { return this.crankThrow.CrankRotationRadius_mm / connectingRod.Length_mm; }
        }

        /// <summary>
        /// Indicates the cylinder's connecting rod length vs. crank radius.
        /// </summary>
        [DisplayName("L/R ratio")]
        [Description("Indicates the cylinder's connecting rod length vs. crank radius.")]
        public double LRRatio
        {
            get { return this.connectingRod.Length_mm / this.crankThrow.CrankRotationRadius_mm; }
        }



        /// <summary>
        /// Indicates the required cylinder's height to accommodate the pistion within it's stroke, in milimeters.
        /// </summary>
        [DisplayName("Cylinder height")]
        [Description("Indicates the required cylinder's height to accommodate the piston within its stroke, in milimeters.")]
        public double CylinderHeight_mm
        {
            get
            {
                return Math.Abs(this.GetPhysicalHeightAbovePiston_mm(0) - this.GetPhysicalHeightUnderPiston_mm(Stroke.StrokeDuration_deg));
            }
        }
        #endregion "Public properties"



        #region "Kinematika"
        public double GetPhysicalHeightAbovePiston_mm(double _crankThrowRotation_deg)
        {
            //najdaljša razdalja od centra
            double _double = GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg);
            _double += this.piston.GudgeonPinDistanceFromTop_mm;

            return _double;
        }
        public double GetPhysicalHeightUnderPiston_mm(double _crankThrowRotation_deg)
        {
            //najkrajša razdalja od centra
            double _double = GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg);
            _double -= (this.piston.SkirtLength_mm - this.piston.GudgeonPinDistanceFromTop_mm);

            return _double;
        }
        public double GetConRodAngle_deg(double _crankThrowRotation_deg)
        {
            double _n = Math.Sin(Conversions.DegToRad(_crankThrowRotation_deg)) * this.crankThrow.CrankRotationRadius_mm;
            double _radians = Math.Asin(_n / connectingRod.Length_mm);

            return Conversions.RadToDeg(_radians);
        }

        public double GetMeanPistonVelocity_mps(double _rpm)
        {
            return Conversions.MmToM(2 * Stroke_mm) * Conversions.RpmToRps(_rpm);
        }

        public double GetCompressionRatio(double _clearanceVolume_cm3)
        {
            double _double = (this.Displacement_cm3 + _clearanceVolume_cm3) / _clearanceVolume_cm3;
            return _double;
        }

        public double GetCombustionChamberHeight_mm(double _crankThrowRotation_deg)
        {
            return this.GetPhysicalHeightAbovePiston_mm(0) - this.GetPhysicalHeightAbovePiston_mm(_crankThrowRotation_deg);
        }

        public double GetPistonTravelFromCrankCenter_mm(double _crankThrowRotation_deg)
        {
            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));


            double _r2sin2A = Math.Pow(this.crankThrow.CrankRotationRadius_mm, 2) * Math.Pow(Math.Sin(_crankThrowRotation_rad), 2);
            double _sqrRoot = Math.Sqrt(Math.Pow(connectingRod.Length_mm, 2) - _r2sin2A);

            double _rcosA = this.crankThrow.CrankRotationRadius_mm * Math.Cos(_crankThrowRotation_rad);

            return (_rcosA + _sqrRoot);
        }
        public double GetPistonTravelFromCrankCenterAproximation_mm(double _crankThrowRotation_deg, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));

            double _prefix = this.ConnectingRod.Length_mm - (Math.Pow(this.crankThrow.CrankRotationRadius_mm, 2) / (4d * this.ConnectingRod.Length_mm));
            double _firstOrder = Math.Cos(_crankThrowRotation_rad);
            double _secondOrder = Math.Cos(2d * _crankThrowRotation_rad) * (this.crankThrow.CrankRotationRadius_mm / (4d * this.ConnectingRod.Length_mm));

            double _result = _prefix;
            if (_orders.Contains<uint>(1))
            {
                _result += this.crankThrow.CrankRotationRadius_mm * _firstOrder;
            }
            if (_orders.Contains<uint>(2))
            {
                _result += this.crankThrow.CrankRotationRadius_mm * _secondOrder;
            }


            return _result;
        }

        public double GetDisplacement_cm3(double _crankThrowRotation_deg)
        {
            return Conversions.Mm3ToCm3(
                this.piston.Area_mm2
                * Math.Abs(this.GetPistonTravelFromCrankCenter_mm(Stroke.StrokeDuration_deg) - this.GetPistonTravelFromCrankCenter_mm(Stroke.StrokeDuration_deg)));

            //GetPistonTravelFromTop_units(GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg), TotalHeight_mm));
        }
        public double GetDisplacementAproximation_cm3(double _crankThrowRotation_deg, params uint[] _orders)
        {
            return Conversions.Mm3ToCm3(
                this.piston.Area_mm2
                * Math.Abs(this.GetPistonTravelFromCrankCenterAproximation_mm(Stroke.StrokeDuration_deg, _orders) - this.GetPistonTravelFromCrankCenterAproximation_mm(Stroke.StrokeDuration_deg, _orders)));
        }

        public double GetPistonVelocity_mpdeg(double _crankThrowRotation_deg)
        {
            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));
            double _sinCrankThrowRotation = Math.Sin(_crankThrowRotation_rad);
            double _crankRadius_m = Conversions.MmToM(this.crankThrow.CrankRotationRadius_mm);
            double _connectingRodLength_m = Conversions.MmToM(this.connectingRod.Length_mm);


            double _rsinA = _crankRadius_m * _sinCrankThrowRotation;

            double _r2sinAcosA = Math.Pow(_crankRadius_m, 2) * _sinCrankThrowRotation * Math.Cos(_crankThrowRotation_rad);

            double _r2sin2A = Math.Pow(_crankRadius_m, 2) * Math.Pow(_sinCrankThrowRotation, 2);
            double _sqrRoot = Math.Sqrt(Math.Pow(_connectingRodLength_m, 2) - _r2sin2A);

            double _xTmp = _r2sinAcosA / _sqrRoot;

            return (-_rsinA - _xTmp);
        }
        public double GetPistonVelocityAproximation_mpdeg(double _crankThrowRotation_deg, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));


            double _result = 0d;
            if (_orders.Contains<uint>(1))
            {
                double _firstOrder = Math.Sin(_crankThrowRotation_rad);
                _result += -this.crankThrow.CrankRotationRadius_mm * _firstOrder;
            }
            if (_orders.Contains<uint>(2))
            {
                double _secondOrder = Math.Sin(2d * _crankThrowRotation_rad) * (this.crankThrow.CrankRotationRadius_mm / (2d * this.ConnectingRod.Length_mm));
                _result += -this.crankThrow.CrankRotationRadius_mm * _secondOrder;
            }


            return _result;
        }
        public double GetPistonVelocity_mps(double _crankThrowRotation_deg, double _rpm)
        {
            return (GetPistonVelocity_mpdeg(_crankThrowRotation_deg) * EngineDesigner.Common.Conversions.RpmToDegps(_rpm));
        }
        public double GetPistonVelocityAproximation_mps(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            return GetPistonVelocityAproximation_mpdeg(_crankThrowRotation_deg, _orders) * EngineDesigner.Common.Conversions.RpmToDegps(_rpm);
        }

        public double GetPistonAcceleration_mpdeg2(double _crankThrowRotation_deg)
        {
            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));
            double _sinCrankThrowRotation = Math.Sin(_crankThrowRotation_rad);
            double _cosCrankThrowRotation = Math.Cos(_crankThrowRotation_rad);
            double _crankRadius_m = Conversions.MmToM(this.crankThrow.CrankRotationRadius_mm);
            double _connectingRodLength_m = Conversions.MmToM(this.connectingRod.Length_mm);


            double _rcosA = _crankRadius_m * _cosCrankThrowRotation;

            double _r2cos2A_sin2A = Math.Pow(_crankRadius_m, 2) * (Math.Pow(_cosCrankThrowRotation, 2) - Math.Pow(_sinCrankThrowRotation, 2));

            double _r2sin2A = Math.Pow(_crankRadius_m, 2) * Math.Pow(_sinCrankThrowRotation, 2);
            double _sqrRoot1 = Math.Sqrt(Math.Pow(_connectingRodLength_m, 2) - _r2sin2A);

            double _r4sin2Acos2A = Math.Pow(_crankRadius_m, 4) * Math.Pow(_sinCrankThrowRotation, 2) * Math.Pow(_cosCrankThrowRotation, 2);

            double _sqrRoot2 = Math.Pow(Math.Sqrt(Math.Pow(_connectingRodLength_m, 2) - _r2sin2A), 3);

            double _1 = _r2cos2A_sin2A / _sqrRoot1;
            double _2 = _r4sin2Acos2A / _sqrRoot2;

            return (-_rcosA - _1 - _2);
        }
        public double GetPistonAccelerationAproximation_mpdeg2(double _crankThrowRotation_deg, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));
            double _crankRadius_m = Conversions.MmToM(this.crankThrow.CrankRotationRadius_mm);
            double _connectingRodLength_m = Conversions.MmToM(this.connectingRod.Length_mm);


            double _result = 0d;
            if (_orders.Contains<uint>(1))
            {
                double _firstOrder = Math.Cos(_crankThrowRotation_rad);
                _result += -_crankRadius_m * _firstOrder;
            }
            if (_orders.Contains<uint>(2))
            {
                double _secondOrder = Math.Cos(2d * _crankThrowRotation_rad) * (_crankRadius_m / _connectingRodLength_m);
                _result += -_crankRadius_m * _secondOrder;
            }


            return _result;
        }
        public double GetPistonAcceleration_mps2(double _crankThrowRotation_deg, double _rpm)
        {
            return (GetPistonAcceleration_mpdeg2(_crankThrowRotation_deg) * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2));
        }
        public double GetPistonAccelerationAproximation_mps2(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            return (GetPistonAccelerationAproximation_mpdeg2(_crankThrowRotation_deg, _orders) * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2));
        }
        #endregion "Kinematika"

        #region "Dinamika"
        //sile
        public double GetGasPressureForce_N(double _crankThrowRotation_deg, double _cylinderPressureAtCrankThrowRotation_Bar)
        {
            return
                Conversions.Mm2ToM2(this.piston.Area_mm2)
                * (_cylinderPressureAtCrankThrowRotation_Bar * 100000);
        }

        public double GetInertiaForceReciprocating_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _rotatingMassInertia = Conversions.GToKg(-this.connectingRod.RotatingMass_g) * Conversions.MmToM(-this.crankThrow.CrankRotationRadius_mm)
                * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2)
                * Math.Cos(Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg)));

            double _balancerInertia = Conversions.GToKg(-this.crankThrow.BalancerMass_g) * Conversions.MmToM(-this.crankThrow.BalancerRotationRadius_mm)
                * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2)
                * Math.Cos(Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg + this.crankThrow.BalancerAngle_deg)));

            double _reciprocatingMassInertia = Conversions.GToKg(this.connectingRod.ReciprocatingMass_g + this.piston.Mass_g) * this.GetPistonAcceleration_mps2(_crankThrowRotation_deg, _rpm);

            double _FIx = (_rotatingMassInertia + _balancerInertia) - _reciprocatingMassInertia;


            return _FIx;
        }
        public double GetInertiaForceReciprocatingAproximation_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _rotatingMassInertia = Conversions.GToKg(-this.connectingRod.RotatingMass_g) * Conversions.MmToM(-this.crankThrow.CrankRotationRadius_mm)
                * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2)
                * Math.Cos(Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg)));

            double _balancerInertia = Conversions.GToKg(-this.crankThrow.BalancerMass_g) * Conversions.MmToM(-this.crankThrow.BalancerRotationRadius_mm)
                * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2)
                * Math.Cos(Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg + this.crankThrow.BalancerAngle_deg)));

            double _reciprocatingMassInertia = Conversions.GToKg(this.connectingRod.ReciprocatingMass_g + this.piston.Mass_g) * this.GetPistonAccelerationAproximation_mps2(_crankThrowRotation_deg, _rpm, _orders);

            double _FIx = (_rotatingMassInertia + _balancerInertia) - _reciprocatingMassInertia;


            return _FIx;
        }
        public double GetInertiaForceRotating_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _rotatingMassInertia = Conversions.GToKg(-this.connectingRod.RotatingMass_g) * Conversions.MmToM(-this.crankThrow.CrankRotationRadius_mm)
                 * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2)
                 * Math.Sin(Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg)));

            double _balancerInertia = Conversions.GToKg(-this.crankThrow.BalancerMass_g) * Conversions.MmToM(-this.crankThrow.BalancerRotationRadius_mm)
                * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2)
                * Math.Sin(Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg + this.crankThrow.BalancerAngle_deg)));

            double _FIy = _rotatingMassInertia + _balancerInertia;


            return _FIy;
        }
        public double GetInertiaForce_N(double _crankThrowRotation_deg, double _rpm)
        {
            return
                this.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm)
                + this.GetInertiaForceRotating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaForceAproximation_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            return
                this.GetInertiaForceReciprocatingAproximation_N(_crankThrowRotation_deg, _rpm, _orders)
                + this.GetInertiaForceRotating_N(_crankThrowRotation_deg, _rpm);
        }

        //navori
        public double GetGasPressureTorque_Nm(double _crankThrowRotation_deg, double _cylinderPressureAtCrankThrowRotation_Bar)
        {
            return
                this.GetGasPressureForce_N(_crankThrowRotation_deg, _cylinderPressureAtCrankThrowRotation_Bar)
                * Math.Tan(Conversions.DegToRad(this.GetConRodAngle_deg(_crankThrowRotation_deg)))
                * Conversions.MmToM(this.GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg));
        }
        public double GetGasPressureTorqueAproximation_Nm(double _crankThrowRotation_deg, double _cylinderPressureAtCrankThrowRotation_Bar, params uint[] _orders)
        {
            return
                this.GetGasPressureForce_N(_crankThrowRotation_deg, _cylinderPressureAtCrankThrowRotation_Bar)
                * Math.Tan(Conversions.DegToRad(this.GetConRodAngle_deg(_crankThrowRotation_deg)))
                * Conversions.MmToM(this.GetPistonTravelFromCrankCenterAproximation_mm(_crankThrowRotation_deg, _orders));
        }
        //torque due to inertia of reciprocating parts (rotating ne vplivajo, ker so samo radialne sile (pri stalni kotni hitrosti)!!!)
        public double GetInertiaTorque_Nm(double _crankThrowRotation_deg, double _rpm)
        {
            //vpliva samo ta masa!!! rotirajoče mase ne vplivajo (pri stalni kotni hitrosti)!
            double _reciprocatingMassInertia = Conversions.GToKg(this.connectingRod.ReciprocatingMass_g + this.piston.Mass_g) * this.GetPistonAcceleration_mps2(_crankThrowRotation_deg, _rpm);

            return
                _reciprocatingMassInertia
                * Math.Tan(Conversions.DegToRad(this.GetConRodAngle_deg(_crankThrowRotation_deg)))
                * Conversions.MmToM(this.GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg));
        }
        public double GetInertiaTorqueAproximation_Nm(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2))
                && (!_orders.Contains<uint>(3)))
            {
                throw new Exception();
            }


            double _crankRadius_m = Conversions.MmToM(this.crankThrow.CrankRotationRadius_mm);
            double _connectingRodLength_m = Conversions.MmToM(this.connectingRod.Length_mm);
            double _crankThrowRotation_rad = Conversions.DegToRad(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg));


            double _double =
                0.5d
                * Conversions.GToKg(this.connectingRod.ReciprocatingMass_g + this.piston.Mass_g) //vpliva samo ta masa!!! rotirajoče mase ne vplivajo (pri stalni kotni hitrosti)!
                * Math.Pow(_crankRadius_m, 2)
                * Math.Pow(EngineDesigner.Common.Conversions.RpmToDegps(_rpm), 2);


            double _result = 0d;
            if (_orders.Contains<uint>(1))
            {
                double _firstOrder = (_crankRadius_m / (2 * _connectingRodLength_m)) * Math.Sin(_crankThrowRotation_rad);
                _result += _double * _firstOrder;
            }
            if (_orders.Contains<uint>(2))
            {
                double _secondOrder = -Math.Sin(2 * _crankThrowRotation_rad);
                _result += _double * _secondOrder;
            }
            if (_orders.Contains<uint>(3))
            {
                double _thirdOrder = -(((3 * _crankRadius_m) / (2 * _connectingRodLength_m)) * Math.Sin(3 * _crankThrowRotation_rad));
                _result += _double * _thirdOrder;
            }


            return _result;
        }
        public double GetTotalTorque_Nm(double _crankThrowRotation_deg, double _cylinderPressureAtCrankThrowRotation_Bar, double _rpm)
        {
            return
                this.GetGasPressureTorque_Nm(_crankThrowRotation_deg, _cylinderPressureAtCrankThrowRotation_Bar)
                + this.GetInertiaTorque_Nm(_crankThrowRotation_deg, _rpm);
        }
        public double GetTotalTorqueAproximation_Nm(double _crankThrowRotation_deg, double _cylinderPressureAtCrankThrowRotation_Bar, double _rpm, params uint[] _orders)
        {
            return
                this.GetGasPressureTorqueAproximation_Nm(_crankThrowRotation_deg, _cylinderPressureAtCrankThrowRotation_Bar, _orders)
                + this.GetInertiaTorqueAproximation_Nm(_crankThrowRotation_deg, _rpm, _orders);
        }

        #endregion "Dinamika"








        /// <summary>
        /// Gets the cylinder's current stroke.
        /// </summary>
        /// <param name="_crankThrowRotation_deg">The crank throw rotation, in degrees.</param>
        public virtual Stroke GetStroke(double _crankThrowRotation_deg)
        {
            if (this.Cycle.RevolutionsToCompleteCycle > 0)
            {
                _crankThrowRotation_deg = EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_crankThrowRotation_deg, this.cycle.RevolutionsToCompleteCycle, true);

                foreach (Stroke _stroke in this.cycle.Strokes)
                {
                    if (_stroke.IsWithin(_crankThrowRotation_deg))
                    {
                        return _stroke;
                    }
                }

                throw new Exception("Failed to find stroke.");
            }
            else
            {
                return Stroke.NaN;
            }
        }





        //public static double GetPistonTravelFromTop_units(double _pistonTravelFromCrankCenter_units, double _totalHeight_units)
        //{
        //    return (_totalHeight_units - _pistonTravelFromCrankCenter_units);
        //}
        //public static double GetPistonTravelFromCrankCenter_units(double _pistionTravelFromTop_units, double _totalHeight_units)
        //{
        //    return (_totalHeight_units - _pistionTravelFromTop_units);
        //}


        //public double GetForceOnPiston_Kgmpdeg2(double _crankThrowRotation_deg)
        //{
        //    return this.GetPistonAcceleration_mpdeg2(_crankThrowRotation_deg) * Conversions.GToKg(this.piston.Mass_g);
        //}
        //public double GetForceOnPiston_N(double _crankThrowRotation_deg, double _rpm)
        //{
        //    return this.GetPistonAcceleration_mps2(_crankThrowRotation_deg, _rpm) * Conversions.GToKg(this.piston.Mass_g);
        //}
        //public double GetForceOnPiston_Kgmpdeg2(double _crankThrowRotation_deg, params uint[] _orders)
        //{
        //    return this.GetPistonAccelerationAproximation_mpdeg2(_crankThrowRotation_deg, _orders) * Conversions.GToKg(this.piston.Mass_g);
        //}
        //public double GetForceOnPiston_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    return this.GetPistonAccelerationAproximation_mps2(_crankThrowRotation_deg, _rpm, _orders) * Conversions.GToKg(this.piston.Mass_g);
        //}

        //public double GetGasTorque_Nm(double _crankThrowRotation_deg)
        //{
        //    double _conRodAngle_rad = Conversions.DegToRad(this.GetConRodAngle_deg(_crankThrowRotation_deg));
        //    double _y = Math.Sin(_conRodAngle_rad) * this.GetPistonTravelFromCrankCenter_mm(_crankThrowRotation_deg)/1000;
        //    double _compressionForce_N 
        //        = this.GetGasForce_N(_crankThrowRotation_deg)
        //        / Math.Cos(_conRodAngle_rad);

        //    return _y * _compressionForce_N;
        //}
        //public double GetGasTorque3_Nm(double _crankThrowRotation_deg)
        //{
        //    return
        //        (
        //        this.GetGasForce_N(_crankThrowRotation_deg)
        //        * this.CrankRadius_mm
        //        * Math.Sin(Conversions.DegToRad(_crankThrowRotation_deg))
        //        * (1 + ((this.CrankRadius_mm / this.connectingRod.Length_mm) * Math.Cos(Conversions.DegToRad(_crankThrowRotation_deg))))
        //        ) / 1000;
        //}
        //public double GetTorqueDueToInertiaOfMovingParts(double _crankThrowRotation_deg)
        //{
        //    double _pistonAcceleration = GetPistonAcceleration_mpdeg2(_crankThrowRotation_deg);
        //    double _pistonVelocity = GetPistonVelocity_mpdeg(_crankThrowRotation_deg);
        //    double _angularVelocity = Utility.GetAngularVelocity_degps(_rpm);

        //    return (-_pistonAcceleration * (_pistonVelocity / _angularVelocity));
        //}
        //public double GetTorqueDueToInertiaOfMovingParts(double _crankThrowRotation_deg, double _rpm)
        //{
        //    return (GetTorqueDueToInertiaOfMovingParts(_crankThrowRotation_deg) / Utility.GetAngularVelocity_degps(_rpm));
        //}
        //public double GetTorqueDueToInertiaOfMovingParts(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _pistonAcceleration = GetPistonAcceleration_mps2(_crankThrowRotation_deg, _rpm, _orders);
        //    double _pistonVelocity = GetPistonVelocity_mps(_crankThrowRotation_deg, _rpm, _orders);
        //    double _angularVelocity = Utility.GetAngularVelocity_degps(_rpm);

        //    return (-_pistonAcceleration * (_pistonVelocity / _angularVelocity));
        //}

        //public double GetTorqueDueToAngularAccelerationOfConnectingRod(double _crankThrowRotation_deg, double _rpm)
        //{
        //    double _conRodMass_Kg = Conversions.GToKg(connectingRod.Mass_g);
        //    //double _conRodInertia = connectingRod.RotatingMassDistanceFromCenterOfGravity_mm * connectingRod.ReciprocatingMassDistanceFromCenterOfGravity_mm * connectingRod.Mass_g;

        //    throw new NotImplementedException();
        //}
        //public double GetTorqueDueToAngularAccelerationOfConnectingRod(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _conRodMass_Kg = Conversions.GToKg(connectingRod.Mass_g);
        //    //double _conRodInertia = connectingRod.RotatingMassDistanceFromCenterOfGravity_mm * connectingRod.ReciprocatingMassDistanceFromCenterOfGravity_mm * connectingRod.Mass_g;

        //    throw new NotImplementedException();
        //}




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
                double _cylinderX = this.Bore_mm / 2d;


                double _crankThrow = this.crankThrow.CrankRotationRadius_mm
                    + (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
                if (_crankThrow > _cylinderX)
                {
                    _cylinderX = _crankThrow;
                }

                if ((this.crankThrow.BalancerMass_g > 0d)
                    && (this.crankThrow.BalancerRotationRadius_mm > 0d))
                {
                    double _crankBalancer =
                        (this.crankThrow.BalancerRotationRadius_mm / 2d)
                        + (EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2d);
                    if (_crankBalancer > _cylinderX)
                    {
                        _cylinderX = _crankBalancer;
                    }
                }


                return -_cylinderX;
            }
        }

        [Browsable(false)]
        public double Bound_Y_Min
        {
            get
            {
                double _cylinderY = this.crankThrow.CrankRotationRadius_mm
                    + (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);

                if ((this.crankThrow.BalancerMass_g > 0d)
                    && (this.crankThrow.BalancerRotationRadius_mm > 0d))
                {
                    double _crankBalancer =
                        (this.crankThrow.BalancerRotationRadius_mm / 2d)
                        + (EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2d);
                    if (_crankBalancer > _cylinderY)
                    {
                        _cylinderY = _crankBalancer;
                    }
                }


                return -_cylinderY;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Min
        {
            get
            {
                double _cylinderZ = this.Bore_mm / 2d;

                double _double =
                    (this.crankThrow.CrankPinWidth_mm / 2d)
                    + EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm;
                if (_double > _cylinderZ)
                {
                    _cylinderZ = _double;
                }

                return -_cylinderZ;
            }
        }

        [Browsable(false)]
        public double Bound_X_Max
        {
            get
            {
                double _cylinderX = this.Bore_mm / 2d;


                double _crankThrow = this.crankThrow.CrankRotationRadius_mm
                    + (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
                if (_crankThrow > _cylinderX)
                {
                    _cylinderX = _crankThrow;
                }

                if ((this.crankThrow.BalancerMass_g > 0d)
                    && (this.crankThrow.BalancerRotationRadius_mm > 0d))
                {
                    double _crankBalancer =
                        (this.crankThrow.BalancerRotationRadius_mm / 2d)
                        + (EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2d);
                    if (_crankBalancer > _cylinderX)
                    {
                        _cylinderX = _crankBalancer;
                    }
                }


                return _cylinderX;
            }
        }

        [Browsable(false)]
        public double Bound_Y_Max
        {
            get
            {
                double _cylinderY = this.GetPhysicalHeightAbovePiston_mm(0);

                return _cylinderY;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Max
        {
            get
            {
                double _cylinderZ = this.Bore_mm / 2d;

                double _double =
                    (this.crankThrow.CrankPinWidth_mm / 2d)
                    + EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm;
                if (_double > _cylinderZ)
                {
                    _cylinderZ = _double;
                }

                return _cylinderZ;
            }
        }


        public void Validate()
        {
            //this.OnValidated(); return;
            #region "RLRatio mora biti manjši od 1"
            if (this.RLRatio > 1d)
            {
                throw new ValidationException("RLRatio cannot be less than 1.");
            }
            #endregion "RLRatio mora biti manjši od 1"

            #region "pod batom mora ostat vsaj za ročično gred"
            if (this.GetPhysicalHeightUnderPiston_mm(Stroke.StrokeDuration_deg) < (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2))
            {
                throw new ValidationException("Piston skirt intersects with crankshaft.");
            }
            #endregion "pod batom mora ostat vsaj za ročično gred"

            #region "utež od ročice se ne sme zaletavat u bat"
            {
                //dobimo funkcijo gibanja "pod batom"
                Function _physicalHeightUnderPiston_mm = Function.Compute(
                    0d, 360d, VALIDATION_INTERSECTION_ACCURACY_deg,
                    delegate(double _x) { return this.GetPhysicalHeightUnderPiston_mm(_x); });

                //in funkcijo gibanja uteži
                Function _crankThrowBalancerRotationY_mm = Function.Compute(
                    0d, 360d, VALIDATION_INTERSECTION_ACCURACY_deg,
                    delegate(double _x) { return this.crankThrow.BalancerRotationRadius_mm * Math.Cos(Conversions.DegToRad(_x + this.crankThrow.BalancerAngle_deg)); });

                if (_crankThrowBalancerRotationY_mm.HasBiggerMagnitudeY(_physicalHeightUnderPiston_mm))
                {
                    //če se prekrivata, pomeni, da se bo utež zaletela v bat
                    throw new ValidationException("The crank throw balancer intersects with piston.");
                }
            }
            #endregion "utež od ročice se ne sme zaletavat u bat"

            #region "ojnica ne sme sekat cilindra"
            {
                //računa samo za center ojnice; širine ne upošteva!!!

                Function _pistonDiameter_mm = Function.Compute(
                    0d, 360d, VALIDATION_INTERSECTION_ACCURACY_deg,
                    delegate(double _x)
                    {
                        return this.Piston.Diameter_mm / 2;
                    });

                Function _conrodIntersection_mm = Function.Compute(
                    0d, 360d, VALIDATION_INTERSECTION_ACCURACY_deg,
                    delegate(double _x)
                    {
                        double _conRodAngle_rad = Conversions.DegToRad(this.GetConRodAngle_deg(_x));
                        //dolžina ojnice na tej velikosti cilindra (hipotenuza)
                        double _cylinderLowerPortion_mm = this.GetPistonTravelFromCrankCenter_mm(_x) - this.GetPhysicalHeightUnderPiston_mm(180d);
                        double _conRodLength_mm = _cylinderLowerPortion_mm / Math.Cos(_conRodAngle_rad);
                        //odmik od navpične premice (centra cilindra); to potem primerjamo s polmerom cilindra
                        double _double = Math.Sin(_conRodAngle_rad) * _conRodLength_mm;
                        //samo pozitivna stran!
                        return Math.Abs(_double);
                    });


                if (_pistonDiameter_mm.ToPolygon().IntersectsWith(_conrodIntersection_mm.ToPolygon()))
                {
                    throw new ValidationException("The connecting rod intersects with cylinder wall.");
                }
            }
            #endregion "ojnica ne sme sekat cilindra"



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
        private void IPart_Validated(IPart _iPart)
        {
            this.Validate();
        }
    }


    /// <summary>
    /// Provides UI integration for Cylinder class.
    /// </summary>
    internal class CylinderConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Cylinder))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Cylinder))
            {
                Cylinder _cylinder = (Cylinder)value;
                return _cylinder.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
