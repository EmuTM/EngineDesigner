using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;
using EngineDesigner.Common.Definitions;

namespace EngineDesigner.Environment.Controls.Charting
{
    [DesignerSerializer(typeof(MultiFunctionChartLegendSerializer), typeof(CodeDomSerializer))]
    public partial class MultiFunctionChartLegend : UserControl
    {
        public event EventHandler<CustomLegendCheckedChangedEventArgs> CustomLegendCheckedChanged;
        public event EventHandler<CustomLegendEventArgs> CustomLegendsSelectedChanged;
        public event EventHandler<CustomLegendEventArgs> CustomLegendsRemoved;
        public event EventHandler LegendContextMenuOpening;
        public event EventHandler LegendContextMenuClosing;
        public event EventHandler<CustomLegendItemEventArgs> LegendItemToolTipShowing;



        private MultiFunctionChart ownerChart = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public MultiFunctionChart OwnerChart
        {
            get { return ownerChart; }
            set { ownerChart = value; }
        }


        private BindingList<Series> seriesCollection;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public BindingList<Series> SeriesCollection
        {
            get { return seriesCollection; }
            set { seriesCollection = value; }
        }

        private bool autoHeight = true;
        [DefaultValue(true)]
        public bool AutoHeight
        {
            get { return autoHeight; }
            set { autoHeight = value; }
        }

        private int maxAllowedHeight = 0;
        [DefaultValue(0)]
        public int MaxAllowedHeight
        {
            get { return maxAllowedHeight; }

            set
            {
                maxAllowedHeight = value;

                this.ComputeAutoHeight();
            }
        }
        private int maxAutoHeight;

        public override ContextMenuStrip ContextMenuStrip
        {
            get { return this.listView1.ContextMenuStrip; }
            set { this.listView1.ContextMenuStrip = value; }
        }

        public Series[] SelectedSeries
        {
            get
            {
                List<Series> _seriesList = new List<Series>();

                foreach (ListViewItem _listViewItem in this.listView1.SelectedItems)
                {
                    _seriesList.Add((Series)_listViewItem.Tag);
                }

                return _seriesList.ToArray();
            }
        }



        public MultiFunctionChartLegend()
        {
            InitializeComponent();


            this.seriesCollection = new BindingList<Series>();
            this.seriesCollection.ListChanged
                += new ListChangedEventHandler(seriesCollection_ListChanged);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            if (!this.DesignMode)
            {
                if (this.ownerChart == null)
                {
                    throw new Exception("Property 'OwnerChart' cannot be null.");
                }
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);


            int _clearance = 5;
            if (this.IsVerticalScrollBarVisible)
            {
                _clearance += SystemInformation.VerticalScrollBarWidth;
            }

            this.listView1.Columns[2].Width =
                this.listView1.Width
                - this.listView1.Columns[0].Width
                - this.listView1.Columns[1].Width
                - _clearance;
        }



