using System;
using System.Collections;
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
using EngineDesigner.Machine.Definitions;
using EngineDesigner.Common.CustomCollections;
using System.Globalization;
using System.Drawing.Design;

namespace EngineDesigner.Controls.Editors
{
    [DefaultEvent("EditedFunctionChanged")]
    internal partial class FunctionEditor : UserControl
    {
        [TypeConverter(typeof(EditableXYConverter))]
        [Serializable] //mora bit, ker uporabljam binary serializer za copy object
        private class EditableXY : ICustomCollectionElement
        {
            private double x;
            [ReadOnly(true)]
            [DisplayName("X")]
            [Description("Defines the X component of the point.")]
            public double X
            {
                get { return x; }
                set { x = value; }
            }

            private double y;
            [DisplayName("Y")]
            [Description("Defines the Y (F(x)) component of the point.")]
            public double Y
            {
                get { return y; }
                set { y = value; }
            }



            public EditableXY()
                : this(0, 0)
            {
            }
            public EditableXY(double _x, double _y)
            {
                this.x = _x;
                this.y = _y;
            }
            public static EditableXY From(XY _xy)
            {
                EditableXY _editableXY = new EditableXY(_xy.X, _xy.Y);
                return _editableXY;
            }
            public override string ToString()
            {
                return string.Format(
                    "{0}; {1}",
                    this.x.ToString(),
                    this.y.ToString());
            }
            public XY ToXY()
            {
                XY _xy = new XY(this.x, this.y);
                return _xy;
            }



            #region ICustomCollectionElement Members
            [Browsable(false)]
            public string DisplayName
            {
                get { return this.x.ToString(); }
            }
            [Browsable(false)]
            public string DisplayDescription
            {
                get { return "The X component."; }
            }
            #endregion
        }
        private class EditableXYConverter : TypeConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(EditableXY))
                {
                    return true;
                }

                return base.CanConvertTo(context, destinationType);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if ((destinationType == typeof(System.String))
                    && (value is EditableXY))
                {
                    EditableXY _editableXY = (EditableXY)value;
                    return _editableXY.Y.ToString();
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }

                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                try
                {
                    EditableXY _oldEditableXY = (EditableXY)context.PropertyDescriptor.GetValue(context);

                    EditableXY _newEditableXY = new EditableXY(
                        _oldEditableXY.X,
                        double.Parse(value.ToString()));
                    return _newEditableXY;
                }
                catch { }

