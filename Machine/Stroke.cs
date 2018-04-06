using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;

using EngineDesigner.Common.CustomCollections;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents well-known piston movement within the cylinder's cycle.
    /// </summary>
    [TypeConverter(typeof(StrokeConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Stroke : ICustomCollectionElement
    {
        public Stroke()
            : this(NaN.StrokeId, NaN.Begin_deg)
        {

        }
        private Stroke(string _strokeId, double _begin_deg)
        {
            strokeId = _strokeId;
            begin_deg = _begin_deg;
        }
        public static Stroke operator +(Stroke _stroke, double _angle_deg)
        {
            double _currentAngle_deg = _stroke.begin_deg + _angle_deg;

            return Stroke.FromAngle_deg(_currentAngle_deg);
        }
        public static Stroke operator ++(Stroke _stroke)
        {
            double _currentAngle_deg = _stroke.begin_deg + Stroke.StrokeDuration_deg;

            return Stroke.FromAngle_deg(_currentAngle_deg);
        }
        public static Stroke operator -(Stroke _stroke, double _angle_deg)
        {
            double _currentAngle_deg = _stroke.begin_deg - _angle_deg;

            return Stroke.FromAngle_deg(_currentAngle_deg);
        }
        public static Stroke operator --(Stroke _stroke)
        {
            double _currentAngle_deg = _stroke.begin_deg - Stroke.StrokeDuration_deg;

            return Stroke.FromAngle_deg(_currentAngle_deg);
        }
        public override bool Equals(object obj)
        {
            if (obj is Stroke)
            {
                Stroke _stroke = (Stroke)obj;

                return ((this.strokeId == _stroke.strokeId)
                    && ((this.begin_deg == _stroke.begin_deg)
                    || ((double.IsNaN(this.begin_deg)) && (double.IsNaN(_stroke.begin_deg)))));
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return strokeId.ToString();
        }
        #region ICustomCollectionElement Members
        [Browsable(false)]
        public string DisplayName
        {
            get { return strokeId; }
        }
        [Browsable(false)]
        public string DisplayDescription
        {
            get
            {
                return "Defines the parameters for the stroke.";
            }
        }
        #endregion



        /// <summary>
        /// Provides the value for standard (by definition) stroke duration, in degrees.
        /// </summary>
        public const double StrokeDuration_deg = 180d;
        /// <summary>
        /// Provides the value for standard (by definition) number of strokes in one engine revolution.
        /// </summary>
        public const int StrokesPerRevolution = 2;



        #region "System-defined strokes"
        /// <summary>
        /// Gets the Intake stroke for the four-stroke cycle.
        /// </summary>
        public static Stroke Intake
        {
            get
            {
                return new Stroke("Intake", 0d);
            }
        }

        /// <summary>
        /// Gets the Compression stroke for the four-stroke cycle.
        /// </summary>
        public static Stroke Compression
        {
            get
            {
                return new Stroke("Compression", 180d);
            }
        }

        /// <summary>
        /// Gets the Combustion stroke for the four-stroke cycle.
        /// </summary>
        public static Stroke Combustion
        {
            get
            {
                return new Stroke("Combustion", 360d);
            }
        }

        /// <summary>
        /// Gets the Exhaust stroke for the four-stroke cycle.
        /// </summary>
        public static Stroke Exhaust
        {
            get
            {
                return new Stroke("Exhaust", 540d);
            }
        }

        /// <summary>
        /// Gets the Combustion-exhaust stroke for the two-stroke cycle.
        /// </summary>
        public static Stroke CombustionExhaust
        {
            get
            {
                return new Stroke("Combustion-Exhaust", 0d);
            }
        }

        /// <summary>
        /// Gets the Wash-compression stroke for the two-stroke cycle.
        /// </summary>
        public static Stroke WashCompression
        {
            get
            {
                return new Stroke("Wash-Compression", 180d);
            }
        }


        /// <summary>
        /// Gets a default undefined stroke.
        /// </summary>
        public static Stroke NaN
        {
            get
            {
                return new Stroke("NaN", double.NaN);
            }
        }


        //vrne takt smo glede na kot
        private static Stroke FromAngle_deg(double _currentAngle_deg)
        {
            Stroke _intake = Stroke.Intake;
            if (_intake.IsWithin(_currentAngle_deg))
            {
                return _intake;
            }

            Stroke _compression = Stroke.Compression;
            if (_compression.IsWithin(_currentAngle_deg))
            {
                return _compression;
            }

            Stroke _combustion = Stroke.Combustion;
            if (_combustion.IsWithin(_currentAngle_deg))
            {
                return _combustion;
            }

            Stroke _exhaust = Stroke.Exhaust;
            if (_exhaust.IsWithin(_currentAngle_deg))
            {
                return _exhaust;
            }

            Stroke _washCompression = Stroke.WashCompression;
            if (_washCompression.IsWithin(_currentAngle_deg))
            {
                return _washCompression;
            }

            Stroke _combustionExhaust = Stroke.CombustionExhaust;
            if (_combustionExhaust.IsWithin(_currentAngle_deg))
            {
                return _combustionExhaust;
            }


            throw new Exception("tak takt ne obstaja.");
        }
        #endregion "System-defined strokes"



        [DataMember]
        private string strokeId;
        [DataMember]
        private double begin_deg;



        #region "Public properties"
        /// <summary>
        /// Defines the unique name for the stroke.
        /// </summary>
        [DisplayName("Stroke ID")]
        [Description("Defines the unique name for the stroke.")]
        [ReadOnly(true)]
        public string StrokeId
        {
            get { return strokeId; }
        }

        /// <summary>
        /// Defines the crank throw angle for the beginning of the stroke, in degrees.
        /// </summary>
        [DisplayName("Begin")]
        [Description("Defines the crank throw angle for the beginning of the stroke, in degrees.")]
        [ReadOnly(true)]
        public double Begin_deg
        {
            get { return begin_deg; }
        }

        /// <summary>
        /// Defines the crank throw angle for the end of the stroke, in degrees.
        /// </summary>
        [DisplayName("End")]
        [Description("Defines the crank throw angle for the end of the stroke, in degrees.")]
        [ReadOnly(true)]
        public double End_deg
        {
            get { return begin_deg + Stroke.StrokeDuration_deg; }
        }
        #endregion "Public properties"



        /// <summary>
        /// Determines whether the given angle, in degrees, is within the stroke.
        /// </summary>
        /// <param name="_angle_deg">The angle, in degrees.</param>
        public bool IsWithin(double _angle_deg)
        {
            if ((_angle_deg >= this.begin_deg)
                && (_angle_deg < this.End_deg)) //za End_deg uporabimo kar property
            {
                return true;
            }

            return false;
        }

    }


    /// <summary>
    /// Provides UI integration for Stroke class.
    /// </summary>
    class StrokeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Stroke))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Stroke))
            {
                Stroke _stroke = (Stroke)value;
                return string.Format(
                    "{0}° - {1}°",
                    _stroke.Begin_deg,
                    _stroke.End_deg);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
