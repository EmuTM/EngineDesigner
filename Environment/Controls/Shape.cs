using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EngineDesigner.Environment.Controls
{
    public partial class Shape : UserControl
    {
        private ShapeType shapeType = ShapeType.LINE;
        public ShapeType @ShapeType
        {
            get { return this.shapeType; }

            set
            {
                this.shapeType = value;
                this.Refresh();
            }
        }

        private bool filled = false;
        [DefaultValue(false)]
        public bool Filled
        {
            get { return this.filled; }

            set
            {
                this.filled = value;
                this.Refresh();
            }
        }

        private int shapeWidth = 1;
        [DefaultValue(1)]
        public int ShapeWidth
        {
            get { return this.shapeWidth; }

            set
            {
                this.shapeWidth = value;
                this.Refresh();
            }
        }



        public Shape()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);



            switch (this.shapeType)
            {
                case ShapeType.LINE:
                    {
                        e.Graphics.DrawLine(
                            new Pen(this.ForeColor, this.shapeWidth),
                            0, 0, this.Width - 1, this.Height - 1);
                        break;
                    }

                case ShapeType.RECTANGLE:
                    {
                        if (this.filled)
                        {
                            e.Graphics.FillRectangle(
                                new SolidBrush(this.ForeColor),
                                0, 0, this.Width - 1, this.Height - 1);
                        }
                        else
                        {
                            e.Graphics.DrawRectangle(
                                new Pen(this.ForeColor, this.shapeWidth),
                                0, 0, this.Width - 1, this.Height - 1);
                        }
                    }
                    break;

                case ShapeType.ELIPSE:
                    {
                        if (this.filled)
                        {
                            e.Graphics.FillEllipse(
                                new SolidBrush(this.ForeColor),
                                0, 0, this.Width - 1, this.Height - 1);
                        }
                        else
                        {
                            e.Graphics.DrawEllipse(
                                new Pen(this.ForeColor, this.shapeWidth),
                                0, 0, this.Width - 1, this.Height - 1);
                        }
                    }
                    break;
            }

        }

    }


    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        ELIPSE
    }

}
