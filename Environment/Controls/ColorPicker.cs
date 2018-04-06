using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EngineDesigner.Environment.Controls
{
    public partial class ColorPicker : ComboBox
    {
        private const string CUSTOM_TEXT = "Custom...";



        private Random random;



        #region "private Color[] embeddedColors"
        private Color[] embeddedColors = new Color[]
        {
            Color.Black,
            Color.Gray,
            Color.Silver,

            Color.Brown,
            Color.Chocolate,
            Color.SandyBrown,

            Color.DarkRed,
            Color.Crimson,
            Color.Red,
            Color.Tomato,

            Color.DarkOrange,
            Color.Goldenrod,
            Color.Gold,

            Color.DarkGreen,
            Color.MediumSeaGreen,
            Color.LawnGreen,
            Color.GreenYellow,

            Color.Teal,
            Color.CadetBlue,
            Color.SkyBlue,
            Color.PaleTurquoise,

            Color.Navy,
            Color.MediumBlue,
            Color.RoyalBlue,
            Color.CornflowerBlue,

            Color.DarkSlateBlue,
            Color.MediumSlateBlue,

            Color.Purple,
            Color.Orchid,

            Color.MediumVioletRed,
            Color.HotPink,
            Color.LightPink,
        };
        #endregion "private Color[] embeddedColors"
        public Color[] EmbeddedColors
        {
            get { return embeddedColors; }

            set
            {
                embeddedColors = value;


                if (!this.DesignMode)
                {
                    #region "dodamo barve"
                    base.Items.Clear();
                    foreach (Color _color in this.embeddedColors)
                    {
                        base.Items.Add(_color);
                    }
                    base.Items.Add(Color.Empty);

                    //in selektamo kar prvo
                    base.SelectedItem = base.Items[0];
                    #endregion "dodamo barve"
                }
            }
        }

        public Color SelectedColor
        {
            get
            {
                return (Color)this.Items[this.SelectedIndex];
            }
        }



        public ColorPicker()
        {
            InitializeComponent();


            base.DropDownStyle = ComboBoxStyle.DropDownList;
            base.DrawMode = DrawMode.OwnerDrawFixed;

            this.random = new Random();
        }



        public Color GetRandomColor()
        {
            return this.GetRandomColor(false);
        }
        public Color GetRandomColor(bool _selectInPicker)
        {
            int _int = this.random.Next(this.embeddedColors.Length - 1);

            if (_selectInPicker)
            {
                this.SelectedIndex = _int;
            }

            return this.embeddedColors[_int];
        }



        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);


            if (this.Items.Count > 0)
            {
                if (e.Index > -1)
                {
                    if (!(base.Items[e.Index] is Color))
                    {
                        throw new Exception("ne sme bit drugo ku color!");
                    }


                    e.DrawBackground();


                    Color _color = (Color)base.Items[e.Index];
                    SolidBrush _solidBrush = new SolidBrush(e.ForeColor);

                    if (_color != Color.Empty)
                    {
                        Rectangle _rectangle = new Rectangle(
                            e.Bounds.X + 2,
                            e.Bounds.Y + 2,
                            20,
                            e.Bounds.Height - 6);

                        e.Graphics.FillRectangle(
                            new SolidBrush(_color),
                            _rectangle);

                        e.Graphics.DrawRectangle(
                            Pens.Black,
                            _rectangle);


                        string _string = base.GetItemText(base.Items[e.Index]);
                        e.Graphics.DrawString(_string, e.Font, _solidBrush,
                            e.Bounds.X + 25,
                            e.Bounds.Y);
                    }
                    else
                    {
                        e.Graphics.DrawString(CUSTOM_TEXT, e.Font, _solidBrush, e.Bounds);
                    }
                }
            }
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            if (this.SelectedItem != null)
            {
                if ((Color)this.SelectedItem == Color.Empty)
                {
                    DialogResult _dialogResult = this.colorDialog1.ShowDialog();
                    if (_dialogResult == DialogResult.OK)
                    {
                        //po potrebi dodamo
                        if (!base.Items.Contains(this.colorDialog1.Color))
                        {
                            base.Items.Insert(base.Items.Count - 1, this.colorDialog1.Color);
                        }

                        //in selektamo
                        this.SelectedItem = this.colorDialog1.Color;

                        return;
                    }
                    else
                    {
                        //selektamo kar prvo
                        base.SelectedItem = base.Items[0];
                        return;
                    }
                }
            }


            base.OnSelectedValueChanged(e);
        }

    }

}
