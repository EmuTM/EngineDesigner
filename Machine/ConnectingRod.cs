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
    /// Represents a connecting rod unit for the cylinder.
    /// </summary>
    [TypeConverter(typeof(ConnectingRodConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class ConnectingRod : IPart
    {
        public ConnectingRod()
            : this(DefaultConnectingRod.rotatingMass_g, DefaultConnectingRod.rotatingMassDistanceFromCenterOfGravity_mm, DefaultConnectingRod.reciprocatingMass_g, DefaultConnectingRod.reciprocatingMassDistanceFromCenterOfGravity_mm)
        {

        }
        /// <param name="_rotatingMass_g">The rotating part of mass of the connecting rod (for dynamic equivalency), in grams.</param>
        /// <param name="_rotatingMassDistanceFromCenterOfGravity_mm">The distance of the rotating part of mass from the connecting rod's center of gravity (for dynamic equivalency), in milimeters.</param>
        /// <param name="_reciprocatingMass_g">The reciprocating part of mass of the connecting rod (for dynamic equivalency), in grams.</param>
        /// <param name="_reciprocatingMassDistanceFromCenterOfGravity_mm">The distance of the reciprocating part of mass from the connecting rod's center of gravity (for dynamic equivalency), in milimeters.</param>
        public ConnectingRod(double _rotatingMass_g, double _rotatingMassDistanceFromCenterOfGravity_mm, double _reciprocatingMass_g, double _reciprocatingMassDistanceFromCenterOfGravity_mm)
            : this(false, _rotatingMass_g, _rotatingMassDistanceFromCenterOfGravity_mm, _reciprocatingMass_g, _reciprocatingMassDistanceFromCenterOfGravity_mm)
        {
        }
        private ConnectingRod(bool _skipValidation, double _rotatingMass_g, double _rotatingMassDistanceFromCenterOfGravity_mm, double _reciprocatingMass_g, double _reciprocatingMassDistanceFromCenterOfGravity_mm)
        {
            this.rotatingMass_g = _rotatingMass_g;
            this.rotatingMassDistanceFromCenterOfGravity_mm = _rotatingMassDistanceFromCenterOfGravity_mm;

            this.reciprocatingMass_g = _reciprocatingMass_g;
            this.reciprocatingMassDistanceFromCenterOfGravity_mm = _reciprocatingMassDistanceFromCenterOfGravity_mm;


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
            if (obj is ConnectingRod)
            {
                ConnectingRod _connectingRod = (ConnectingRod)obj;

                if (this.guid.ToString() == _connectingRod.guid.ToString())
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
                "{0}g; {1}mm",
                Mass_g.ToString(),
                Length_mm.ToString());
        }



        #region "System-defined connecting rods"
        /// <summary>
        /// Gets the default connecting rod (for UI integration).
        /// </summary>
        public static ConnectingRod DefaultConnectingRod
        {
            get
            {
                double _1 = 1d * EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodWeightAndDistanceDistribution;
                double _2 = 1d - _1;

                return new ConnectingRod(
                    true,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodTotalMass_g * _2,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodLength_mm * _1,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodTotalMass_g * _1,
                    EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodLength_mm * _2);
            }
        }

        /// <summary>
        /// Gets a simple connecting rod based on given parameters.
        /// </summary>
        /// <param name="_mass_g">The total mass of connecting rod, in grams.</param>
        /// <param name="_length_mm">The total length of connecting rod, in milimeters.</param>
        public static ConnectingRod FromParameters(double _mass_g, double _length_mm)
        {
            double _1 = 1d * EngineDesigner.Machine.Properties.Settings.Default.DefaultConnectingRodWeightAndDistanceDistribution;
            double _2 = 1d - _1;

            return new ConnectingRod(
                _mass_g * _2,
                _length_mm * _1,
                _mass_g * _1,
                _length_mm * _2);
        }
        #endregion "System-defined connecting rods"



        [DataMember]
        private double rotatingMass_g;
        [DataMember]
        private double rotatingMassDistanceFromCenterOfGravity_mm;
        [DataMember]
        private double reciprocatingMass_g;
        [DataMember]
        private double reciprocatingMassDistanceFromCenterOfGravity_mm;



        #region "Public properties"
        /// <summary>
        /// Defines the total (max measurable) length of the connecting rod, in milimeters.
        /// </summary>
        [DisplayName("Length")]
        [Description("Indicates the total (max measurable) length of the connecting rod, in milimeters.")]
        public double Length_mm
        {
            get { return rotatingMassDistanceFromCenterOfGravity_mm + reciprocatingMassDistanceFromCenterOfGravity_mm; }
        }

        /// <summary>
        /// Defines the total mass of the connecting rod (all included), in grams.
        /// </summary>
        [DisplayName("Mass")]
        [Description("Indicates the total mass of the connecting rod (all included), in grams.")]
        public double Mass_g
        {
            get { return rotatingMass_g + reciprocatingMass_g; }
        }

        /// <summary>
        /// Defines the rotating part of mass of the connecting rod (for dynamic equivalency), in grams.
        /// </summary>
        [DisplayName("Rotating mass")]
        [Description("Defines the rotating part of mass of the connecting rod (for dynamic equivalency), in grams.")]
        public double RotatingMass_g
        {
            get { return rotatingMass_g; }

            set
            {
                rotatingMass_g = value;
                Validate();
            }
        }

        /// <summary>
        /// Defines the distance of the rotating part of mass from the connecting rod's center of gravity (for dynamic equivalency), in milimeters.
        /// </summary>
        [DisplayName("Rotating mass distance from CG")]
        [Description("Defines the distance of the rotating part of mass from the connecting rod's center of gravity (for dynamic equivalency), in milimeters.")]
        public double RotatingMassDistanceFromCenterOfGravity_mm
        {
            get { return rotatingMassDistanceFromCenterOfGravity_mm; }

            set
            {
                rotatingMassDistanceFromCenterOfGravity_mm = value;
                Validate();
            }
        }

        /// <summary>
        /// Defines the reciprocating part of mass of the connecting rod (for dynamic equivalency), in grams.
        /// </summary>
        [DisplayName("Reciprocating mass")]
        [Description("Defines the reciprocating part of mass of the connecting rod (for dynamic equivalency), in grams.")]
        public double ReciprocatingMass_g
        {
            get { return reciprocatingMass_g; }

            set
            {
                reciprocatingMass_g = value;
                Validate();
            }
        }

        /// <summary>
        /// Defines the distance of the reciprocating part of mass from the connecting rod's center of gravity (for dynamic equivalency), in milimeters.
        /// </summary>
        [DisplayName("Reciprocating mass distance from CG")]
        [Description("Defines the distance of the reciprocating part of mass from the connecting rod's center of gravity (for dynamic equivalency), in milimeters.")]
        public double ReciprocatingMassDistanceFromCenterOfGravity_mm
        {
            get { return reciprocatingMassDistanceFromCenterOfGravity_mm; }

            set
            {
                reciprocatingMassDistanceFromCenterOfGravity_mm = value;
                Validate();
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
                    -EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d;
            }
        }

        [Browsable(false)]
        public double Bound_Y_Min
        {
            get
            {
                return
                    -this.Length_mm / 2d;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Min
        {
            get
            {
                return
                    -EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2d;
            }
        }

        [Browsable(false)]
        public double Bound_X_Max
        {
            get
            {
                return
                    EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2;
            }
        }

        [Browsable(false)]
        public double Bound_Y_Max
        {
            get
            {
                return
                    this.Length_mm / 2d;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Max
        {
            get
            {
                return
                    EngineDesigner.Machine.Properties.Settings.Default.ConRodBigEndPinAndCrankshaftDiameter_mm / 2;
            }
        }


        public void Validate()
        {
            if (this.rotatingMass_g < 0)
            {
                throw new ValidationException("this.rotatingMass_g < 0");
            }

            if (this.reciprocatingMass_g < 0)
            {
                throw new ValidationException("this.reciprocatingMass_g < 0");
            }

            if (this.rotatingMassDistanceFromCenterOfGravity_mm < 0)
            {
                throw new ValidationException("this.rotatingMassDistanceFromCenterOfGravity_mm < 0");
            }

            if (this.reciprocatingMassDistanceFromCenterOfGravity_mm < 0)
            {
                throw new ValidationException("this.reciprocatingMassDistanceFromCenterOfGravity_mm < 0");
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
    /// Provides UI integration for ConnectingRod class.
    /// </summary>
    class ConnectingRodConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(ConnectingRod))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is ConnectingRod))
            {
                ConnectingRod _connectingRod = (ConnectingRod)value;
                return _connectingRod.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
