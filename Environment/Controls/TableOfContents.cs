using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using EngineDesigner.Common.CustomCollections;
using System.CodeDom;
using System.ComponentModel.Design.Serialization;

namespace EngineDesigner.Environment.Controls
{
    public partial class TableOfContents : UserControl
    {
        //OBSOLETE: tega verjetno sploh ne rabimo
        //[DefaultValue(typeof(Font), Defaults.DefaultFontString)]
        //public override Font Font
        //{
        //    get
        //    {
        //        return base.Font;
        //    }
        //    set
        //    {
        //        base.Font = value;
        //        this.treeView1.Font = value;
        //    }
        //}

        private Bookmark[] bookmarks = new Bookmark[0];
        public Bookmark[] Bookmarks
        {
            get { return this.bookmarks; }

            set
            {
                this.bookmarks = value;

                this.treeView1.Nodes.Clear();
                if (this.bookmarks != null)
                {
                    foreach (Bookmark _bookmark in this.bookmarks)
                    {
                        this.treeView1.Nodes.Add(_bookmark.TreeNode);
                    }
                }
                this.treeView1.ExpandAll();
            }
        }

        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        private Bookmark selectedBookmark = null;
        public Bookmark SelectedBookmark
        {
            get { return selectedBookmark; }
            set { selectedBookmark = value; }
        }

        [DefaultValue(19)]
        public int Indent
        {
            get { return this.treeView1.Indent; }
            set { this.treeView1.Indent = value; }
        }
        [DefaultValue(16)]
        public int ItemHeight
        {
            get { return this.treeView1.ItemHeight; }
            set { this.treeView1.ItemHeight = value; }
        }



        public TableOfContents()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            this.treeView1.ExpandAll();
        }



        public Bookmark GetBookmarkByTitle(string _title)
        {
            Bookmark[] _bookmarks = this.GetAllBookmarks();

            foreach (Bookmark _bookmark in _bookmarks)
            {
                if (_bookmark.Title == _title)
                {
                    return _bookmark;
                }
            }


            throw new ArgumentException();
        }
        public Bookmark GetBookmarkByTag(object _tag)
        {
            Bookmark[] _bookmarks = this.GetAllBookmarks();

            foreach (Bookmark _bookmark in _bookmarks)
            {
                if (_bookmark.Tag == _tag)
                {
                    return _bookmark;
                }
            }


            throw new ArgumentException();
        }


        private void treeView1_DrawNode(object sender, System.Windows.Forms.DrawTreeNodeEventArgs e)
        {
            int _offsetX = (e.Node.Level * this.treeView1.Indent);

            Size _textSize = e.Graphics.MeasureString(
                e.Node.Text,
                this.Font).ToSize();

            try
            {
                if (e.Node.Nodes.Count > 0)
                {
                    Image _image = this.imageList1.Images[0];
                    int _offsetY = (int)Math.Ceiling((_textSize.Height - _image.Height) / 2f);
                    e.Graphics.DrawImage(_image, e.Bounds.X + _offsetX, e.Bounds.Y + _offsetY);
                }
            }
            catch { }

            _offsetX += (int)(this.imageList1.ImageSize.Width * 1.5f);

            Rectangle _rectangle = new Rectangle(
                e.Bounds.X + _offsetX,
                e.Bounds.Y,
                _textSize.Width,
                _textSize.Height);

            if (!((Bookmark)e.Node.Tag).Enabled)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(SystemColors.ControlLightLight),  //NOTE: tle bi sicer moralo bit this.BackColor, samo tu ni tu
                    _rectangle);

