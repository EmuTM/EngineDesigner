using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Common.CustomCollections;
using System.Drawing.Design;
using EngineDesigner.Machine.Definitions;
using EngineDesigner.Common.Serialization;
using EngineDesigner.Common;

namespace EngineDesigner.Machine
{
    /// <summary>
    /// Represents an engine, resulting from combination of various cylinders and their setups.
    /// </summary>
    [TypeConverter(typeof(EngineConverter))]
    [DataContract]
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    public class Engine : Serializable<Engine>, IPart
    {
        /// <param name="_positionedCylinders">The cylinder units positioned within this engine.</param>
        public Engine(params PositionedCylinder[] _positionedCylinders)
        {
            this.flywheel = Flywheel.DefaultFlywheel;

            this.positionedCylinders = new CustomList<PositionedCylinder>(false);
            this.positionedCylinders.AddRange(_positionedCylinders);

            this.Validate();

            //NOTE: naj bo to zaenkrat disablano, ker povzroča preveč težav; razen tega se konstruktor NE KLIČE ob deserializaciji v datacontractserializerju

            //this.positionedCylinders.CollectionChanging
            //    += new EventHandler<CustomListEventArgs<PositionedCylinder>>(positionedCylinders_CollectionChanging);

            //to je pa zato, da se validira tudi, ko je engine objekt že narejen
            //foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
            //{
            //    _positionedCylinder.Validated
            //        += new IPartDelegate(this.PositionedCylinder_Validated);
            //}
        }
        //NOTE: naj bo to disablano, ker povzroča preveč težav
        //private void positionedCylinders_CollectionChanging(object sender, CustomListEventArgs<PositionedCylinder> e)
        //{
        //    try
        //    {
        //        this.Validate();
        //    }
        //    catch
        //    {
        //        e.Elements = null;
        //    }
        //}
        //[System.Runtime.Serialization.OnDeserialized]
        //void OnDeserialized(System.Runtime.Serialization.StreamingContext c)
        //{
        //    this.positionedCylinders.CollectionChanging
        //        += new EventHandler<CustomListEventArgs<PositionedCylinder>>(positionedCylinders_CollectionChanging);
        //}
        public override bool Equals(object obj)
        {
            if (obj is Engine)
            {
                Engine _engine = (Engine)obj;

                if (this.guid.ToString() == _engine.guid.ToString())
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
                "{0} cylinder(s) / {1} cm3",
                positionedCylinders.Count.ToString(),
                TotalDisplacement_cm3.ToString(Defaults.ROUNDING));
        }



        //#region "System-defined engines"
        ///// <summary>
        ///// Gets an in-line engine based on given parameters.
        ///// </summary>
        ///// <param name="_cycle">The type of the cycle for the engine.</param>
        ///// <param name="_totalDisplacement_cm3">The total displacement of the engine, in cubic centimeters.</param>
        ///// <param name="_firingAngles_deg">The firing angles, in degrees, which will also result in number of cylinders.</param>
        //public static Engine InLine(Cycle _cycle, double _totalDisplacement_cm3, params double[] _firingAngles_deg)
        //{
        //    int _numberOfCylinders = _firingAngles_deg.Length;
        //    double _cylinderDisplacement = _totalDisplacement_cm3 / _numberOfCylinders;
        //    double _cylinderFiringAngle = _cycle.Duration_deg / _numberOfCylinders;

        //    List<PositionedCylinder> _positionedCylinders = new List<PositionedCylinder>();
        //    double _longitudinalOffset = 0;
        //    for (int a = 0; a < _numberOfCylinders; a++)
        //    {
        //        Cylinder _cylinder = Cylinder.FromParameters(_cycle, _cylinderDisplacement);

        //        if (a > 0)
        //        {
        //            _longitudinalOffset += _cylinder.Bore_mm + (_cylinder.Bore_mm / EngineDesigner.Machine.Properties.Settings.Default.DefaultOffsetDivisor);
        //        }
        //        PositionedCylinder _positionedCylinder = new PositionedCylinder(
        //            _cylinder,
        //            a + 1,
        //            _longitudinalOffset,
        //            0,
        //            _firingAngles_deg[a]);


        //        _positionedCylinders.Add(_positionedCylinder);
        //    }

        //    return new Engine(_positionedCylinders.ToArray());
        //}

        ///// <summary>
        ///// Gets a vee-type engine based on given parameters.
        ///// </summary>
        ///// <param name="_cycle">The type of the cycle for the engine.</param>
        ///// <param name="_totalDisplacement_cm3">The total displacement of the engine, in cubic centimeters.</param>
        ///// <param name="_veeAngle_deg">The angle between cylinder banks, in degrees.</param>
        ///// <param name="_firingAngles_deg">The firing angles, in degrees, which will also result in number of cylinders.</param>
        //public static Engine Vee(Cycle _cycle, double _totalDisplacement_cm3, double _veeAngle_deg, params double[] _firingAngles_deg)
        //{
        //    Engine _engine = InLine(_cycle, _totalDisplacement_cm3, _firingAngles_deg);

        //    for (int a = 0; a < _engine.NumberOfCylinders; a++)
        //    {
        //        if (EngineDesigner.Common.Mathematics.IsOdd(a + 1))
        //        {
        //            _engine.PositionedCylinders[a].Tilt_deg = -(_veeAngle_deg / 2d);
        //        }
        //        else
        //        {
        //            _engine.PositionedCylinders[a].Tilt_deg = (_veeAngle_deg / 2d);
        //        }
        //    }

        //    return _engine;
        //}
        //#endregion "System-defined engines"




        [DataMember]
        private CustomList<PositionedCylinder> positionedCylinders;

        [DataMember]
        private Flywheel flywheel;




        #region "Public properties"
        ///// <summary>
        ///// Gets the positioned cylinder by it's position within the engine.
        ///// </summary>
        //[Browsable(false)]
        //public PositionedCylinder this[int _cylinderPosition]
        //{
        //    get
        //    {
        //        foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //        {
        //            if (_positionedCylinder.Position == _cylinderPosition)
        //            {
        //                return _positionedCylinder;
        //            }
        //        }

        //        throw new Exception("PositionedCylinder not found.");
        //    }
        //}

        /// <summary>
        /// Defines the cylinder units positioned within this engine.
        /// </summary>
        [DisplayName("Cylinders")]
        [Description("Defines the cylinders and their positioning within the engine.")]
        [TypeConverter(typeof(PositionedCylindersConverter))]
        [Editor(typeof(PositionedCylindersEditor), typeof(UITypeEditor))]
        public CustomList<PositionedCylinder> PositionedCylinders
        {
            get { return this.positionedCylinders; }
        }

        /// <summary>
        /// Defines the flywheel unit for this engine.
        /// </summary>
        [DisplayName("Flywheel")]
        [Description("Defines the flywheel unit for this engine.")]
        public Flywheel @Flywheel
        {
            get { return this.flywheel; }
        }

        ///// <summary>
        ///// Defines the cycle of the  If different cycles are defined for each cylinder, value will be n/a. When set, all cylinders within the engine will be set to the same cycle.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Cycle")]
        //[Description("Defines the cycle of the  If different cycles are defined for each cylinder, value will be n/a. When set, all cylinders within the engine will be set to the same cycle.")]
        //public Cycle @Cycle
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            Cycle _cycle = Cycle.NaN;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _cycle = positionedCylinders[a].Cycle;
        //                }
        //                else
        //                {
        //                    if (!positionedCylinders[a].Cycle.Equals(_cycle))
        //                    {
        //                        return Cycle.NaN;
        //                    }
        //                }
        //            }

        //            return _cycle;
        //        }
        //        else
        //        {
        //            return Cycle.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.Cycle = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Defines the bore of the cylinder(s), in milimeters. If different bore is defined for each cylinder, value will be n/a. When set, all cylinders within the engine will be set to the same bore.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Bore")]
        //[Description("Defines the bore of the cylinder(s), in milimeters. If different bore is defined for each cylinder, value will be n/a. When set, all cylinders within the engine will be set to the same bore.")]
        //public double Bore_mm
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _bore = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _bore = positionedCylinders[a].Bore_mm;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].Bore_mm != _bore)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _bore;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.Bore_mm = value;
        //        }

        //        Validate();
        //    }
        //}

        ///// <summary>
        ///// Defines the stroke of the cylinder(s), in milimeters. If different stroke is defined for each cylinder, value will be n/a. When set, all cylinders within the engine will be set to the same stroke.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Stroke")]
        //[Description("Defines the stroke of the cylinder(s), in milimeters. If different stroke is defined for each cylinder, value will be n/a. When set, all cylinders within the engine will be set to the same stroke.")]
        //public double Stroke_mm
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _stroke = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _stroke = positionedCylinders[a].Stroke_mm;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].Stroke_mm != _stroke)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _stroke;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.Stroke_mm = value;
        //        }

        //        Validate();
        //    }
        //}

        ///// <summary>
        ///// Defines the mass of the piston(s), in grams. If different mass is defined for each piston, value will be n/a. When set, all pistons within the engine will be set to the same mass.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Piston mass")]
        //[Description("Defines the mass of the piston(s), in grams. If different mass is defined for each piston, value will be n/a. When set, all pistons within the engine will be set to the same mass.")]
        //public double PistonMass_g
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _mass = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _mass = positionedCylinders[a].Piston.Mass_g;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].Piston.Mass_g != _mass)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _mass;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.Piston.Mass_g = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Defines the rotating mass of the connecting rod(s), in grams. If different mass is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same rotating mass.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Connecting rod rotating mass")]
        //[Description("Defines the rotating mass of the connecting rod(s), in grams. If different mass is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same rotating mass.")]
        //public double ConnectingRodRotatingMass_g
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _mass = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _mass = positionedCylinders[a].ConnectingRod.RotatingMass_g;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].ConnectingRod.RotatingMass_g != _mass)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _mass;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.ConnectingRod.RotatingMass_g = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Defines the rotating mass distance from the center of gravity of the connecting rod(s), in milimeters. If different distance is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same rotating mass distance.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Connecting rod rotating mass distance from CG")]
        //[Description("Defines the rotating mass distance from the center of gravity of the connecting rod(s), in milimeters. If different distance is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same rotating mass distance.")]
        //public double ConnectingRodRotatingMassDistanceFromCenterOfGravity_mm
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _distance = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _distance = positionedCylinders[a].ConnectingRod.RotatingMassDistanceFromCenterOfGravity_mm;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].ConnectingRod.RotatingMassDistanceFromCenterOfGravity_mm != _distance)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _distance;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.ConnectingRod.RotatingMassDistanceFromCenterOfGravity_mm = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Defines the reciprocating mass of the connecting rod(s), in grams. If different mass is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same reciprocating mass.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Connecting rod reciprocating mass")]
        //[Description("Defines the reciprocating mass of the connecting rod(s), in grams. If different mass is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same reciprocating mass.")]
        //public double ConnectingRodReciprocatingMass_g
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _mass = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _mass = positionedCylinders[a].ConnectingRod.ReciprocatingMass_g;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].ConnectingRod.ReciprocatingMass_g != _mass)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _mass;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.ConnectingRod.ReciprocatingMass_g = value;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Defines the reciprocating mass distance from the center of gravity of the connecting rod(s), in milimeters. If different distance is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same reciprocating mass distance.
        ///// </summary>
        //[Category(CATEGORY_COMMON)]
        //[DisplayName("Connecting rod reciprocating mass distance from CG")]
        //[Description("Defines the reciprocating mass distance from the center of gravity of the connecting rod(s), in milimeters. If different distance is defined for each connecting rod, value will be n/a. When set, all connecting rods within the engine will be set to the same reciprocating mass distance.")]
        //public double ConnectingRodReciprocatingMassDistanceFromCenterOfGravity_mm
        //{
        //    get
        //    {
        //        if (positionedCylinders.Count > 0)
        //        {
        //            double _distance = -1;
        //            for (int a = 0; a < positionedCylinders.Count; a++)
        //            {
        //                if (a == 0)
        //                {
        //                    _distance = positionedCylinders[a].ConnectingRod.ReciprocatingMassDistanceFromCenterOfGravity_mm;
        //                }
        //                else
        //                {
        //                    if (positionedCylinders[a].ConnectingRod.ReciprocatingMassDistanceFromCenterOfGravity_mm != _distance)
        //                    {
        //                        return double.NaN;
        //                    }
        //                }
        //            }

        //            return _distance;
        //        }
        //        else
        //        {
        //            return double.NaN;
        //        }
        //    }

        //    set
        //    {
        //        foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //        {
        //            _positionedCylinderAsReference.ConnectingRod.ReciprocatingMassDistanceFromCenterOfGravity_mm = value;
        //        }
        //    }
        //}

        /// <summary>
        /// Indicates the engine's total displacement, in cubic centimeters.
        /// </summary>
        [DisplayName("Total displacement")]
        [Description("Indicates the engine's total displacement, in cubic centimeters.")]
        [ReadOnly(true)]
        public double TotalDisplacement_cm3
        {
            get
            {
                double _double = 0;
                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    _double += _positionedCylinder.Displacement_cm3;
                }
                return _double;
            }
        }

        /// <summary>
        /// Indicates the engine's crank throws angles (resulting from defined cylinder setups), in degrees per array position, where the array position is the cylinder number (0 = first).
        /// </summary>
        [DisplayName("Crank throws")]
        [Description("Indicates the engine's crank throws angles (resulting from defined cylinder setups), in degrees per array position, where the array position is the cylinder number (0 = first).")]
        [ReadOnly(true)]
        [TypeConverter(typeof(CrankThrowsConverter))]
        public double[] CrankThrows_deg
        {
            get
            {
                List<double> _crankThrows_deg = new List<double>();
                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    _crankThrows_deg.Add(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(_positionedCylinder.GetAbsoluteCrankThrowRotation_deg(0), true));
                }

                return _crankThrows_deg.ToArray();
            }
        }

        /// <summary>
        /// Indicates the engine's firing order, in cylinder positions (resulting from defined cylinder setups).
        /// </summary>
        [DisplayName("Firing order")]
        [Description("Indicates the engine's firing order, in cylinder positions.")]
        [ReadOnly(true)]
        public string FiringOrder
        {
            get
            {
                List<PositionedCylinder> _list = positionedCylinders.ToList();
                _list.Sort(delegate(PositionedCylinder x, PositionedCylinder y)
                {
                    if (x.FiringAngle_deg > y.FiringAngle_deg)
                    {
                        return 1;
                    }
                    else if (x.FiringAngle_deg < y.FiringAngle_deg)
                    {
                        return -1;
                    }
                    else
                    {
                        if (x.Position > y.Position)
                        {
                            return 1;
                        }
                        else if (x.Position < y.Position)
                        {
                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                });


                StringBuilder _stringBuilder = new StringBuilder();
                for (int a = 0; a < _list.Count; a++)
                {
                    if (a == 0)
                    {
                        _stringBuilder.Append("[");
                    }

                    _stringBuilder.Append(_list[a].Position);

                    if (a < _list.Count - 1)
                    {
                        if (_list[a + 1].FiringAngle_deg == _list[a].FiringAngle_deg)
                        {
                            _stringBuilder.Append(", ");
                        }
                        else
                        {
                            _stringBuilder.Append("] - [");
                        }
                    }
                    else
                    {
                        _stringBuilder.Append("]");
                    }
                }


                return _stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Indicates the engine's firing angles (resulting from defined cylinder setups), in degrees per array position, where the array position is the cylinder number (0 = first).
        /// </summary>
        [DisplayName("Firing angles")]
        [Description("Indicates the engine's firing angles (resulting from defined cylinder setups), in degrees per array position, where the array position is the cylinder number (0 = first).")]
        [ReadOnly(true)]
        [TypeConverter(typeof(FiringAnglesConverter))]
        public double[] FiringAngles_deg
        {
            get
            {
                List<double> _firingAngles = new List<double>();
                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    if (!_positionedCylinder.Cycle .Equals(Cycle.NaN))
                    {
                        _firingAngles.Add(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(
                            _positionedCylinder.FiringAngle_deg,
                            _positionedCylinder.Cycle.RevolutionsToCompleteCycle));
                    }
                    else
                    {
                        _firingAngles.Add(EngineDesigner.Common.Mathematics.GetAbsoluteAngle_deg(
                            _positionedCylinder.FiringAngle_deg));
                    }
                }


                //OBSOLETE:
                //#region "popravimo, da so koti absolutni - torej, ni negativnih in ni več kot 360"
                ////poiščemo najmanjšega-negativnega
                //double _double = 0;
                //foreach (double _firingAngle in _firingAngles)
                //{
                //    if ((_firingAngle < 0)
                //        && (_firingAngle < _double))
                //    {
                //        _double = _firingAngle;
                //    }
                //}

                ////povečamo vse za najmanjšega
                //if (_double < 0)
                //{
                //    _double = Math.Abs(_double);

                //    for (int a = 0; a < _firingAngles.Count; a++)
                //    {
                //        _firingAngles[a] += _double;
                //    }
                //}
                //#endregion


                return _firingAngles.ToArray();
            }
        }

        /// <summary>
        /// Indicates the total number of cylinders in this engine (resulting from defined cylinder setups).
        /// </summary>
        [DisplayName("Number of cylinders")]
        [Description("Indicates the total number of cylinders in this engine (resulting from defined positioned cylinders).")]
        [ReadOnly(true)]
        public int NumberOfCylinders
        {
            get
            {
                return positionedCylinders.Count;
            }
        }

        /// <summary>
        /// Indicates the duration, in degrees, of the engine's combined cycle (important when an engine has cylinder with different cycles).
        /// </summary>
        [DisplayName("Combined cycle duration")]
        [Description("Indicates the duration, in degrees, of the engine's combined cycle (important when an engine has cylinder with different cycles).")]
        [ReadOnly(true)]
        public double CombinedCycleDuration_deg
        {
            get
            {
                double _double = 0d;

                foreach (Cylinder _cylinder in this.positionedCylinders)
                {
                    if (_cylinder.Cycle.Duration_deg > _double)
                    {
                        _double = _cylinder.Cycle.Duration_deg;
                    }
                }

                return _double;
            }
        }

        /// <summary>
        /// Indicates the number of revolutions needed to complete the engine's combined cycle.
        /// </summary>
        [DisplayName("Revolutions to complete combined cycle")]
        [Description("Indicates the number of revolutions needed to complete the engine's combined cycle.")]
        [ReadOnly(true)]
        public int RevolutionsToCompleteCombinedCycle
        {
            get { return (int)EngineDesigner.Common.Mathematics.GetFullRotations(CombinedCycleDuration_deg); }
        }


        /// <summary>
        /// dolžina motorja, brez vztrajnika
        /// </summary>
        [Browsable(false)]
        public double Length_mm
        {
            get
            {
                double _minZ = 0; //najmanjša dolžina (če je negativna, se upošteva)
                PositionedCylinder _firstPositionedCylinder = this.GetFirstPositionedCylinder();
                if (_firstPositionedCylinder != null)
                {
                    _minZ = -(_firstPositionedCylinder.Offset_mm + _firstPositionedCylinder.Bore_mm / 2d);
                }

                double _maxZ = 0; //največja dolžina
                PositionedCylinder _lastPositionedCylinder = this.GetLastPositionedCylinder();
                if (_lastPositionedCylinder != null)
                {
                    _maxZ = (_lastPositionedCylinder.Offset_mm + _lastPositionedCylinder.Bore_mm / 2d);
                }


                if (_minZ < 0)
                {
                    return Math.Abs(_maxZ + Math.Abs(_minZ));
                }
                else
                {
                    //NOTE: to je po novem, vendar ne vem, če je uredu za vse IParte, ma očitno samo za tega, ker ostali vsi začnejo z 0 ali pa v negativi
                    return Math.Abs(_maxZ) - Math.Abs(_minZ);
                    //return Math.Abs(_maxLength);
                }
            }
        }

        #endregion "Public properties"



        #region "Mates"
        public PositionedCylinder[][] GetMatedPositionedCylinders()
        {
            List<PositionedCylinder[]> _allMatedPositionedCylinders = new List<PositionedCylinder[]>();


            foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
            {
                bool _skipCylinder = false;
                #region "ugotovimo ali smo ta cilinder že obravnavali"
                foreach (PositionedCylinder[] _positionedCylindersTmp in _allMatedPositionedCylinders)
                {
                    foreach (PositionedCylinder _positionedCylinderTmp in _positionedCylindersTmp)
                    {
                        if (_positionedCylinder == _positionedCylinderTmp)
                        {
                            _skipCylinder = true;
                            break;
                        }
                    }

                    if (_skipCylinder)
                    {
                        break;
                    }
                }
                #endregion "ugotovimo ali smo ta cilinder že obravnavali"

                if (!_skipCylinder)
                {
                    List<PositionedCylinder> _matedPositionedCylinders = new List<PositionedCylinder>();
                    this.GetMatedPositionedCylindersRecursive(_positionedCylinder, ref _matedPositionedCylinders);

                    //če je count 1, pomeni, da ni matanih
                    if (_matedPositionedCylinders.Count > 1)
                    {
                        _allMatedPositionedCylinders.Add(_matedPositionedCylinders.ToArray());
                    }
                }
            }

            return _allMatedPositionedCylinders.ToArray();
        }
        private void GetMatedPositionedCylindersRecursive(PositionedCylinder _positionedCylinderAsReference, ref List<PositionedCylinder> _matedPositionedCylinders)
        {
            _matedPositionedCylinders.Add(_positionedCylinderAsReference);

            foreach (PositionedCylinder _positionedCylinderTmp in this.positionedCylinders)
            {
                if (_positionedCylinderTmp.Position > _positionedCylinderAsReference.Position)
                {
                    if (_positionedCylinderAsReference.IsMatedWith(_positionedCylinderTmp))
                    {
                        this.GetMatedPositionedCylindersRecursive(_positionedCylinderTmp, ref _matedPositionedCylinders);
                    }
                }
            }
        }
        public PositionedCylinder[] GetNonMatedPositionedCylinders()
        {
            List<PositionedCylinder> _allMatedPositionedCylinders = new List<PositionedCylinder>();
            foreach (PositionedCylinder[] _matedPositionedCylinders in this.GetMatedPositionedCylinders())
            {
                _allMatedPositionedCylinders.AddRange(_matedPositionedCylinders);
            }


            List<PositionedCylinder> _nonMatedPositionedCylinders = new List<PositionedCylinder>();
            foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
            {
                if (!_allMatedPositionedCylinders.Contains(_positionedCylinder))
                {
                    _nonMatedPositionedCylinders.Add(_positionedCylinder);
                }
            }


            return _nonMatedPositionedCylinders.ToArray();
        }
        public bool IsPositionedCylinderMated(PositionedCylinder _positionedCylinder)
        {
            PositionedCylinder[][] _allMatedPositionedCylinders = this.GetMatedPositionedCylinders();
            foreach (PositionedCylinder[] _matedPositionedCylinders in _allMatedPositionedCylinders)
            {
                if (_matedPositionedCylinders.Contains(_positionedCylinder))
                {
                    return true;
                }
            }


            return false;
        }
        public PositionedCylinder[] GetMatedPositionedCylindersOf(PositionedCylinder _positionedCylinder)
        {
            PositionedCylinder[][] _allMatedPositionedCylinders = this.GetMatedPositionedCylinders();
            foreach (PositionedCylinder[] _matedPositionedCylinders in _allMatedPositionedCylinders)
            {
                if (_matedPositionedCylinders.Contains(_positionedCylinder))
                {
                    return _matedPositionedCylinders;
                }
            }


            return null;
        }

        public PositionedCylinder GetFirstPositionedCylinder()
        {
            return this.GetFirstPositionedCylinder(this.positionedCylinders.ToArray());
        }
        public PositionedCylinder GetFirstPositionedCylinder(PositionedCylinder[] _positionedCylinders)
        {
            double _currentOffset = double.MaxValue;
            PositionedCylinder _currentPositionedCylinder = null;

            foreach (PositionedCylinder _positionedCylinder in _positionedCylinders)
            {
                if (_positionedCylinder.Offset_mm < _currentOffset)
                {
                    _currentPositionedCylinder = _positionedCylinder;
                    _currentOffset = _positionedCylinder.Offset_mm;
                }
            }

            return _currentPositionedCylinder;
        }
        public PositionedCylinder GetLastPositionedCylinder()
        {
            return this.GetLastPositionedCylinder(this.positionedCylinders.ToArray());
        }
        public PositionedCylinder GetLastPositionedCylinder(PositionedCylinder[] _positionedCylinders)
        {
            double _currentOffset = double.MinValue;
            PositionedCylinder _currentPositionedCylinder = null;

            foreach (PositionedCylinder _positionedCylinder in _positionedCylinders)
            {
                if (_positionedCylinder.Offset_mm >= _currentOffset)
                {
                    _currentPositionedCylinder = _positionedCylinder;
                    _currentOffset = _positionedCylinder.Offset_mm;
                }
            }

            return _currentPositionedCylinder;
        }
        public PositionedCylinder GetNextPositionedCylinder(PositionedCylinder _currentPositionedCylinder)
        {
            return this.GetNextPositionedCylinder(this.positionedCylinders.ToArray(), _currentPositionedCylinder);
        }
        public PositionedCylinder GetNextPositionedCylinder(PositionedCylinder[] _positionedCylinders, PositionedCylinder _currentPositionedCylinder)
        {
            //damo v sorted dictionary, da je sortirano po poziciji
            SortedDictionary<int, PositionedCylinder> _crankThrows = new SortedDictionary<int, PositionedCylinder>();
            foreach (PositionedCylinder _positionedCylinder in _positionedCylinders)
            {
                _crankThrows.Add(_positionedCylinder.Position, _positionedCylinder);
            }

            foreach (PositionedCylinder _positionedCylinder in _crankThrows.Values)
            {
                if (_positionedCylinder.Offset_mm > _currentPositionedCylinder.Offset_mm)
                {
                    return _positionedCylinder;
                }
            }

            return null;
        }
        public PositionedCylinder GetPreviousPositionedCylinder(PositionedCylinder _currentPositionedCylinder)
        {
            return this.GetPreviousPositionedCylinder(this.positionedCylinders.ToArray(), _currentPositionedCylinder);
        }
        public PositionedCylinder GetPreviousPositionedCylinder(PositionedCylinder[] _positionedCylinders, PositionedCylinder _currentPositionedCylinder)
        {
            //damo v sorted dictionary, da je sortirano po poziciji
            SortedDictionary<int, PositionedCylinder> _crankThrows = new SortedDictionary<int, PositionedCylinder>();
            foreach (PositionedCylinder _positionedCylinder in _positionedCylinders)
            {
                _crankThrows.Add(_positionedCylinder.Position, _positionedCylinder);
            }

            foreach (KeyValuePair<int, PositionedCylinder> _keyValuePair in _crankThrows.Reverse())
            {
                PositionedCylinder _positionedCylinder = _keyValuePair.Value;

                if (_positionedCylinder.Offset_mm < _currentPositionedCylinder.Offset_mm)
                {
                    return _positionedCylinder;
                }
            }

            return null;
        }
        #endregion "Mates"



        //public double GetGasPressureForceX_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _double = 0;

        //    foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //    {
        //        double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
        //        _double += _positionedCylinder.GetGasPressureForceX_N(_cylinderRelativeCrankThrowRotation_deg, _rpm);
        //    }

        //    return _double;
        //}
        //public double GetGasPressureForceY_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _double = 0;

        //    foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //    {
        //        double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
        //        _double += _positionedCylinder.GetGasPressureForceY_N(_cylinderRelativeCrankThrowRotation_deg, _rpm);
        //    }

        //    return _double;
        //}
        //public double GetInertiaForceReciprocatingX_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _double = 0;

        //    foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //    {
        //        double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
        //        _double += _positionedCylinder.GetInertiaForceReciprocatingX_N(_cylinderRelativeCrankThrowRotation_deg, _rpm);
        //    }

        //    return _double;
        //}
        //public double GetInertiaForceReciprocatingY_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _double = 0;

        //    foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //    {
        //        double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
        //        _double += _positionedCylinder.GetInertiaForceReciprocatingY_N(_cylinderRelativeCrankThrowRotation_deg, _rpm);
        //    }

        //    return _double;
        //}
        //public double GetInertiaForceX_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _double = 0;

        //    foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //    {
        //        double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
        //        _double += _positionedCylinder.GetInertiaForceX_N(_cylinderRelativeCrankThrowRotation_deg, _rpm);
        //    }

        //    return _double;
        //}
        //public double GetInertiaForceY_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _double = 0;

        //    foreach (PositionedCylinder _positionedCylinder in this.positionedCylinders)
        //    {
        //        double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
        //        _double += _positionedCylinder.GetInertiaForceY_N(_cylinderRelativeCrankThrowRotation_deg, _rpm);
        //    }

        //    return _double;
        //}


        //public double GetUnbalancedForceSummationOnXaxis_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _fX = 0;
        //    foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //    {
        //        _fX += _positionedCylinderAsReference.GetForceOnPistonAboutXaxis_N(_crankshaftRotation_deg, _rpm);
        //    }

        //    return _fX;
        //}
        //public double GetUnbalancedForceSummationOnXaxis_N(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _fX = 0;
        //    foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //    {
        //        _fX += _positionedCylinderAsReference.GetForceOnPistonAboutXaxis_N(_crankshaftRotation_deg, _rpm, _orders);
        //    }

        //    return _fX;
        //}
        //public double GetUnbalancedForceSummationOnYaxis_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _fY = 0;
        //    foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //    {
        //        _fY += _positionedCylinderAsReference.GetForceOnPistonAboutYaxis_N(_crankshaftRotation_deg, _rpm);
        //    }


        //    return _fY;
        //}
        //public double GetUnbalancedForceSummationOnYaxis_N(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _fY = 0;
        //    foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //    {
        //        _fY += _positionedCylinderAsReference.GetForceOnPistonAboutYaxis_N(_crankshaftRotation_deg, _rpm, _orders);
        //    }


        //    return _fY;
        //}
        //public double GetUnbalancedForceSummation_N(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _fSum = Math.Sqrt(
        //        Math.Pow(GetUnbalancedForceSummationOnXaxis_N(_crankshaftRotation_deg, _rpm, _orders), 2)
        //        + Math.Pow(GetUnbalancedForceSummationOnYaxis_N(_crankshaftRotation_deg, _rpm, _orders), 2));


        //    return _fSum;
        //}
        //public double GetUnbalancedForceSummation_N(double _crankshaftRotation_deg, double _rpm)
        //{
        //    double _fSum = Math.Sqrt(
        //        Math.Pow(GetUnbalancedForceSummationOnXaxis_N(_crankshaftRotation_deg, _rpm), 2)
        //        + Math.Pow(GetUnbalancedForceSummationOnYaxis_N(_crankshaftRotation_deg, _rpm), 2));


        //    return _fSum;
        //}

        //public double GetUnbalancedMomentSummationOnXaxis_Nm(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _mX = 0;
        //    foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //    {
        //        _mX += _positionedCylinderAsReference.GetMomentOnPistonAboutXaxis_Nm(_crankshaftRotation_deg, _rpm, _orders);
        //    }

        //    return _mX;
        //}
        //public double GetUnbalancedMomentSummationOnYaxis_Nm(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _mY = 0;
        //    foreach (PositionedCylinder _positionedCylinderAsReference in positionedCylinders)
        //    {
        //        _mY += _positionedCylinderAsReference.GetMomentOnPistonAboutYaxis_Nm(_crankshaftRotation_deg, _rpm, _orders);
        //    }


        //    return _mY;
        //}
        //public double GetUnbalancedMomentSummation_Nm(double _crankshaftRotation_deg, double _rpm, params uint[] _orders)
        //{
        //    double _mSum = Math.Sqrt(
        //        Math.Pow(GetUnbalancedMomentSummationOnXaxis_Nm(_crankshaftRotation_deg, _rpm, _orders), 2)
        //        + Math.Pow(GetUnbalancedMomentSummationOnYaxis_Nm(_crankshaftRotation_deg, _rpm, _orders), 2));


        //    return _mSum;
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
                    //NOTE: to je po novem, vendar ne vem, če je uredu za vse IParte, ma očitno samo za tega, ker ostali vsi začnejo z 0 ali pa v negativi
                    return Math.Abs(_maxLength) - Math.Abs(_minLength);
                    //return Math.Abs(_maxLength);
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
                double _minX = 0; //več kot 0, ne more bit, ker motor začne v centru, torej 0

                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    double tilt_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
                    double _cylinderX = Math.Sin(tilt_rad) * _positionedCylinder.GetPhysicalHeightAbovePiston_mm(0d);
                    if (_cylinderX <= 0)
                    {
                        //koliko vrtina pripomore k širini
                        _cylinderX -= Math.Abs(Math.Cos(tilt_rad) * (_positionedCylinder.Bore_mm / 2));
                    }

                    //če je _double od cilindra pozitiven ali pa ni "dovolj negativen", potem ročica vpliva na širino motojra
                    if ((_cylinderX > 0)
                        || ((Math.Abs(_cylinderX) < _positionedCylinder.CrankThrow.CrankRotationRadius_mm)))
                    {
                        _cylinderX = -_positionedCylinder.CrankThrow.CrankRotationRadius_mm;
                    }


                    if (_cylinderX < _minX)
                    {
                        _minX = _cylinderX;
                    }
                }

                if (this.flywheel.Mass_g > 0)
                {
                    double _flywheelDiameterBound = -(this.flywheel.Diameter_mm / 2d);

                    if (_flywheelDiameterBound < _minX)
                    {
                        _minX = _flywheelDiameterBound;
                    }
                }

                return _minX;
            }
        }

        [Browsable(false)]
        public double Bound_Y_Min
        {
            get
            {
                double _minY = 0; //več kot 0, ne more bit, ker motor začne v centru, torej 0

                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    //pozicija cilindra
                    double _tilt_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
                    double _cylinderY = Math.Cos(_tilt_rad) * _positionedCylinder.GetPhysicalHeightAbovePiston_mm(0);
                    if (_cylinderY <= 0)
                    {
                        //koliko vrtina pripomore k višini
                        _cylinderY -= Math.Abs(Math.Sin(_tilt_rad) * (_positionedCylinder.Bore_mm / 2));
                    }

                    //če je y od cilindra pozitiven ali pa ni "dovolj negativen", potem ročica vpliva na višino motojra
                    if ((_cylinderY > 0)
                        || ((Math.Abs(_cylinderY) < _positionedCylinder.CrankThrow.CrankRotationRadius_mm)))
                    {
                        _cylinderY = -_positionedCylinder.CrankThrow.CrankRotationRadius_mm;
                    }


                    if (_cylinderY < _minY)
                    {
                        _minY = _cylinderY;
                    }
                }

                if (this.flywheel.Mass_g > 0)
                {
                    double _flywheelDiameterBound = -(this.flywheel.Diameter_mm / 2d);

                    if (_flywheelDiameterBound < _minY)
                    {
                        _minY = _flywheelDiameterBound;
                    }
                }

                return _minY;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Min
        {
            get
            {
                double _minZ = 0d;


                PositionedCylinder _firstPositionedCylinder = this.GetFirstPositionedCylinder();
                if (_firstPositionedCylinder != null)
                {
                    _minZ = -(_firstPositionedCylinder.Offset_mm + _firstPositionedCylinder.Bore_mm / 2d);
                }

                if (this.flywheel.Mass_g > 0)
                {
                    _minZ -= this.flywheel.Length;
                }


                return _minZ;
            }
        }

        [Browsable(false)]
        public double Bound_X_Max
        {
            get
            {
                double _maxX = 0; //manj kot 0, ne more bit, ker motor začne v centru, torej 0

                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    double _tilt_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
                    double _cylinderX = Math.Sin(_tilt_rad) * _positionedCylinder.GetPhysicalHeightAbovePiston_mm(0d);
                    if (_cylinderX >= 0)
                    {
                        //koliko vrtina pripomore k širini
                        _cylinderX += Math.Abs(Math.Cos(_tilt_rad) * (_positionedCylinder.Bore_mm / 2));
                    }

                    //če je y od cilindra negativen ali pa ni "dovolj pozitiven", potem ročica vpliva na širino motojra
                    if ((_cylinderX < 0)
                        || ((Math.Abs(_cylinderX) < _positionedCylinder.CrankThrow.CrankRotationRadius_mm)))
                    {
                        _cylinderX = _positionedCylinder.CrankThrow.CrankRotationRadius_mm;
                    }


                    if (_cylinderX > _maxX)
                    {
                        _maxX = _cylinderX;
                    }
                }

                if (this.flywheel.Mass_g > 0)
                {
                    double _flywheelDiameterBound = (this.flywheel.Diameter_mm / 2d);

                    if (_flywheelDiameterBound > _maxX)
                    {
                        _maxX = _flywheelDiameterBound;
                    }
                }

                return _maxX;
            }
        }

        [Browsable(false)]
        public double Bound_Y_Max
        {
            get
            {
                double _maxY = 0; //manj kot 0, ne more bit, ker motor začne v centru, torej 0

                foreach (PositionedCylinder _positionedCylinder in positionedCylinders)
                {
                    double _tilt_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
                    double _cylinderY = Math.Cos(_tilt_rad) * _positionedCylinder.GetPhysicalHeightAbovePiston_mm(0d);
                    if (_cylinderY >= 0)
                    {
                        //koliko vrtina pripomore k višini
                        _cylinderY += Math.Abs(Math.Sin(_tilt_rad) * (_positionedCylinder.Bore_mm / 2));
                    }

                    //če je y od cilindra negativen ali pa ni "dovolj pozitiven", potem ročica vpliva na višino motojra
                    if ((_cylinderY < 0)
                        || ((Math.Abs(_cylinderY) < _positionedCylinder.CrankThrow.CrankRotationRadius_mm)))
                    {
                        _cylinderY = _positionedCylinder.CrankThrow.CrankRotationRadius_mm;
                    }


                    if (_cylinderY > _maxY)
                    {
                        _maxY = _cylinderY;
                    }
                }

                if (this.flywheel.Mass_g > 0)
                {
                    double _flywheelDiameterBound = (this.flywheel.Diameter_mm / 2d);

                    if (_flywheelDiameterBound > _maxY)
                    {
                        _maxY = _flywheelDiameterBound;
                    }
                }

                return _maxY;
            }
        }

        [Browsable(false)]
        public double Bound_Z_Max
        {
            get
            {
                double _maxZ = 0d;


                PositionedCylinder _lastPositionedCylinder = this.GetLastPositionedCylinder();
                if (_lastPositionedCylinder != null)
                {
                    _maxZ = (_lastPositionedCylinder.Offset_mm + _lastPositionedCylinder.Bore_mm / 2d);
                }


                return _maxZ;
            }
        }


        public void Validate()
        {
            List<PositionedCylinder> _positionedCylindersToBeValidated = new List<PositionedCylinder>();
            _positionedCylindersToBeValidated.AddRange(positionedCylinders);

            if (_positionedCylindersToBeValidated.Count > 0)
            {
                CustomList<PositionedCylinder> _positionedCylindersTmp = new CustomList<PositionedCylinder>(this.positionedCylinders.AllowDuplicates);


                #region "pogledamo, če je tak setup sploh fizično možen in dodamo cilindre"
                foreach (PositionedCylinder _positionedCylinder in _positionedCylindersToBeValidated)
                {
                    CheckPositionedCylinder(_positionedCylinder, _positionedCylindersTmp);

                    //če je, ga dodamo
                    _positionedCylindersTmp.Add(_positionedCylinder);
                }
                #endregion "pogledamo, če je tak setup sploh fizično možen in dodamo cilindre"

                #region "pogledamo, če si pozicije sledijo"
                //najprej sortiramo po vrsti
                List<double> _positions = new List<double>();
                foreach (PositionedCylinder _positionedCylinder in _positionedCylindersTmp)
                {
                    _positions.Add(_positionedCylinder.Position);
                }
                EngineDesigner.Common.Mathematics.BubbleSort<double>(ref _positions);

                //če je minimum 1...
                if (EngineDesigner.Common.Mathematics.GetMin(_positions) != 1)
                {
                    throw new ValidationException("No cylinder positioned as 1 found.");
                }
                //...in je maksimum št. cilindrov...
                if (EngineDesigner.Common.Mathematics.GetMax(_positions) != _positions.Count)
                {
                    throw new ValidationException(string.Format(
                        "The last cylinder position is not the same as the number of cylinders ({0}).",
                        _positions.Count.ToString()));
                }
                //...in si cilindri sledijo po vrsti...
                for (int a = 0; a < _positions.Count - 1; a++)
                {
                    if (_positions[a + 1] - _positions[a] != 1)
                    {
                        throw new ValidationException("Cylinder positions are not in sorted order (1, 2, 3, n).");
                    }
                }
                #endregion "pogledamo, če si pozicije sledijo"


                #region "če je vse ok, uporabimo _positionedCylindersTmp in ga sortiranega damo v positionedCylinders"
                positionedCylinders.Clear();
                foreach (double _position in _positions)
                {
                    foreach (PositionedCylinder _positionedCylinder in _positionedCylindersTmp)
                    {
                        if (_positionedCylinder.Position == _position)
                        {
                            positionedCylinders.Add(_positionedCylinder);
                            break;
                        }
                    }
                }
                #endregion "če je vse ok, uporabimo _positionedCylindersTmp in ga sortiranega damo v positionedCylinders"
            }


            this.OnValidated();
        }
        private void CheckPositionedCylinder(PositionedCylinder _positionedCylinderToAdd, IEnumerable<PositionedCylinder> _positionedCylindersToCheck)
        {
            foreach (PositionedCylinder _positionedCylinder in _positionedCylindersToCheck)
            {
                #region "Offset_mm"
                if (_positionedCylinderToAdd.Offset_mm < _positionedCylinder.Offset_mm)
                {
                    throw new ValidationException("A cylinder's longitudinal offset can not be less than the longitudinal offset of the previous cylinder.");
                }
                #endregion "Offset_mm"

                #region "Intersection glede na offset"
                Exception _exception;
                if (_positionedCylinder.IsIntersectingWith(_positionedCylinderToAdd, out _exception))
                {
                    throw _exception;
                }
                #endregion "Intersection glede na offset"
            }
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


        //NOTE: naj bo to zaenkrat disablano, ker povzroča preveč težav
        //private void PositionedCylinder_Validated(IPart _iPart)
        //{
        //    this.Validate();
        //}

    }


    /// <summary>
    /// Provides UI integration for Engine class.
    /// </summary>
    internal class EngineConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Engine))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Engine))
            {
                Engine _engine = (Engine)value;
                return _engine.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }


    /// <summary>
    /// Provides UI integration for CrankThrows_deg property.
    /// </summary>
    internal class CrankThrowsConverter : ArrayConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(double[]))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is double[]))
            {
                return "...";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }


    /// <summary>
    /// Provides UI integration for FiringAngles_deg property.
    /// </summary>
    internal class FiringAnglesConverter : ArrayConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(double[]))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is double[]))
            {
                return "...";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }


    /// <summary>
    /// Provides UI integration for PositionedCylinders property.
    /// </summary>
    internal class PositionedCylindersConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is CustomList<PositionedCylinder>))
            {
                return "...";
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }


    /// <summary>
    /// Provides UI editor for PositionedCylinders property.
    /// </summary>
    internal class PositionedCylindersEditor : CustomListEditor
    {
        public PositionedCylindersEditor()
            : base("Cylinders and positioning", typeof(PositionedCylinder))
        {
        }


        protected override ICustomCollectionElement OnItemAdd(ICustomCollectionElement _itemToAdd, IList _currentList)
        {
            //omogočimo nastavljanje positiona
            PropertyDescriptor _propertyDescriptor = TypeDescriptor.GetProperties(typeof(PositionedCylinder))["Position"];
            ReadOnlyAttribute _readOnlyAttribute = (ReadOnlyAttribute)_propertyDescriptor.Attributes[typeof(ReadOnlyAttribute)];
            System.Reflection.FieldInfo _fieldToChange = _readOnlyAttribute.GetType().GetField("isReadOnly",
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);
            _fieldToChange.SetValue(_readOnlyAttribute, false);



            PositionedCylinder _positionedCylinder = PositionedCylinder.DefaultPositionedCylinder;

            //position je kar ++
            _positionedCylinder.Position = _currentList.Count + 1;

            //offset je zadnji offset + vrtina cilindra
            if (_currentList.Count > 0)
            {
                PositionedCylinder _positionedCylinderTmp = (PositionedCylinder)_currentList[_currentList.Count - 1];
                _positionedCylinder.Offset_mm = _positionedCylinderTmp.Offset_mm + _positionedCylinder.Bore_mm;
            }
            else
            {
                _positionedCylinder.Offset_mm = 0d;
            }


            return _positionedCylinder;
        }
        protected override void OnListValidate(ref IList _listToValidate, ref Exception _exceptionWhileValidating, ref string _messageToDisplay)
        {
            List<PositionedCylinder> _list = new List<PositionedCylinder>();
            foreach (PositionedCylinder _positionedCylinder in _listToValidate)
            {
                _list.Add(_positionedCylinder);
            }

            try
            {
                //provamo naredit engine...
                Engine _engine = new Engine(_list.ToArray());
            }
            catch (ValidationException _machineException)
            {
                System.Diagnostics.Debug.WriteLine(_machineException.Message);

                _exceptionWhileValidating = null;
                _messageToDisplay = _machineException.Message;
            }
            catch (Exception _exception)
            {
                System.Diagnostics.Debug.WriteLine(_exception.Message);

                _exceptionWhileValidating = _exception;
                _messageToDisplay = null;
            }
        }

        protected override void OnEditorLoading(object _sender, EventArgs _eventArgs)
        {
            {
                //omogočimo nastavljanje positiona
                PropertyDescriptor _propertyDescriptor = TypeDescriptor.GetProperties(typeof(PositionedCylinder))["Position"];
                ReadOnlyAttribute _readOnlyAttribute = (ReadOnlyAttribute)_propertyDescriptor.Attributes[typeof(ReadOnlyAttribute)];
                FieldInfo _fieldInfo = _readOnlyAttribute.GetType().GetField(
                    "isReadOnly",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                _fieldInfo.SetValue(_readOnlyAttribute, false);
            }


            base.OnEditorLoading(_sender, _eventArgs);
        }
        protected override void OnEditorClosing(object _sender, EventArgs _eventArgs)
        {
            //spet onemogočimo nastavljanje positiona
            PropertyDescriptor _propertyDescriptor = TypeDescriptor.GetProperties(typeof(PositionedCylinder))["Position"];
            ReadOnlyAttribute _readOnlyAttribute = (ReadOnlyAttribute)_propertyDescriptor.Attributes[typeof(ReadOnlyAttribute)];
            FieldInfo _fieldInfo = _readOnlyAttribute.GetType().GetField(
                "isReadOnly",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            _fieldInfo.SetValue(_readOnlyAttribute, true);


            base.OnEditorClosing(_sender, _eventArgs);
        }

    }

}
