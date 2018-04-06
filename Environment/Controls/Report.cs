using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using EngineDesigner.Common.Converters;
using EngineDesigner.Common.CustomCollections;

namespace EngineDesigner.Environment.Controls
{
    public partial class Report : UserControl
    {
        private event EventHandler<ReportItemEventArgs> reportItemClicked;
        public event EventHandler<ReportItemEventArgs> ReportItemClicked
        {
            add { this.reportItemClicked += value; }
            remove { this.reportItemClicked -= value; }
        }

        private event EventHandler<ReportItemEventArgs> reportItemDoubleClicked;
        public event EventHandler<ReportItemEventArgs> ReportItemDoubleClicked
        {
            add { this.reportItemDoubleClicked += value; }
            remove { this.reportItemDoubleClicked -= value; }
        }



        public Report()
        {
            InitializeComponent();
        }



        //when Group is IFormattable
        private string groupStringFormat = null;
        [DefaultValue(null)]
        public string GroupStringFormat
        {
            get { return groupStringFormat; }
            set { groupStringFormat = value; }
        }

        //when Key is IFormattable
        private string keyStringFormat = null;
        [DefaultValue(null)]
        public string KeyStringFormat
        {
            get { return keyStringFormat; }
            set { keyStringFormat = value; }
        }

        //when Value is IFormattable
        private string valueStringFormat = null;
        [DefaultValue(null)]
        public string ValueStringFormat
        {
            get { return valueStringFormat; }
            set { valueStringFormat = value; }
        }

        [DefaultValue(32)]
        public int ColumnWidthImage
        {
            get { return this.columnHeader_Image.Width; }
            set { this.columnHeader_Image.Width = value; }
        }
        [DefaultValue(60)]
        public int ColumnWidthKey
        {
            get { return this.columnHeader_Key.Width; }
            set { this.columnHeader_Key.Width = value; }
        }
        [DefaultValue(60)]
        public int ColumnWidthValue
        {
            get { return this.columnHeader_Value.Width; }
            set { this.columnHeader_Value.Width = value; }
        }

        [DefaultValue("")]
        public string ColumnHeaderTextImage
        {
            get { return this.columnHeader_Image.Text; }
            set { this.columnHeader_Image.Text = value; }
        }
        [DefaultValue("")]
        public string ColumnHeaderTextKey
        {
            get { return this.columnHeader_Key.Text; }
            set { this.columnHeader_Key.Text = value; }
        }
        [DefaultValue("")]
        public string ColumnHeaderTextValue
        {
            get { return this.columnHeader_Value.Text; }
            set { this.columnHeader_Value.Text = value; }
        }

        [DefaultValue(false)]
        public bool Clickable
        {
            get { return !this.listView1.OwnerDraw; }
            set { this.listView1.OwnerDraw = !value; }
        }


        [DefaultValue(null)]
        public ImageList ImageList
        {
            get { return this.listView1.SmallImageList; }
            set { this.listView1.SmallImageList = value; }
        }


        private ReportItem[] reportItems = new ReportItem[0];
        public ReportItem[] ReportItems
        {
            get { return this.reportItems; }

            set
            {
                this.reportItems = value;
                this.SetupListView();
            }
        }


        //private CustomList<ReportItem> reportItemCollection = new CustomList<ReportItem>();
        //public CustomList<ReportItem> ReportItemCollection
        //{
        //    get { return reportItemCollection; }
        //    set { reportItemCollection = value; }
        //}



        private ListViewGroup GetGroupByTag(object _tag)
        {
            foreach (ListViewGroup _listViewGroup in this.listView1.Groups)
            {
                if (_listViewGroup.Tag == _tag)
                {
                    return _listViewGroup;
                }
            }

            return null;
        }


        private void SetupListView()
        {
            this.listView1.Items.Clear();
            this.listView1.Groups.Clear();


            if (this.reportItems != null)
            {
                #region "dobimo različne grupe in jih kreiramo"
                IEnumerable<object> _groups = this.GetDifferentGroups(this.reportItems);
                foreach (object _group in _groups)
                {
                    ListViewGroup _listViewGroup = null;

                    if (_group != null)
                    {
                        _listViewGroup = new ListViewGroup();
                        _listViewGroup.Tag = _group;
                        _listViewGroup.Header = this.FormatString(_group, this.groupStringFormat);
                        this.listView1.Groups.Add(_listViewGroup);
                    }
                }
                #endregion "dobimo različne grupe in jih kreiramo"


                //dobimo različne ključe
                IEnumerable<object> _keys = this.GetDifferentKeys(this.reportItems);


                #region "gremo po grupah in vpišemo ustrezne ključe"
                foreach (object _group in _groups)
                {
                    //grupa morda obstaja, morda tudi ne (če je null)
                    ListViewGroup _listViewGroup = this.GetGroupByTag(_group);

                    //tukaj si zapomnemo, kateri ključ smo že dodali, da ne izpisujemo 2x njegovega teksta
                    List<object> _addedKeys = new List<object>();

                    //gremo po vseh ključih
                    foreach (object _key in _keys)
                    {
                        foreach (ReportItem _reportItem in this.reportItems)
                        {
                            if ((_reportItem.Group == _group)
                                && (_reportItem.Key == _key))
                            {
                                #region "dodamo item"
                                ListViewItem _listViewItem = new ListViewItem();
                                _listViewItem.Group = _listViewGroup;
                                _listViewItem.Tag = _reportItem;


                                ListViewItem.ListViewSubItem _listViewSubItem_Key = new ListViewItem.ListViewSubItem();
                                _listViewSubItem_Key.Tag = _key;
                                //teksta od ključa ne napišemo dvakrat
                                if (!_addedKeys.Contains(_key))
                                {
                                    //tukaj dodamo tudi image (da ga ne narišemo 2x)!
                                    _listViewItem.ImageIndex = _reportItem.ImageIndex;

                                    _listViewSubItem_Key.Text = this.FormatString(_key, this.keyStringFormat);
                                }
                                _listViewItem.SubItems.Add(_listViewSubItem_Key);


                                //dodamo value
                                ListViewItem.ListViewSubItem _listViewSubItem_Value = new ListViewItem.ListViewSubItem();
                                _listViewSubItem_Value.Tag = _reportItem.Value;
                                _listViewSubItem_Value.Text = this.FormatString(_reportItem.Value, this.valueStringFormat);
                                _listViewItem.SubItems.Add(_listViewSubItem_Value);


                                this.listView1.Items.Add(_listViewItem);
                                _addedKeys.Add(_key);
                                #endregion "dodamo item"
                            }
                        }
                    }
                }
                #endregion "gremo po grupah in vpišemo ustrezne ključe"
            }
        }

