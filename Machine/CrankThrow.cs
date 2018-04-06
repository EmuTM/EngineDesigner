using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;

using EngineDesigner.Machine.Definitions;
using EngineDesigner.Common;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents a crank throw unit for the cylinder.
    /// </summary>
    [TypeConverter(typeof(CrankThrowConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class CrankThrow : IPart
    {
        public CrankThrow()
            : this(DefaultCrankThrow.crankRotationRadius_mm, DefaultCrankThrow.balancerMass_g, DefaultCrankThrow.balancerRotationRadius_mm, DefaultCrankThrow.balancerAngle_deg, DefaultCrankThrow.crankPinWidth_mm)
        {

        }
        /// <param name="_crankRotationRadius_mm">The crank throw's rotation radius (resulting in cylinder's stroke), in milimeters.</param>
        /// <param name="double _balancerMass_g">The mass of the crank balancer, in grams.</param>
        /// <param name="double _balancerRotationRadius_mm">The rotation radius of the crank balancer, in milimeters.</param>
        /// <param name="double _balancerAngle_deg">The angle of the balancer with respect to the crank throw, in degrees.</param>
        /// <param name="double _crankPinWidth_mm">The width of the crank pin, in milimeters.</param>
        public CrankThrow(double _crankRotationRadius_mm, double _balancerMass_g, double _balancerRotationRadius_mm, double _balancerAngle_deg, double _crankPinWidth_mm)
            : this(false, _crankRotationRadius_mm, _balancerMass_g, _balancerRotationRadius_mm, _balancerAngle_deg, _crankPinWidth_mm)
        {
        }
        private CrankThrow(bool _skipValidation, double _crankRotationRadius_mm, double _balancerMass_g, double _balancerRotationRadius_mm, double _balancerAngle_deg, double _crankPinWidth_mm)
        {
            this.crankRotationRadius_mm = _crankRotationRadius_mm;
            this.balancerMass_g = _balancerMass_g;
            this.balancerRotationRadius_mm = _balancerRotationRadius_mm;
            this.balancerAngle_deg = _balancerAngle_deg;
            this.crankPinWidth_mm = _crankPinWidth_mm;


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
            if (obj is CrankThrow)
            {
                CrankThrow _crankThrow = (CrankThrow)obj;

                if (this.guid.ToString() == _crankThrow.guid.ToString())
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
                "{0}mm",
                this.crankRotationRadius_mm.ToString());
        }



        #region "System-defined crank throws"
        /// <summary>
        /// Gets the default crank throw (for UI integration).
        /// </summary>
        public static CrankThrow DefaultCrankThrow
        {
            get
            {
                double _crankPinWidth_mm = EngineDesigner.Machine.Properties.Settings.Default.DefaultCrankThrowCrankRadius_mm
                    * EngineDesigner.Machine.Properties.Settings.Default.CrankThrowCrankPinWidthVsCrankRadius;

                return new CrankThrow(
                    true,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultCrankThrowCrankRadius_mm,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultCrankThrowBalancerMass_g,
                    (EngineDesigner.Machine.Properties.Settings.Default.DefaultCrankThrowCrankRadius_mm
                        * EngineDesigner.Machine.Properties.Settings.Default.CrankThrowBalancerRadiusVsCrankThrowRadius),
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultCrankThrowBalancerAngle_deg,
                    _crankPinWidth_mm);
            }
        }

        /// <summary>
        /// Gets a simple crank throw based on given parameters.
        /// </summary>
        /// <param name="_crankRotationRadius_mm">The crank throw's rotation radius (resulting in cylinder's stroke), in milimeters.</param>
        public static CrankThrow FromParameters(double _crankRotationRadius_mm)
        {
            return CrankThrow.FromParameters(
                _crankRotationRadius_mm,
                0d,
                0d);
        }
        /// <summary>
        /// Gets a simple crank throw based on given parameters.
        /// </summary>
        /// <param name="_crankRotationRadius_mm">The crank throw's rotation radius (resulting in cylinder's stroke), in milimeters.</param>
        /// <param name="double _balancerMass_g">The mass of the crank balancer, in grams.</param>
        /// <param name="double _balancerRotationRadius_mm">The rotation radius of the crank balancer, in milimeters.</param>
        public static CrankThrow FromParameters(double _crankRotationRadius_mm, double _balancerMass_g, double _balancerRotationRadius_mm)
        {
            return new CrankThrow(
                _crankRotationRadius_mm,
                _balancerMass_g,
                _balancerRotationRadius_mm,
                EngineDesigner.Machine.Properties.Settings.Default.DefaultCrankThrowBalancerAngle_deg,
                _crankRotationRadius_mm * EngineDesigner.Machine.Properties.Settings.Default.CrankThrowCrankPinWidthVsCrankRadius);
        }
        /// <summary>
        /// Gets a simple crank throw based on given parameters.
        /// </summary>
        /// <param name="_crankRotationRadius_mm">The crank throw's rotation radius (resulting in cylinder's stroke), in milimeters.</param>
        /// <param name="double _balancerMass_g">The mass of the crank balancer, in grams.</param>
        /// <param name="double _balancerRotationRadius_mm">The rotation radius of the crank balancer, in milimeters.</param>
        /// <param name="double _balancerAngle_deg">The angle of the balancer with respect to the crank throw, in degrees.</param>
        public static CrankThrow FromParameters(double _crankRotationRadius_mm, double _balancerMass_g, double _balancerRotationRadius_mm, double _balancerAngle_deg)
        {
            return new CrankThrow(
                _crankRotationRadius_mm,
                _balancerMass_g,
                _balancerRotationRadius_mm,
                _balancerAngle_deg,
                _crankRotationRadius_mm * EngineDesigner.Machine.Properties.Settings.Default.CrankThrowCrankPinWidthVsCrankRadius);
        }
        #endregion "System-defined crank throws"



        [DataMember]
        private double crankRotationRadius_mm;
        [DataMember]
        private double balancerMass_g;
        [DataMember]
        private double balancerRotationRadius_mm;
        [DataMember]
        private double balancerAngle_deg;
        [DataMember]
        private double crankPinWidth_mm;



        #region "Public properties"
        /// <summary>
        /// Defines the crank throw's rotation radius (resulting in cylinder's stroke), in milimeters.
        /// </summary>
        [DisplayName("Crank rotation radius")]
        [Description("Defines the crank throw's rotation radius (resulting in cylinder's stroke), in milimeters.")]
        public double CrankRotationRadius_mm
        {
            get { return this.crankRotationRadius_mm; }

            set
            {
                this.crankRotationRadius_mm = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the mass of the crank balancer, in grams.
        /// </summary>
        [DisplayName("Balancer mass")]
        [Description("Defines the mass of the crank balancer, in grams.")]
        public double BalancerMass_g
        {
            get { return this.balancerMass_g; }

            set
            {
                this.balancerMass_g = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the rotation radius of the crank balancer, in milimeters.
        /// </summary>
        [DisplayName("Balancer rotation radius")]
        [Description("Defines the rotation radius of the crank balancer, in milimeters.")]
        public double BalancerRotationRadius_mm
        {
            get { return this.balancerRotationRadius_mm; }

            set
            {
                this.balancerRotationRadius_mm = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the angle of the balancer with respect to the crank throw, in degrees.
        /// </summary>
        [DisplayName("Balancer angle")]
        [Description("Defines the angle of the balancer with respect to the crank throw, in degrees.")]
        public double BalancerAngle_deg
        {
            get { return this.balancerAngle_deg; }

            set
            {
                this.balancerAngle_deg = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the width of the crank pin, in milimeters.
        /// </summary>
        [DisplayName("Crank pin width")]
        [Description("Defines the width of the crank pin, in milimeters.")]
        public double CrankPinWidth_mm
        {
            get { return this.crankPinWidth_mm; }

            set
            {
                this.crankPinWidth_mm = value;
                this.Validate();
            }
        }
        #endregion "Public properties"




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
                if ((this.balancerMass_g > 0d)
                    && (this.balancerRotationRadius_mm > 0d))
                {
                    if (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm >= EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm)
                    {
                        return
                            -(EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
                    }
                    else
                    {
                        return
                            -(EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2d);
                    }
                }
                else
                {
                    return
                        -(EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
                }
            }
        }

        [Browsable(false)]
        public double Bound_Y_Min
        {
            get
            {
                double _double = 0d;

                if ((this.balancerMass_g > 0d)
                    && (this.balancerRotationRadius_mm > 0d))
                {
                    _double +=
                        (Math.Cos(Conversions.DegToRad(this.balancerAngle_deg)) * this.balancerRotationRadius_mm)
                        - (EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2d);
                }

                return _double;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Min
        {
            get
            {
                if ((this.balancerMass_g > 0d)
                    && (this.balancerRotationRadius_mm > 0d))
                {
                    return
                        -((this.crankPinWidth_mm / 2d)
                        + EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm);
                }
                else
                {
                    return
                        -(this.crankPinWidth_mm / 2d);
                }
            }
        }

        [Browsable(false)]
        public double Bound_X_Max
        {
            get
            {
                if ((this.balancerMass_g > 0d)
                    && (this.balancerRotationRadius_mm > 0d))
                {
                    if (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm >= EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm)
                    {
                        return
                            (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
                    }
                    else
                    {
                        return
                            (EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerDiameter_mm / 2d);
                    }
                }
                else
                {
                    return
                        (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
                }
            }
        }

        [Browsable(false)]
        public double Bound_Y_Max
        {
            get
            {
                return
                    this.crankRotationRadius_mm
                    + (EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Z_Max
        {
            get
            {
                if ((this.balancerMass_g > 0d)
                    && (this.balancerRotationRadius_mm > 0d))
                {
                    return
                        ((this.crankPinWidth_mm / 2d)
                        + EngineDesigner.Machine.Properties.Settings.Default.CrankBalancerWidth_mm);
                }
                else
                {
                    return
                        (this.crankPinWidth_mm / 2d);
                }
            }
        }


        public void Validate()
        {
            if (this.crankRotationRadius_mm < 0d)
            {
                throw new ValidationException("this.crankRotationRadius_mm < 0d");
            }

            if (this.balancerMass_g < 0d)
            {
                throw new ValidationException("this.balancerMass_g < 0d");
            }

            if (this.balancerRotationRadius_mm < 0d)
            {
                throw new ValidationException("this.balancerRotationRadius_mm < 0d");
            }

            if (this.balancerAngle_deg < 0d)
            {
                throw new ValidationException("this.balancerAngle_deg < 0d");
            }

            if (this.balancerAngle_deg >= 360d)
            {
                throw new ValidationException("this.balancerAngle_deg >= 360d");
            }

            if (this.crankPinWidth_mm < 0d)
            {
                throw new ValidationException("this.crankPinWidth_mm < 0d");
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
    /// Provides UI integration for CrankThrow class.
    /// </summary>
    class CrankThrowConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(CrankThrow))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is CrankThrow))
            {
                CrankThrow _crankThrow = (CrankThrow)value;
                return _crankThrow.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
