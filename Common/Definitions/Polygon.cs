using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace EngineDesigner.Common.Definitions
{
    /// <summary>
    /// Represents a graphic polygon.
    /// </summary>
    public class Polygon
    {
        private Polygon()
        {
            points = new List<XY>();
            edges = new List<XY>();
        }
        public override string ToString()
        {
            string _string = "";

            for (int a = 0; a < points.Count; a++)
            {
                _string += string.Format(
                    "({0})",
                    points[a].ToString());

                if (a < points.Count - 1)
                {
                    _string += "; ";
                }
            }

            return _string;
        }



        private List<XY> points = null;
        private List<XY> edges = null;



        /// <summary>
        /// Gets the points defining the polygon (edges).
        /// </summary>
        public XY this[int _index]
        {
            get
            {
                return this.points[_index];
            }
        }



        /// <summary>
        /// Gets a polygon from given points.
        /// </summary>
        /// <param name="_xyArray">The array of points that represent the polygon edges.</param>
        public static Polygon From(IEnumerable<XY> _points)
        {
            Polygon _poligon = new Polygon();
            _poligon.AddRange(_points);
            return _poligon;
        }
        /// <summary>
        /// Gets a polygon from given points.
        /// </summary>
        /// <param name="_pointArray">The array of points that represent the polygon edges.</param>
        public static Polygon From(IEnumerable<Point> _points)
        {
            Polygon _poligon = new Polygon();
            _poligon.AddRange(_points);
            return _poligon;
        }



        /// <summary>
        /// Adds another edge to the polygon.
        /// </summary>
        /// <param name="_xy">The edge to be added to the polygon.</param>
        public void Add(XY _xy)
        {
            points.Add(_xy);
        }
        public void Add(Polygon _polygon)
        {
            points.AddRange(_polygon.points);
        }
        /// <summary>
        /// Adds a collection of another edges to the polygon.
        /// </summary>
        /// <param name="_xyArray">The collection of edges to be added to the polygon.</param>
        public void AddRange(IEnumerable<XY> _xyCollection)
        {
            points.AddRange(_xyCollection);
        }
        /// <summary>
        /// Adds another edge to the polygon.
        /// </summary>
        /// <param name="_point">The edge to be added to the polygon.</param>
        public void Add(Point _point)
        {
            points.Add(XY.FromPoint(_point));
        }
        /// <summary>
        /// Adds a collection of another edges to the polygon.
        /// </summary>
        /// <param name="_pointArray">The collection of edges to be added to the polygon.</param>
        public void AddRange(IEnumerable<Point> _pointCollection)
        {
            foreach (Point _point in _pointCollection)
            {
                Add(_point);
            }
        }
        public int Length
        {
            get { return this.points.Count; }
        }

        /// <summary>
        /// Checks whether the polygon intersects with a given one.
        /// </summary>
        /// <param name="_polygon">The polygon to compare with for intersection.</param>
        public bool IntersectsWith(Polygon _polygon)
        {
            this.BuildEdges();
            _polygon.BuildEdges();


            bool _intersects = true;

            //gremo skozi vse kote poligona
            XY _edge;
            for (int a = 0; a < (edges.Count + _polygon.edges.Count); a++)
            {
                if (a < edges.Count)
                {
                    _edge = edges[a];
                }
                else
                {
                    _edge = _polygon.edges[a - edges.Count];
                }

                //dobimo normalo
                XY _axis = new XY(-_edge.Y, _edge.X);
                _axis.Normalize();

                //projeciramo oba poligona na našo normalno os
                double _minA = 0;
                double _minB = 0;
                double _maxA = 0;
                double _maxB = 0;
                ProjectPolygonOnAxis(this, _axis, ref _minA, ref _maxA);
                ProjectPolygonOnAxis(_polygon, _axis, ref _minB, ref _maxB);

                //in pogledamo, èe se projekcije prekrivajo
                if (CalculateIntervalDistance(_minA, _maxA, _minB, _maxB) >= 0)
                {
                    //èe je razdalja pozitivna, se ne prekrivata
                    _intersects = false;
                }
            }

            return _intersects;
        }
        /// <summary>
        /// Gets the center point of the polygon.
        /// </summary>
        public XY GetCenter()
        {
            double _totalX = 0;
            double _totalY = 0;

            for (int a = 0; a < points.Count; a++)
            {
                _totalX += points[a].X;
                _totalY += points[a].Y;
            }

            return new XY(_totalX / (double)points.Count, _totalY / (double)points.Count);
        }

        /// <summary>
        /// Draws the polygon on the graphics object, in specified color, at the specified starting point.
        /// </summary>
        /// <param name="_graphics">The graphics object to draw on.</param>
        /// <param name="_color">The color to use when drawing.</param>
        /// <param name="_centerX">The _double dot of the starting point.</param>
        /// <param name="_centerY">The y dot of the starting point.</param>
        /// <param name="_negativeY">Defines whether y-points should be placed negatively with respect to starting point.</param>
        public void Draw(Graphics _graphics, Color _color, int _centerX, int _centerY, bool _negativeY)
        {
            List<Point> _points = new List<Point>();
            foreach (XY _xy in points)
            {
                Point _point = _xy;
                _point.X += _centerX;
                if (_negativeY)
                {
                    _point.Y = _centerY - _point.Y;
                }
                else
                {
                    _point.Y += _centerY;
                }
                _points.Add(_point);
            }

            if (_points.Count > 0)
            {
                _graphics.DrawPolygon(new Pen(_color), _points.ToArray());
            }
        }
        /// <summary>
        /// Draws the polygon's points on the graphics object, in specified color, at the specified starting point.
        /// </summary>
        /// <param name="_graphics">The graphics object to draw on.</param>
        /// <param name="_color">The color to use when drawing.</param>
        /// <param name="_centerX">The _double dot of the starting point.</param>
        /// <param name="_centerY">The y dot of the starting point.</param>
        /// <param name="_negativeY">Defines whether y-points should be placed negatively with respect to starting point.</param>
        public void DrawAsPoints(Graphics _graphics, Color _color, int _centerX, int _centerY, bool _negativeY)
        {
            int _r = 4;

            for (int a = 0; a < points.Count; a++)
            {
                Point _point = points[a];
                _point.X += _centerX;
                if (_negativeY)
                {
                    _point.Y = _centerY - _point.Y;
                }
                else
                {
                    _point.Y += _centerY;
                }
                _point.X -= (_r / 2);
                _point.Y -= (_r / 2);

                _graphics.FillEllipse(new SolidBrush(_color), _point.X, _point.Y, _r, _r);
                _graphics.DrawString(a.ToString(), new Font("Arial", 8), new SolidBrush(_color), _point.X + _r, _point.Y + _r);
            }
        }
        /// <summary>
        /// Draws the polygon as spline on the graphics object, in specified color, at the specified starting point.
        /// </summary>
        /// <param name="_graphics">The graphics object to draw on.</param>
        /// <param name="_color">The color to use when drawing.</param>
        /// <param name="_centerX">The _double dot of the starting point.</param>
        /// <param name="_centerY">The y dot of the starting point.</param>
        /// <param name="_negativeY">Defines whether y-points should be placed negatively with respect to starting point.</param>
        public void DrawAsSpline(Graphics _graphics, Color _color, int _centerX, int _centerY, bool _negativeY)
        {
            List<Point> _points = new List<Point>();
            foreach (XY _xy in points)
            {
                Point _point = _xy;
                _point.X += _centerX;
                if (_negativeY)
                {
                    _point.Y = _centerY - _point.Y;
                }
                else
                {
                    _point.Y += _centerY;
                }
                _points.Add(_point);
            }
            _graphics.DrawCurve(new Pen(_color), _points.ToArray(), 0f);
        }



        private void BuildEdges()
        {
            XY _xy1;
            XY _xy2;

            edges.Clear();
            for (int a = 0; a < points.Count; a++)
            {
                _xy1 = points[a];

                if (a + 1 >= points.Count)
                {
                    _xy2 = points[0];
                }
                else
                {
                    _xy2 = points[a + 1];
                }

                edges.Add(_xy2 - _xy1);
            }
        }

        private static double CalculateIntervalDistance(double _minA, double _maxA, double _minB, double _maxB)
        {
            //* èe se intervala prekrivata, bo razdalja negativna

            if (_minA < _minB)
            {
                return _minB - _maxA;
            }
            else
            {
                return _minA - _maxB;
            }
        }
        private static void ProjectPolygonOnAxis(Polygon _polygon, XY _axis, ref double _min, ref double _max)
        {
            double _dotProduct = _axis.GetDotProduct(_polygon[0]);
            _min = _dotProduct;
            _max = _dotProduct;

            for (int a = 0; a < _polygon.Length; a++)
            {
                _dotProduct = _polygon[a].GetDotProduct(_axis);

                if (_dotProduct < _min)
                {
                    _min = _dotProduct;
                }
                else
                {
                    if (_dotProduct > _max)
                    {
                        _max = _dotProduct;
                    }
                }
            }
        }



        public static Polygon Empty
        {
            get
            {
                return new Polygon();
            }
        }
        public static Polygon Arc(double _centerX, double _centerY, double _radiusX, double _radiusY, double _startAngle_deg, double _sweepAngle_deg, double _precision_deg)
        {
            List<XY> _points = new List<XY>();


            for (double _angle = _startAngle_deg; _angle <= _startAngle_deg + _sweepAngle_deg; _angle += _precision_deg)
            {
                double _rX = (_radiusX * Math.Sin(Conversions.DegToRad(_angle - 90)));
                double _rY = -(_radiusY * Math.Cos(Conversions.DegToRad(_angle - 90)));

                _points.Add(new XY(
                    _centerX + _rX,
                    _centerY + _rY));
            }


            return Polygon.From(_points);
        }
        public static Polygon Circle(double _centerX, double _centerY, double _radius, double _precision_deg)
        {
            return Arc(_centerX, _centerY, _radius, _radius, 0d, 360d, _precision_deg);
        }
        public static Polygon Line(double _x1, double _y1, double _x2, double _y2)
        {
            List<XY> _points = new List<XY>();

            _points.Add(new XY(_x1, _y1));
            _points.Add(new XY(_x2, _y2));

            return Polygon.From(_points);
        }
        public static Polygon Rectangle(double _x1, double _y1, double _x2, double _y2, double _x3, double _y3, double _x4, double _y4)
        {
            List<XY> _points = new List<XY>();

            _points.Add(new XY(_x1, _y1));
            _points.Add(new XY(_x2, _y2));
            _points.Add(new XY(_x4, _y4));
            _points.Add(new XY(_x3, _y3));

            return Polygon.From(_points);
        }

    }

}

