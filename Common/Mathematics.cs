using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineDesigner.Common
{
    /// <summary>
    /// Provides basic utility for number manipulation.
    /// </summary>
    public static class Mathematics
    {
        /// <summary>
        /// Returns true, if the number is even.
        /// </summary>
        /// <param name="_number">Any real number.</param>
        public static bool IsEven(double _number)
        {
            if (_number % 2 == 0)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Returns true, if the number is odd.
        /// </summary>
        /// <param name="_number">Any real number.</param>
        public static bool IsOdd(double _number)
        {
            if (_number % 2 != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the multiple of a number, if exists; otherwise returns 0.
        /// </summary>
        /// <param name="_multipleOf">The number which is to be the n-th multiplier of the '_number'.</param>
        /// <param name="_number">The number to be multiple.</param>
        public static long GetMultiple(double _multipleOf, double _number)
        {
            if (_multipleOf % _number == 0)
            {
                return (long)(Math.Floor(_multipleOf / _number));
            }


            return 0;
        }

        /// <summary>
        /// Returns true, if the number is a multiple; otherwise returns false.
        /// </summary>
        /// <param name="_multipleOf">The number which is to be the n-th multiplier of the '_number'.</param>
        /// <param name="_number">The number to be multiple.</param>
        public static bool IsMultiple(double _multipleOf, double _number)
        {
            return !(GetMultiple(_multipleOf, _number) == 0);
        }

        /// <summary>
        /// Gets the minimum value within the array.
        /// </summary>
        /// <param name="_numbers">The array composed from real numbers.</param>
        public static double GetMin(params double[] _numbers)
        {
            double _min = double.MaxValue;

            foreach (double _double in _numbers)
            {
                if (_double < _min)
                {
                    _min = _double;
                }
            }

            return _min;
        }
        /// <summary>
        /// Gets the minimum value within the list.
        /// </summary>
        /// <param name="_numbers">The collection composed from real numbers.</param>
        public static double GetMin(ICollection<double> _numbers)
        {
            double _min = double.MaxValue;

            foreach (double _double in _numbers)
            {
                if (_double < _min)
                {
                    _min = _double;
                }
            }

            return _min;
        }
        /// <summary>
        /// Gets the maximum value within the array.
        /// </summary>
        /// <param name="_numbers">The array composed from real numbers.</param>
        /// <returns></returns>
        public static double GetMax(params double[] _numbers)
        {
            double _max = double.MinValue;

            foreach (double _double in _numbers)
            {
                if (_double > _max)
                {
                    _max = _double;
                }
            }

            return _max;
        }
        /// <summary>
        /// Gets the maximum value within the list.
        /// </summary>
        /// <param name="_numbers">The collection composed from real numbers.</param>
        /// <returns></returns>
        public static double GetMax(ICollection<double> _numbers)
        {
            double _max = double.MinValue;

            foreach (double _double in _numbers)
            {
                if (_double > _max)
                {
                    _max = _double;
                }
            }

            return _max;
        }
        /// <summary>
        /// Gets the average value within the array.
        /// </summary>
        /// <param name="_numbers">The array composed from real numbers.</param>
        public static double GetAverage(double[] _numbers)
        {
            double _average = 0;

            foreach (double _double in _numbers)
            {
                _average += _double;
            }
            _average /= _numbers.Length;

            return _average;
        }
        /// <summary>
        /// Gets the average value within the list.
        /// </summary>
        /// <param name="_numbers">The list composed from real numbers.</param>
        public static double GetAverage(ICollection<double> _numbers)
        {
            double _average = 0;

            foreach (double _double in _numbers)
            {
                _average += _double;
            }
            _average /= _numbers.Count;

            return _average;
        }

        ///// <summary>
        ///// Rounds a value to a specified number of fractional digits and ommits the rest.
        ///// </summary>
        ///// <param name="_double">A number to be rounded.</param>
        ///// <param name="_decimals">The number of fractional digits in the return value.</param>
        //public static double Round(double _double, int _decimals)
        //{
        //    double _roundedDouble = Math.Round(_double, _decimals);


        //    StringBuilder _stringBuilder = new StringBuilder();
        //    _stringBuilder.Append("#");
        //    if (_decimals > 0)
        //    {
        //        _stringBuilder.Append(".");

        //        for (int a = 0; a < _decimals; a++)
        //        {
        //            _stringBuilder.Append("0");
        //        }
        //    }


        //    string _roudedString = _roundedDouble.ToString(_stringBuilder.ToString());
        //    return double.Parse(_roudedString);
        //}


        /// <summary>
        /// Sorts the array using the BubbleSort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array.</typeparam>
        /// <param name="_array">The array to be sorted.</param>
        public static void BubbleSort<T>(ref T[] _array) where T : IComparable
        {
            long _rightBorder = _array.Length - 1;

            do
            {
                long _lastExchange = 0;

                for (int a = 0; a < _rightBorder; a++)
                {
                    if (_array[a].CompareTo(_array[a + 1]) > 0)
                    {
                        T _t = _array[a];
                        _array[a] = _array[a + 1];
                        _array[a + 1] = _t;

                        _lastExchange = a;
                    }
                }

                _rightBorder = _lastExchange;
            }
            while (_rightBorder > 0);
        }
        /// <summary>
        /// Sorts the list using the BubbleSort algorithm.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="_list">The list to be sorted.</param>
        public static void BubbleSort<T>(ref List<T> _list) where T : IComparable
        {
            long _rightBorder = _list.Count - 1;

            do
            {
                long _lastExchange = 0;

                for (int a = 0; a < _rightBorder; a++)
                {
                    if (_list[a].CompareTo(_list[a + 1]) > 0)
                    {
                        T _t = _list[a];
                        _list[a] = _list[a + 1];
                        _list[a + 1] = _t;

                        _lastExchange = a;
                    }
                }

                _rightBorder = _lastExchange;
            }
            while (_rightBorder > 0);
        }


        /// <summary>
        /// Gets the absolute angle (scaled down by all full-circle angles).
        /// </summary>
        /// <param name="_angle_deg">The angle in degrees.</param>
        public static float GetAbsoluteAngle_deg(float _angle_deg)
        {
            return (float)GetAbsoluteAngle_deg(_angle_deg, 1);
        }
        /// <summary>
        /// Gets the absolute angle (scaled down by all full-circle angles).
        /// </summary>
        /// <param name="_angle_deg">The angle in degrees.</param>
        public static double GetAbsoluteAngle_deg(double _angle_deg)
        {
            return GetAbsoluteAngle_deg(_angle_deg, 1);
        }
        /// <summary>
        /// Gets the absolute angle (scaled down by all full-circle angles).
        /// </summary>
        /// <param name="_angle_deg">The angle in degrees.</param>
        /// <param name="_forcePositive">Defines whether the resulting angle should always be a positive value.</param>
        public static double GetAbsoluteAngle_deg(double _angle_deg, bool _forcePositive)
        {
            return GetAbsoluteAngle_deg(_angle_deg, 1, _forcePositive);
        }
        /// <summary>
        /// Gets the absolute angle (scaled down by '_fullRotationsToGo' full-circle angles).
        /// </summary>
        /// <param name="_angle">The angle in degrees.</param>
        /// <param name="_fullRotationsToGo">Defines how many full rotations should be thought as full-circle angles.</param>
        public static double GetAbsoluteAngle_deg(double _angle_deg, int _fullRotationsToGo)
        {
            return GetAbsoluteAngle_deg(_angle_deg, _fullRotationsToGo, false);
        }
        /// <summary>
        /// Gets the absolute angle (scaled down by '_fullRotationsToGo' full-circle angles).
        /// </summary>
        /// <param name="_angle">The angle in degrees.</param>
        /// <param name="_fullRotationsToGo">Defines how many full rotations should be thought as full-circle angles.</param>
        /// <param name="_forcePositive">Defines whether the resulting angle should always be a positive value.</param>
        public static double GetAbsoluteAngle_deg(double _angle_deg, int _fullRotationsToGo, bool _forcePositive)
        {
            if (_fullRotationsToGo < 1)
            {
                throw new Exception("At least one circle has to be completed.");
            }

            double _repeatAngle = 360d * (double)_fullRotationsToGo;
            double _double = _angle_deg % _repeatAngle;

            if ((_double < 0)
                && (_forcePositive))
            {
                _double = _repeatAngle + _double;
            }

            return _double;
        }

        /// <summary>
        /// Gets the number of full rotations of an angle (how many circles the angle described).
        /// </summary>
        /// <param name="_angle_deg">The angle in degrees.</param>
        public static long GetFullRotations(double _angle_deg)
        {
            long _long = (long)(_angle_deg / 360d);

            return _long;
        }
        /// <summary>
        /// Gets the number of rotations of an angle (any angle is thought as a rotation!).
        /// </summary>
        /// <param name="_angle_deg">The angle in degrees.</param>
        public static long GetAnyRotations(double _angle_deg)
        {
            return GetAnyRotations(_angle_deg, 1);
        }
        /// <summary>
        /// Gets the number of rotations of an angle (any angle is thought as a rotation!).
        /// </summary>
        /// <param name="_angle_deg">The angle in degrees.</param>
        /// <param name="_fullRotationsToGo">Defines how many full rotations should be thought as full-circle angles.</param>
        public static long GetAnyRotations(double _angle_deg, int _fullRotationsToGo)
        {
            long _long = (long)(_angle_deg / (360d * _fullRotationsToGo));
            double _double = _angle_deg % (360d * _fullRotationsToGo);

            if (_double < 0)
            {
                _long--;
            }
            else if (_double > 0)
            {
                _long++;
            }

            return _long;
        }

        /// <summary>
        /// Computes an angle, in degrees, that is  the n-th cyclic angle between _angleMin_deg and _angleMax_deg.
        /// </summary>
        /// <param name="_angle_deg">The input angle in degrees.</param>
        /// <param name="_angleMin_deg">The minimum angle, in degrees, of the view frame.</param>
        /// <param name="_angleMax_deg">The maximum angle, in degrees, of the view frame.</param>
        /// <param name="_cycle_deg">The angle, in degrees, of the cycle to be computed as minimum.</param>
        public static double GetCyclicAngle(double _angle_deg, double _angleMin_deg, double _angleMax_deg, double _cycle_deg)
        {
            double _angleAltered_deg = _angle_deg;


            if (_angle_deg > _angleMax_deg)
            {
                double _deltaOnChart = Math.Abs(_angleMax_deg - _angleMin_deg);
                int _combinedCyclesForOneDelta = (int)Math.Ceiling(_deltaOnChart / _cycle_deg);
                double _degreesToRollBackForOneDelta = _combinedCyclesForOneDelta * _cycle_deg;

                double _deltaToEnd = Math.Abs(_angle_deg - _angleMax_deg);
                if (_deltaToEnd < _degreesToRollBackForOneDelta)
                {
                    _deltaToEnd = _degreesToRollBackForOneDelta;
                }

                int _timesToRollBack = (int)Math.Ceiling(_deltaToEnd / _degreesToRollBackForOneDelta);
                _angleAltered_deg -= (_degreesToRollBackForOneDelta * _timesToRollBack);
            }
            else if (_angle_deg < _angleMin_deg)
            {
                double _deltaOnChart = Math.Abs(_angleMax_deg - _angleMin_deg);
                int _combinedCyclesForOneDelta = (int)Math.Ceiling(_deltaOnChart / _cycle_deg);
                double _degreesToRollForthForOneDelta = _combinedCyclesForOneDelta * _cycle_deg;

                double _deltaToStart = Math.Abs(_angle_deg - _angleMin_deg);
                if (_deltaToStart < _degreesToRollForthForOneDelta)
                {
                    _deltaToStart = _degreesToRollForthForOneDelta;
                }

                int _timesToRollForth = (int)Math.Ceiling(_deltaToStart / _degreesToRollForthForOneDelta);
                _angleAltered_deg += (_degreesToRollForthForOneDelta * _timesToRollForth);
            }


            return _angleAltered_deg;
        }

        /// <summary>
        /// Gets the percental difference between two values.
        /// </summary>
        public static double GetPercentalDifference(double _value1, double _value2)
        {
            if ((double.IsNaN(_value1))
                || (double.IsInfinity(_value1))
                || (double.IsNaN(_value2))
                || (double.IsInfinity(_value2)))
            {
                throw new ArgumentException();
            }


            if (_value1 == 0)
            {
                _value1 += 0.0000000001d;
            }
            if (_value2 == 0)
            {
                _value2 += 0.0000000001d;
            }


            double _difference = ((_value1 / _value2) * 100d) - 100d;
            _difference = Math.Abs(_difference);


            return _difference;
        }

        /// <summary>
        /// Gets linearly interpolated value between two given values.
        /// </summary>
        /// <param name="_start">The starting value.</param>
        /// <param name="_end">The ending value.</param>
        public static double GetLinearInterpolation(double _start, double _end)
        {
            return (_start + (_end - _start) / 2d);
        }

        public static double CosineInterpolate(double y1, double y2, double mu)
        {
            double mu2;

            mu2 = (1 - Math.Cos(mu * Math.PI)) / 2;
            return (y1 * (1 - mu2) + y2 * mu2);
        }

        public static double GetTimeOfRotation_s(double _rpm, double _angle_deg)
        {
            double _degps = Conversions.RpmToDegps(_rpm);
            double _timeOfRotation_s = _angle_deg / _degps;

            return _timeOfRotation_s;
        }
        public static double GetAngleOfRotation_deg(double _rpm, double _time_s)
        {
            double _degps = Conversions.RpmToDegps(_rpm);
            double _angleOfRotation_deg = _degps * _time_s;

            return _angleOfRotation_deg;
        }


        #region "MathParser"
        public class MathParser
        {
            //markers (each marker should have length equals to 1)
            private const string NumberMarker = "#";
            private const string OperatorMarker = "$";
            private const string FunctionMarker = "@";

            //tokens
            private const string Plus = OperatorMarker + "+";
            private const string UnPlus = OperatorMarker + "un+";
            private const string Minus = OperatorMarker + "-";
            private const string UnMinus = OperatorMarker + "un-";
            private const string Multiply = OperatorMarker + "*";
            private const string Divide = OperatorMarker + "/";
            private const string Power = OperatorMarker + "^";
            private const string LeftParentheses = OperatorMarker + "(";
            private const string RightParentheses = OperatorMarker + ")";
            private const string SquareRoot = FunctionMarker + "sqrt";
            private const string Sine = FunctionMarker + "sin";
            private const string Cosine = FunctionMarker + "cos";
            private const string Tangent = FunctionMarker + "tg";
            private const string Cotangent = FunctionMarker + "ctg";
            private const string HyperbolicSine = FunctionMarker + "sh";
            private const string HyperbolicCosine = FunctionMarker + "ch";
            private const string HyperbolicTangent = FunctionMarker + "th";
            private const string Logarithm = FunctionMarker + "log";
            private const string NaturalLogarithm = FunctionMarker + "ln";
            private const string Exponent = FunctionMarker + "exp";
            private const string Absolute = FunctionMarker + "abs";

            //supported input token, internal token or number
            private readonly Dictionary<string, Tuple<string, string>> supportedOperators = new Dictionary<string, Tuple<string, string>>
            {
                //key - value; description
                { "+", new Tuple<string, string>(Plus, "Plus") },                
                { "-", new Tuple<string, string>(Minus, "Minus") },
                { "*", new Tuple<string, string>(Multiply, "Multiply") },
                { "/", new Tuple<string, string>(Divide, "Divide") },
                { "^", new Tuple<string, string>(Power, "Power") },
                { "(", new Tuple<string, string>(LeftParentheses, "Left parentheses") },
                { ")", new Tuple<string, string>(RightParentheses, "Right parentheses")  }
            };
            private readonly Dictionary<string, Tuple<string, string>> supportedFunctions = new Dictionary<string, Tuple<string, string>>
            {
                //key - value; description
                { "sqrt", new Tuple<string, string>(SquareRoot, "Square root") },
                { "sin", new Tuple<string, string>(Sine, "Sine") },
                { "cos", new Tuple<string, string>(Cosine, "Cosine") },
                { "tg", new Tuple<string, string>(Tangent, "Tangent") },
                { "ctg", new Tuple<string, string>(Cotangent, "Cotangent") },
                { "sh", new Tuple<string, string>(HyperbolicSine, "Hyperbolic sine") },
                { "ch", new Tuple<string, string>(HyperbolicCosine, "Hyperbolic cosine") },
                { "th", new Tuple<string, string>(HyperbolicTangent, "Hyperbolic tangent") },
                { "ln", new Tuple<string, string>(NaturalLogarithm, "Natural logarithm") },
                { "log", new Tuple<string, string>(Logarithm, "Logarithm") },
                { "exp", new Tuple<string, string>(Exponent, "Exponent") },
                { "abs", new Tuple<string, string>(Absolute, "Absolute") }
            };
            private readonly Dictionary<string, Tuple<string, string>> supportedConstants = new Dictionary<string, Tuple<string, string>>
            {
                //key - value; description
                {"pi", new Tuple<string, string>(NumberMarker +  Math.PI.ToString(), Math.PI.ToString(Defaults.ROUNDING)) },
                {"e", new Tuple<string, string>(NumberMarker + Math.E.ToString(), Math.E.ToString(Defaults.ROUNDING)) }
            };

            private readonly char decimalSeparator;
            private readonly bool radians;



            //constant name - constant value
            public MathParser(params KeyValuePair<string, double>[] _constants)
                : this(false, _constants)
            {
            }
            public MathParser(bool _radians, params KeyValuePair<string, double>[] _constants)
            {
                this.radians = _radians;

                foreach (KeyValuePair<string, double> _constant in _constants)
                {
                    this.supportedConstants.Add(
                        _constant.Key,
                        new Tuple<string, string>(NumberMarker + _constant.Value.ToString(), "User defined"));
                }

                this.decimalSeparator = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            }



            /// <summary>
            /// Gets the result of the given mathematical expression.
            /// </summary>
            /// <param name="_expression">Mathematical expression (in standard notation)</param>
            /// <param name="_constants">List of additional constants within the expression (constant name - constant value)</param>
            public double Compute(string _expression)
            {
                string _formattedExpression = this.FormatString(_expression);
                string _rpnExpression = this.ConvertToRPN(_formattedExpression);
                return this.Calculate(_rpnExpression);
            }

            public KeyValuePair<string, string>[] GetSupportedOperators()
            {
                Dictionary<string, string> _supportedOperators = new Dictionary<string, string>();
                foreach (KeyValuePair<string, Tuple<string, string>> _keyValuePair in this.supportedOperators)
                {
                    string _key = _keyValuePair.Key;
                    string _value = _keyValuePair.Value.Item2;

                    _supportedOperators.Add(_key, _value);
                }
                return _supportedOperators.ToArray();
            }
            public KeyValuePair<string, string>[] GetSupportedFunctions()
            {
                Dictionary<string, string> _supportedFunctions = new Dictionary<string, string>();
                foreach (KeyValuePair<string, Tuple<string, string>> _keyValuePair in this.supportedFunctions)
                {
                    string _key = _keyValuePair.Key;
                    string _value = _keyValuePair.Value.Item2;

                    _supportedFunctions.Add(_key, _value);
                }
                return _supportedFunctions.ToArray();
            }
            public KeyValuePair<string, string>[] GetSupportedConstants()
            {
                Dictionary<string, string> _supportedConstants = new Dictionary<string, string>();
                foreach (KeyValuePair<string, Tuple<string, string>> _keyValuePair in this.supportedConstants)
                {
                    string _key = _keyValuePair.Key;
                    string _value = _keyValuePair.Value.Item2;

                    _supportedConstants.Add(_key, _value);
                }
                return _supportedConstants.ToArray();
            }



            /// <summary>
            /// Produce formatted string by the given string
            /// </summary>
            /// <param name="_expression">Unformatted math expression</param>
            /// <returns>Formatted math expression</returns>
            private string FormatString(string _expression)
            {
                if (string.IsNullOrEmpty(_expression))
                {
                    throw new Exception("Expression is null or empty.");
                }


                StringBuilder _formattedString = new StringBuilder();

                int _balanceOfParenth = 0; // Check number of parenthesis
                // Format string in one iteration and check number of parenthesis
                // (this function do 2 tasks because performance priority)
                for (int i = 0; i < _expression.Length; i++)
                {
                    char _char = _expression[i];

                    if (_char == '(')
                    {
                        _balanceOfParenth++;
                    }
                    else if (_char == ')')
                    {
                        _balanceOfParenth--;
                    }

                    if (Char.IsWhiteSpace(_char))
                    {
                        continue;
                    }
                    else if (Char.IsUpper(_char))
                    {
                        _formattedString.Append(Char.ToLower(_char));
                    }
                    else
                    {
                        _formattedString.Append(_char);
                    }
                }

                if (_balanceOfParenth != 0)
                {
                    throw new Exception("Number of left and right parenthesis is not equal.");
                }


                return _formattedString.ToString();
            }
            /// <summary>
            /// Produce math expression in reverse polish notation
            /// by the given string
            /// </summary>
            /// <param name="_expression">Math expression in infix notation</param>
            /// <returns>Math expression in postfix notation (RPN)</returns>
            private string ConvertToRPN(string _expression)
            {
                int _position = 0; // Current position of lexical analysis
                StringBuilder _outputString = new StringBuilder();
                Stack<string> _stack = new Stack<string>();

                // While there is unhandled char in _expression
                while (_position < _expression.Length)
                {
                    string token = this.LexicalAnalysisInfixNotation(_expression, ref _position);

                    _outputString = this.SyntaxAnalysisInfixNotation(token, _outputString, _stack);
                }

                // Pop all elements from stack to output string            
                while (_stack.Count > 0)
                {
                    // There should be only operators
                    if (_stack.Peek()[0] == OperatorMarker[0])
                    {
                        _outputString.Append(_stack.Pop());
                    }
                    else
                    {
                        throw new Exception("A function must have parenthesis.");
                    }
                }

                return _outputString.ToString();
            }
            /// <summary>
            /// Calculate expression in reverse-polish notation
            /// </summary>
            /// <param name="_expression">Math expression in reverse-polish notation</param>
            /// <returns>Result</returns>
            private double Calculate(string _expression)
            {
                int _position = 0; // Current position of lexical analysis
                Stack<double> _stack = new Stack<double>(); // Contains operands

                // Analyse entire _expression
                while (_position < _expression.Length)
                {
                    string token = this.LexicalAnalysisRPN(_expression, ref _position);

                    _stack = SyntaxAnalysisRPN(_stack, token);
                }

                // At end of analysis in stack should be only one operand (result)
                if (_stack.Count > 1)
                {
                    throw new Exception("Excess operand.");
                }

                return _stack.Pop();
            }

            #region "Convert to Reverse-Polish Notation"
            /// <summary>
            /// Produce token by the given math expression
            /// </summary>
            /// <param name="_expression">Math expression in infix notation</param>
            /// <param name="_position">Current position in string for lexical analysis</param>
            /// <returns>Token</returns>
            private string LexicalAnalysisInfixNotation(string _expression, ref int _position)
            {
                // Receive first char
                StringBuilder _token = new StringBuilder();
                _token.Append(_expression[_position]);

                // If it is a operator
                if (this.supportedOperators.ContainsKey(_token.ToString()))
                {
                    // Determine it is unary or binary operator
                    bool _isUnary = _position == 0 || _expression[_position - 1] == '(';
                    _position++;

                    switch (_token.ToString())
                    {
                        case "+":
                            return _isUnary ? UnPlus : Plus;
                        case "-":
                            return _isUnary ? UnMinus : Minus;
                        default:
                            return supportedOperators[_token.ToString()].Item1;
                    }
                }
                else if (Char.IsLetter(_token[0])
                    || this.supportedFunctions.ContainsKey(_token.ToString())
                    || this.supportedConstants.ContainsKey(_token.ToString()))
                {
                    // Read function or constant name

                    while (++_position < _expression.Length
                        && Char.IsLetter(_expression[_position]))
                    {
                        _token.Append(_expression[_position]);
                    }

                    if (this.supportedFunctions.ContainsKey(_token.ToString()))
                    {
                        return this.supportedFunctions[_token.ToString()].Item1;
                    }
                    else if (this.supportedConstants.ContainsKey(_token.ToString()))
                    {
                        return this.supportedConstants[_token.ToString()].Item1;
                    }
                    else
                    {
                        throw new Exception(string.Format(
                            "Unknown token ({0}).",
                            _token));
                    }

                }
                else if (Char.IsDigit(_token[0]) || _token[0] == this.decimalSeparator)
                {
                    // Read number

                    // Read the whole part of number
                    if (Char.IsDigit(_token[0]))
                    {
                        while (++_position < _expression.Length
                        && Char.IsDigit(_expression[_position]))
                        {
                            _token.Append(_expression[_position]);
                        }
                    }
                    else
                    {
                        // Because system decimal separator
                        // will be added below
                        _token.Clear();
                    }

                    // Read the fractional part of number
                    if (_position < _expression.Length
                        && _expression[_position] == this.decimalSeparator)
                    {
                        // Add current system specific decimal separator
                        _token.Append(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                        while (++_position < _expression.Length
                        && Char.IsDigit(_expression[_position]))
                        {
                            _token.Append(_expression[_position]);
                        }
                    }

                    // Read scientific notation (suffix)
                    if (_position + 1 < _expression.Length && _expression[_position] == 'e'
                        && (Char.IsDigit(_expression[_position + 1])
                            || (_position + 2 < _expression.Length
                                && (_expression[_position + 1] == '+'
                                    || _expression[_position + 1] == '-')
                                && Char.IsDigit(_expression[_position + 2]))))
                    {
                        _token.Append(_expression[_position++]); // e

                        if (_expression[_position] == '+' || _expression[_position] == '-')
                            _token.Append(_expression[_position++]); // sign

                        while (_position < _expression.Length
                            && Char.IsDigit(_expression[_position]))
                        {
                            _token.Append(_expression[_position++]); // power
                        }

                        // Convert number from scientific notation to decimal notation
                        return NumberMarker + Convert.ToDouble(_token.ToString());
                    }

                    return NumberMarker + _token.ToString();
                }
                else
                {
                    throw new Exception(string.Format(
                        "Unknown token ({0}).",
                        _token));
                }
            }
            /// <summary>
            /// Syntax analysis of infix notation
            /// </summary>
            /// <param name="_token">Token</param>
            /// <param name="_outputString">Output string (math expression in RPN)</param>
            /// <param name="_stack">Stack which contains operators (or functions)</param>
            /// <returns>Output string (math expression in RPN)</returns>
            private StringBuilder SyntaxAnalysisInfixNotation(string _token, StringBuilder _outputString, Stack<string> _stack)
            {
                // If it's a number just put to string            
                if (_token[0] == NumberMarker[0])
                {
                    _outputString.Append(_token);
                }
                else if (_token[0] == FunctionMarker[0])
                {
                    // if it's a function push to stack
                    _stack.Push(_token);
                }
                else if (_token == LeftParentheses)
                {
                    // If its '(' push to stack
                    _stack.Push(_token);
                }
                else if (_token == RightParentheses)
                {
                    // If its ')' pop elements from stack to output string
                    // until find the ')'

                    string elem;
                    while ((elem = _stack.Pop()) != LeftParentheses)
                    {
                        _outputString.Append(elem);
                    }

                    // if after this a function is in the peek of stack then put it to string
                    if (_stack.Count > 0 &&
                        _stack.Peek()[0] == FunctionMarker[0])
                    {
                        _outputString.Append(_stack.Pop());
                    }
                }
                else
                {
                    // While priority of elements at peek of stack >= (>) token's priority
                    // put these elements to output string
                    while ((_stack.Count > 0)
                        && (this.Priority(_token, _stack.Peek())))
                    {
                        _outputString.Append(_stack.Pop());
                    }

                    _stack.Push(_token);
                }

                return _outputString;
            }
            /// <summary>
            /// Is priority of token less (or equal) to priority of p
            /// </summary>
            private bool Priority(string _token, string _p)
            {
                return this.IsRightAssociated(_token) ?
                    this.GetPriority(_token) < this.GetPriority(_p) :
                    this.GetPriority(_token) <= this.GetPriority(_p);
            }
            /// <summary>
            /// Is right associated operator
            /// </summary>
            private bool IsRightAssociated(string _token)
            {
                return _token == Power;
            }
            /// <summary>
            /// Get priority of operator
            /// </summary>
            private int GetPriority(string _token)
            {
                switch (_token)
                {
                    case LeftParentheses:
                        return 0;

                    case Plus:
                    case Minus:
                        return 2;

                    case Multiply:
                    case Divide:
                        return 4;

                    case UnPlus:
                    case UnMinus:
                        return 6;

                    case Power:
                    case SquareRoot:
                        return 8;

                    case Sine:
                    case Cosine:
                    case Tangent:
                    case Cotangent:
                    case HyperbolicSine:
                    case HyperbolicCosine:
                    case HyperbolicTangent:
                    case Logarithm:
                    case NaturalLogarithm:
                    case Exponent:
                    case Absolute:
                        return 10;


                    default:
                        throw new Exception(string.Format(
                            "Unknown operator ({0}).",
                            _token));
                }
            }
            #endregion "Convert to Reverse-Polish Notation"

            #region "Calculate expression in RPN"
            /// <summary>
            /// Produce token by the given math expression
            /// </summary>
            /// <param name="_expression">Math expression in reverse-polish notation</param>
            /// <param name="_position">Current position of lexical analysis</param>
            /// <returns>Token</returns>
            private string LexicalAnalysisRPN(string _expression, ref int _position)
            {
                StringBuilder _token = new StringBuilder();

                // Read token from marker to next marker
                _token.Append(_expression[_position++]);

                while (_position < _expression.Length && _expression[_position] != NumberMarker[0]
                    && _expression[_position] != OperatorMarker[0]
                    && _expression[_position] != FunctionMarker[0])
                {
                    _token.Append(_expression[_position++]);
                }

                return _token.ToString();
            }
            /// <summary>
            /// Syntax analysis of reverse-polish notation
            /// </summary>
            /// <param name="_stack">Stack which contains operands</param>
            /// <param name="_token">Token</param>
            /// <returns>Stack which contains operands</returns>
            private Stack<double> SyntaxAnalysisRPN(Stack<double> _stack, string _token)
            {
                // if it's operand then just push it to stack
                if (_token[0] == NumberMarker[0])
                {
                    _stack.Push(double.Parse(_token.Remove(0, 1)));
                }
                // Otherwise apply operator or function to elements in stack
                else if (this.NumberOfArguments(_token) == 1)
                {
                    #region
                    double arg = _stack.Pop();
                    double rst;

                    switch (_token)
                    {
                        case UnPlus:
                            rst = arg;
                            break;

                        case UnMinus:
                            rst = -arg;
                            break;

                        case SquareRoot:
                            rst = Math.Sqrt(arg);
                            break;

                        case Sine:
                            rst = ApplyTrigFunction(Math.Sin, arg);
                            break;

                        case Cosine:
                            rst = ApplyTrigFunction(Math.Cos, arg);
                            break;

                        case Tangent:
                            rst = ApplyTrigFunction(Math.Tan, arg);
                            break;

                        case Cotangent:
                            rst = 1 / ApplyTrigFunction(Math.Tan, arg);
                            break;

                        case HyperbolicSine:
                            rst = Math.Sinh(arg);
                            break;

                        case HyperbolicCosine: rst =
                             rst = Math.Cosh(arg);
                            break;

                        case HyperbolicTangent:
                            rst = Math.Tanh(arg);
                            break;

                        case NaturalLogarithm:
                            rst = Math.Log(arg);
                            break;

                        case Exponent:
                            rst = Math.Exp(arg);
                            break;

                        case Absolute:
                            rst = Math.Abs(arg);
                            break;


                        default:
                            throw new Exception(string.Format(
                                "Unknown operator ({0}).",
                                _token));
                    }

                    _stack.Push(rst);
                    #endregion
                }
                else
                {
                    #region
                    // otherwise operator's number of arguments equals to 2

                    double arg2 = _stack.Pop();
                    double arg1 = _stack.Pop();

                    double rst;

                    switch (_token)
                    {
                        case Plus:
                            rst = arg1 + arg2;
                            break;

                        case Minus:
                            rst = arg1 - arg2;
                            break;

                        case Multiply:
                            rst = arg1 * arg2;
                            break;

                        case Divide:
                            if (arg2 == 0)
                            {
                                throw new Exception("Divide by zero.");
                            }
                            rst = arg1 / arg2;
                            break;

                        case Power:
                            rst = Math.Pow(arg1, arg2);
                            break;

                        case Logarithm:
                            rst = Math.Log(arg2, arg1);
                            break;


                        default:
                            throw new Exception(string.Format(
                                "Unknown operator ({0}).",
                                _token));
                    }

                    _stack.Push(rst);
                    #endregion
                }

                return _stack;
            }

            /// <summary>
            /// Apply trigonometric function
            /// </summary>
            /// <param name="_function">Trigonometric function</param>
            /// <param name="_argument">Argument</param>
            /// <returns>Result of function</returns>
            private double ApplyTrigFunction(Func<double, double> _function, double _argument)
            {
                if (!this.radians)
                {
                    _argument = Conversions.DegToRad(_argument);
                }

                return _function(_argument);
            }
            /// <summary>
            /// Produce number of arguments for the given operator
            /// </summary>
            private int NumberOfArguments(string _token)
            {
                switch (_token)
                {
                    case UnPlus:
                    case UnMinus:
                    case SquareRoot:
                    case Tangent:
                    case HyperbolicSine:
                    case HyperbolicCosine:
                    case HyperbolicTangent:
                    case NaturalLogarithm:
                    case Cotangent:
                    case Sine:
                    case Cosine:
                    case Exponent:
                    case Absolute:
                        return 1;

                    case Plus:
                    case Minus:
                    case Multiply:
                    case Divide:
                    case Power:
                    case Logarithm:
                        return 2;


                    default:
                        throw new Exception(string.Format(
                            "Unknown operator ({0}).",
                            _token));
                }
            }
            #endregion "Calculate expression in RPN"

        }
        #endregion "MathParser"

    }
}