                e.Graphics.DrawString(
                    e.Node.Text,
                    this.Font,
                    new SolidBrush(SystemColors.InactiveCaption),
                    e.Bounds.X + _offsetX,
                    e.Bounds.Y);
            }
            else if (e.Node.Tag == this.selectedBookmark)
            {
                if (this.selectedBookmark != null)
                {
                    //System.Diagnostics.Debug.WriteLine("*" + this.selectedBookmark.Title);
                    //System.Diagnostics.Debug.WriteLine(((Bookmark)e.Node.Tag).Title);
                }

                e.Graphics.FillRectangle(
                    new SolidBrush(SystemColors.MenuHighlight),
                    _rectangle);

                e.Graphics.DrawString(
                    e.Node.Text,
                    this.Font,
                    new SolidBrush(SystemColors.HighlightText),
                    e.Bounds.X + _offsetX,
                    e.Bounds.Y);
            }
            else
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(SystemColors.ControlLightLight),  //NOTE: tle bi sicer moralo bit this.BackColor, samo tu ni tu
                    _rectangle);

                e.Graphics.DrawString(
                    e.Node.Text,
                    this.Font,
                    new SolidBrush(this.ForeColor),
                    e.Bounds.X + _offsetX,
                    e.Bounds.Y);
            }
        }
        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private Bookmark[] GetAllBookmarks()
        {
            List<Bookmark> _list = new List<Bookmark>();
            foreach (Bookmark _bookmark in this.bookmarks)
            {
                this.GetAllBookmarks(_bookmark, ref _list);
            }

            return _list.ToArray();
        }
        private void GetAllBookmarks(Bookmark _bookmark, ref List<Bookmark> _list)
        {
            _list.Add(_bookmark);

            foreach (Bookmark _subBookmark in _bookmark.SubBookmarks)
            {
                this.GetAllBookmarks(_subBookmark, ref _list);
            }
        }
    }


    [TypeConverter(typeof(BookmarkConverter))]
    public class Bookmark
    {
        [NonSerialized]
        private TreeNode treeNode = new TreeNode();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        internal TreeNode TreeNode
        {
            get { return this.treeNode; }
        }


        private string title;
        public string Title
        {
            get { return title; }

            set
            {
                title = value;
                this.treeNode.Text = this.title;
            }
        }

        private bool enabled = true;
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return enabled; }

            set
            {
                bool _nodeNeedsRefreshing = false;
                if (value != this.enabled)
                {
                    _nodeNeedsRefreshing = true;
                }

                this.enabled = value;

                if (_nodeNeedsRefreshing)
                {
                    //NOTE: to je samo zaradi refrešanja, ker ni druge možnosti
                    this.treeNode.Text = this.treeNode.Text;
                }
            }
        }

        private object tag = null;
        [DefaultValue(null)]
        [TypeConverter(typeof(StringConverter))]
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }


        private Bookmark[] subBookmarks = new Bookmark[0];
        public Bookmark[] SubBookmarks
        {
            get { return this.subBookmarks; }

            set
            {
                this.subBookmarks = value;

                this.treeNode.Nodes.Clear();
                if (this.subBookmarks != null)
                {
                    this.AddNodes(this.treeNode, this.subBookmarks);
                }
                this.treeNode.ExpandAll();
            }
        }
        private void AddNodes(TreeNode _treeNode, Bookmark[] _bookmarks)
        {
            foreach (Bookmark _bookmark in _bookmarks)
            {
                _treeNode.Nodes.Add(_bookmark.TreeNode);

                if (_bookmark.SubBookmarks != null)
                {
                    this.AddNodes(_bookmark.TreeNode, _bookmark.SubBookmarks);
                }
            }
        }



        public Bookmark()
            : this(string.Empty)
        {
        }
        public Bookmark(string _title)
        {
            this.title = _title;

            this.treeNode = new TreeNode();
            this.treeNode.Text = this.title;
            this.treeNode.Tag = this;
        }
        public override string ToString()
        {
            return string.Format(
                "Bookmark: {0}",
                this.title);
        }
    }


    /// <summary>
    /// Provides UI integration for Bookmark class.
    /// </summary>
    internal class BookmarkConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Bookmark))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(System.String))
                && (value is Bookmark))
            {
                Bookmark _bookmark = (Bookmark)value;
                return _bookmark.Title;
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
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string _string = value.ToString();
            return new Bookmark(_string);
        }
    }




    //TODO: mogoče en dan (zaenkrat form designer ne zna serializirat tega, oz. prova serializirat v resourse, ma ne gre skozi očitno)...
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Serializable]
    //public class BookmarkCollection : System.Collections.ArrayList
    //{
    //    public Bookmark Add(Bookmark obj)
    //    {
    //        base.Add(obj);
    //        return obj;
    //    }


    //    public void Insert(int index, Bookmark obj)
    //    {
    //        base.Insert(index, obj);
    //    }

    //    public void Remove(Bookmark obj)
    //    {
    //        base.Remove(obj);
    //    }

    //    new public Bookmark this[int index]
    //    {
    //        get { return (Bookmark)base[index]; }
    //        set { base[index] = value; }
    //    }
    //}



    //[DesignerSerializer(typeof(BookmarkCollectionSerializer), typeof(CodeDomSerializer))]
    //public class BookmarkCollection : List<Bookmark>
    //{
    //    private Bookmark[] bookmarks = new Bookmark[0];


    //    public Bookmark this[int _index]
    //    {
    //        get { return this.bookmarks[_index]; }
    //        set { this.bookmarks[_index] = value; }
    //    }




    //    #region ICollection<Bookmark> Members


    //    public void Clear()
    //    {
    //    }

    //    public bool Contains(Bookmark item)
    //    {
    //        return bookmarks.ToList<Bookmark>().Contains(item);
    //    }

    //    public void CopyTo(Bookmark[] array, int arrayIndex)
    //    {
    //    }

    //    public int Count
    //    {
    //        get { return bookmarks.Length; }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { return false; }
    //    }

    //    bool ICollection<Bookmark>.Remove(Bookmark item)
    //    {
    //        return bookmarks.ToList<Bookmark>().Remove(item);
    //    }

    //    #endregion

    //    #region IEnumerable<Bookmark> Members

    //    IEnumerator<Bookmark> IEnumerable<Bookmark>.GetEnumerator()
    //    {
    //        return bookmarks.ToList<Bookmark>().GetEnumerator();
    //    }

    //    #endregion

    //    #region List<Bookmark> Members

    //    public int IndexOf(Bookmark item)
    //    {
    //        return bookmarks.ToList<Bookmark>().IndexOf(item);
    //    }

    //    public void Insert(int index, Bookmark item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void RemoveAt(int index)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion

    //    #region ICollection<Bookmark> Members

    //    public void Add(Bookmark item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion

    //    #region IEnumerable Members

    //    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    //    {
    //        return bookmarks.ToList<Bookmark>().GetEnumerator();
    //    }

    //    #endregion
    //}


    ///// <summary>
    ///// Provides additional code serialization for Bookmark class.
    ///// </summary>
    //internal class BookmarkCollectionSerializer : CodeDomSerializer
    //{
    //    public override object Serialize(IDesignerSerializationManager manager, object value)
    //    {
    //        CodeDomSerializer _codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(
    //            typeof(BookmarkCollectionSerializer).BaseType, typeof(CodeDomSerializer));

    //        object _object = _codeDomSerializer.Serialize(manager, value);
    //        if (_object is CodeStatementCollection)
    //        {
    //            CodeStatementCollection _codeStatementCollection = (CodeStatementCollection)_object;

    //            CodeExpression _targetObject = base.GetExpression(manager, value);
    //            if (_targetObject != null)
    //            {
    //                BookmarkCollection _bookmarkCollection = (BookmarkCollection)value;

    //                foreach (Bookmark _bookmark in _bookmarkCollection)
    //                {
    //                    CodeConstructor _codeConstructor = new CodeConstructor();
    //                }



    //                CodePropertyReferenceExpression _codePropertyReferenceExpression = new CodePropertyReferenceExpression(_targetObject, "OwnerChart");


    //                CodeAssignStatement _codeAssignStatement = new CodeAssignStatement(_codePropertyReferenceExpression, new CodeThisReferenceExpression());
    //                _codeStatementCollection.Insert(0, _codeAssignStatement);


    //                CodeCommentStatement _codeCommentStatement = new CodeCommentStatement(
    //                    new CodeComment("WARNING: This generates an exception in design time, but is ok (select 'Ignore and continue')." + value.GetType().ToString()));
    //                _codeStatementCollection.Insert(0, _codeCommentStatement);
    //            }
    //        }


    //        return _object;
    //    }

    //}

}
