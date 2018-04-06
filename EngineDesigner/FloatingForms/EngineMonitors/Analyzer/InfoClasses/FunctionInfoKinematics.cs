using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EngineDesigner.Machine;
using System.Runtime.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class FunctionInfoKinematic : FunctionInfoReference
    {
        protected FunctionInfoKinematic(string _name, bool _requiresHarmonicOrder)
            : base(_name)
        {
            this.requiresHarmonicOrder = _requiresHarmonicOrder;
        }
        public override string ToLongString()
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.Append(base.ToLongString());

            if (this.positionedCylinder != null)
            {
                _stringBuilder.Append("; ");
                _stringBuilder.Append(this.positionedCylinder.ToString());
            }
            if (this.harmonicOrder != null)
            {
                _stringBuilder.Append("; ");
                _stringBuilder.Append(this.harmonicOrder.Name);
            }
            if (this.cylinderRelative)
            {
                _stringBuilder.Append("; ");
                _stringBuilder.Append("Cylinder relative");
            }

            return _stringBuilder.ToString();
        }



        [DataMember]
        private PositionedCylinder positionedCylinder;
        public PositionedCylinder PositionedCylinder
        {
            set { positionedCylinder = value; }
            get { return positionedCylinder; }
        }

        [DataMember]
        private HarmonicOrderInfo harmonicOrder;
        public HarmonicOrderInfo HarmonicOrder
        {
            set { harmonicOrder = value; }
            get { return harmonicOrder; }
        }

        [DataMember]
        private bool requiresHarmonicOrder;
        public bool RequiresHarmonicOrder
        {
            get { return requiresHarmonicOrder; }
        }

        [DataMember]
        private bool cylinderRelative;
        public bool CylinderRelative
        {
            set { cylinderRelative = value; }
            get { return cylinderRelative; }
        }



        public static FunctionInfoKinematic PistonTravelFromCrankCenter_mm
        {
            get
            {
                return new FunctionInfoKinematic("Piston travel from crank center (mm)", true);
            }
        }
        public static FunctionInfoKinematic PistonVelocity_mpdeg
        {
            get
            {
                return new FunctionInfoKinematic("Piston velocity (m/deg)", true);
            }
        }
        public static FunctionInfoKinematic PistonVelocity_mps
        {
            get
            {
                return new FunctionInfoKinematic("Piston velocity (m/s)", true);
            }
        }
        public static FunctionInfoKinematic PistonAcceleration_mpdeg2
        {
            get
            {
                return new FunctionInfoKinematic("Piston acceleration (m/deg^2)", true);
            }
        }
        public static FunctionInfoKinematic PistonAcceleration_mps2
        {
            get
            {
                return new FunctionInfoKinematic("Piston acceleration (m/s^2)", true);
            }
        }



        public new static FunctionInfoKinematic[] GetAvailableFunctions()
        {
            PropertyInfo[] _propertyInfos = typeof(FunctionInfoKinematic).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            List<FunctionInfoKinematic> _functionInfos = new List<FunctionInfoKinematic>();

            foreach (PropertyInfo _propertyInfo in _propertyInfos)
            {
                _functionInfos.Add((FunctionInfoKinematic)_propertyInfo.GetValue(null, null));
            }

            return _functionInfos.ToArray();
        }

    }

}