                return base.ConvertFrom(context, culture, value);
            }
        }

        private class EditableFunction
        {
            private CustomList<EditableXY> points = new CustomList<EditableXY>();
            [TypeConverter(typeof(PointsConverter))]
            [Editor(typeof(PointsEditor), typeof(UITypeEditor))]
            [DisplayName("Points")]
            [Description("Defines the array of points within the function.")]
            public CustomList<EditableXY> Points
            {
                get { return points; }
                set { points = value; }
            }



            private EditableFunction()
            {
            }
            public static EditableFunction From(Function _function)
            {
                EditableFunction _editableFunction = new EditableFunction();

                for (int a = 0; a < _function.Length; a++)
                {
                    EditableXY _editableXY = EditableXY.From(_function[a]);
                    _editableFunction.points.Add(_editableXY);
                }

                return _editableFunction;
            }
            public Function ToFunction()
            {
                List<XY> _list = new List<XY>();
                foreach (EditableXY _editableXY in this.points)
                {
                    _list.Add(_editableXY.ToXY());
                }

                Function _function = Function.FromPoints(_list);
                return _function;
            }

        }
        private class PointsConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if ((destinationType == typeof(System.String))
                    && (value is EngineDesigner.Common.CustomCollections.CustomList<EditableXY>))
                {
                    return "...";
                }

                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
        private class PointsEditor : CustomListEditor
        {
            public PointsEditor()
                : base("Function points", typeof(EditableXY))
            {
            }



            protected override void OnListValidate(ref IList _listToValidate, ref Exception _exceptionWhileValidating, ref string _messageToDisplay)
            {
                _messageToDisplay = null;

                try
                {
                    List<XY> _list = new List<XY>();
                    foreach (EditableXY _editableXY in _listToValidate)
                    {
                        _list.Add(new XY(_editableXY.X, _editableXY.Y));
                    }

                    Function _function = Function.FromPoints(_list);


                    CustomList<ICustomCollectionElement> _sortedCollection = new CustomList<ICustomCollectionElement>();
                    foreach (XY _xy in _function)
                    {
                        EditableXY _editableXY = EditableXY.From(_xy);
                        _sortedCollection.Add(_editableXY);
                    }
                }
                catch (Exception _exception)
                {
                    System.Diagnostics.Debug.WriteLine(_exception.Message);

                    _exceptionWhileValidating = _exception;
                }
            }

            protected override void OnEditorLoading(object _sender, EventArgs _eventArgs)
            {
                //omogočimo nastavljanje X-a
                PropertyDescriptor _propertyDescriptor = TypeDescriptor.GetProperties(typeof(EditableXY))["X"];
                ReadOnlyAttribute _readOnlyAttribute = (ReadOnlyAttribute)_propertyDescriptor.Attributes[typeof(ReadOnlyAttribute)];
                System.Reflection.FieldInfo _fieldInfo = _readOnlyAttribute.GetType().GetField(
                    "isReadOnly",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                _fieldInfo.SetValue(_readOnlyAttribute, false);


                base.OnEditorLoading(_sender, _eventArgs);
            }
            protected override void OnEditorClosing(object _sender, EventArgs _eventArgs)
            {
                //spet onemogočimo nastavljanje positiona
                PropertyDescriptor _propertyDescriptor = TypeDescriptor.GetProperties(typeof(EditableXY))["X"];
                ReadOnlyAttribute _readOnlyAttribute = (ReadOnlyAttribute)_propertyDescriptor.Attributes[typeof(ReadOnlyAttribute)];
                System.Reflection.FieldInfo _fieldInfo = _readOnlyAttribute.GetType().GetField(
                    "isReadOnly",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                _fieldInfo.SetValue(_readOnlyAttribute, true);


                base.OnEditorClosing(_sender, _eventArgs);
            }
        }



        private event EventHandler<EditedFunctionChangedEventArgs> editedFunctionChanged;
        public event EventHandler<EditedFunctionChangedEventArgs> EditedFunctionChanged
        {
            add { this.editedFunctionChanged += value; }
            remove { this.editedFunctionChanged -= value; }
        }



        [DefaultValue(null)]
        public Function @Function
        {
            get
            {
                Function _function = null;

                if (this.propertyGrid1.SelectedObject != null)
                {
                    EditableFunction _editableFunction = (EditableFunction)this.propertyGrid1.SelectedObject;
                    _function = _editableFunction.ToFunction();
                }

                return _function;
            }

            set
            {
                EditableFunction _editableFunction = null;

                if (value != null)
                {
                    _editableFunction = EditableFunction.From(value);
                }

                this.propertyGrid1.SelectedObject = _editableFunction;
                this.propertyGrid1.ExpandAllGridItems();
            }
        }



        public FunctionEditor()
        {
            InitializeComponent();
        }



        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                this.OnEditedFunctionChanged(this.@Function);
            }
            catch (Exception _exception)
            {
                EngineDesigner.Common.Utility.Exception(
                    this,
                    this.Text,
                    _exception);

                //this.SetItemValue(e.ChangedItem, e.OldValue);
            }
        }
        protected virtual void OnEditedFunctionChanged(Function _function)
        {
            if (this.editedFunctionChanged != null)
            {
                this.editedFunctionChanged(this, new EditedFunctionChangedEventArgs(_function));
            }
        }

    }


    internal class EditedFunctionChangedEventArgs : EventArgs
    {
        private Function function;
        public Function Function
        {
            get { return function; }
        }



        internal EditedFunctionChangedEventArgs(Function _function)
        {
            this.function = _function;
        }
    }

}
