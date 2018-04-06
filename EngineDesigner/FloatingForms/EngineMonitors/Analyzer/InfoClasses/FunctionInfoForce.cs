using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using System.Runtime.Serialization;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    [Serializable] //mora bit, ker uporabljam binary serializer za copy object
    [DataContract]
    internal class FunctionInfoForce : FunctionInfoMoment
    {
        protected FunctionInfoForce(string _name, bool _requiresHarmonicOrder, bool _requiresIndicatorFunction)
            : base(_name, _requiresHarmonicOrder)
        {
            this.requiresIndicatorFunction = _requiresIndicatorFunction;
        }



        [DataMember]
        private Function cylinderPressureVsCrankAngleIndicatorFunction;
        public Function CylinderPressureVsCrankAngleIndicatorFunction
        {
            set { cylinderPressureVsCrankAngleIndicatorFunction = value; }
            get { return cylinderPressureVsCrankAngleIndicatorFunction; }
        }

        [DataMember]
        private InterpolationMethodInfo interpolationMethod;
        public InterpolationMethodInfo InterpolationMethod
        {
            set { interpolationMethod = value; }
            get { return interpolationMethod; }
        }

        [DataMember]
        private bool requiresIndicatorFunction;
        public bool RequiresIndicatorFunction
        {
            get { return requiresIndicatorFunction; }
        }


        public static FunctionInfoForce CylinderGasPressureForceAxial_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder gas-pressure force; axial (N)", false, true);
            }
        }
        public static FunctionInfoForce CylinderGasPressureForceX_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder gas-pressure force; X (N)", false, true);
            }
        }
        public static FunctionInfoForce CylinderGasPressureForceY_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder gas-pressure force; Y (N)", false, true);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceReciprocatingAxial_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder reciprocating inertia force; axial (N)", true, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceReciprocatingX_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder reciprocating inertia force; X (N)", true, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceReciprocatingY_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder reciprocating inertia force; Y (N)", true, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceRotating_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder rotating inertia force (N)", false, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceTotalAxial_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder total inertia force; axial (N)", true, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceTotalX_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder total inertia force; X (N)", true, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceTotalY_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder total inertia force; Y (N)", true, false);
            }
        }
        public static FunctionInfoForce CylinderInertiaForceReciprocatingVsRotating_N
        {
            get
            {
                return new FunctionInfoForce("Cylinder inertia force reciprocating vs. rotating (N)", true, false);
            }
        }



        public new static FunctionInfoForce[] GetAvailableFunctions()
        {
            PropertyInfo[] _propertyInfos = typeof(FunctionInfoForce).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            List<FunctionInfoForce> _functionInfos = new List<FunctionInfoForce>();

            foreach (PropertyInfo _propertyInfo in _propertyInfos)
            {
                _functionInfos.Add((FunctionInfoForce)_propertyInfo.GetValue(null, null));
            }

            return _functionInfos.ToArray();
        }

    }

}
