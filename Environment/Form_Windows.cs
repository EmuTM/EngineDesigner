using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EngineDesigner.Environment
{
    internal partial class Form_Windows : Form
    {
        private Form_MainBase lastActivatedForm = null; //zadnjo aktivirano formo shranimo tukaj, da nam je po zaprtju okna ne zmeša (zaradi ShowDialoga)
        public Form_MainBase LastActivatedForm
        {
            get { return lastActivatedForm; }
        }



        public Form_Windows(params Form_MainBase[] _forms_Main)
        {
            InitializeComponent();


            foreach (Form_MainBase _form_MainBase in _forms_Main)
            {
                ListViewItem _listViewItem = new ListViewItem();
                _listViewItem.Tag = _form_MainBase;
                _listViewItem.Text = _form_MainBase.Caption;

                ListViewItem.ListViewSubItem _listViewSubItem = new ListViewItem.ListViewSubItem();
                if (_form_MainBase.EditedFile.Exists)
                {
                    _listViewSubItem.Text = _form_MainBase.EditedFile.DirectoryName;
                }
                else
                {
                    _listViewSubItem.Text = "n/a";
                }

                _listViewItem.SubItems.Add(_listViewSubItem);

                listView1.Items.Add(_listViewItem);
            }
        }
        private void Form_Windows_Load(object sender, EventArgs e)
        {
            SetListViewWidth();
            listView1_SelectedIndexChanged(this, null);
        }
        private void Form_Windows_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region "pogledamo, če 'lastActivatedForm' še vedno obstaja, ali pa smo jo mogoče zaprli"
            bool _lastActivatedFormStillExists = false;
            foreach (ListViewItem _listViewItem in listView1.SelectedItems)
            {
                Form_MainBase _form_MainBase = (Form_MainBase)_listViewItem.Tag;
                if (_form_MainBase == lastActivatedForm)
                {
                    _lastActivatedFormStillExists = true;
                    break;
                }
            }

            if (!_lastActivatedFormStillExists)
            {
                lastActivatedForm = null;
            }
            #endregion "pogledamo, če 'lastActivatedForm' še vedno obstaja, ali pa smo jo mogoče zaprli"
        }



        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetListViewWidth()
        {
            if (listView1.Items.Count > 0)
            {
                listView1.Columns[1].Width = -1;
            }
            else
            {
                listView1.Columns[1].Width = -2;
            }
        }

        #region "aktivacija in zapiranje form"
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button_Activate.Enabled = true;
                button_Close.Enabled = true;
            }
            else
            {
                button_Activate.Enabled = false;
                button_Close.Enabled = false;
            }
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ActivateItem();
        }
        private void button_Activate_Click(object sender, EventArgs e)
        {
            ActivateItem();
        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem _listViewItem in listView1.SelectedItems)
            {
                Form_MainBase _form_MainBase = (Form_MainBase)_listViewItem.Tag;

                _form_MainBase.FormClosed += delegate(object _sender, FormClosedEventArgs _e)
                {
                    listView1.Items.Remove(_listViewItem);
                };

                _form_MainBase.Close();
            }

            SetListViewWidth();
        }

        private void ActivateItem()
        {
            foreach (ListViewItem _listViewItem in listView1.SelectedItems)
            {
                if (_listViewItem.Focused)
                {
                    Form_MainBase _form_MainBase = (Form_MainBase)_listViewItem.Tag;
                    if (!_form_MainBase.Focused)
                    {
                        _form_MainBase.Activate();
                        lastActivatedForm = _form_MainBase;
                    }

                    break;
                }
            }
        }
        #endregion "aktivacija in zapiranje form"

    }
}
