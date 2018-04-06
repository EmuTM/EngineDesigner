using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Reflection;

namespace EngineDesigner.Common.CustomCollections
{
    /// <summary>
    /// Provides the form for CustomListEditor class.
    /// </summary>
    internal sealed partial class Form_CustomListEditor : Form
    {
        public delegate ICustomCollectionElement ItemAddDelegate(ICustomCollectionElement _itemToAdd, IList _currentList);
        public delegate void ListValidateDelegate(ref IList _listToValidate, ref Exception _exceptionWhileValidating, ref string _messageToDisplay);
        public delegate string GetItemTextDelegate(ICustomCollectionElement _item, IList _currentList);



        private ItemAddDelegate onItemAdd = null;
        private ListValidateDelegate onCollectionValidate = null;
        private GetItemTextDelegate getItemText = null;

        public event EventHandler OnEditorLoading = null;
        public event EventHandler OnEditorClosing = null;



        public Form_CustomListEditor()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                throw new Exception("This constructor is only meant for design time purposes.");
            }


            this.Constructor();
        }
        public Form_CustomListEditor(
            IList _list,
            Type _typeOfElementsInCollection,
            ItemAddDelegate _onItemAdd,
            ListValidateDelegate _onCollectionValidate,
            GetItemTextDelegate _getItemText)
        {
            this.list = _list;
            this.listTmp = Utility.CopyObject(list); //obvezno kopije!!!
            this.typeOfElementsInCollection = _typeOfElementsInCollection;

            this.onItemAdd = _onItemAdd;
            this.onCollectionValidate = _onCollectionValidate;
            this.getItemText = _getItemText;

            this.Constructor();
        }
        private void Constructor()
        {
            InitializeComponent();
        }
        private void Form_CustomListEditor_Load(object sender, EventArgs e)
        {
            if (this.OnEditorLoading != null)
            {
                this.OnEditorLoading(this, new EventArgs());
            }


            foreach (ICustomCollectionElement _iCustomCollectionElement in this.listTmp)
            {
                this.AddItem(_iCustomCollectionElement);
            }
        }
        private void Form_CustomListEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.OnEditorClosing != null)
            {
                this.OnEditorClosing(this, new EventArgs());
            }
        }



        private Type typeOfElementsInCollection;
        private IList listTmp; //s tem delamo dokler smo v editorju, da imamo original intakten

        private IList list;
        public IList List
        {
            get { return list; }
        }



        private void AddItem(ICustomCollectionElement _iCustomCollectionElement)
        {
            ListViewItem _listViewItem = new ListViewItem();
            _listViewItem.Tag = _iCustomCollectionElement;
            _listViewItem.Text = GetText(_iCustomCollectionElement);

            listView_Collection.Items.Add(_listViewItem);

            listView_Collection.MultiSelect = false;
            _listViewItem.EnsureVisible();
            _listViewItem.Selected = true;
            listView_Collection.MultiSelect = true;
        }
        private string GetText(ICustomCollectionElement _iCustomCollectionElement)
        {
            string _itemText = null;

            if (this.getItemText != null)
            {
                _itemText = getItemText(_iCustomCollectionElement, this.list);
            }

            if (string.IsNullOrEmpty(_itemText))
            {
                _itemText = _iCustomCollectionElement.ToString();
            }

            return _itemText;
        }


        private void listView_Collection_Resize(object sender, EventArgs e)
        {
            listView_Collection.Columns[0].Width = listView_Collection.Width - 10;
        }
        private void listView_Collection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //OBSOLETE: po novem lahko editiramo več itemov naenkrat
            //propertyGrid1.SelectedObject = e.Item.Tag;

            List<object> _list = new List<object>();
            foreach (ListViewItem _listViewItem in listView_Collection.SelectedItems)
            {
                _list.Add(_listViewItem.Tag);
            }
            propertyGrid1.SelectedObjects = _list.ToArray();
        }
        private void listView_Collection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Collection.SelectedItems.Count > 0)
            {
                button_Remove.Enabled = true;
            }
            else
            {
                button_Remove.Enabled = false;
                propertyGrid1.SelectedObject = null;
            }
        }

        private void propertyGrid1_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            //OBSOLETE: po novem lahko editiramo več itemov naenkrat
            //foreach (ListViewItem _listViewItem in listView_Collection.Items)
            //{
            //    if (_listViewItem.Tag == propertyGrid1.SelectedObject)
            //    {
            //        _listViewItem.Text = GetText((ICustomCollectionElement)propertyGrid1.SelectedObject);
            //    }
            //}


            foreach (ListViewItem _listViewItem in listView_Collection.Items)
            {
                foreach (object _object in propertyGrid1.SelectedObjects)
                {
                    if (_listViewItem.Tag == _object)
                    {
                        _listViewItem.Text = GetText((ICustomCollectionElement)_object);
                    }
                }
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            DialogResult _dialogResult = MessageBox.Show(
                this,
                "Are you sure you want to remove this item(s)?",
                "Confirm item(s) remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (_dialogResult == DialogResult.Yes)
            {
                ArrayList _itemsToRemove = new ArrayList(listView_Collection.SelectedItems);
                foreach (ListViewItem _listViewItem in _itemsToRemove)
                {
                    this.listTmp.Remove((ICustomCollectionElement)_listViewItem.Tag);
                    listView_Collection.Items.Remove(_listViewItem);
                }
            }
        }
        private void button_Add_Click(object sender, EventArgs e)
        {
            ICustomCollectionElement _itemToAdd = null;
            try
            {
                //poskušamo narediti inštanco (če ni parameterless konstruktorja, ne bo šlo)
                _itemToAdd = (ICustomCollectionElement)Activator.CreateInstance(typeOfElementsInCollection);
            }
            catch (Exception _exception)
            {
                System.Diagnostics.Debug.WriteLine(_exception.Message);
            }


            if (onItemAdd != null)
            {
                _itemToAdd = this.onItemAdd(_itemToAdd, this.listTmp);
            }


            if (_itemToAdd != null)
            {
                this.listTmp.Add(_itemToAdd);
                AddItem(_itemToAdd);
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            Exception _exceptionWhileValidating = null;
            string _messageToDisplay = null;
            if (onCollectionValidate != null)
            {
                onCollectionValidate(ref this.listTmp, ref _exceptionWhileValidating, ref _messageToDisplay);
            }

            if (_exceptionWhileValidating != null)
            {
                Utility.WarningMessage(
                    this,
                    this.Text,
                    "Validation error",
                    string.Format(
                        "Object could not be validated:{0}{1}",
                        Environment.NewLine,
                        _exceptionWhileValidating.Message));
            }
            else if (!string.IsNullOrEmpty(_messageToDisplay))
            {
                Utility.NoticeMessage(
                    this,
                    this.Text,
                    _messageToDisplay);
            }
            else
            {
                this.list.Clear();
                foreach (ICustomCollectionElement _iCustomCollectionElement in this.listTmp)
                {
                    this.list.Add(_iCustomCollectionElement);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

}
