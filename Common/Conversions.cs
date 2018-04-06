using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineDesigner.Common
{
    /// <summary>
    /// Provides functionality for conversions between different quantities.
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// Gets radians from degrees.
        /// </summary>
        /// <param name="_deg">The angle in degrees.</param>
        public static double DegToRad(double _deg)
        {
            double _oneRadian = Math.PI / 180;
            return (_oneRadian * _deg);
        }
        /// <summary>
        /// Gets radians from degrees.
        /// </summary>
        /// <param name="_deg">The angle in degrees.</param>
        public static float DegToRad(float _deg)
        {
            float _oneRadian = (float)Math.PI / 180;
            return (_oneRadian * _deg);
        }
        /// <summary>
        /// Gets degrees from radians.
        /// </summary>
        /// <param name="_rad">The angle in radians.</param>
        public static double RadToDeg(double _rad)
        {
            double _oneDegree = 180 / Math.PI;
            return (_oneDegree * _rad);
        }
        /// <summary>
        /// Gets degrees from radians.
        /// </summary>
        /// <param name="_rad">The angle in radians.</param>
        public static float RadToDeg(float _rad)
        {
            float _oneDegree = 180 / (float)Math.PI;
            return (_oneDegree * _rad);
        }

        /// <summary>
        /// Gets rotations per second from rotations per minute.
        /// </summary>
        /// <param name="_rpm">The rotation speed in rotations per minute.</param>
        public static double RpmToRps(double _rpm)
        {
            return (_rpm / 60d);
        }
        /// <summary>
        /// Gets rotations per minute from rotations per seconds.
        /// </summary>
        /// <param name="_rpm">The rotation speed in rotations per seconds.</param>
        public static double RpsToRpm(double _rps)
        {
            return (_rps * 60d);
        }

        /// <summary>
        /// Gets kilograms from grams.
        /// </summary>
        /// <param name="_g">The mass in grams.</param>
        public static double GToKg(double _g)
        {
            return _g / 1000;
        }
        /// <summary>
        /// Gets grams from kilograms.
        /// </summary>
        /// <param name="_g">The mass in kilograms.</param>
        public static double KgToG(double _Kg)
        {
            return _Kg * 1000;
        }

        /// <summary>
        /// Gets meters from milimeters.
        /// </summary>
        /// <param name="_mm">The distance in milimeters.</param>
        public static double MmToM(double _mm)
        {
            return _mm / 1000d;
        }
        /// <summary>
        /// Gets milimeters from meters.
        /// </summary>
        /// <param name="_m">The distance in meters.</param>
        public static double MToMM(double _m)
        {
            return _m * 1000d;
        }

        /// <summary>
        /// Gets cubic centimeters from cubic milimeters.
        /// </summary>
        /// <param name="_mm3">The volume in cubic milimeters.</param>
        public static double Mm3ToCm3(double _mm3)
        {
            return _mm3 / 1000d;
        }
        /// <summary>
        /// Gets the cubic milimeters from cubic centimeters.
        /// </summary>
        /// <param name="_cm3">The volume in cubic centimeters.</param>
        public static double Cm3ToMm3(double _cm3)
        {
            return _cm3 * 1000d;
        }

        /// <summary>
        /// Gets square meters from square milimeters.
        /// </summary>
        /// <param name="_mm">The area in square milimeters.</param>
        public static double Mm2ToM2(double _mm2)
        {
            return _mm2 / 1000000d;
        }
        /// <summary>
        /// Gets square milimeters from square meters.
        /// </summary>
        /// <param name="_m">The area in square meters.</param>
        public static double M2ToMM2(double _m2)
        {
            return _m2 * 1000000d;
        }


        /// <summary>
        /// Gets the angular velocity, in degrees per second.
        /// </summary>
        /// <param name="_rpm">Rotations per minute.</param>
        public static double RpmToDegps(double _rpm)
        {
            double _radians = (2 * Math.PI) * Conversions.RpmToRps(_rpm);
            return Conversions.RadToDeg(_radians);
        }
        /// <summary>
        /// Gets the rotations per minute.
        /// </summary>
        /// <param name="_angularVelocity_degps">Angular velocity, in degrees per second.</param>
        public static double DegspsToRpm(double _angularVelocity_degps)
        {
            double _rps = Conversions.DegToRad(_angularVelocity_degps) / (2 * Math.PI);
            return Conversions.RpsToRpm(_rps);
        }

    }
}
