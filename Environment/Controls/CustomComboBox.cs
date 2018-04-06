using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace EngineDesigner.Environment.Controls
{
    public class CustomComboBox : ComboBox
    {
        private string selectedText = string.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string SelectedText
        {
            get { return this.selectedText; }

            set
            {
                this.selectedText = value;
                base.SelectedText = value;
            }
        }

        private int selectionStart = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int SelectionStart
        {
            get { return this.selectionStart; }

            set
            {
                this.selectionStart = value;
                base.SelectionStart = value;
            }
        }

        private int selectionLength = 0;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int SelectionLength
        {
            get { return this.selectionLength; }

            set
            {
                this.selectionLength = value;
                base.SelectionLength = value;
            }
        }


        public string Cut()
        {
            int _selectionStart = this.selectionStart;
            int _selectionLength = this.selectionLength;


            string _cutText = string.Empty;

            if (_selectionLength > 0)
            {
                _cutText = base.Text.Substring(_selectionStart, _selectionLength);
                base.Text = base.Text.Remove(_selectionStart, _selectionLength);

                this.SelectionLength = 0;
            }

            Debug.WriteLine("Selection start: " + base.SelectionStart);
            Debug.WriteLine("Selection length: " + base.SelectionLength);

            return _cutText;
        }
        public string Copy()
        {
            int _selectionStart = this.selectionStart;
            int _selectionLength = this.selectionLength;


            string _copyedText = string.Empty;

            if (_selectionLength > 0)
            {
                _copyedText = base.Text.Substring(_selectionStart, _selectionLength);
            }

            Debug.WriteLine("Selection start: " + base.SelectionStart);
            Debug.WriteLine("Selection length: " + base.SelectionLength);

            return _copyedText;
        }
        public void Paste(string _string)
        {
            int _selectionStart = this.selectionStart;
            int _selectionLength = this.selectionLength;

            this.Cut();
            base.Text = base.Text.Insert(_selectionStart, _string);

            this.SelectionStart = _selectionStart + _string.Length;
            this.SelectionLength = 0;

            Debug.WriteLine("Selection start: " + base.SelectionStart);
            Debug.WriteLine("Selection length: " + base.SelectionLength);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.selectedText = base.SelectedText;
            this.selectionStart = base.SelectionStart;
            this.selectionLength = base.SelectionLength;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.selectedText = base.SelectedText;
            this.selectionStart = base.SelectionStart;
            this.selectionLength = base.SelectionLength;
        }

    }

}
