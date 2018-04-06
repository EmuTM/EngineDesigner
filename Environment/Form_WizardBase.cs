using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment.Controls;

namespace EngineDesigner.Environment
{
    //NOTE: ta class bi moral bit abstract, samo se neki ica form designer
    public partial class Form_WizardBase : Form
    {
        private WizardDialogResult wizardDialogResult = WizardDialogResult.CANCEL;



        [DefaultValue(null)]
        public Image @Image
        {
            get { return this.pictureBox1.Image; }
            set { this.pictureBox1.Image = value; }
        } //NOTE: ta property bi moral biti protected, samo se neki ica form designer

        [DefaultValue("Title")]
        public string Title
        {
            get { return this.label_Title.Text; }

            set
            {
                this.label_Title.Text = value;
                this.Text = this.label_Title.Text;
            }
        } //NOTE: ta property bi moral biti protected, samo se neki ica form designer

        private int totalSteps = 0;
        [DefaultValue(0)]
        public int TotalSteps
        {
            get { return this.totalSteps; }

            set
            {
                this.totalSteps = value;
                this.SetStep();
            }
        } //NOTE: ta property bi moral biti protected, samo se neki ica form designer

        private int currentStep = 0;
        [DefaultValue(0)]
        public int CurrentStep
        {
            get { return this.currentStep; }

            set
            {
                this.currentStep = value;
                this.SetStep();
            }
        } //NOTE: ta property bi moral biti protected, samo se neki ica form designer

        [DefaultValue(true)]
        public bool BackEnabled
        {
            get { return this.button_Back.Enabled; }

            set
            {
                this.button_Back.Enabled = value;
                this.SetButtons();
            }
        } //NOTE: ta property bi moral biti protected, samo se neki ica form designer

        [DefaultValue(true)]
        public bool NextEnabled
        {
            get { return this.button_Next.Enabled; }
            set
            {
                this.button_Next.Enabled = value;
                this.SetButtons();
            }
        } //NOTE: ta property bi moral biti protected, samo se neki ica form designer

        [DefaultValue(true)]
        public bool FinishEnabled
        {
            get { return this.button_Finish.Enabled; }
            set
            {
                this.button_Finish.Enabled = value;
                this.SetButtons();
            }
        }//NOTE: ta property bi moral biti protected, samo se neki ica form designer

        [DefaultValue(true)]
        public bool CloseEnabled
        {
            get { return this.button_Close.Enabled; }
            set
            {
                this.button_Close.Enabled = value;
                this.SetButtons();
            }
        }//NOTE: ta property bi moral biti protected, samo se neki ica form designer

        private Form_WizardBase previous = null;
        [DefaultValue(null)]
        protected internal Form_WizardBase Previous
        {
            get { return previous; }
        }

        private Form_WizardBase next = null;
        [DefaultValue(null)]
        protected internal Form_WizardBase Next
        {
            get { return next; }

            set
            {
                //ker se lahko večkrat spremeni, poskrbimo, da disposamo prejšnjo
                if ((this.next != null)
                    && (!this.next.Disposing))
                {
                    this.next.Dispose();
                }

                this.next = value;

                this.SetButtons();
            }
        }

        private Form_WizardBase finish = null;
        [DefaultValue(null)]
        protected internal Form_WizardBase Finish
        {
            get { return finish; }

            set
            {
                //ker se lahko večkrat spremeni, poskrbimo, da disposamo prejšnjo
                if ((this.finish != null)
                    && (!this.finish.Disposing))
                {
                    this.finish.Dispose();
                }

                this.finish = value;

                this.SetButtons();
            }
        }

        private WizardStateBase state = null;
        [DefaultValue(null)]
        public WizardStateBase State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        private object result = null;
        [DefaultValue(null)]
        protected internal object Result
        {
            get { return result; }
            protected set { result = value; }
        }



        public Form_WizardBase()
            : this(null)
        {
        }
        public Form_WizardBase(Form_WizardBase _previous)
        {
            InitializeComponent();


            this.previous = _previous;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            this.SetButtons();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);


            if (this.wizardDialogResult == WizardDialogResult.CANCEL)
            {
                e.Cancel = !this.OnCancel();
            }
        }



        public new WizardDialogResult ShowDialog()
        {
            base.ShowDialog();
            return this.wizardDialogResult;
        }



