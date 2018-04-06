using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.Serialization;

namespace EngineDesigner.Common.CustomCollections
{
    /// <summary>
    /// Provides List-type functionality with UI integration.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [TypeConverter(typeof(CustomListConverter))]
    [Editor(typeof(CustomListEditor), typeof(UITypeEditor))]
    [Serializable]
    public class CustomList<T> : IList, IList<T>, ICustomCollection, ICustomTypeDescriptor where T : ICustomCollectionElement
    {
        public CustomList()
            : this(true)
        {
        }
        /// <param name="_allowDuplicates">Defines whether duplicates (by HashCode) will be allowed in the list.</param>
        public CustomList(bool _allowDuplicates)
        {
            allowDuplicates = _allowDuplicates;
        }
        public override string ToString()
        {
            return string.Format(
                "CustomList<{0}>[{1}]",
                typeof(T).Name,
                this.Count);
        }



        private event EventHandler<CustomListEventArgs<T>> collectionChanging;
        public event EventHandler<CustomListEventArgs<T>> CollectionChanging
        {
            add { this.collectionChanging += value; }
            remove { this.collectionChanging -= value; }
        }

        //private event EventHandler collectionChanged;
        //public event EventHandler CollectionChanged
        //{
        //    add { this.collectionChanged += value; }
        //    remove { this.collectionChanged -= value; }
        //}



        private List<T> list = new List<T>();

        protected bool allowDuplicates;



        protected virtual IList<T> OnCollectionChanging(params T[] _elements)
        {
            if (this.collectionChanging != null)
            {
                CustomListEventArgs<T> _customListEventArgs = new CustomListEventArgs<T>(_elements);
                this.collectionChanging(this, _customListEventArgs);

                return _customListEventArgs.Elements;
            }
            else
            {
                return _elements;
            }
        }
        //protected virtual void OnCollectionChanged()
        //{
        //    if (this.collectionChanged != null)
        //    {
        //        this.collectionChanged(this, new EventArgs());
        //    }
        //}



        #region ICustomTypeDescriptor Members
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }
        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection _propertyDescriptorCollection = new PropertyDescriptorCollection(null);

            for (int a = 0; a < this.Count; a++)
            {
                CustomCollectionPropertyDescriptor<T> _customCollectionPropertyDescriptor = new CustomCollectionPropertyDescriptor<T>(this[a], a);
                _propertyDescriptorCollection.Add(_customCollectionPropertyDescriptor);
            }

