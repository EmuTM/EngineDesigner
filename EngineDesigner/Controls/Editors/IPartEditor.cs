using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using EngineDesigner.Machine;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Common;
using EngineDesigner.Machine.Definitions;

namespace EngineDesigner.Controls.Editors
{
    internal partial class IPartEditor : UserControl
    {
        private event EventHandler<EditedPartChangedEventArgs> editedPartChanged;
        public event EventHandler<EditedPartChangedEventArgs> EditedPartChanged
        {
            add { this.editedPartChanged += value; }
            remove { this.editedPartChanged -= value; }
        }

        private event EventHandler<EditedItemSelectedEventArgs> editedItemSelected;
        public event EventHandler<EditedItemSelectedEventArgs> EditedItemSelected
        {
            add { this.editedItemSelected += value; }
            remove { this.editedItemSelected -= value; }
        }



        public IPart EditedPart
        {
            get { return (IPart)this.propertyGrid1.SelectedObject; }
            set { this.propertyGrid1.SelectedObject = value; }
        }



        public IPartEditor()
        {
            InitializeComponent();
        }



        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            this.OnEditedItemSelected(e.NewSelection);
        }
        protected virtual void OnEditedItemSelected(GridItem _selectedItem)
        {
            if (this.editedItemSelected != null)
            {
                this.editedItemSelected(this, new EditedItemSelectedEventArgs(_selectedItem));
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                this.EditedPart.Validate();

                this.OnEditedPartChanged(this.EditedPart);
            }
            catch (ValidationException _validationException)
            {
                Utility.WarningMessage(
                    this,
                    this.Text,
                    "Validation error",
                    string.Format(
                        "Object could not be validated:{0}{1}",
                        System.Environment.NewLine,
                        _validationException.Message));

                this.SetItemValue(e.ChangedItem, e.OldValue);
            }
            catch (Exception _exception)
            {
                EngineDesigner.Common.Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                this.SetItemValue(e.ChangedItem, e.OldValue);
            }
        }
        //NOTE: to potrebujemo, ker se čene property grid ne zaveda, da se je spremenil collection (PositionedCylinders konkretno)
        private void propertyGrid1_Validating(object sender, CancelEventArgs e)
        {
            this.propertyGrid1.SelectedObject = this.EditedPart;
        }
        protected virtual void OnEditedPartChanged(IPart _editedPart)
        {
            if (this.editedPartChanged != null)
            {
                this.editedPartChanged(this, new EditedPartChangedEventArgs(_editedPart));
            }
        }



        protected void SetItemValue(GridItem _itemToSet, object _value)
        {
            if (_itemToSet.PropertyDescriptor.Name.Contains(Defaults.HASH))
            {
                int _index = int.Parse(_itemToSet.PropertyDescriptor.Name.Substring(1));

                PropertyInfo _propertyInfo = _itemToSet.Parent.PropertyDescriptor.PropertyType.GetProperty(Defaults.ITEM);
                _propertyInfo.SetValue(_itemToSet.Parent.Value, _value, new object[] { _index });
            }
            else
            {
                //TODO: AL TU
                PropertyInfo _propertyInfo = _itemToSet.PropertyDescriptor.ComponentType.GetProperty(_itemToSet.PropertyDescriptor.Name);

                //TODO: AL TU
                //PropertyInfo _propertyInfo = _itemToSet.PropertyDescriptor.PropertyType.GetProperty(_itemToSet.PropertyDescriptor.Name);

                _propertyInfo.SetValue(_itemToSet.Parent.Value, _value, null);
            }


            propertyGrid1.Refresh();
        }

    }


    internal class EditedPartChangedEventArgs : EventArgs
    {
        private IPart editedPart;
        public IPart EditedPart
        {
            get { return editedPart; }
        }



        internal EditedPartChangedEventArgs(IPart _editedPart)
        {
            editedPart = _editedPart;
        }
    }


    internal class EditedItemSelectedEventArgs : EventArgs
    {
        private GridItem selectedItem;
        public GridItem SelectedItem
        {
            get { return selectedItem; }
        }



        internal EditedItemSelectedEventArgs(GridItem _selectedItem)
        {
            selectedItem = _selectedItem;
        }
    }

}
