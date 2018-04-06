using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EngineDesigner.Machine;
using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;
using System.Drawing;

namespace EngineDesigner.Media.Graphics.GDI
{
    public class BasicEngineSketchFront : BasicEngineSketch
    {
        protected override Polygon GetCylinderView(PositionedCylinder _positionedCylinder)
        {
            double _physicalHeightAbovePiston_mm = _positionedCylinder.GetPhysicalHeightAbovePiston_mm(0);
            double _physicalHeightUnderPiston_mm = _positionedCylinder.GetPhysicalHeightUnderPiston_mm(Stroke.StrokeDuration_deg);

            double _cylinderHalfBore = _positionedCylinder.Bore_mm / 2;

            double _offset_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
            double _sinOffset = Math.Sin(_offset_rad);
            double _cosOffset = Math.Cos(_offset_rad);


            //zgornja center točka bata
            double _xTDC = (_sinOffset * _physicalHeightAbovePiston_mm);
            double _yTDC = (_cosOffset * _physicalHeightAbovePiston_mm);

            //spodnja center točka bata
            double _xBDC = (_sinOffset * _physicalHeightUnderPiston_mm);
            double _yBDC = (_cosOffset * _physicalHeightUnderPiston_mm);

            //razlike od center do skrajnih točk
            double _x_ = (_cosOffset * _cylinderHalfBore);
            double _y_ = (_sinOffset * _cylinderHalfBore);

            //zgornje skrajne točke
            double _x1 = _xTDC - _x_;
            double _y1 = _yTDC + _y_;
            double _x2 = _xTDC + _x_;
            double _y2 = _yTDC - _y_;

            //spodnje skrajne točke
            double _x3 = _xBDC - _x_;
            double _y3 = _yBDC + _y_;
            double _x4 = _xBDC + _x_;
            double _y4 = _yBDC - _y_;


            return Polygon.Rectangle(_x1, _y1, _x2, _y2, _x3, _y3, _x4, _y4);
        }
        protected override Polygon GetPistonView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);

            double _physicalHeightAbovePiston_mm = _positionedCylinder.GetPhysicalHeightAbovePiston_mm(_cylinderRelativeCrankThrowRotation_deg);
            double _physicalHeightUnderPiston_mm = _positionedCylinder.GetPhysicalHeightUnderPiston_mm(_cylinderRelativeCrankThrowRotation_deg);

            double _cylinderHalfBore = _positionedCylinder.Bore_mm / 2d * EngineDesigner.Media.Properties.Settings.Default.PistonVsCylinderClearance;

            double _offset_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
            double _sinOffset = Math.Sin(_offset_rad);
            double _cosOffset = Math.Cos(_offset_rad);


            //zgornja center točka bata
            double _xTDC = (_sinOffset * _physicalHeightAbovePiston_mm);
            double _yTDC = (_cosOffset * _physicalHeightAbovePiston_mm);

            //spodnja center točka bata
            double _xBDC = (_sinOffset * _physicalHeightUnderPiston_mm);
            double _yBDC = (_cosOffset * _physicalHeightUnderPiston_mm);

            //razlike od center do skrajnih točk
            double _x_ = (_cosOffset * _cylinderHalfBore);
            double _y_ = (_sinOffset * _cylinderHalfBore);

            //zgornje skrajne točke
            double _x1 = _xTDC - _x_;
            double _y1 = _yTDC + _y_;
            double _x2 = _xTDC + _x_;
            double _y2 = _yTDC - _y_;

            //spodnje skrajne točke
            double _x3 = _xBDC - _x_;
            double _y3 = _yBDC + _y_;
            double _x4 = _xBDC + _x_;
            double _y4 = _yBDC - _y_;


            return Polygon.Rectangle(_x1, _y1, _x2, _y2, _x3, _y3, _x4, _y4);
        }
        protected override Polygon GetConnectingRodView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            double _absoluteCrankThrowRotation_deg = _positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg);
            double _absoluteCrankThrowRotation_rad = Conversions.DegToRad(_absoluteCrankThrowRotation_deg);

            double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
            double _pistonTravelFromCeter_mm = _positionedCylinder.GetPistonTravelFromCrankCenter_mm(_cylinderRelativeCrankThrowRotation_deg);

            double _offset_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);


            //zgornja točka ojnice
            double _xTDC = (Math.Sin(_offset_rad) * _pistonTravelFromCeter_mm);
            double _yTDC = (Math.Cos(_offset_rad) * _pistonTravelFromCeter_mm);

            //spodnja točka ojnice
            double _xBDC = (Math.Sin(_absoluteCrankThrowRotation_rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm);
            double _yBDC = (Math.Cos(_absoluteCrankThrowRotation_rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm);


            return Polygon.Line(_xTDC, _yTDC, _xBDC, _yBDC);
        }
        protected override Polygon GetCrankThrowView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            double _absoluteCrankThrowRotation_deg = _positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg);
            double _absoluteCrankThrowRotation_rad = Conversions.DegToRad(_absoluteCrankThrowRotation_deg);

            return Polygon.Line(
                0d,
                0d,
                Math.Sin(_absoluteCrankThrowRotation_rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm,
                Math.Cos(_absoluteCrankThrowRotation_rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm);
        }
        protected override Polygon GetCrankshaftView(PositionedCylinder _positionedCylinder)
        {
            return Polygon.Circle(0, 0, 1, EngineDesigner.Media.Properties.Settings.Default.BasicEngineSketchArcPrecision);
        }

    }
}
