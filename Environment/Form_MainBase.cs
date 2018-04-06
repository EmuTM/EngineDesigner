using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using EngineDesigner.Environment;
using EngineDesigner.Common.Definitions;
using System.Reflection;

namespace EngineDesigner.Environment
{
    public partial class Form_MainBase : Form
    {
        //hrani imena fajlov in zapovrstno številko, če je več fajlov z istim imenom (samo zaradi captiona)
        private static Dictionary<string, int> fileNames = new Dictionary<string, int>();



        protected MenuStrip MenuStrip1
        {
            get { return this.menuStrip1; }
        }
        protected ToolStrip ToolStrip1
        {
            get { return this.toolStrip1; }
        }
        protected StatusStrip StatusStrip1
        {
            get { return this.statusStrip1; }
        }



        private FileInfo editedFile;
        public FileInfo EditedFile
        {
            get { return editedFile; }
        }

        private string caption;
        public string Caption
        {
            get { return caption; }
        }



        protected bool changesSaved = true;



        public Form_MainBase()
            : this(null, null)
        {
        }
        public Form_MainBase(object _object)
            : this(null, _object)
        {
        }
        public Form_MainBase(FileInfo _fileInfo)
            : this(_fileInfo, null)
        {
        }
        public Form_MainBase(FileInfo _fileInfo, object _object)
        {
            InitializeComponent();


            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                this.saveFileDialog1.Filter = this.FileTypeFilter;


                if (_fileInfo == null)
                {
                    this.editedFile = new FileInfo(
                        string.Format(
                            "{0}{1}",
                            this.saveFileDialog1.InitialDirectory,
                            this.DefaultEmptyFileName));
                }
                else
                {
                    this.editedFile = _fileInfo;
                }

                this.saveFileDialog1.FileName = this.editedFile.Name;
                this.saveFileDialog1.InitialDirectory = this.editedFile.DirectoryName;

                this.LoadFileOrObject(this.editedFile, _object);
            }
        }
        private void Form_MainBase_Load(object sender, EventArgs e)
        {
            this.CreateCaptionAndText();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!this.changesSaved)
            {
                DialogResult _dialogResult = MessageBox.Show(
                    this,
                    string.Format(
                        "Do you want to save changes made to '{0}'?",
                        this.caption),
                    this.Text,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);


                switch (_dialogResult)
                {
                    case DialogResult.Yes:
                        this.Save();

                        if (!this.changesSaved)
                        {
                            e.Cancel = true;
                        }
                        break;

                    case DialogResult.No:
                        //ne ukrepamo nič
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;


                    default:
                        throw new NotSupportedException();
                }

            }


