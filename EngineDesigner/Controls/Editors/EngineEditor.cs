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
using EngineDesigner.Common.CustomCollections;

namespace EngineDesigner.Controls.Editors
{
    internal partial class EngineEditor : IPartEditor
    {
        public EngineEditor()
        {
            InitializeComponent();



            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                this.menuStrip1.Visible = false;
                this.toolStrip1.Visible = false;
            }
        }



        public new Engine EditedPart
        {
            get { return (Engine)base.EditedPart; }
            set { base.EditedPart = value; }
        }



        public bool MergeMenuStripWith(MenuStrip _menuStrip)
        {
            return ToolStripManager.Merge(this.menuStrip1, _menuStrip);
        }
        public bool MergeToolStripWith(ToolStrip _toolStrip)
        {
            return ToolStripManager.Merge(this.toolStrip1, _toolStrip);
        }



        private void toolStripMenuItem_CopyAsTemplate_Click(object sender, EventArgs e)
        {
            this.CopyAsTemplate(base.propertyGrid1.SelectedGridItem);
        }
        private void toolStripMenuItem_PasteFromTemplate_Click(object sender, EventArgs e)
        {
            this.PasteFromTemplate(base.propertyGrid1.SelectedGridItem);
        }

        private void toolStripMenuItem_Edit_CopyAsTemplate_Click(object sender, EventArgs e)
        {
            this.CopyAsTemplate(base.propertyGrid1.SelectedGridItem);
        }
        private void toolStripMenuItem_Edit_PasteFromTemplate_Click(object sender, EventArgs e)
        {
            this.PasteFromTemplate(base.propertyGrid1.SelectedGridItem);
        }

        private void toolStripButton_CopyAsTemplate_Click(object sender, EventArgs e)
        {
            this.CopyAsTemplate(base.propertyGrid1.SelectedGridItem);
        }
        private void toolStripButton_PastFromTemplate_Click(object sender, EventArgs e)
        {
            this.PasteFromTemplate(base.propertyGrid1.SelectedGridItem);
        }

        private void CopyAsTemplate(GridItem _gridItem)
        {
            Clipboard.SetData(DataFormats.Serializable, EngineDesigner.Common.Utility.CopyObject(_gridItem.Value));
            this.PasteFromTemplateEnabled = true;
        }
        private void PasteFromTemplate(GridItem _gridItem)
        {
            object _object = Clipboard.GetData(DataFormats.Serializable);


            if (_gridItem.Value is CustomList<PositionedCylinder>)
            {
                CustomList<PositionedCylinder> _selectedItem = (CustomList<PositionedCylinder>)_gridItem.Value;
                PositionedCylinder _fromClipboard = (PositionedCylinder)_object;

                //NOTE: to je bilo po starem
                //@List<PositionedCylinder> _positionedCylinders = new @List<PositionedCylinder>();
                //foreach (PositionedCylinder _existingPositionedCylinder in _selectedItem)
                //{
                //    PositionedCylinder _newPositionedCylinder = EngineDesigner.Common.Utility.CopyObject<PositionedCylinder>(_fromClipboard);
                //    _newPositionedCylinder.Position = _existingPositionedCylinder.Position; //pozicije ne popravljamo!
                //    _newPositionedCylinder.Offset_mm = _existingPositionedCylinder.Offset_mm; //offseta ne popravljamo!
                //    _newPositionedCylinder.Tilt_deg = _existingPositionedCylinder.Tilt_deg; //offseta ne popravljamo!
                //    _newPositionedCylinder.FiringAngle_deg = _existingPositionedCylinder.FiringAngle_deg; //kota vžiga ne popravljamo!
                //    _positionedCylinders.Add(_newPositionedCylinder);
                //}
                //base.SetItemValue(_gridItem, _positionedCylinders);

                //NOTE: po novem ne spreminjamo več celega propetija, ker ni prav (in nima niti več setterja), ampak samo cilindre!
                for (int a = 0; a < _selectedItem.Count; a++)
                {
                    int _position = _selectedItem[a].Position;
                    double _offset_mm = _selectedItem[a].Offset_mm;
                    double _tilt_deg = _selectedItem[a].Tilt_deg;
                    double _firingAngle_deg = _selectedItem[a].FiringAngle_deg;

                    _selectedItem[a] = EngineDesigner.Common.Utility.CopyObject<PositionedCylinder>(_fromClipboard);
                    _selectedItem[a].Position = _position; //pozicije ne popravljamo!
                    _selectedItem[a].Offset_mm = _offset_mm; //offseta ne popravljamo!
                    _selectedItem[a].Tilt_deg = _tilt_deg; //offseta ne popravljamo!
                    _selectedItem[a].FiringAngle_deg = _firingAngle_deg; //kota vžiga ne popravljamo!
                }
            }
            else if (_gridItem.Value is PositionedCylinder)
            {
                PositionedCylinder _selectedItem = (PositionedCylinder)_gridItem.Value;
                PositionedCylinder _fromClipboard = (PositionedCylinder)_object;

                PositionedCylinder _positionedCylinder = EngineDesigner.Common.Utility.CopyObject<PositionedCylinder>(_fromClipboard);
                _positionedCylinder.Position = _selectedItem.Position; //pozicije ne popravljamo!
                _positionedCylinder.Offset_mm = _selectedItem.Offset_mm; //offseta ne popravljamo!
                _positionedCylinder.Tilt_deg = _selectedItem.Tilt_deg; //offseta ne popravljamo!
                _positionedCylinder.FiringAngle_deg = _selectedItem.FiringAngle_deg; //kota vžiga ne popravljamo!

                base.SetItemValue(_gridItem, _positionedCylinder);
            }
            else if (_gridItem.Value is Cycle)
            {
                Cycle _fromClipboard = (Cycle)_object;

                base.SetItemValue(_gridItem, EngineDesigner.Common.Utility.CopyObject<Cycle>(_fromClipboard));
            }
            else if (_gridItem.Value is Piston)
            {
                Piston _fromClipboard = (Piston)_object;

                base.SetItemValue(_gridItem, EngineDesigner.Common.Utility.CopyObject<Piston>(_fromClipboard));
            }
            else if (_gridItem.Value is ConnectingRod)
            {
                ConnectingRod _fromClipboard = (ConnectingRod)_object;

                base.SetItemValue(_gridItem, EngineDesigner.Common.Utility.CopyObject<ConnectingRod>(_fromClipboard));
            }
            else
            {
                throw new NotSupportedException();
            }

            this.EditedPart.Validate();
        }

