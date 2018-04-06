using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;

namespace EngineDesigner.Common.CustomCollections
{
    /// <summary>
    /// Provides UI integration for CustomCollection classes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the CustomCollection.</typeparam>
    public class CustomCollectionPropertyDescriptor<T> : PropertyDescriptor where T : ICustomCollectionElement
    {
        private T property;
        private int indexWithinArray;



        public CustomCollectionPropertyDescriptor(T _property, int _index) :
            base(Defaults.HASH + _index.ToString(), null)
        {
            property = _property;
            indexWithinArray = _index;
        }



        public override AttributeCollection Attributes
        {
            get { return base.Attributes; }
        }
        public override bool CanResetValue(object component)
        {
            return true;
        }
        public override Type ComponentType
        {
            get { throw new NotImplementedException(); }
        }
        public override string DisplayName
        {
            get { return property.DisplayName; }
        }
        public override string Description
        {
            get { return property.DisplayDescription; }
        }
        public override object GetValue(object component)
        {
            return this.property;
        }
        public override bool IsReadOnly
        {
            get { return false; }
        }
        public override string Name
        {
            get { return Defaults.HASH + indexWithinArray.ToString(); }
        }
        public override Type PropertyType
        {
            get { return property.GetType(); }
        }
        public override void ResetValue(object component)
        {
            throw new NotImplementedException();
        }
        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
        public override void SetValue(object component, object value)
        {
            CustomList<T> _customList = (CustomList<T>)component;
            _customList[this.indexWithinArray] = (T)value;

            this.property = (T)value;
        }

    }

}
