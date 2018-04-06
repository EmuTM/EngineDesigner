using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;
using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.Wizards.NewFunction
{
    internal partial class Form_NewFunctionWizardBase : Form_WizardBase
    {
        public Form_NewFunctionWizardBase()
            : this(null)
        {
        }
        public Form_NewFunctionWizardBase(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }



        //utility
        protected Function GenerateFunctionFromState(NewFunctionWizardState _newFunctionWizardState)
        {
            Function _function = null;

            switch (_newFunctionWizardState.SelectedFunctionType)
            {
                case NewFunctionWizardState.FunctionType.IndicatorDiagram_GasPressureVsFiringAngle_TwoStroke:
                    {
                        #region
                        List<XY> _points = new List<XY>();


                        XY _xy;

                        _xy = new XY(
                            0d,
                            _newFunctionWizardState.HighestPressureOnPowerStroke);
                        _points.Add(_xy);

                        _xy = new XY(
                            100d,
                            _newFunctionWizardState.AverageAtmosphericPressure);
                        _points.Add(_xy);

                        _xy = new XY(
                            200d,
                            _newFunctionWizardState.LowestPressureOnIntakeStroke);
                        _points.Add(_xy);

                        _xy = new XY(
                            320d,
                            _newFunctionWizardState.AverageAtmosphericPressure);
                        _points.Add(_xy);

                        _xy = new XY(
                            360d,
                            _newFunctionWizardState.HighestPressureOnPowerStroke);
                        _points.Add(_xy);


                        _function = Function.FromPoints(_points);
                        _function = this.ObtainRequestedNumberOfPoints(_function, _newFunctionWizardState.NumberOfStartingPoints);
                        #endregion
                    }
                    break;

                case NewFunctionWizardState.FunctionType.IndicatorDiagram_GasPressureVsFiringAngle_FourStroke:
                    {
                        #region
                        List<XY> _points = new List<XY>();


                        XY _xy;

                        _xy = new XY(
                            0d,
                            _newFunctionWizardState.LowestPressureOnIntakeStroke);
                        _points.Add(_xy);

                        _xy = new XY(
                            270d,
                            _newFunctionWizardState.AverageAtmosphericPressure);
                        _points.Add(_xy);

                        _xy = new XY(
                            360d,
                            _newFunctionWizardState.HighestPressureOnPowerStroke);
                        _points.Add(_xy);

                        _xy = new XY(
                            580d,
                            _newFunctionWizardState.AverageAtmosphericPressure);
                        _points.Add(_xy);

                        _xy = new XY(
                            720d,
                            _newFunctionWizardState.LowestPressureOnIntakeStroke);
                        _points.Add(_xy);


                        _function = Function.FromPoints(_points);
                        _function = this.ObtainRequestedNumberOfPoints(_function, _newFunctionWizardState.NumberOfStartingPoints);
                        #endregion
                    }
                    break;
            }

            return _function;
        }
        private Function ObtainRequestedNumberOfPoints(Function _function, int _numberOfPoints)
        {
            if (_function.Length != _numberOfPoints)
            {
                Function _interpolatedFunction = _function.Interpolate(InterpolationMethod.Polynomial, _numberOfPoints);
                return _interpolatedFunction;
            }
            else
            {
                return _function;
            }
        }
    }


    internal class NewFunctionWizardState : WizardStateBase
    {
        internal enum FunctionType
        {
            IndicatorDiagram_GasPressureVsFiringAngle_TwoStroke,
            IndicatorDiagram_GasPressureVsFiringAngle_FourStroke
        }


        private string selectedFunctionTypeNode = null;
        public string SelectedFunctionTypeNode
        {
            get { return selectedFunctionTypeNode; }
            set { selectedFunctionTypeNode = value; }
        }
        private string functionTypeName = null;
        public string FunctionTypeName
        {
            get { return functionTypeName; }
            set { functionTypeName = value; }
        }
        private FunctionType selectedFunctionType;
        public FunctionType SelectedFunctionType
        {
            get { return selectedFunctionType; }
            set { selectedFunctionType = value; }
        }

        //private double minX = double.NaN;
        //public double MinX
        //{
        //    get { return minX; }
        //    set { minX = value; }
        //}

        //private double maxX = double.NaN;
        //public double MaxX
        //{
        //    get { return maxX; }
        //    set { maxX = value; }
        //}

        //private double minY = double.NaN;
        //public double MinY
        //{
        //    get { return minY; }
        //    set { minY = value; }
        //}

        //private double maxY = double.NaN;
        //public double MaxY
        //{
        //    get { return maxY; }
        //    set { maxY = value; }
        //}

        private double lowestPressureOnIntakeStroke = double.NaN;
        public double LowestPressureOnIntakeStroke
        {
            get { return lowestPressureOnIntakeStroke; }
            set { lowestPressureOnIntakeStroke = value; }
        }

        private double highestPressureOnPowerStroke = double.NaN;
        public double HighestPressureOnPowerStroke
        {
            get { return highestPressureOnPowerStroke; }
            set { highestPressureOnPowerStroke = value; }
        }

        private double averageAtmosphericPressure = double.NaN;
        public double AverageAtmosphericPressure
        {
            get { return averageAtmosphericPressure; }
            set { averageAtmosphericPressure = value; }
        }

        private int numberOfStartingPoints = 0;
        public int NumberOfStartingPoints
        {
            get { return numberOfStartingPoints; }
            set { numberOfStartingPoints = value; }
        }

        private Function function = null;
        public Function Function
        {
            get { return function; }
            set { function = value; }
        }


        #region IWizardState Members
        public new void SetDefaults()
        {
            this.lowestPressureOnIntakeStroke = -1d;
            this.highestPressureOnPowerStroke = 10d;
            this.averageAtmosphericPressure = 1d;

            this.numberOfStartingPoints = 5;
        }
        #endregion
    }

}
