using System.Drawing;

namespace EngineDesigner.Common
{
    public static class Defaults
    {
        public static Font DefaultFont
        {
            get { return new Font("Microsoft Sans Serif", 8f); }
        }
        public const string DefaultFontString = "Microsoft Sans Serif, 8.0pt, style=Regular";
        public static Font DefaultFontBold
        {
            get { return new Font("Microsoft Sans Serif", 8f, FontStyle.Bold); }
        }
        public const string DefaultFontBoldString = "Microsoft Sans Serif, 8.0pt, style=Bold";


        public const string BlackColorString = "Black";
        public static Color BlackColor
        {
            get { return Color.FromName(BlackColorString); }
        }

        public const string GrayColorString = "DarkGray";
        public static Color GrayColor
        {
            get { return Color.FromName(GrayColorString); }
        }

        public const string RedColorString = "Red";
        public static Color RedColor
        {
            get { return Color.FromName(RedColorString); }
        }

        public const string LightRedColorString = "LightSalmon";
        public static Color LightRedColor
        {
            get { return Color.FromName(LightRedColorString); }
        }

        public const string GreenColorString = "Green";
        public static Color GreenColor
        {
            get { return Color.FromName(GreenColorString); }
        }

        public const string LightGreenColorString = "PaleGreen";
        public static Color LightGreenColor
        {
            get { return Color.FromName(LightGreenColorString); }
        }


        public const string IntakeColorString = "PaleTurquoise";
        public static Color IntakeColor
        {
            get { return Color.FromName(IntakeColorString); }
        }

        public const string CompressionColorString = "Thistle";
        public static Color CompressionColor
        {
            get { return Color.FromName(CompressionColorString); }
        }

        public const string CombustionColorString = "LightPink";
        public static Color CombustionColor
        {
            get { return Color.FromName(CombustionColorString); }
        }

        public const string ExhaustColorString = "LightGray";
        public static Color ExhaustColor
        {
            get { return Color.FromName(ExhaustColorString); }
        }

        public const string WashCompressionColorString = "Orchid";
        public static Color WashCompressionColor
        {
            get { return Color.FromName(WashCompressionColorString); }
        }

        public const string CombustionExhaustColorString = "PeachPuff";
        public static Color CombustionExhaustColor
        {
            get { return Color.FromName(CombustionExhaustColorString); }
        }


        public const string SelectedIPartColorString = "RoyalBlue";
        public static Color SelectedIPartColor
        {
            get { return Color.FromName(SelectedIPartColorString); }
        }


        public const string ITEM = "Item";
        public const string HASH = "#";


        public const string ROUNDING = "0.##";
        public static int RoundingDecimals
        {
            get
            {
                int _point = ROUNDING.IndexOf('.');

                int _length = 0;
                if (_point > -1)
                {
                    _length = ROUNDING.Substring(_point).Length - 1;
                }

                if (_length > -1)
                {
                    return _length;
                }
                else
                {
                    return 0;
                }
            }
        }


        public const double RPMTimerInterval = 50;


        public const double DefaultCycle_deg = 720d;


        /// <summary>
        /// Provides the value for standard gravitational acceleration, in meters per second squared.
        /// </summary>
        public const double StandardGravity_mps2 = 9.80665;

    }
}