        private bool IsVerticalScrollBarVisible
        {
            get
            {
                if (this.Height < this.GetRequiredListViewHeight())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private int GetRequiredListViewHeight()
        {
            int _requiredHeight;
            if (this.listView1.Items.Count > 0)
            {
                int _itemHeight = this.listView1.GetItemRect(0).Height;
                _requiredHeight =
                    this.listView1.Items.Count * _itemHeight
                    + _itemHeight / 4;
            }
            else
            {
                _requiredHeight = 0;
            }


            return _requiredHeight;
        }
        private void ComputeAutoHeight()
        {
            if (this.maxAllowedHeight > 0)
            {
                this.maxAutoHeight = this.maxAllowedHeight;
            }
            else
            {
                this.maxAutoHeight = this.Height;
            }


            if (this.autoHeight)
            {
                int _requiredHeight = this.GetRequiredListViewHeight();

                if (_requiredHeight > this.maxAutoHeight)
                {
                    this.Height = this.maxAutoHeight;
                }
                else
                {
                    this.Height = _requiredHeight;
                }
            }
        }
        private int GetTextWidth(ListViewItem _listViewItem)
        {
            Size _size = TextRenderer.MeasureText(_listViewItem.SubItems[2].Text, _listViewItem.Font);
            return _size.Width;
        }


        private void seriesCollection_ListChanged(object sender, ListChangedEventArgs e)
        {
            BindingList<Series> _seriesCollection = (BindingList<Series>)sender;


            #region "dodamo kar je novega"
            List<Series> _itemsToAdd = new List<Series>();
            foreach (Series _seriesToAdd in _seriesCollection)
            {
                if (!this.ContainsSeries(_seriesToAdd))
                {
                    _itemsToAdd.Add(_seriesToAdd);
                }
            }

            foreach (Series _series in _itemsToAdd)
            {
                this.AddSeries(_series);
            }
            #endregion "dodamo kar je novega"

            #region "pobrišemo kar ni več"
            List<ListViewItem> _itemsToDelete = new List<ListViewItem>();

            foreach (ListViewItem _listViewItem in this.listView1.Items)
            {
                Series _series = (Series)_listViewItem.Tag;

                if (!_seriesCollection.Contains(_series))
                {
                    _itemsToDelete.Add(_listViewItem);
                }
            }

            foreach (ListViewItem _listViewItem in _itemsToDelete)
            {
                this.listView1.Items.Remove(_listViewItem);
            }
            #endregion "pobrišemo kar ni več"

            this.ComputeAutoHeight();

            if (this.listView1.Items.Count > 0)
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }
        private bool ContainsSeries(Series _series)
        {
            bool _bool = false;

            foreach (ListViewItem _listViewItem in this.listView1.Items)
            {
                if (_listViewItem.Tag == _series)
                {
                    _bool = true;
                    break;
                }
            }

            return _bool;
        }
        private void AddSeries(Series _series)
        {
            this.cancelListView1_ItemCheck = true;


            ListViewItem _listViewItem = new ListViewItem();
            _listViewItem.Checked = true;
            _listViewItem.Tag = _series;

            ListViewItem.ListViewSubItem _listViewSubItem;

            _listViewSubItem = new ListViewItem.ListViewSubItem();
            _listViewSubItem.BackColor = _series.Color;
            _listViewItem.SubItems.Add(_listViewSubItem);

            _listViewSubItem = new ListViewItem.ListViewSubItem();
            _listViewSubItem.Text = _series.Label;
            _listViewItem.SubItems.Add(_listViewSubItem);

            this.listView1.Items.Add(_listViewItem);


            this.cancelListView1_ItemCheck = false;
        }


        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip1.RemoveAll();
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (this.listView1.SelectedItems.Contains(e.Item))
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.MenuHighlight), e.Bounds);
            }
        }
        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        Point _point = new Point(
                            e.Bounds.Location.X + 4,
                            e.Bounds.Location.Y + ((e.Bounds.Height - 12) / 2));

                        if (e.Item.Checked)
                        {
                            CheckBoxRenderer.DrawCheckBox(
                                e.Graphics,
                                _point,
                                System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
                        }
                        else
                        {
                            CheckBoxRenderer.DrawCheckBox(
                                e.Graphics,
                                _point,
                                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                        }
                    }
                    break;

                case 1:
                    {
                        Rectangle _rectangle = new Rectangle(
                            e.Bounds.Location.X,
                            e.Bounds.Location.Y + 1 + ((e.Bounds.Height - 12) / 2),
                            e.Bounds.Width,
                            11);

                        e.Graphics.FillRectangle(new SolidBrush(e.SubItem.BackColor), _rectangle);
                    }
                    break;

                case 2:
                    {
                        if (this.listView1.SelectedItems.Contains(e.Item))
                        {
                            e.SubItem.ForeColor = SystemColors.HighlightText;
                        }
                        else
                        {
                            e.SubItem.ForeColor = SystemColors.MenuText;
                        }

                        e.DrawText(TextFormatFlags.Default);
                    }
                    break;
            }
        }

        private bool cancelListView1_ItemCheck = false; //pove če upošteva event ali ne
        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.cancelListView1_ItemCheck)
            {
                return;
            }


            ListView _listView = (ListView)sender;

            if (_listView.SelectedItems.Count > 1)
            {
                e.NewValue = e.CurrentValue;
            }

            Series _series = (Series)_listView.Items[e.Index].Tag;

            if (e.NewValue == CheckState.Checked)
            {
                _series.Enabled = true;

                this.OnCustomLegendCheckedChanged(
                    _series,
                    true);
            }
            else
            {
                _series.Enabled = false;

                this.OnCustomLegendCheckedChanged(
                    _series,
                    false);
            }
        }
        protected virtual void OnCustomLegendCheckedChanged(Series _series, bool _checked)
        {
            if (CustomLegendCheckedChanged != null)
            {
                CustomLegendCheckedChanged(
                    this,
                    new CustomLegendCheckedChangedEventArgs(
                        _series,
                        _checked));
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.OnCustomLegendSelectedChanged(
                this.SelectedSeries);
        }
        protected virtual void OnCustomLegendSelectedChanged(Series[] _seriesArray)
        {
            if (CustomLegendsSelectedChanged != null)
            {
                CustomLegendsSelectedChanged(
                    this,
                    new CustomLegendEventArgs(
                        _seriesArray));
            }
        }

        private void listView1_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            CustomLegendItemEventArgs _customLegendItemEventArgs = new CustomLegendItemEventArgs((Series)e.Item.Tag);
            this.OnLegendItemToolTipShowing(ref _customLegendItemEventArgs);

            if ((this.listView1.Columns[0].Width
                + this.listView1.Columns[1].Width
                + this.GetTextWidth(e.Item)
                + 20 //NOTE: ne vem točno zakaj tu, ma je točno
                > this.listView1.Width)
                || (!_customLegendItemEventArgs.ShowOnlyWhenTextDoesNotFitScreen))
            {
                if (string.IsNullOrEmpty(_customLegendItemEventArgs.AlternativeText))
                {
                    this.toolTip1.Show(
                       e.Item.SubItems[2].Text,
                       this,
                       this.PointToClient(System.Windows.Forms.Cursor.Position));
                }
                else
                {
                    this.toolTip1.ToolTipTitle = e.Item.SubItems[2].Text;

                    //NOTE: če je alternative text null, potem se ne prikaže nič (to se lahko uporabi)!
                    this.toolTip1.Show(
                       _customLegendItemEventArgs.AlternativeText,
                       this,
                       this.PointToClient(System.Windows.Forms.Cursor.Position));
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                e.Cancel = true;
            }


            if (!e.Cancel)
            {
                this.OnLegendContextMenuOpening();
            }
        }
        private void contextMenuStrip1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            this.OnLegendContextMenuClosing();
        }

        private void toolStripMenuItem_Remove_Click(object sender, EventArgs e)
        {
            this.RemoveFunctions(this.SelectedSeries);
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    List<Series> _seriesList = new List<Series>();

                    foreach (ListViewItem _listViewItem in this.listView1.SelectedItems)
                    {
                        _seriesList.Add((Series)_listViewItem.Tag);
                    }

                    this.RemoveFunctions(_seriesList.ToArray());
                }
            }
        }
        private void RemoveFunctions(Series[] _seriesArray)
        {
            DialogResult _dialogResult = MessageBox.Show(
                this,
                "Are you sure, you want to remove the selected function(s)?",
                "Confirm function(s) remove",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (_dialogResult == DialogResult.Yes)
            {
                foreach (Series _series in _seriesArray)
                {
                    this.ownerChart.RemoveFunction((Function)_series.Tag);
                }

                this.OnCustomLegendRemoved(_seriesArray);
            }
        }
        protected virtual void OnCustomLegendRemoved(Series[] _seriesArray)
        {
            if (CustomLegendsRemoved != null)
            {
                CustomLegendsRemoved(
                    this,
                    new CustomLegendEventArgs(
                        _seriesArray));
            }
        }

        protected virtual void OnLegendContextMenuOpening()
        {
            if (this.LegendContextMenuOpening != null)
            {
                this.LegendContextMenuOpening(
                    this,
                    EventArgs.Empty);
            }
        }
        protected virtual void OnLegendContextMenuClosing()
        {
            if (this.LegendContextMenuClosing != null)
            {
                this.LegendContextMenuClosing(
                    this,
                    EventArgs.Empty);
            }
        }

        protected virtual void OnLegendItemToolTipShowing(ref CustomLegendItemEventArgs _customLegendItemEventArgs)
        {
            if (this.LegendItemToolTipShowing != null)
            {
                this.LegendItemToolTipShowing(
                    this,
                    _customLegendItemEventArgs);
            }
        }
    }


    public class CustomLegendEventArgs : EventArgs
    {
        public CustomLegendEventArgs(Series[] _seriesArray)
        {
            this.seriesArray = _seriesArray;
        }


        private Series[] seriesArray;
        public Series[] SeriesArray
        {
            get { return seriesArray; }
        }

    }
    public class CustomLegendCheckedChangedEventArgs : EventArgs
    {
        public CustomLegendCheckedChangedEventArgs(Series _series, bool _checked)
        {
            this.series = _series;
            this.@checked = _checked;
        }


        private Series series;
        public Series Series
        {
            get { return series; }
        }

        private bool @checked;
        public bool Checked
        {
            get { return @checked; }
        }

    }
    public class CustomLegendItemEventArgs : EventArgs
    {
        public CustomLegendItemEventArgs(Series _series)
        {
            this.series = _series;
        }


        private Series series = null;
        public Series @Series
        {
            get { return series; }
        }

        private bool showOnlyWhenTextDoesNotFitScreen = true;
        public bool ShowOnlyWhenTextDoesNotFitScreen
        {
            get { return showOnlyWhenTextDoesNotFitScreen; }
            set { showOnlyWhenTextDoesNotFitScreen = value; }
        }

        private string alternativeText = null;
        public string AlternativeText
        {
            get { return alternativeText; }
            set { alternativeText = value; }
        }
    }


    /// <summary>
    /// Provides additional code serialization for MultiFunctionChartLegend class.
    /// </summary>
    internal class MultiFunctionChartLegendSerializer : CodeDomSerializer
    {
        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            CodeDomSerializer _codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(
                typeof(MultiFunctionChartLegend).BaseType, typeof(CodeDomSerializer));

            object _object = _codeDomSerializer.Serialize(manager, value);
            if (_object is CodeStatementCollection)
            {
                CodeStatementCollection _codeStatementCollection = (CodeStatementCollection)_object;

                CodeExpression _targetObject = base.GetExpression(manager, value);
                if (_targetObject != null)
                {
                    CodePropertyReferenceExpression _codePropertyReferenceExpression = new CodePropertyReferenceExpression(_targetObject, "OwnerChart");


                    CodeAssignStatement _codeAssignStatement = new CodeAssignStatement(_codePropertyReferenceExpression, new CodeThisReferenceExpression());
                    _codeStatementCollection.Insert(0, _codeAssignStatement);


                    CodeCommentStatement _codeCommentStatement = new CodeCommentStatement(
                        new CodeComment("WARNING: This generates an exception in design time, but is ok (select 'Ignore and continue')."));
                    _codeStatementCollection.Insert(0, _codeCommentStatement);
                }
            }


            return _object;
        }

    }

}
