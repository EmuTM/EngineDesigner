using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SlimDX;
using SlimDX.Direct3D9;

using EngineDesigner.Machine;

namespace EngineDesigner.Media
{
    internal static class Utility
    {
        public static Vector3 ExtractLookAt(Matrix _viewMatrix)
        {
            return new Vector3(
                _viewMatrix.M13, //look._double
                _viewMatrix.M23, //look.y
                _viewMatrix.M33); //look.z
        }
        public static Vector3 ExtractRight(Matrix _viewMatrix)
        {
            return new Vector3(
                _viewMatrix.M12, //right._double
                _viewMatrix.M22, //right.y
                _viewMatrix.M32); //right.z
        }
        public static Vector3 ExtractUp(Matrix _viewMatrix)
        {
            return new Vector3(
                _viewMatrix.M11, //up._double
                _viewMatrix.M21, //up.y
                _viewMatrix.M31); //up.z
        }

    }
}
