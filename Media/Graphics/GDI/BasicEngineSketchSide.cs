using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EngineDesigner.Machine;
using EngineDesigner.Common;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.Media.Graphics.GDI
{
    public class BasicEngineSketchSide : BasicEngineSketch
    {
        protected override Polygon GetCylinderView(PositionedCylinder _positionedCylinder)
        {
            double _offset_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
            double _cosOffset = Math.Cos(_offset_rad);


            double _TDC = (_cosOffset * _positionedCylinder.GetPhysicalHeightAbovePiston_mm(0));
            double _BDC = (_cosOffset * _positionedCylinder.GetPhysicalHeightUnderPiston_mm(Stroke.StrokeDuration_deg));

            double _rX = _positionedCylinder.Bore_mm / 2;
            double _rY = (Math.Abs(Math.Sin(_offset_rad) * _rX));

            double _rTilt = _rX * Math.Sin(Conversions.DegToRad(_positionedCylinder.Tilt_deg));

            Polygon _polygon = Polygon.Line(
                -_rX + _positionedCylinder.Offset_mm,
                _BDC,
                -_rX + _positionedCylinder.Offset_mm,
                _TDC);

            _polygon.Add(Polygon.Arc(_positionedCylinder.Offset_mm, _TDC, _rX, _rTilt, 0, 540, EngineDesigner.Media.Properties.Settings.Default.BasicEngineSketchArcPrecision));

            _polygon.Add(Polygon.Line(
                 _rX + _positionedCylinder.Offset_mm,
                 _TDC,
                 _rX + _positionedCylinder.Offset_mm,
                 _BDC));

            _polygon.Add(Polygon.Arc(_positionedCylinder.Offset_mm, _BDC, _rX, _rTilt, 180, 540, EngineDesigner.Media.Properties.Settings.Default.BasicEngineSketchArcPrecision));


            return _polygon;
        }
        protected override Polygon GetPistonView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);
            double _offset_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
            double _cosOffset = Math.Cos(_offset_rad);


            double _TDC = (_cosOffset * _positionedCylinder.GetPhysicalHeightAbovePiston_mm(_cylinderRelativeCrankThrowRotation_deg));
            double _BDC = (_cosOffset * _positionedCylinder.GetPhysicalHeightUnderPiston_mm(_cylinderRelativeCrankThrowRotation_deg));

            double _rX = _positionedCylinder.Bore_mm / 2d * EngineDesigner.Media.Properties.Settings.Default.PistonVsCylinderClearance;
            double _rY = (Math.Abs(Math.Sin(_offset_rad) * _rX));

            double _rTilt = _rX * Math.Sin(Conversions.DegToRad(_positionedCylinder.Tilt_deg));


            Polygon _polygon = Polygon.Line(
                -_rX + _positionedCylinder.Offset_mm,
                _BDC,
                -_rX + _positionedCylinder.Offset_mm,
                _TDC);

            _polygon.Add(Polygon.Arc(_positionedCylinder.Offset_mm, _TDC, _rX, _rTilt, 0, 540, EngineDesigner.Media.Properties.Settings.Default.BasicEngineSketchArcPrecision));

            _polygon.Add(Polygon.Line(
                 _rX + _positionedCylinder.Offset_mm,
                 _TDC,
                 _rX + _positionedCylinder.Offset_mm,
                 _BDC));

            _polygon.Add(Polygon.Arc(_positionedCylinder.Offset_mm, _BDC, _rX, _rTilt, 180, 540, EngineDesigner.Media.Properties.Settings.Default.BasicEngineSketchArcPrecision));


            return _polygon;
        }
        protected override Polygon GetConnectingRodView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            double _cylinderRelativeCrankThrowRotation_deg = _positionedCylinder.GetCylinderRelativeCrankThrowRotation_deg(_crankshaftRotation_deg);

            double _offset_rad = Conversions.DegToRad(_positionedCylinder.Tilt_deg);
            double _cosOffset = Math.Cos(_offset_rad);

            double _pistonTravelFromCeter_mm = _positionedCylinder.GetPistonTravelFromCrankCenter_mm(_cylinderRelativeCrankThrowRotation_deg);
            double _absoluteCrankThrowRotation_deg = _positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg);
            double _absoluteCrankThrowRotation_rad = Conversions.DegToRad(_absoluteCrankThrowRotation_deg);


            double _TDC = (_cosOffset * _pistonTravelFromCeter_mm);
            double _BDC = (Math.Cos(_absoluteCrankThrowRotation_rad) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm);


            return Polygon.Line(
                _positionedCylinder.Offset_mm,
                _TDC,
                _positionedCylinder.Offset_mm,
                _BDC);
        }
        protected override Polygon GetCrankThrowView(PositionedCylinder _positionedCylinder, double _crankshaftRotation_deg)
        {
            double _TDC = (Math.Cos(Conversions.DegToRad(_positionedCylinder.GetAbsoluteCrankThrowRotation_deg(_crankshaftRotation_deg))) * _positionedCylinder.CrankThrow.CrankRotationRadius_mm);


            return Polygon.Line(
                _positionedCylinder.Offset_mm,
                0,
                _positionedCylinder.Offset_mm,
                _TDC);
        }
        protected override Polygon GetCrankshaftView(PositionedCylinder _positionedCylinder)
        {
            double _cylinderHalfBore = _positionedCylinder.Bore_mm / 2;


            double _left = _positionedCylinder.Offset_mm - _cylinderHalfBore;
            double _right = _positionedCylinder.Offset_mm + _cylinderHalfBore;


            return Polygon.Line(
                _left,
                0,
                _right,
                0);
        }

    }
}
