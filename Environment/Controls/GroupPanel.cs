using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace EngineDesigner.Environment.Controls
{
    public partial class GroupPanel : Panel
    {
        private const int MARGIN = 0;
        private const string HEADERFORECOLOR = "ActiveCaptionText";
        private const string HEADERBACKCOLOR = "ActiveCaption";



        [Browsable(true)]
        [DefaultValue("Text")]
        public override string Text
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }

        [DefaultValue(typeof(System.Drawing.Font), EngineDesigner.Common.Defaults.DefaultFontString)]
        public Font HeaderFont
        {
            get { return this.label1.Font; }
            set { this.label1.Font = value; }
        }

        [DefaultValue(typeof(Color), HEADERFORECOLOR)]
        public Color HeaderForeColor
        {
            get { return this.label1.ForeColor; }
            set { this.label1.ForeColor = value; }
        }

        [DefaultValue(typeof(Color), HEADERBACKCOLOR)]
        public Color HeaderBackColor
        {
            get { return this.label1.BackColor; }
            set { this.label1.BackColor = value; }
        }



        public GroupPanel()
        {
            InitializeComponent();

        }



        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);


            int _y = this.label1.Location.Y + this.label1.Height + MARGIN;

            foreach (Control _control in this.Controls)
            {
                if (_control != this.label1)
                {
                    if (_control.Location.Y < _y)
                    {
                        _control.Location = new Point(
                            _control.Location.X,
                            _y);
                    }
                }
            }
        }

    }

}