            return _propertyDescriptorCollection;
        }
        #endregion

        #region ICustomCollection Members
        public bool AllowDuplicates
        {
            get { return allowDuplicates; }
        }
        #endregion


        private int _Add(object value)
        {
            if (value.GetType() != typeof(T))
            {
                throw new ArgumentException(string.Format(
                    "value must be of type '{0}'.",
                    typeof(T).ToString()));
            }


            IList<T> _elements = this.OnCollectionChanging((T)value);

            if ((_elements != null) && (_elements.Count > 0))
            {
                return ((IList)this.list).Add(_elements[0]);
            }

            return -1;
        }
        private bool _Contains(object value)
        {
            return ((IList)this.list).Contains(value);
        }
        private int _IndexOf(object value)
        {
            return ((IList)this.list).IndexOf(value);
        }
        private void _Insert(int index, object value)
        {
            if (value.GetType() != typeof(T))
            {
                throw new ArgumentException(string.Format(
                    "value must be of type '{0}'.",
                    typeof(T).ToString()));
            }


            IList<T> _elements = this.OnCollectionChanging((T)value);

            if ((_elements != null) && (_elements.Count > 0))
            {
                ((IList)this.list).Insert(index, _elements[0]);
            }
        }
        private bool _Remove(object value)
        {
            if (value.GetType() != typeof(T))
            {
                throw new ArgumentException(string.Format(
                    "value must be of type '{0}'.",
                    typeof(T).ToString()));
            }


            IList<T> _elements = this.OnCollectionChanging((T)value);

            if ((_elements != null) && (_elements.Count > 0))
            {
                return this.list.Remove(_elements[0]);
            }

            return false;
        }
        private object _GetByIndex(int index)
        {
            return ((IList)this.list)[index];
        }
        private void _SetByIndex(int index, object value)
        {
            if (value.GetType() != typeof(T))
            {
                throw new ArgumentException(string.Format(
                    "value must be of type '{0}'.",
                    typeof(T).ToString()));
            }


            IList<T> _elements = this.OnCollectionChanging((T)value);

            if ((_elements != null) && (_elements.Count > 0))
            {
                ((IList)this.list)[index] = value;
            }
        }
        private void _CopyTo(Array array, int index)
        {
            ((IList)this.list).CopyTo(array, index);
        }


        #region IList Members
        public int Add(object value)
        {
            return this._Add(value);
        }
        public bool Contains(object value)
        {
            return this._Contains(value);
        }
        public int IndexOf(object value)
        {
            return this._IndexOf(value);
        }
        public void Insert(int index, object value)
        {
            this._Insert(index, value);
        }
        public bool IsFixedSize
        {
            get { return ((IList)this.list).IsFixedSize; }
        }
        public void Remove(object value)
        {
            this._Remove(value);
        }
        object IList.this[int index]
        {
            get { return this._GetByIndex(index); }
            set { this._SetByIndex(index, value); }
        }
        #endregion

        #region ICollection Members
        public void CopyTo(Array array, int index)
        {
            this._CopyTo(array, index);
        }
        public bool IsSynchronized
        {
            get { return ((ICollection)this.list).IsSynchronized; }
        }
        public object SyncRoot
        {
            get { return ((ICollection)this.list).SyncRoot; }
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
        #endregion


        #region IList<T> Members
        public int IndexOf(T item)
        {
            return this._IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            this._Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            IList<T> _elements = this.OnCollectionChanging(this[index]);

            if ((_elements != null) && (_elements.Count > 0))
            {
                this.list.Remove(_elements[0]);
            }
        }
        #endregion

        #region ICollection<T> Members
        public void Add(T item)
        {
            this._Add(item);
        }
        public void Clear()
        {
            IList<T> _elements = this.OnCollectionChanging();

            if (_elements != null)
            {
                this.list.Clear();
            }
        }
        public bool Contains(T item)
        {
            return this._Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            this._CopyTo(array, arrayIndex);
        }
        public int Count
        {
            get { return this.list.Count; }
        }
        public bool IsReadOnly
        {
            get { return ((IList)this.list).IsReadOnly; }
        }
        public bool Remove(T item)
        {
            return this._Remove(item);
        }
        #endregion

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
        #endregion

        public T this[int index]
        {
            get { return (T)this._GetByIndex(index); }
            set { this._SetByIndex(index, value); }
        }
        public void AddRange(IEnumerable<T> collection)
        {
            List<T> _list = new List<T>();
            _list.AddRange(collection);
            IList<T> _elements = this.OnCollectionChanging(_list.ToArray());

            if ((_elements != null) && (_elements.Count > 0))
            {
                this.list.AddRange(_elements);
            }
        }
        public void RemoveRange(int index, int count)
        {
            List<T> _list = new List<T>();
            _list.AddRange(this.list.GetRange(index, count));
            IList<T> _elements = this.OnCollectionChanging(_list.ToArray());

            if ((_elements != null) && (_elements.Count > 0))
            {
                foreach (T _t in _elements)
                {
                    this.list.Remove(_t);
                }
            }
        }

    }


    public class CustomListEventArgs<T> : EventArgs
    {
        internal CustomListEventArgs(params T[] _elements)
        {
            this.elements = _elements;
        }



        private IList<T> elements;
        public IList<T> Elements
        {
            get { return elements; }
            set { elements = value; }
        }
    }


    /// <summary>
    /// Provides UI integration for @List class.
    /// </summary>
    class CustomListConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value.ToString();
        }
    }

}
