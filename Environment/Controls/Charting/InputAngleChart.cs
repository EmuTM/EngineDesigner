using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;


namespace EngineDesigner.Environment.Controls.Charting
{
    [DefaultEvent("AngleChanged")]
    public partial class InputAngleChart : AngleChart
    {
        private const double CYCLING_MINIMAL_DIFFERENCE = 300d;



        public event EventHandler<AngleChangedEventArgs> AngleChanged;



        private bool cyclic = false;
        [DefaultValue(false)]
        public bool Cyclic
        {
            get { return cyclic; }
            set { cyclic = value; }
        }

        //-1 = disabled
        private int rounding = -1;
        [DefaultValue(-1)]
        public int Rounding
        {
            get { return rounding; }
            set { rounding = value; }
        }



        public InputAngleChart()
        {
            InitializeComponent();
        }



        private DataPoint selectedDataPoint = null; //drži trenutno izbran data point



        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataPoint _dataPoint = GetDataPointAt(e.X, e.Y);

                if (_dataPoint != null)
                {
                    selectedDataPoint = _dataPoint;
                    base.chart1.Cursor = Cursors.Hand;
                }
            }
        }
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedDataPoint != null)
            {
                double _oldAngle = selectedDataPoint.XValue;


                #region "poskrbimo, da koordinate od miši niso izven charta"
                int _mouseX = e.X;
                if (_mouseX < 0)
                {
                    _mouseX = 0;
                }
                else if (_mouseX > base.chart1.Size.Width - 10) //upoštevamo malo manjše območje
                {
                    _mouseX = base.chart1.Size.Width - 10;
                }

                int _mouseY = e.Y;
                if (_mouseY < 0)
                {
                    _mouseY = 0;
                }
                else if (_mouseY > base.chart1.Size.Height - 10) //upoštevamo malo manjše območje
                {
                    _mouseY = base.chart1.Size.Height - 10;
                }
                #endregion "poskrbimo, da koordinate od miši niso izven charta"

                #region "dobimo koordinate na grafu"
                double _chartX = base.BaseAxisX.PixelPositionToValue(_mouseX);
                _chartX = Math.Min(_chartX, base.BaseAxisX.Maximum);
                _chartX = Math.Max(_chartX, base.BaseAxisX.Minimum);

                double _chartY = base.BaseAxisY.PixelPositionToValue(_mouseY);
                _chartY = Math.Min(_chartY, base.BaseAxisY.Maximum);
                _chartY = Math.Max(_chartY, base.BaseAxisY.Minimum);
                #endregion "dobimo koordinate na grafu"

                #region "postavimo v center našega grafa"
                //izhodišče prikaza grafa je na sredini X in Y osi, pravo izhodišče pa je levo spodaj, zato je treba koordinate popravljat!!
                double _centerX = base.BaseAxisX.Minimum + ((base.BaseAxisX.Maximum - base.BaseAxisX.Minimum) / 2);
                double _centerY = base.BaseAxisY.Minimum + ((base.BaseAxisY.Maximum - base.BaseAxisY.Minimum) / 2);

                _chartX -= _centerX;
                _chartY -= _centerY;
                #endregion "postavimo v center našega grafa"

                #region "preračunamo koordinate na 0, 0"
                _chartX /= (base.BaseAxisX.Maximum - base.BaseAxisX.Minimum);
                _chartY /= (base.BaseAxisY.Maximum - base.BaseAxisY.Minimum);
                #endregion "preračunamo koordinate na 0, 0"

                //System.Diagnostics.Debug.WriteLine("X: " + _chartX);
                //System.Diagnostics.Debug.WriteLine("Y: " + _chartY);

                #region "izračunamo kot + popravki kota"
                double _newAngle = Math.Atan(_chartX / _chartY);
                _newAngle = Conversions.RadToDeg(_newAngle);
                _newAngle += base.BaseAxisX.Minimum;

                if ((_chartX > 0d) && (_chartY > 0d))
                {
                    //System.Diagnostics.Debug.WriteLine("desno zgoraj");
                }
                else if ((_chartX > 0d) && (_chartY < 0d))
                {
                    //System.Diagnostics.Debug.WriteLine("desno spodaj");
                    _newAngle += 180d;
                }
                else if ((_chartX < 0d) && (_chartY < 0d))
                {
                    //System.Diagnostics.Debug.WriteLine("levo spodaj");
                    _newAngle += 180d;
                }
                else if ((_chartX < 0d) && (_chartY > 0d))
                {
                    //System.Diagnostics.Debug.WriteLine("levo zgoraj");
                    _newAngle += 360d;
                }
                #endregion "izračunamo kot + popravki kota"


                if (this.rounding > -1)
                {
                    _newAngle = Math.Round(_newAngle, this.rounding);
                }
                //System.Diagnostics.Debug.WriteLine("ANGLE: " + _newAngle);


                #region "cycling"
                if (!this.cyclic)
                {
                    if (_newAngle < _oldAngle) //smo šli iz 359 na 0 (krog v desno)
                    {
                        if (Math.Abs(_oldAngle - _newAngle) >= CYCLING_MINIMAL_DIFFERENCE) //če je razlika vsaj minimalna določena
                        {
                            System.Diagnostics.Debug.WriteLine("KROG V DESNO");

                            if (base.Circle == -1)
                            {
                                base.Circle += 2;
                            }
                            else
                            {
                                base.Circle++;
                            }
                            _newAngle += 360d;
                        }
                    }
                    else if (_newAngle > _oldAngle) //smo šli iz 0 na 359 (krog v levo)
                    {
                        if (Math.Abs(_oldAngle - _newAngle) >= CYCLING_MINIMAL_DIFFERENCE) //če je razlika vsaj minimalna določena
                        {
                            System.Diagnostics.Debug.WriteLine("KROG V LEVO");

                            if (base.Circle == 1)
                            {
                                base.Circle -= 2;
                            }
                            else
                            {
                                base.Circle--;
                            }
                            _newAngle -= 360d;
                        }
                    }
                }
                #endregion "cycling"


                if (_newAngle != _oldAngle)
                {
                    selectedDataPoint.XValue = _newAngle;


                    base.chart1.Invalidate();
                    base.chart1.Update();


                    //in dvignemo event
                    OnAngleChanged(selectedDataPoint, _newAngle);
                }
            }
            else
            {
                if (GetDataPointAt(e.X, e.Y) != null)
                {
                    base.chart1.Cursor = Cursors.Hand;
                }
                else
                {
                    base.chart1.Cursor = Cursors.Default;
                }
            }
        }
        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedDataPoint != null)
            {
                selectedDataPoint = null;

                base.chart1.Cursor = Cursors.Default;
            }
        }

        private DataPoint GetDataPointAt(int _x, int _y)
        {
            //System.Diagnostics.Debug.WriteLine("X: " + _double + "  Y: " + _y);

            DataPoint _hitDataPoint = null;


            HitTestResult[] _hitTestResults = base.chart1.HitTest(_x, _y, false, new ChartElementType[] { ChartElementType.DataPoint });
            foreach (HitTestResult _hitTestResult in _hitTestResults)
            {
                if (_hitTestResult.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint _dataPoint = (DataPoint)_hitTestResult.Object;

                    //System.Diagnostics.Debug.WriteLine("dataX: " + _dataPoint.XValue + "  dataY: " + _dataPoint.YValues[0]);

                    //TODO: nerodno spisan if, ma mora bit prou tku; izhodišča ne smemo tikat, ker to je center točka na sredini kroga!
                    if (!((_dataPoint.XValue == 0)
                        && (_dataPoint.YValues[0]) == 0))
                    {
                        _hitDataPoint = _dataPoint;
                        break;
                    }
                }
            }


            return _hitDataPoint;
        }



        protected virtual void OnAngleChanged(DataPoint _dataPoint, double _newAngle)
        {
            if (AngleChanged != null)
            {
                AngleChanged(this, new AngleChangedEventArgs(/*_dataPoint, */_newAngle));
            }
        }
    }


    public class AngleChangedEventArgs : EventArgs
    {
        //private DataPoint dataPoint;
        //public DataPoint DataPoint
        //{
        //    get { return dataPoint; }
        //}

        private double newAngle;
        public double NewAngle
        {
            get { return newAngle; }
        }


        internal AngleChangedEventArgs(/*DataPoint _dataPoint, */double _newAngle)
        {
            //dataPoint = _dataPoint;
            newAngle = _newAngle;
        }
    }

}