            base.OnClosing(e);
        }



        public bool MergeMenuStripWith(MenuStrip _menuStrip)
        {
            return ToolStripManager.Merge(this.menuStrip1, _menuStrip);
        }
        public bool MergeToolStripWith(ToolStrip _toolStrip)
        {
            return ToolStripManager.Merge(this.toolStrip1, _toolStrip);
        }
        public bool MergeStatusStripWith(StatusStrip _statusStrip)
        {
            return ToolStripManager.Merge(this.statusStrip1, _statusStrip);
        }



        #region "File"
        private void toolStripMenuItem_File_Open_Click(object sender, EventArgs e)
        {
            this.Open();
        }
        private void toolStripMenuItem_File_Close_Click(object sender, EventArgs e)
        {
            this.CloseWindow();
        }
        private void toolStripMenuItem_File_Save_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        private void toolStripMenuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }
        private void toolStripMenuItem_File_SaveAll_Click(object sender, EventArgs e)
        {
            this.SaveAll();
        }
        private void toolStripMenuItem_File_Exit_Click(object sender, EventArgs e)
        {
            Main.CloseAll();
        }
        #endregion "File"

        #region "Window"
        private void toolStripMenuItem_Window_DropDownOpening(object sender, EventArgs e)
        {
            #region "najprej odstranimo vse"
            List<ToolStripMenuItem> _toolStripMenuItemsToRemove = new List<ToolStripMenuItem>();
            _toolStripMenuItemsToRemove.AddRange(toolStripMenuItem_Window.DropDownItems.OfType<ToolStripMenuItem>());

            foreach (ToolStripMenuItem _toolStripMenuItem in _toolStripMenuItemsToRemove)
            {
                //seveda samo tiste, ki imajo formo v tagu
                if (_toolStripMenuItem.Tag is Form_MainBase)
                {
                    _toolStripMenuItem.Click
                        -= new EventHandler(toolStripMenuItem_Window_Item_Click);

                    toolStripMenuItem_Window.DropDownItems.Remove(_toolStripMenuItem);
                }
            }
            #endregion "najprej odstranimo vse"

            #region "dodamo morebitne forme na menu, v pravem vrstnem redu"
            Form_MainBase _activeForm = null;
            #region "poiščemo aktivno formo"
            foreach (Form_MainBase _form_MainBase in Main.ActiveForms)
            {
                //če je to naša forma, potem je seveda aktivna!
                if (_form_MainBase == this)
                {
                    _activeForm = _form_MainBase;
                    break;
                }
            }

            //če nismo našli nobene, vzamemo pač prvo
            if (_activeForm == null)
            {
                _activeForm = Main.ActiveForms.ElementAt(0);
            }
            #endregion "poiščemo aktivno mdi formo"

            #region "dodamo aktivno, da je na vrhu in jo čekiramo"
            int _menuItemPosition = 0;
            AddMenuItem(_activeForm, _menuItemPosition, true);
            #endregion "dodamo aktivno, da je na vrhu in jo čekiramo"

            #region "dodamo vse forme, razen aktivne - torej so pod aktivno in niso čekirane"
            foreach (Form_MainBase _form_MainBase in Main.ActiveForms)
            {
                if (_form_MainBase != _activeForm)
                {
                    _menuItemPosition++;
                    this.AddMenuItem(_form_MainBase, _menuItemPosition, false);
                }
            }
            #endregion "dodamo vse forme, razen aktivne - torej so pod aktivno in niso čekirane"
            #endregion "dodamo morebitne forme na menu, v pravem vrstnem redu"
        }
        private void AddMenuItem(Form_MainBase _form_MainBase, int _positionInMenu, bool _checked)
        {
            ToolStripMenuItem _toolStripMenuItem = new ToolStripMenuItem();
            _toolStripMenuItem.Checked = _checked;
            _toolStripMenuItem.CheckOnClick = true;

            int _itemIndex = _positionInMenu + 1;
            _toolStripMenuItem.Text = string.Format(
                "{0} {1}",
                _itemIndex.ToString(),
                _form_MainBase.caption);

            _toolStripMenuItem.Tag = _form_MainBase;

            _toolStripMenuItem.Click
                += new EventHandler(toolStripMenuItem_Window_Item_Click);


            //this.toolStripMenuItem_Window.DropDownItems.Add(_toolStripMenuItem);
            this.toolStripMenuItem_Window.DropDownItems.Insert(_positionInMenu, _toolStripMenuItem);
        }

        private void toolStripMenuItem_Window_Item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _toolStripMenuItem_mdiChild = (ToolStripMenuItem)sender;
            Form_MainBase _form_MainBase = (Form_MainBase)_toolStripMenuItem_mdiChild.Tag;

            //če je to naša forma, potem je seveda aktivna!
            if (_form_MainBase != this)
            {
                _form_MainBase.Activate();
            }
        }

        private void toolStripMenuItem_Window_Windows_Click(object sender, EventArgs e)
        {
            Form_Windows _form_Windows = new Form_Windows(Main.ActiveForms.ToArray());
            _form_Windows.Text = string.Format(
                "{0} - {1}",
                _form_Windows.Text,
                this.DefaultTopic);
            _form_Windows.ShowDialog();

            //po končanem ShowDialogu, moramo formo (če še obstaja) še enkrat aktivirati (če že ni), ker čene ShowDialog aktivira po svoje
            if (_form_Windows.LastActivatedForm != null)
            {
                if (!_form_Windows.LastActivatedForm.Focused)
                {
                    _form_Windows.LastActivatedForm.Activate();
                }
            }
        }
        #endregion "Window"

        #region "Help"
        private void toolStripMenuItem_Contents_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, string.Format(
                "file:\\{0}\\EngineDesigner.chm",
                System.Environment.CurrentDirectory));
        }

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            Form_AboutBase _form_AboutBase = this.ObtainAboutForm();
            _form_AboutBase.ShowDialog();
        }
        #endregion "Help"

        #region "ToolBar"
        private void toolStripButton_Open_Click(object sender, EventArgs e)
        {
            this.Open();
        }
        private void toolStripButton_Save_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        private void toolStripButton_SaveAll_Click(object sender, EventArgs e)
        {
            this.SaveAll();
        }
        #endregion "ToolBar"

        #region "Drag&Drop"
        private const string DRAG_DROP_FileNameW = "FileNameW";


        private void Form_Main_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;


            if (e.Data.GetDataPresent(Form_MainBase.DRAG_DROP_FileNameW))
            {
                string[] _fileNames = (string[])e.Data.GetData(Form_MainBase.DRAG_DROP_FileNameW);

                foreach (string _fileName in _fileNames)
                {
                    FileInfo _fileInfo = new FileInfo(_fileName);
                    if (_fileInfo.Exists)
                    {
                        if (_fileInfo.Extension == this.DefaultFileNameExtension)
                        {
                            e.Effect = DragDropEffects.Move;
                        }
                    }
                }
            }
        }
        private void Form_Main_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(Form_MainBase.DRAG_DROP_FileNameW))
            {
                string[] _fileNames = (string[])e.Data.GetData(Form_MainBase.DRAG_DROP_FileNameW);

                foreach (string _fileName in _fileNames)
                {
                    FileInfo _fileInfo = new FileInfo(_fileName);
                    if (_fileInfo.Exists)
                    {
                        Main.ObtainNewForm(this.ObtainNewForm(_fileInfo));
                    }
                }
            }
        }
        #endregion "Drag&Drop"



        private void Open()
        {
            DialogResult _dialogResult = openFileDialog1.ShowDialog();

            if (_dialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    FileInfo _fileInfo = new FileInfo(openFileDialog1.FileName);

                    if (_fileInfo.Exists)
                    {
                        Form_MainBase _form_MainBase = this.ObtainNewForm(_fileInfo);

                        if (_form_MainBase != null)
                        {
                            Main.ObtainNewForm(_form_MainBase);

                            //če v tej inštanci ni nič pametnega in/ali nimamo sprememb v zraku, ugasnemo trenutno okno
                            if ((!this.editedFile.Exists)
                                && (this.changesSaved))
                            {
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
        private void CloseWindow()
        {
            if (Main.ActiveForms.Count < 2)
            {
                this.StartNewWindow(new Form_MainBase());
            }

            this.Close();
        }
        private void Save()
        {
            if (this.editedFile.Exists)
            {
                this.changesSaved = this.SaveFile(this.editedFile);
            }
            else
            {
                this.SaveAs();
            }
        }
        private void SaveAs()
        {
            DialogResult _dialogResult = saveFileDialog1.ShowDialog();

            if (_dialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    this.editedFile = new FileInfo(saveFileDialog1.FileName);
                    this.changesSaved = this.SaveFile(this.editedFile);

                    this.CreateCaptionAndText();
                }
            }
        }
        private void SaveAll()
        {
            foreach (Form_MainBase _form_MainBase in Main.ActiveForms)
            {
                _form_MainBase.Save();
            }
        }

        private void CreateCaptionAndText()
        {
            //določimo caption
            if (!Form_MainBase.fileNames.ContainsKey(this.editedFile.Name))
            {
                Form_MainBase.fileNames.Add(this.editedFile.Name, 0);
            }
            Form_MainBase.fileNames[this.editedFile.Name]++;

            this.caption = this.editedFile.Name.Replace(this.editedFile.Extension, string.Empty);
            if (Form_MainBase.fileNames[this.editedFile.Name] > 1)
            {
                this.caption = string.Format(
                    "{0} ({1})",
                    this.caption,
                    Form_MainBase.fileNames[this.editedFile.Name]);
            }

            this.caption = string.Format(
                "{0}{1}",
                this.caption,
                this.editedFile.Extension);

            //in še text
            this.Text = string.Format(
                "{0} - {1}",
                this.caption,
                this.DefaultTopic);
        }



        protected virtual Form_MainBase ObtainNewForm(FileInfo _fileInfo)
        {
            throw new NotImplementedException();
        }
        protected virtual Form_AboutBase ObtainAboutForm()
        {
            return new Form_AboutBase();
        }
        protected virtual void LoadFileOrObject(FileInfo _fileInfo, object _object)
        {
            //to se žal kliče tukaj, kar ni najboljše; je pa zato vsaj v ločeni metodi
            this.DisableItemsItems();
        }
        protected virtual bool SaveFile(FileInfo _fileInfo)
        {
            throw new NotImplementedException();
        }
        protected virtual string FileTypeFilter
        {
            get
            {
                return string.Format(
                    "{0} {1}",
                    About.AssemblyTitle,
                    "files (*.xml)|*.xml");
            }
        }
        protected virtual string DefaultEmptyFileName
        {
            get
            {
                return "Untitled.xml";
            }
        }
        protected virtual string DefaultFileNameExtension
        {
            get
            {
                return "xml";
            }
        }
        protected virtual string DefaultTopic
        {
            get
            {
                return About.AssemblyTitle;
            }
        }
        protected void StartNewWindow(Form_MainBase _form_MainBase)
        {
            Main.ObtainNewForm(_form_MainBase);

            if (this.changesSaved)
            {
                this.Close();
            }
        }



        //NOTE: to ni najboljše rešeno; u glavnem, tukaj skrijemo menu in toolstrip iteme, ki v base formi ne smejo obstajat
        private void DisableItemsItems()
        {
            this.toolStripMenuItem_File_Save.Visible = false;
            this.toolStripButton_Save.Visible = false;

            this.toolStripMenuItem_File_SaveAs.Visible = false;

            this.toolStripMenuItem_File_SaveAll.Visible = false;
            this.toolStripButton_SaveAll.Visible = false;

            this.toolStripSeparator_Save.Visible = false;

            this.toolStripMenuItem_Edit.Visible = false;
            this.toolStripMenuItem_View.Visible = false;
        }

    }
}
