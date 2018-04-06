using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;

using EngineDesigner.Machine.Definitions;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents a piston unit for the cylinder.
    /// </summary>
    [TypeConverter(typeof(PistonConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Piston : IPart
    {
        public Piston()
            : this(DefaultPiston.diameter_mm, DefaultPiston.mass_g, DefaultPiston.skirtLength_mm, DefaultPiston.gudgeonPinDistanceFromTop_mm)
        {

        }
        /// <param name="_diameter_mm">The piston's diameter, in milimeters.</param>
        /// <param name="_mass_g">The total mass of the piston (accessories included), in grams.</param>
        /// <param name="_diameter_mm">The total (max  measurable) length of the piston's skirt (from topmost point to lowest point), in milimeters.</param>
        /// <param name="_mass_g">The distance of the center of the gudgeon pin from the topmost point of the piston, in milimeters.</param>
        public Piston(double _diameter_mm, double _mass_g, double _skirtLength_mm, double _gudgeonPinDistanceFromTop_mm)
            : this(false, _diameter_mm, _mass_g, _skirtLength_mm, _gudgeonPinDistanceFromTop_mm)
        {
        }
        private Piston(bool _skipValidation, double _diameter_mm, double _mass_g, double _skirtLength_mm, double _gudgeonPinDistanceFromTop_mm)
        {
            this.diameter_mm = _diameter_mm;
            this.mass_g = _mass_g;
            this.skirtLength_mm = _skirtLength_mm;
            this.gudgeonPinDistanceFromTop_mm = _gudgeonPinDistanceFromTop_mm;


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
            if (obj is Piston)
            {
                Piston _piston = (Piston)obj;

                if (this.guid.ToString() == _piston.guid.ToString())
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
                "{0}g",
                mass_g.ToString());
        }



        #region "System-defined pistons"
        /// <summary>
        /// Gets the default piston (for UI integration).
        /// </summary>
        public static Piston DefaultPiston
        {
            get
            {
                double _skirtLength_mm = EngineDesigner.Machine.Properties.Settings.Default.DefaultPistonDiameter_mm
                    * EngineDesigner.Machine.Properties.Settings.Default.PistonSkirtLengthVsCylinderBore;
                double _gudgeonPinDistanceFromTop_mm = _skirtLength_mm
                    * EngineDesigner.Machine.Properties.Settings.Default.GudgeonPinDistanceFromTopVsPistonSkirtLength;

                return new Piston(
                    true,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultPistonDiameter_mm,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultPistonMass_g,
                    _skirtLength_mm,
                    _gudgeonPinDistanceFromTop_mm);
            }
        }

        /// <summary>
        /// Gets a simple piston based on given parameters.
        /// </summary>
        /// <param name="_diameter_mm">The piston's diameter, in milimeters.</param>
        /// <param name="_mass_g">The total mass of the piston (accessories included), in grams.</param>
        public static Piston FromParameters(double _diameter_mm, double _mass_g)
        {
            double _skirtLength_mm = _diameter_mm * EngineDesigner.Machine.Properties.Settings.Default.PistonSkirtLengthVsCylinderBore;
            double _gudgeonPinDistanceFromTop_mm = _skirtLength_mm * EngineDesigner.Machine.Properties.Settings.Default.GudgeonPinDistanceFromTopVsPistonSkirtLength;

            return new Piston(
                _diameter_mm,
                _mass_g,
                _skirtLength_mm,
                _gudgeonPinDistanceFromTop_mm);
        }
        #endregion "System-defined pistons"



        [DataMember]
        private double diameter_mm;
        [DataMember]
        private double mass_g;
        [DataMember]
        private double skirtLength_mm;
        [DataMember]
        private double gudgeonPinDistanceFromTop_mm;



        #region "Public properties"
        /// <summary>
        /// Defines the piston's diameter (resulting in cylinder's bore), in milimeters.
        /// </summary>
        [DisplayName("Diameter")]
        [Description("Defines the piston's diameter (resulting in cylinder's bore), in milimeters.")]
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
        /// Defines the total mass of the piston (accessories included), in grams.
        /// </summary>
        [DisplayName("Mass")]
        [Description("Defines the total mass of the piston (accessories included), in grams.")]
        public double Mass_g
        {
            get { return this.mass_g; }

            set
            {
                this.mass_g = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the total (max  measurable) length of the piston's skirt (from topmost point to lowest point), in milimeters.
        /// </summary>
        [DisplayName("Skirt length")]
        [Description("Defines the total (max  measurable) length of the piston's skirt (from topmost point to lowest point), in milimeters.")]
        public double SkirtLength_mm
        {
            get { return this.skirtLength_mm; }

            set
            {
                this.skirtLength_mm = value;
                this.Validate();
            }
        }

        /// <summary>
        /// Defines the distance of the center of the gudgeon pin from the topmost point of the piston, in milimeters.
        /// </summary>
        [DisplayName("Gudgeon pin distance from top of the piston")]
        [Description("Defines the distance of the center of the gudgeon pin from the topmost point of the piston, in milimeters.")]
        public double GudgeonPinDistanceFromTop_mm
        {
            get { return this.gudgeonPinDistanceFromTop_mm; }

            set
            {
                this.gudgeonPinDistanceFromTop_mm = value;
                this.Validate();
            }
        }




        /// <summary>
        /// Indicates the piston's area, in square milimeters.
        /// </summary>
        [DisplayName("Area")]
        [Description("Indicates the piston's area, in square milimeters.")]
        public double Area_mm2
        {
            get
            {
                double _r2 = Math.Pow((this.diameter_mm / 2), 2);
                return Math.PI * _r2;
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
                    -(this.skirtLength_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Z_Min
        {
            get
            {
                return
                    -(this.diameter_mm / 2d);
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
                    (this.skirtLength_mm / 2d);
            }
        }

        [Browsable(false)]
        public double Bound_Z_Max
        {
            get
            {
                return
                    (this.diameter_mm / 2d);
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
            if (this.skirtLength_mm < 0d)
            {
                throw new ValidationException("this.skirtLength_mm < 0d");
            }
            if (this.gudgeonPinDistanceFromTop_mm < 0d)
            {
                throw new ValidationException("this.gudgeonPinDistanceFromTop_mm < 0d");
            }

            if (this.gudgeonPinDistanceFromTop_mm > this.skirtLength_mm)
            {
                throw new ValidationException("this.gudgeonPinDistanceFromTop_mm > this.skirtLength_mm");
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
    /// Provides UI integration for Piston class.
    /// </summary>
    class PistonConverter : ExpandableObjectConverter
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
                && (value is Piston))
            {
                Piston _piston = (Piston)value;
                return _piston.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
