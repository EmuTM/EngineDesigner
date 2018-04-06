using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Common.CustomCollections;
using EngineDesigner.Machine.Definitions;
using EngineDesigner.Common;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents a cylinder unit positioned within the engine.
    /// </summary>
    [TypeConverter(typeof(PositionedCylinderConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class PositionedCylinder : Cylinder, IPart, ICustomCollectionElement
    {
        public PositionedCylinder()
            : this(DefaultPositionedCylinder, DefaultPositionedCylinder.Position, DefaultPositionedCylinder.Offset_mm, DefaultPositionedCylinder.Tilt_deg, DefaultPositionedCylinder.FiringAngle_deg)
        {
        }
        /// <param name="_cylinder">The cylinder to be positioned by given parameters.</param>
        /// <param name="_position">The cylinder's numerical position within the engine.</param>
        /// <param name="_offset_mm">The cylinder's phisical position within the engine (measured from engine's front plane), in milimeters.</param>
        /// <param name="_tilt_deg">The cylinder's tilt angle within the engine (measured from engine's vertical line, clockwise), in degrees.</param>
        /// <param name="_firingAngle_deg">The cylinder's firing angle within the engine, in degrees.</param>
        public PositionedCylinder(Cylinder _cylinder, int _position, double _offset_mm, double _tilt_deg, double _firingAngle_deg)
            : this(false, _cylinder, _position, _offset_mm, _tilt_deg, _firingAngle_deg)
        {
        }
        private PositionedCylinder(bool _skipValidation, Cylinder _cylinder, int _position, double _offset_mm, double _tilt_deg, double _firingAngle_deg)
            : base(_cylinder.Cycle, _cylinder.Piston, _cylinder.ConnectingRod, _cylinder.CrankThrow)
        {
            this.position = _position;
            this.tilt_deg = EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_tilt_deg);
            this.offset_mm = _offset_mm;
            this.firingAngle_deg = _firingAngle_deg;


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
            if (obj is PositionedCylinder)
            {
                PositionedCylinder _positionedCylinder = (PositionedCylinder)obj;

                if (this.guid.ToString() == _positionedCylinder.guid.ToString())
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
                "Cylinder [{0}]; Firing @ {1}°; {2}",
                this.position.ToString(),
                firingAngle_deg.ToString(),
                Cycle.CycleId);
        }
        #region ICustomCollectionElement Members
        [Browsable(false)]
        public string DisplayName
        {
            get
            {
                return string.Format(
                    "Cylinder: {0}",
                    position.ToString());
            }
        }
        [Browsable(false)]
        public string DisplayDescription
        {
            get
            {
                return "Defines the parameters for the cylinder.";
            }
        }
        #endregion



        #region "System-defined positionedCylinders"
        /// <summary>
        /// Gets the default positionedCylinder (for UI integration).
        /// </summary>
        public static PositionedCylinder DefaultPositionedCylinder
        {
            get
            {
                return new PositionedCylinder(
                    true,
                    Cylinder.DefaultCylinder,
                    EngineDesigner.Machine.Properties.Settings.Default.PositionedCylinderDefaultPosition,
                    EngineDesigner.Machine.Properties.Settings.Default.PositionedCylinderDefaultOffset_mm,
                    EngineDesigner.Machine.Properties.Settings.Default.PositionedCylinderDefaultTilt_deg,
                    EngineDesigner.Machine.Properties.Settings.Default.PositionedCylinderDefaultFiringAngle_deg);
            }
        }
        #endregion "System-defined positionedCylinders"



        [DataMember]
        private int position;
        [DataMember]
        private double tilt_deg;
        [DataMember]
        private double offset_mm;
        [DataMember]
        private double firingAngle_deg;



        #region "Public properties"
        /// <summary>
        /// Defines the cylinder's numerical position within the engine.
        /// </summary>
        [DisplayName("Position")]
        [Description("Defines the cylinder's numerical position within the engine.")]
        [ReadOnly(true)]
        public int Position
        {
            get { return this.position; }

            set
            {
                this.position = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the cylinder's tilt angle within the engine (measured from engine's vertical line, clockwise), in degrees.
        /// </summary>
        [DisplayName("Tilt")]
        [Description("Defines the cylinder's tilt angle within the engine (measured from engine's vertical line, clockwise), in degrees.")]
        public double Tilt_deg
        {
            get { return tilt_deg; }

            set
            {
                this.tilt_deg = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the cylinder's center phisical position within the engine (measured from engine's front plane), in milimeters.
        /// </summary>
        [DisplayName("Offset")]
        [Description("Defines the cylinder's center phisical position within the engine (measured from engine's front plane), in milimeters.")]
        public double Offset_mm
        {
            get { return offset_mm; }

            set
            {
                this.offset_mm = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the cylinder's firing angle within the engine, in (crankshaft!) degrees.
        /// </summary>
        [DisplayName("Firing angle")]
        [Description("Defines the cylinder's firing angle within the engine, in (crankshaft!) degrees.")]
        public double FiringAngle_deg
        {
            get { return firingAngle_deg; }

            set
            {
                this.firingAngle_deg = value;
                this.Validate();
            }
        }
        #endregion "Public properties"



        //vrne kot ročice tega cilindra, glede na kot ročične gredi, upoštevajoč kot vžiga (glede na cikel), ne pa tudi tilta
        //uporabljamo, ko računamo za objekt, ki že stoji pod tiltom (cilinder, bat, ojnica); torej za nas v tistem trenutku stoji navpično pokončno, 0°
        public double GetCylinderRelativeCrankThrowRotation_deg(double _crankshaftRotation_deg)
        {
            if (!double.IsNaN(base.Cycle.DefaultFiringAngle)) //v primeru, da imam nedefiniran cikel, ne upoštevamo kota vžiga
            {
                return
                    +_crankshaftRotation_deg
                    - firingAngle_deg
                    + Cycle.DefaultFiringAngle; //default kot vžiga za cikel
            }
            else
            {
                return
                    +_crankshaftRotation_deg
                    - firingAngle_deg;
            }
        }
        public double GetCylinderRelativeCrankshaftRotation_deg(double _cylinderRelativeCrankThrowRotation_deg)
        {
            if (!double.IsNaN(base.Cycle.DefaultFiringAngle)) //v primeru, da imam nedefiniran cikel, ne upoštevamo kota vžiga
            {
                return _cylinderRelativeCrankThrowRotation_deg
                    + firingAngle_deg
                    - Cycle.DefaultFiringAngle; //default kot vžiga za cikel
            }
            else
            {
                return _cylinderRelativeCrankThrowRotation_deg
                    + firingAngle_deg;
            }
        }


        //vrne absolutni kot ročice
        public double GetAbsoluteCrankThrowRotation_deg(double _crankshaftRotation_deg)
        {
            if (!double.IsNaN(base.Cycle.DefaultFiringAngle)) //v primeru, da imam nedefiniran cikel, ne upoštevamo kota vžiga
            {
                return
                    +_crankshaftRotation_deg
                    - firingAngle_deg
                    + this.tilt_deg
                    + Cycle.DefaultFiringAngle; //default kot vžiga za cikel
            }
            else
            {
                return
                    +_crankshaftRotation_deg
                    - firingAngle_deg
                    + this.tilt_deg;
            }
        }
        public double GetAbsoluteCrankshaftRotation_deg(double _absoluteCrankThrowRotation_deg)
        {
            if (!double.IsNaN(base.Cycle.DefaultFiringAngle)) //v primeru, da imam nedefiniran cikel, ne upoštevamo kota vžiga
            {
                return _absoluteCrankThrowRotation_deg
                    + firingAngle_deg
                    - this.tilt_deg
                    - Cycle.DefaultFiringAngle; //default kot vžiga za cikel
            }
            else
            {
                return _absoluteCrankThrowRotation_deg
                    + firingAngle_deg
                    - this.tilt_deg;
            }
        }


        //public double GetForceOnPistonAboutXaxis_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Math.Sin(Conversions.DegToRad(verticalOffset_deg)) * GetForceOnPiston_N(_offsetIndipendentCrankThrowRotation_deg, _rpm));
        //}
        //public double GetForceOnPistonAboutXaxis_N(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Math.Sin(Conversions.DegToRad(verticalOffset_deg)) * GetForceOnPiston_N(_offsetIndipendentCrankThrowRotation_deg, _rpm, _orders));
        //}
        //public double GetForceOnPistonAboutYaxis_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Math.Cos(Conversions.DegToRad(verticalOffset_deg)) * GetForceOnPiston_N(_offsetIndipendentCrankThrowRotation_deg, _rpm));
        //}
        //public double GetForceOnPistonAboutYaxis_N(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Math.Cos(Conversions.DegToRad(verticalOffset_deg)) * GetForceOnPiston_N(_offsetIndipendentCrankThrowRotation_deg, _rpm, _orders));
        //}
        //public double GetMomentOnPistonAboutXaxis_Nm(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Conversions.MmToM(offset_mm) * GetForceOnPistonAboutXaxis_N(_offsetIndipendentCrankThrowRotation_deg, _rpm));
        //}
        //public double GetMomentOnPistonAboutXaxis_Nm(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Conversions.MmToM(offset_mm) * GetForceOnPistonAboutXaxis_N(_offsetIndipendentCrankThrowRotation_deg, _rpm, _orders));
        //}
        //public double GetMomentOnPistonAboutYaxis_Nm(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Conversions.MmToM(offset_mm) * GetForceOnPistonAboutYaxis_N(_offsetIndipendentCrankThrowRotation_deg, _rpm));
        //}
        //public double GetMomentOnPistonAboutYaxis_Nm(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _offsetIndipendentCrankThrowRotation_deg = GetTiltIndipendentCrankThrowRotation_deg(_crankshaftRotation_deg);
        //    return (Conversions.MmToM(offset_mm) * GetForceOnPistonAboutYaxis_N(_offsetIndipendentCrankThrowRotation_deg, _rpm, _orders));
        //}



        public override Stroke GetStroke(double _crankshaftRotation_deg)
        {
            if (base.Cycle.RevolutionsToCompleteCycle > 0)
            {
                return base.GetStroke(
                    EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(
                        this.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg),
                        base.Cycle.RevolutionsToCompleteCycle, true));
            }
            else
            {
                return Stroke.NaN;
            }
        }
        //public double GetStrokeBegin(Stroke _stroke)
        //{
        //    if (base.Cycle.RevolutionsToCompleteCycle > 0)
        //    {
        //        return Utility.GetAbsoluteAngle_deg(
        //            _stroke.Begin_deg + this.verticalOffset_deg,
        //            base.Cycle.RevolutionsToCompleteCycle, true);
        //    }
        //    else
        //    {
        //        return double.NaN;
        //    }
        //}
        //TODO: to bo moralo ratat virtual v Cylinder
        public double GetElapsedStroke(Stroke _stroke, double _crankshaftRotation_deg)
        {
            if (base.Cycle.RevolutionsToCompleteCycle > 0)
            {
                //izračunamo koliko takta je že preteklo
                return EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(
                    this.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg) - _stroke.Begin_deg,
                    base.Cycle.RevolutionsToCompleteCycle, true);
            }
            else
            {
                return double.NaN;
            }
        }



        #region "Cilinder views - Poligonski prikaz za računanje intersekcije"
        public bool IsIntersectingWith(PositionedCylinder _positionedCylinder, out Exception _exception)
        {
            _exception = null;


            Polygon _polygonToCheckFront = _positionedCylinder.GetCylinderFrontView();
            Polygon _polygonToCheckSide = _positionedCylinder.GetCylinderSideView();
            Polygon _polygonThisFront = this.GetCylinderFrontView();
            Polygon _polygonThisSide = this.GetCylinderSideView();

            if ((_polygonToCheckFront.IntersectsWith(_polygonThisFront))
                && (_polygonToCheckSide.IntersectsWith(_polygonThisSide)))
            {
                _exception = new ValidationException("Two or more cylinders intersect physically.");
                return true;
            }

            if (_positionedCylinder.Offset_mm < this.Offset_mm + _positionedCylinder.CrankThrow.CrankPinWidth_mm)
            {
                double _double1 = Mathematics.GetAbsoluteAngle_deg(_positionedCylinder.GetAbsoluteCrankThrowRotation_deg(0), true);
                double _double2 = Mathematics.GetAbsoluteAngle_deg(this.GetAbsoluteCrankThrowRotation_deg(0), true);

                //če imata ročici isti kot, potem gre za t.a. "articulated" connecting rod
                if (_double1 != _double2)
                {
                    _exception = new ValidationException("Two or more connecting rods intersect physically.");
                    return true;
                }
            }


            return false;
        }


        private const double PRECISION = 10d;


        //samo grafična rutina
        private Polygon GetCylinderFrontView()
        {
            double _physicalHeightAbovePiston_mm = GetPhysicalHeightAbovePiston_mm(0);
            double _physicalHeightUnderPiston_mm = GetPhysicalHeightUnderPiston_mm(Stroke.StrokeDuration_deg);

            double _cylinderHalfBore = Bore_mm / 2;

            double _offset_rad = Conversions.DegToRad(tilt_deg);
            double _sinOffset = Math.Sin(_offset_rad);
            double _cosOffset = Math.Cos(_offset_rad);


            //zgornja center točka bata
            double _xTDC = (_sinOffset * _physicalHeightAbovePiston_mm);
            double _yTDC = (_cosOffset * _physicalHeightAbovePiston_mm);

            //spodnja center točka bata
            double _xBDC = (_sinOffset * _physicalHeightUnderPiston_mm);
            double _yBDC = (_cosOffset * _physicalHeightUnderPiston_mm);

            //razlike od center do skrajnih točk
            double _x_ = (_cosOffset * _cylinderHalfBore);
            double _y_ = (_sinOffset * _cylinderHalfBore);

            //zgornje skrajne točke
            double _x1 = _xTDC - _x_;
            double _y1 = _yTDC + _y_;
            double _x2 = _xTDC + _x_;
            double _y2 = _yTDC - _y_;

            //spodnje skrajne točke
            double _x3 = _xBDC - _x_;
            double _y3 = _yBDC + _y_;
            double _x4 = _xBDC + _x_;
            double _y4 = _yBDC - _y_;


            return Polygon.Rectangle(_x1, _y1, _x2, _y2, _x3, _y3, _x4, _y4);
        }
        private Polygon GetCylinderSideView()
        {
            double _offset_rad = Conversions.DegToRad(this.tilt_deg);
            double _cosOffset = Math.Cos(_offset_rad);


            double _TDC = (_cosOffset * GetPhysicalHeightAbovePiston_mm(0));
            double _BDC = (_cosOffset * GetPhysicalHeightUnderPiston_mm(Stroke.StrokeDuration_deg));

            double _rX = Bore_mm / 2;
            double _rY = (Math.Abs(Math.Sin(_offset_rad) * _rX));

            double _rTilt = _rX * Math.Sin(Conversions.DegToRad(this.tilt_deg));

            Polygon _polygon = Polygon.Line(
                -_rX + offset_mm,
                _BDC,
                -_rX + offset_mm,
                _TDC);

            _polygon.Add(Polygon.Arc(offset_mm, _TDC, _rX, _rTilt, 0, 540, PRECISION));

            _polygon.Add(Polygon.Line(
                 _rX + offset_mm,
                 _TDC,
                 _rX + offset_mm,
                 _BDC));

            _polygon.Add(Polygon.Arc(offset_mm, _BDC, _rX, _rTilt, 180, 540, PRECISION));


            return _polygon;
        }
        #endregion "Cilinder views - Poligonski prikaz za računanje intersekcije"




        public bool IsMatedWith(PositionedCylinder _positionedCylinder)
        {
            if (_positionedCylinder.offset_mm == this.offset_mm)
            {
                return true;
            }
            else if (_positionedCylinder.offset_mm > this.offset_mm)
            {
                //če je njegov offset največ ta offset + ta pin width
                if (_positionedCylinder.Offset_mm <= this.offset_mm + this.CrankThrow.CrankPinWidth_mm)
                {
                    return true;
                }
            }
            else //(_positionedCylinder.offset_mm < this.offset_mm)
            {
                //če je njegov offset največ ta offset - ta pin width
                if (_positionedCylinder.Offset_mm >= this.offset_mm - this.CrankThrow.CrankPinWidth_mm)
                {
                    return true;
                }
            }


            return false;
        }




        //zavisi od tilta
        #region "Dinamika"
        public double GetGasPressureForceX_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _sin
                * base.GetGasPressureForce_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetGasPressureForceY_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _cos
                * base.GetGasPressureForce_N(_crankThrowRotation_deg, _rpm);
        }

        public double GetInertiaForceReciprocatingX_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _sin
                * base.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaForceReciprocatingAproximationX_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _sin
                * base.GetInertiaForceReciprocatingAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }
        public double GetInertiaForceReciprocatingY_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _cos
                * base.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaForceReciprocatingAproximationY_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _cos
                * base.GetInertiaForceReciprocatingAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }

        public double GetInertiaForceX_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _sin
                * base.GetInertiaForce_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaForceAproximationX_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _sin
                * base.GetInertiaForceAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }
        public double GetInertiaForceY_N(double _crankThrowRotation_deg, double _rpm)
        {
            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _cos
                * base.GetInertiaForce_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaForceAproximationY_N(double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));

            return
                _cos
                * base.GetInertiaForceAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }

        public double GetInertiaMomentRotating_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _lever_mm
                * base.GetInertiaForceRotating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentReciprocating_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _lever_mm
                * base.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentReciprocatingAproximation_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _lever_mm
                * base.GetInertiaForceReciprocatingAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }
        public double GetInertiaMomentReciprocatingX_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _sin
                * _lever_mm
                * base.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentReciprocatingAproximationX_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _sin
                * _lever_mm
                * base.GetInertiaForceReciprocatingAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }
        public double GetInertiaMomentReciprocatingY_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _cos
                * _lever_mm
                * base.GetInertiaForceReciprocating_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentReciprocatingAproximationY_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _cos
                * _lever_mm
                * base.GetInertiaForceReciprocatingAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }

        public double GetInertiaMoment_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _lever_mm
                * base.GetInertiaForce_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentAproximation_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _lever_mm
                * base.GetInertiaForceAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }
        public double GetInertiaMomentX_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _sin
                * _lever_mm
                * base.GetInertiaForce_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentAproximationX_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _sin = Math.Sin(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _sin
                * _lever_mm
                * base.GetInertiaForceAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }
        public double GetInertiaMomentY_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm)
        {
            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _cos
                * _lever_mm
                * base.GetInertiaForce_N(_crankThrowRotation_deg, _rpm);
        }
        public double GetInertiaMomentAproximationY_N(double _totalEngineLength_mm, double _crankThrowRotation_deg, double _rpm, params uint[] _orders)
        {
            if ((!_orders.Contains<uint>(1))
                && (!_orders.Contains<uint>(2)))
            {
                throw new Exception();
            }


            double _cos = Math.Cos(EngineDesigner.Common.Conversions.DegToRad(this.tilt_deg));
            double _lever_mm = (((_totalEngineLength_mm / 2d) - this.offset_mm) - (base.Piston.Diameter_mm / 2d));

            return
                _cos
                * _lever_mm
                * base.GetInertiaForceAproximation_N(_crankThrowRotation_deg, _rpm, _orders);
        }

        #endregion "Dinamika"



        #region IPart Members
        [DataMember]
        private Guid guid = Guid.NewGuid();
        [Browsable(false)]
        public new Guid Guid
        {
            get { return guid; }
        }

        [Browsable(false)]
        public new double Length
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
                    //NOTE: to je po novem, vendar ne vem, če je uredu za vse IParte, ma očitno samo za tega, ker ostali vsi začnejo z 0 ali pa v negativi
                    return Math.Abs(_maxLength) - Math.Abs(_minLength);
                }
            }
        }

        [Browsable(false)]
        public new double Width
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
        public new double Height
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
        public new double Bound_X_Min
        {
            get
            {
                double _minX = 0; //več kot 0, ne more bit, ker motor začne v centru, torej 0

                double tilt_rad = Conversions.DegToRad(this.tilt_deg);
                double _cylinderX = Math.Sin(tilt_rad) * this.GetPhysicalHeightAbovePiston_mm(0d);
                if (_cylinderX <= 0)
                {
                    //koliko vrtina pripomore k širini
                    _cylinderX -= Math.Abs(Math.Cos(tilt_rad) * (this.Bore_mm / 2));
                }

                //če je _double od cilindra pozitiven ali pa ni "dovolj negativen", potem ročica vpliva na širino motojra
                if ((_cylinderX > 0)
                    || ((Math.Abs(_cylinderX) < this.CrankThrow.CrankRotationRadius_mm)))
                {
                    _cylinderX = -this.CrankThrow.CrankRotationRadius_mm;
                }


                if (_cylinderX < _minX)
                {
                    _minX = _cylinderX;
                }

                return _minX;
            }
        }

        [Browsable(false)]
        public new double Bound_Y_Min
        {
            get
            {
                double _minY = 0; //več kot 0, ne more bit, ker motor začne v centru, torej 0

                //pozicija cilindra
                double _tilt_rad = Conversions.DegToRad(this.tilt_deg);
                double _cylinderY = Math.Cos(_tilt_rad) * this.GetPhysicalHeightAbovePiston_mm(0);
                if (_cylinderY <= 0)
                {
                    //koliko vrtina pripomore k višini
                    _cylinderY -= Math.Abs(Math.Sin(_tilt_rad) * (this.Bore_mm / 2));
                }

                //če je y od cilindra pozitiven ali pa ni "dovolj negativen", potem ročica vpliva na višino motojra
                if ((_cylinderY > 0)
                    || ((Math.Abs(_cylinderY) < this.CrankThrow.CrankRotationRadius_mm)))
                {
                    _cylinderY = -this.CrankThrow.CrankRotationRadius_mm;
                }


                if (_cylinderY < _minY)
                {
                    _minY = _cylinderY;
                }

                return _minY;
            }
        }

        [Browsable(false)]
        public new double Bound_Z_Min
        {
            get
            {
                double _minZ = double.MaxValue;

                double _totalMinZ =
                    this.Offset_mm
                    - this.Bore_mm / 2;

                if (_totalMinZ < _minZ)
                {
                    _minZ = _totalMinZ;
                }

                return _minZ;
            }
        }

        [Browsable(false)]
        public new double Bound_X_Max
        {
            get
            {
                double _maxX = 0; //manj kot 0, ne more bit, ker motor začne v centru, torej 0

                double _tilt_rad = Conversions.DegToRad(this.tilt_deg);
                double _cylinderX = Math.Sin(_tilt_rad) * this.GetPhysicalHeightAbovePiston_mm(0d);
                if (_cylinderX >= 0)
                {
                    //koliko vrtina pripomore k širini
                    _cylinderX += Math.Abs(Math.Cos(_tilt_rad) * (this.Bore_mm / 2));
                }

                //če je y od cilindra negativen ali pa ni "dovolj pozitiven", potem ročica vpliva na širino motojra
                if ((_cylinderX < 0)
                    || ((Math.Abs(_cylinderX) < this.CrankThrow.CrankRotationRadius_mm)))
                {
                    _cylinderX = this.CrankThrow.CrankRotationRadius_mm;
                }


                if (_cylinderX > _maxX)
                {
                    _maxX = _cylinderX;
                }

                return _maxX;
            }
        }

        [Browsable(false)]
        public new double Bound_Y_Max
        {
            get
            {
                double _maxY = 0; //manj kot 0, ne more bit, ker motor začne v centru, torej 0

                double _tilt_rad = Conversions.DegToRad(this.tilt_deg);
                double _cylinderY = Math.Cos(_tilt_rad) * this.GetPhysicalHeightAbovePiston_mm(0d);
                if (_cylinderY >= 0)
                {
                    //koliko vrtina pripomore k višini
                    _cylinderY += Math.Abs(Math.Sin(_tilt_rad) * (this.Bore_mm / 2));
                }

                //če je y od cilindra negativen ali pa ni "dovolj pozitiven", potem ročica vpliva na višino motojra
                if ((_cylinderY < 0)
                    || ((Math.Abs(_cylinderY) < this.CrankThrow.CrankRotationRadius_mm)))
                {
                    _cylinderY = this.CrankThrow.CrankRotationRadius_mm;
                }


                if (_cylinderY > _maxY)
                {
                    _maxY = _cylinderY;
                }

                return _maxY;
            }
        }

        [Browsable(false)]
        public new double Bound_Z_Max
        {
            get
            {
                double _maxZ = double.MinValue;

                double _totalMaxZ =
                    this.Offset_mm
                    + this.Bore_mm / 2;

                if (_totalMaxZ > _maxZ)
                {
                    _maxZ = _totalMaxZ;
                }

                return _maxZ;
            }
        }


        public new void Validate()
        {
            base.Validate();


            if (this.position < 1)
            {
                throw new ValidationException("this.position < 1");
            }

            if (this.offset_mm < 0d)
            {
                throw new ValidationException("this.offset_mm < 0d");
            }
        }
        #endregion

    }


    /// <summary>
    /// Provides UI integration for PositionedCylinder class.
    /// </summary>
    internal class PositionedCylinderConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(PositionedCylinder))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is PositionedCylinder))
            {
                PositionedCylinder _positionedCylinder = (PositionedCylinder)value;
                return _positionedCylinder.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
