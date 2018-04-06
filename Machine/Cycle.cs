using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Globalization;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Common.CustomCollections;


namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents a complete cycle for a cylinder.
    /// </summary>
    [TypeConverter(typeof(CycleConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Cycle
    {
        public Cycle()
            : this(NaN.CycleId, NaN.Strokes, 0d)
        {

        }
        /// <param name="_cycleId">The unique name of the cycle.</param>
        /// <param name="_strokes">The strokes taking place within this cycle.</param>
        /// <param name="_defaultFiringAngle">The crank throw angle, in degrees, where the power stroke is to take place.</param>
        public Cycle(string _cycleId, Stroke[] _strokes, double _defaultFiringAngle)
        {
            this.cycleId = _cycleId;
            this.strokes = _strokes;
            this.defaultFiringAngle = _defaultFiringAngle;
        }
        public override bool Equals(object obj)
        {
            if (obj is Cycle)
            {
                Cycle _cycle = (Cycle)obj;

                bool _bool1 = this.cycleId == _cycle.cycleId;
                if (!double.IsNaN(this.defaultFiringAngle)
                    && !double.IsNaN(_cycle.defaultFiringAngle))
                {
                    _bool1 = _bool1 && this.defaultFiringAngle == _cycle.defaultFiringAngle;
                }


                bool _bool2 = true;
                if (this.strokes.Length == _cycle.strokes.Length)
                {
                    for (int a = 0; a < _cycle.strokes.Length; a++)
                    {
                        if (!_cycle.strokes[a].Equals(this.strokes[a]))
                        {
                            _bool2 = false;
                            break;
                        }
                    }
                }
                else
                {
                    _bool2 = false;
                }

                return _bool1 && _bool2;
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
                "{0} (Firing @ {1}°)",
                cycleId.ToString(),
                defaultFiringAngle.ToString());
        }



        #region "System-defined cycles"
        /// <summary>
        /// Gets the default two-stroke cycle.
        /// </summary>
        public static Cycle TwoStroke
        {
            get
            {
                Cycle _cycle = new Cycle(
                    "Two-Stroke",
                    new Stroke[2]
                    {
                        Stroke.CombustionExhaust,
                        Stroke.WashCompression
                    },
                    0d);

                return _cycle;
            }
        }

        /// <summary>
        /// Gets the default four-stroke cycle.
        /// </summary>
        public static Cycle FourStroke
        {
            get
            {
                Cycle _cycle = new Cycle(
                    "Four-Stroke",
                    new Stroke[4]
                    {
                        Stroke.Intake,
                        Stroke.Compression,
                        Stroke.Combustion,
                        Stroke.Exhaust
                    },
                    360d);

                return _cycle;
            }
        }

        /// <summary>
        /// Gets a default undefined cycle.
        /// </summary>
        public static Cycle NaN
        {
            get
            {
                Cycle _cycle = new Cycle(
                    "NaN",
                    new Stroke[0],
                    double.NaN);
                return _cycle;
            }
        }
        #endregion "System-defined cycles"



        [DataMember]
        private string cycleId;
        [DataMember]
        private Stroke[] strokes;
        [DataMember]
        private double defaultFiringAngle;



        #region "Public properties"
        /// <summary>
        /// Defines the unique name of the cycle.
        /// </summary>
        [DisplayName("Cycle ID")]
        [Description("Defines the unique name of the cycle.")]
        [ReadOnly(true)]
        public string CycleId
        {
            get { return cycleId; }
        }

        /// <summary>
        /// Defines the duration (in crank throw angle) required for cycle completion, in degrees.
        /// </summary>
        [DisplayName("Duration")]
        [Description("Indicates the duration (in crank throw angle) required for cycle completion, in degrees.")]
        [ReadOnly(true)]
        public double Duration_deg
        {
            get
            {
                return strokes.Length * Stroke.StrokeDuration_deg;
            }
        }

        /// <summary>
        /// Defines the strokes taking place within this cycle.
        /// </summary>
        [DisplayName("Strokes")]
        [Description("Defines the strokes taking place within this cycle.")]
        [ReadOnly(true)]
        [TypeConverter(typeof(StrokesConverter))]
        public Stroke[] Strokes
        {
            get { return strokes; }
            set { strokes = value; }
        }

        /// <summary>
        /// Defines the crank throw angle, in degrees, where the power stroke is to take place.
        /// </summary>
        [DisplayName("Default firing angle")]
        [Description("Defines the crank throw angle, in degrees, where the power stroke is to take place.")]
        [ReadOnly(true)]
        public double DefaultFiringAngle
        {
            get { return defaultFiringAngle; }
        }

        /// <summary>
        /// Indicates the number of revolutions needed to complete the cycle.
        /// </summary>
        [DisplayName("Revolutions to complete cycle")]
        [Description("Indicates the number of revolutions needed to complete the cycle.")]
        [ReadOnly(true)]
        public int RevolutionsToCompleteCycle
        {
            get { return strokes.Length / 2; }
        }



        #endregion "Public properties"



        public Stroke GetPreviousStroke(Stroke _stroke)
        {
            if (!this.strokes.Contains(_stroke))
            {
                throw new Exception("v tem ciklu ni takšnega takta.");
            }


            foreach (Stroke _strokeTmp in this.strokes)
            {
                double _angle_deg = _stroke.Begin_deg - Stroke.StrokeDuration_deg;
                _angle_deg = EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_angle_deg, this.RevolutionsToCompleteCycle, true);

                if (_strokeTmp.Begin_deg == _angle_deg)
                {
                    return _strokeTmp;
                }
            }


            throw new Exception("ni takta.");
        }
        public Stroke GetNextStroke(Stroke _stroke)
        {
            if (!this.strokes.Contains(_stroke))
            {
                throw new Exception("v tem ciklu ni takšnega takta.");
            }


            foreach (Stroke _strokeTmp in this.strokes)
            {
                double _angle_deg = _stroke.Begin_deg + Stroke.StrokeDuration_deg;
                _angle_deg = EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_angle_deg, this.RevolutionsToCompleteCycle, true);

                if (_strokeTmp.Begin_deg == _angle_deg)
                {
                    return _strokeTmp;
                }
            }


            throw new Exception("ni takta.");
        }


    }


    /// <summary>
    /// Provides UI integration for Cycle class.
    /// </summary>
    class CycleConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Cycle))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Cycle))
            {
                Cycle _cycle = (Cycle)value;
                return _cycle.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { Cycle.TwoStroke.ToString(), Cycle.FourStroke.ToString() });
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            //NOTE: to še ni preverjeno
            if (sourceType == typeof(string))
            {
                return true;
            }
            //NOTE: to še ni preverjeno

            return true;
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string _string = value.ToString();

            Cycle _cycle = null;
            if (_string == Cycle.TwoStroke.ToString())
            {
                _cycle = Cycle.TwoStroke;
            }
            else if (_string == Cycle.FourStroke.ToString())
            {
                _cycle = Cycle.FourStroke;
            }

            return _cycle;
        }

    }


    /// <summary>
    /// Provides UI integration for Strokes property.
    /// </summary>
    class StrokesConverter : ArrayConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Stroke[]))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Stroke[]))
            {
                return "...";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