        private void SetStep()
        {
            this.label_Step.Text = string.Format(
                "Step: {0}/{1}",
                this.currentStep,
                this.totalSteps);
        }
        private void SetButtons()
        {
            //obvezno v tem vrstnem redu gumbi!!!

            if (this.previous != null)
            {
                this.button_Back.Enabled = true;
                this.AcceptButton = this.button_Back;
                this.button_Back.Focus();
            }
            else
            {
                this.button_Back.Enabled = false;
            }

            if (this.finish != null)
            {
                this.button_Finish.Visible = true;
                this.AcceptButton = this.button_Finish;
                this.button_Finish.Focus();
            }
            else
            {
                this.button_Finish.Visible = false;
            }


            if (this.next != null)
            {
                this.button_Next.Visible = true;
                this.AcceptButton = this.button_Next;
                this.button_Next.Focus();
            }
            else
            {
                this.button_Next.Visible = false;
            }


            if ((this.next == null)
                && (this.finish == null))
            {
                this.button_Close.Visible = true;
                this.AcceptButton = this.button_Close;
                this.button_Close.Focus();
            }
            else
            {
                this.button_Close.Visible = false;
            }
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            bool _bool = this.OnBack();

            if (_bool)
            {
                this.wizardDialogResult = WizardDialogResult.BACK;
                this.Close();
            }
        }
        private void button_Next_Click(object sender, EventArgs e)
        {
            bool _bool = this.OnNext();

            if (_bool)
            {
                this.wizardDialogResult = WizardDialogResult.NEXT;
                this.Close();
            }
        }
        private void button_Finish_Click(object sender, EventArgs e)
        {
            bool _bool = this.OnFinish();

            if (_bool)
            {
                this.wizardDialogResult = WizardDialogResult.FINISH;
                this.Close();
            }
        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            bool _bool = this.OnClose();

            if (_bool)
            {
                this.wizardDialogResult = WizardDialogResult.CLOSE;
                this.Close();
            }
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.wizardDialogResult = WizardDialogResult.CANCEL;
            this.Close();
        }



        protected virtual bool OnCancel()
        {
            DialogResult _dialogResult = MessageBox.Show(
                this,
                "Are you sure, you want to quit this wizard?",
                "Confirm wizard quit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (_dialogResult == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected virtual bool OnBack()
        {
            return true;
        }
        protected virtual bool OnNext()
        {
            return true;
        }
        protected virtual bool OnFinish()
        {
            return true;
        }
        protected virtual bool OnClose()
        {
            return true;
        }

        protected virtual int NumberOfExpectedNextParameters
        {
            get { return 1; }
        }

    }


    public enum WizardDialogResult
    {
        CANCEL,
        BACK,
        NEXT,
        FINISH,
        CLOSE
    }

    public class WizardStateBase
    {
        public WizardStateBase()
        {
            this.SetDefaults();
        }
        protected virtual void SetDefaults()
        {
        }
    }

    public static class WizardManager
    {
        public static T StartWizard<T>(Form_WizardBase _entryForm)
        {
            if (_entryForm.Previous != null)
            {
                throw new Exception("EntryForm ne sme imet previousa!!!");
            }


            T _t = WizardManager.RunForm<T>(_entryForm);

            #region "Dispose all"
            List<Form_WizardBase> _list = new List<Form_WizardBase>();
            WizardManager.GetAllForms(ref _list, _entryForm);
            foreach (Form_WizardBase _form_WizardBase in _list)
            {
                if (!_entryForm.Disposing)
                {
                    _form_WizardBase.Dispose();
                }
            }
            #endregion "Dispose all"

            return _t;
        }


        private static void GetAllForms(ref List<Form_WizardBase> _list, Form_WizardBase _entryForm)
        {
            _list.Add(_entryForm);

            if (_entryForm.Next != null)
            {
                WizardManager.GetAllForms(ref _list, _entryForm.Next);
            }
        }

        private static T RunForm<T>(Form_WizardBase _form_WizardBase)
        {
            WizardDialogResult _wizardDialogResult = _form_WizardBase.ShowDialog();

            switch (_wizardDialogResult)
            {
                case WizardDialogResult.BACK:
                    {
                        _form_WizardBase.Previous.StartPosition = FormStartPosition.Manual;
                        _form_WizardBase.Previous.Location = _form_WizardBase.Location;
                        _form_WizardBase.Previous.Size = _form_WizardBase.Size;
                        _form_WizardBase.Previous.State = _form_WizardBase.State;

                        //trenutno formo disposamo, ker je lahko vedno druga zaradi različnih opcij
                        Form_WizardBase _previous = _form_WizardBase.Previous;
                        _form_WizardBase.Dispose();

                        return WizardManager.RunForm<T>(_previous);
                    }


                case WizardDialogResult.NEXT:
                    {
                        _form_WizardBase.Next.StartPosition = FormStartPosition.Manual;
                        _form_WizardBase.Next.Location = _form_WizardBase.Location;
                        _form_WizardBase.Next.Size = _form_WizardBase.Size;
                        _form_WizardBase.Next.State = _form_WizardBase.State;

                        //trenutne forme ne smemo disposat, ker jo potrebujemo za Back

                        return WizardManager.RunForm<T>(_form_WizardBase.Next);
                    }

                case WizardDialogResult.FINISH:
                    {
                        _form_WizardBase.Finish.StartPosition = FormStartPosition.Manual;
                        _form_WizardBase.Finish.Location = _form_WizardBase.Location;
                        _form_WizardBase.Finish.Size = _form_WizardBase.Size;
                        _form_WizardBase.Finish.State = _form_WizardBase.State;

                        //trenutne forme ne smemo disposat, ker jo potrebujemo za Back

                        return WizardManager.RunForm<T>(_form_WizardBase.Finish);
                    }

                case WizardDialogResult.CLOSE:
                    {
                        return (T)_form_WizardBase.Result;
                    }


                default:
                    {
                        return default(T);
                    }
            }
        }

    }

}