        private bool CopyAsTemplateEnabled
        {
            set
            {
                toolStripMenuItem_CopyAsTemplate.Enabled = value;
                toolStripMenuItem_Edit_CopyAsTemplate.Enabled = value;
                toolStripButton_CopyAsTemplate.Enabled = value;
            }
        }
        private bool PasteFromTemplateEnabled
        {
            set
            {
                toolStripMenuItem_PasteFromTemplate.Enabled = value;
                toolStripMenuItem_Edit_PasteFromTemplate.Enabled = value;
                toolStripButton_PastFromTemplate.Enabled = value;
            }
        }

        protected override void OnEditedItemSelected(GridItem _selectedItem)
        {
            base.OnEditedItemSelected(_selectedItem);


            //disablamo vse
            foreach (ToolStripMenuItem _toolStripMenuItem in contextMenuStrip1.Items)
            {
                _toolStripMenuItem.Enabled = false;
            }


            #region "copy enabled"
            bool _copyEnabled = false;

            if (_selectedItem != null)
            {
                if (_selectedItem.Value is PositionedCylinder)
                {
                    _copyEnabled = true;
                }
                else if (_selectedItem.Value is Cycle)
                {
                    _copyEnabled = true;
                }
                else if (_selectedItem.Value is Piston)
                {
                    _copyEnabled = true;
                }
                else if (_selectedItem.Value is ConnectingRod)
                {
                    _copyEnabled = true;
                }
            }

            this.CopyAsTemplateEnabled = _copyEnabled;
            #endregion "copy enabled"

            #region "paste enabled"
            bool _pasteEnabled = true;

            if (!Clipboard.ContainsData(DataFormats.Serializable))
            {
                _pasteEnabled = false;
            }
            else if (_selectedItem == null)
            {
                _pasteEnabled = false;
            }
            else if (_selectedItem.Value.GetType() != Clipboard.GetData(DataFormats.Serializable).GetType())
            {
                //to je zato, da lahko pastamo čez vse cilindre
                if ((_selectedItem.Value is CustomList<PositionedCylinder>)
                    && (Clipboard.GetData(DataFormats.Serializable) is PositionedCylinder))
                {
                    _pasteEnabled = true;
                }
                else
                {
                    _pasteEnabled = false;
                }
            }

            this.PasteFromTemplateEnabled = _pasteEnabled;
            #endregion "paste enabled"
        }

    }
}
