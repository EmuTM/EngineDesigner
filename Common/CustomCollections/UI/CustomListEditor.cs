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
    /// Provides UI editor for CustomCollection class.
    /// </summary>
    public class CustomListEditor : CollectionEditor
    {
        public CustomListEditor()
            : this("CustomList editor", typeof(ICustomCollectionElement))
        {
        }
        /// <param name="_title">Defines the title of the dialog form to display.</param>
        public CustomListEditor(string _title)
            : this(_title, typeof(ICustomCollectionElement))
        {
        }
        /// <param name="_title">Defines the title of the dialog form to display.</param>
        /// <param name="_typeOfElementsInCollection">Defines the type of the elements in the collection to edit.</param>
        public CustomListEditor(string _title, Type _typeOfElementsInCollection)
            : base(_typeOfElementsInCollection)
        {
            title = _title;
        }



        protected string title;



        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService _iWindowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));


            //castamo v list, da ni odvisno od tipa
            IList _iList = (IList)value;


            //dobimo tip elementov v collectionu, ker rabimo za formo
            Type _typeOfCollection = context.PropertyDescriptor.PropertyType;
            PropertyInfo _propertyInfo = _iList.GetType().GetProperty(Defaults.ITEM, new Type[] { typeof(int) });
            Type _typeOfElementsInCollection = _propertyInfo.PropertyType;


            //zloadamo formo
            Form_CustomListEditor _form_CustomListEditor = new Form_CustomListEditor(
                _iList, _typeOfElementsInCollection,
                new Form_CustomListEditor.ItemAddDelegate(OnItemAdd),
                new Form_CustomListEditor.ListValidateDelegate(OnListValidate),
                new Form_CustomListEditor.GetItemTextDelegate(OnGetItemText));
            _form_CustomListEditor.Text = title;

            _form_CustomListEditor.OnEditorLoading
                += new EventHandler(OnEditorLoading);
            _form_CustomListEditor.OnEditorClosing
                += new EventHandler(OnEditorClosing);

            _iWindowsFormsEditorService.ShowDialog(_form_CustomListEditor); //baje, da iz property grida je treba uporabljat to za odpret formo


            return _iList;
        }

        //delegati in eventi iz editor forme - za overridanje
        protected virtual ICustomCollectionElement OnItemAdd(ICustomCollectionElement _itemToAdd, IList _currentList)
        {
            return _itemToAdd;
        }
        protected virtual void OnListValidate(ref IList _listToValidate, ref Exception _exceptionWhileValidating, ref string _messageToDisplay)
        {
        }
        protected virtual string OnGetItemText(ICustomCollectionElement _item, IList _currentList)
        {
            return _item.ToString();
        }

        protected virtual void OnEditorClosing(object _sender, EventArgs _eventArgs)
        {
        }
        protected virtual void OnEditorLoading(object _sender, EventArgs _eventArgs)
        {
        }

    }
}
