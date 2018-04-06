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
    internal class FunctionInfoMoment : FunctionInfoKinematic
    {
        protected FunctionInfoMoment(string _name, bool _requiresHarmonicOrder)
            : base(_name, _requiresHarmonicOrder)
        {
        }



        public static FunctionInfoMoment CylinderInertiaMomentReciprocatingAxial_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder reciprocating inertia moment; axial (N)", true);
            }
        }
        public static FunctionInfoMoment CylinderInertiaMomentReciprocatingX_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder reciprocating inertia moment; X (N)", true);
            }
        }
        public static FunctionInfoMoment CylinderInertiaMomentReciprocatingY_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder reciprocating inertia moment; Y (N)", true);
            }
        }
        public static FunctionInfoMoment CylinderInertiaMomentRotating_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder rotating inertia moment (N)", true);
            }
        }
        public static FunctionInfoMoment CylinderInertiaMomentTotalAxial_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder total inertia moment; axial (N)", true);
            }
        }
        public static FunctionInfoMoment CylinderInertiaMomentTotalX_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder total inertia moment; X (N)", true);
            }
        }
        public static FunctionInfoMoment CylinderInertiaMomentTotalY_N
        {
            get
            {
                return new FunctionInfoMoment("Cylinder total inertia moment; Y (N)", true);
            }
        }



        public new static FunctionInfoMoment[] GetAvailableFunctions()
        {
            PropertyInfo[] _propertyInfos = typeof(FunctionInfoMoment).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            List<FunctionInfoMoment> _functionInfos = new List<FunctionInfoMoment>();

            foreach (PropertyInfo _propertyInfo in _propertyInfos)
            {
                _functionInfos.Add((FunctionInfoMoment)_propertyInfo.GetValue(null, null));
            }

            return _functionInfos.ToArray();
        }

    }

}
