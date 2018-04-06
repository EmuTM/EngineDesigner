using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;


namespace EngineDesigner.Environment.Controls
{
    [DefaultEvent("ValueChanged")]
    public class NumericTextBox : TextBox
    {
        private event EventHandler valueChanged;
        public event EventHandler ValueChanged
        {
            add { valueChanged += value; }
            remove { valueChanged -= value; }
        }



        public NumericTextBox()
        {
            NumberFormatInfo _numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            this.decimalSeparator = _numberFormatInfo.NumberDecimalSeparator;
            this.negativeSign = _numberFormatInfo.NegativeSign;

            base.Text = "0";
            base.TextAlign = HorizontalAlignment.Right;
        }



        string decimalSeparator;
        string negativeSign;



        private bool allowDecimals = true;
        [DefaultValue(true)]
        public bool AllowDecimals
        {
            get { return allowDecimals; }
            set { allowDecimals = value; }
        }

        private bool allowNegative = true;
        [DefaultValue(true)]
        public bool AllowNegative
        {
            get { return allowNegative; }
            set { allowNegative = value; }
        }

        //[DefaultValue(0)]
        //public int IntValue
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(base.Text))
        //        {
        //            return int.Parse(base.Text);
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    set { base.Text = value.ToString(); }
        //}
        [DefaultValue(0d)]
        public double Value
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Text))
                {
                    return double.Parse(base.Text);
                }
                else
                {
                    return 0;
                }
            }
            set { base.Text = value.ToString(); }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        [DefaultValue("0")]
        public new string Text
        {
            get { return null; }
            set { }
        }



        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);


            string _keyCharString = e.KeyChar.ToString();


            if (Char.IsDigit(e.KeyChar))
            {
                //številke ok
            }
            else if ((this.allowNegative)
                && (_keyCharString.Equals(negativeSign))
                && ((!string.IsNullOrEmpty(base.Text)) && (!base.Text.Contains(negativeSign))))
            {
                //minus je ok, če je samo eden
            }
            else if ((this.allowDecimals)
                && (_keyCharString.Equals(decimalSeparator))
                && ((!string.IsNullOrEmpty(base.Text)) && (!base.Text.Contains(decimalSeparator))))
            {
                //vejica je ok, če je samo ena
            }
            else if (e.KeyChar == '\b')
            {
                //backspace je ok
            }
            else
            {
                //drugo ni ok
                e.Handled = true;
            }


            //preverimo še tako
            if (!e.Handled)
            {
                string _text = base.Text;

                if (base.SelectionLength > 0)
                {
                    _text = _text.Remove(base.SelectionStart, base.SelectionLength);
                }

                if ((Char.IsDigit(e.KeyChar)) || (_keyCharString.Equals(negativeSign)) || (_keyCharString.Equals(decimalSeparator)))
                {
                    _text = _text.Insert(base.SelectionStart, _keyCharString);

                    double _double;
                    if (!double.TryParse(_text, out _double))
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);


            if (this.valueChanged != null)
            {
                double _double;
                if (double.TryParse(base.Text, out _double))
                {
                    this.valueChanged(this, new EventArgs());
                }
            }
        }
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);


            if (string.IsNullOrEmpty(base.Text))
            {
                base.Text = double.NaN.ToString();
            }
        }

    }

}