        private string FormatString(object _object, string _format)
        {
            if (_object is IFormattable)
            {
                IFormattable _iFormattable = (IFormattable)_object;
                return _iFormattable.ToString(_format, null);
            }
            else
            {
                if (_object != null)
                {
                    return _object.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        private IEnumerable<object> GetDifferentGroups(IEnumerable<ReportItem> _reportItems)
        {
            List<object> _groups = new List<object>();

            foreach (ReportItem _reportItem in _reportItems)
            {
                this.GetDifferentElements(ref _groups, _reportItem.Group);
            }

            return _groups;
        }
        private IEnumerable<object> GetDifferentKeys(IEnumerable<ReportItem> _reportItems)
        {
            List<object> _keys = new List<object>();

            foreach (ReportItem _reportItem in _reportItems)
            {
                this.GetDifferentElements(ref _keys, _reportItem.Key);
            }

            return _keys;
        }
        private void GetDifferentElements(ref List<object> _differentElements, object _currentElement)
        {
            if (!_differentElements.Contains(_currentElement))
            {
                _differentElements.Add(_currentElement);
            }
        }

        //to je zato, da nimamo focusa ob selectionu
        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }
        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }
        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }



        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Clickable)
            {
                ListView _listView = (ListView)sender;
                ListViewHitTestInfo _listViewHitTestInfo = _listView.HitTest(e.Location);

                if (_listViewHitTestInfo.Item != null)
                {
                    this.OnReportItemClicked((ReportItem)_listViewHitTestInfo.Item.Tag);
                };
            }
        }
        protected virtual void OnReportItemClicked(ReportItem _reportItem)
        {
            if (this.reportItemClicked != null)
            {
                this.reportItemClicked(this, new ReportItemEventArgs(_reportItem));
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Clickable)
            {
                ListView _listView = (ListView)sender;
                ListViewHitTestInfo _listViewHitTestInfo = _listView.HitTest(e.Location);

                if (_listViewHitTestInfo.Item != null)
                {
                    this.OnReportItemDoubleClicked((ReportItem)_listViewHitTestInfo.Item.Tag);
                };
            }
        }
        protected virtual void OnReportItemDoubleClicked(ReportItem _reportItem)
        {
            if (this.reportItemDoubleClicked != null)
            {
                this.reportItemDoubleClicked(this, new ReportItemEventArgs(_reportItem));
            }
        }

    }


    [Serializable]
    public class ReportItem : ICustomCollectionElement
    {
        public ReportItem()
            : this(null, null)
        {
        }
        public ReportItem(object _key, object _value)
            : this(_key, _value, null)
        {
        }
        public ReportItem(object _key, object _value, object _group)
            : this(_key, _value, _group, -1)
        {
        }
        public ReportItem(object _key, object _value, object _group, int _imageIndex)
        {
            this.key = _key;
            this.value = _value;
            this.group = _group;
            this.imageIndex = _imageIndex;
        }


        private object key;
        [TypeConverter(typeof(ObjectConverter))]
        public object Key
        {
            get { return key; }
            set { key = value; }
        }

        private object value;
        [TypeConverter(typeof(ObjectConverter))]
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private object group;
        [TypeConverter(typeof(ObjectConverter))]
        public object Group
        {
            get { return group; }
            set { group = value; }
        }

        private int imageIndex;
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }


        #region ICustomCollectionElement Members
        [Browsable(false)]
        public string DisplayName
        {
            get { throw new NotSupportedException(); }
        }
        [Browsable(false)]
        public string DisplayDescription
        {
            get { throw new NotSupportedException(); }
        }
        #endregion


        public override string ToString()
        {
            string _key = "null";
            if (this.key != null)
            {
                _key = this.key.ToString();
            }

            string _value = "null";
            if (this.value != null)
            {
                _value = this.value.ToString();
            }

            string _group = "null";
            if (this.group != null)
            {
                _group = this.group.ToString();
            }


            return string.Format(
                "GROUP: ({0}); KEY: ({1}); VALUE: ({2})",
                _group,
                _key,
                _value);
        }
    }


    public class ReportItemEventArgs : EventArgs
    {
        public ReportItemEventArgs(ReportItem _reportItem)
        {
            this.reportItem = _reportItem;
        }


        private ReportItem reportItem;
        public ReportItem ReportItem
        {
            get { return reportItem; }
        }

    }

}
