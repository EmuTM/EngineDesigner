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
    internal class FunctionInfoTorque : FunctionInfoForce
    {
        protected FunctionInfoTorque(string _name, bool _requiresHarmonicOrder, bool _requiresIndicatorFunction)
            : base(_name, _requiresHarmonicOrder, _requiresIndicatorFunction)
        {
        }



        public static FunctionInfoTorque CylinderGasPressureTorque_Nm
        {
            get
            {
                return new FunctionInfoTorque("Cylinder gas-pressure torque (Nm)", true, true);
            }
        }
        public static FunctionInfoTorque CylinderInertiaTorque_Nm
        {
            get
            {
                return new FunctionInfoTorque("Cylinder inertia torque (Nm)", true, false);
            }
        }
        public static FunctionInfoTorque CylinderTotalTorque_Nm
        {
            get
            {
                return new FunctionInfoTorque("Cylinder total torque (Nm)", true, true);
            }
        }
        public static FunctionInfoTorque FlywheelAngularVelocityBecauseOfCylinderGasPressureTorque_degps
        {
            get
            {
                return new FunctionInfoTorque("Flywheel angular velocity (cylinder gas pressure torque) (deg/s)", true, true);
            }
        }
        public static FunctionInfoTorque FlywheelAngularVelocityBecauseOfCylinderInertiaTorque_degps
        {
            get
            {
                return new FunctionInfoTorque("Flywheel angular velocity (cylinder inertia torque) (deg/s)", true, false);
            }
        }
        public static FunctionInfoTorque FlywheelAngularVelocityBecauseOfCylinderTotalTorque_degps
        {
            get
            {
                return new FunctionInfoTorque("Flywheel angular velocity (cylinder total torque) (deg/s)", true, true);
            }
        }
        public static FunctionInfoTorque FlywheelAngularAccelerationBecauseOfCylinderGasPressureTorque_degpsmpdeg2
        {
            get
            {
                return new FunctionInfoTorque("Flywheel angular acceleration (cylinder gas pressure torque) (deg/s^2)", true, true);
            }
        }
        public static FunctionInfoTorque FlywheelAngularAccelerationBecauseOfCylinderInertiaTorque_degpsmpdeg2
        {
            get
            {
                return new FunctionInfoTorque("Flywheel angular acceleration (cylinder inertia torque) (deg/s^2)", true, false);
            }
        }
        public static FunctionInfoTorque FlywheelAngularAccelerationBecauseOfCylinderTotalTorque_degpsmpdeg2
        {
            get
            {
                return new FunctionInfoTorque("Flywheel angular acceleration (cylinder total torque) (deg/s^2)", true, true);
            }
        }
        public static FunctionInfoTorque FlywheelSmoothedCylinderGasPressureTorque_Nm
        {
            get
            {
                return new FunctionInfoTorque("Flywheel smoothed cylinder gas-pressure torque (Nm)", true, true);
            }
        }
        public static FunctionInfoTorque FlywheelSmoothedCylinderInertiaTorque_Nm
        {
            get
            {
                return new FunctionInfoTorque("Flywheel smoothed cylinder inertia torque (Nm)", true, false);
            }
        }
        public static FunctionInfoTorque FlywheelSmoothedCylinderTotalTorque_Nm
        {
            get
            {
                return new FunctionInfoTorque("Flywheel smoothed cylinder total torque (Nm)", true, true);
            }
        }


        public new static FunctionInfoTorque[] GetAvailableFunctions()
        {
            PropertyInfo[] _propertyInfos = typeof(FunctionInfoTorque).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);

            List<FunctionInfoTorque> _functionInfos = new List<FunctionInfoTorque>();

            foreach (PropertyInfo _propertyInfo in _propertyInfos)
            {
                _functionInfos.Add((FunctionInfoTorque)_propertyInfo.GetValue(null, null));
            }

            return _functionInfos.ToArray();
        }

    }

}
