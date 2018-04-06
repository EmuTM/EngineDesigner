using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;
using EngineDesigner.Machine;

namespace EngineDesigner.Wizards.NewEngine
{
    internal partial class Form_NewEngineWizard_EngineLayout : Form_NewEngineWizardBase
    {
        private List<TreeNode> removedNodes = new List<TreeNode>();



        public Form_NewEngineWizard_EngineLayout()
            : this(null)
        {
        }
        public Form_NewEngineWizard_EngineLayout(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[2];

            base.Next = new Form_NewEngineWizard_BoreAndStroke(this);
            base.NextEnabled = false;

            //če hočemo, da je gumb finish viden, mora imet neko ciljno formo!
            base.Finish = new Form_NewEngineWizard_Finish(this);
            //ga pa moramo disablat, dokler ni primeren
            base.FinishEnabled = false;


            //zmečemo node dol s treeViewa
            foreach (TreeNode _treeNode in this.treeView1.Nodes)
            {
                this.removedNodes.Add(_treeNode);
            }
            foreach (TreeNode _treeNode in this.removedNodes)
            {
                this.treeView1.Nodes.Remove(_treeNode);
            }

            //damo nazaj gor ustrezne
            if (((NewEngineWizardState)base.State).Cycle .Equals( Cycle.TwoStroke))
            {
                foreach (TreeNode _treeNode in this.removedNodes)
                {
                    if (_treeNode.Name == "Node_TwoStroke")
                    {
                        treeView1.Nodes.Add(_treeNode);
                        break;
                    }
                }
            }
            else if (((NewEngineWizardState)base.State).Cycle .Equals( Cycle.FourStroke))
            {
                foreach (TreeNode _treeNode in this.removedNodes)
                {
                    if (_treeNode.Name == "Node_FourStroke")
                    {
                        treeView1.Nodes.Add(_treeNode);
                        break;
                    }
                }
            }
            else
            {
                throw new NotSupportedException();
            }


            //selektamo pravo
            if (!string.IsNullOrEmpty(((NewEngineWizardState)base.State).SelectedEngineLayoutNode))
            {
                TreeNode[] _treeNodes = this.treeView1.Nodes.Find(((NewEngineWizardState)base.State).SelectedEngineLayoutNode, true);

                //morda smo zamenjali cikel in ta node ni več na razpolago!
                try
                {
                    this.treeView1.SelectedNode = _treeNodes[0];
                }
                catch
                {
                    this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
                }
            }
            else
            {
                this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
            }

            this.treeView1_AfterSelect(this.treeView1, new TreeViewEventArgs(this.treeView1.SelectedNode));


            //in expandamo
            this.treeView1.SelectedNode.Expand();
        }

        protected override bool OnNext()
        {
            this.SetEngineLayoutToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetEngineLayoutToState();

            return base.OnFinish();
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.label_SelectedNodeInfo.Text = e.Node.ToolTipText;


            if (e.Node.Nodes.Count == 0)
            {
                base.NextEnabled = true;
                base.FinishEnabled = true;
            }
            else
            {
                base.NextEnabled = false;
                base.FinishEnabled = false;
            }
        }


        private void SetEngineLayoutToState()
        {
            string[] _strings = this.treeView1.SelectedNode.Name.Split('_');

            //_strings[1] = TwoStroke, FourStroke
            //_strings[2] = Single, Inline, Vee, Boxer
            //_strings[3] = Number

            switch (_strings[1]) //TwoStroke, FourStroke
            {
                case "TwoStroke":
                    #region
                    switch (_strings[2]) //Single, Inline
                    {
                        case "Single":
                            #region
                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                            {
                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d)
                            };
                            #endregion
                            break;

                        case "Inline":
                            #region
                            switch (_strings[3]) //Number
                            {
                                case "2":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 180d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "3":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 120d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 240d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "4":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 180d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 90d, 0d),
                                        new NewEngineWizardState.CylinderLyout(4, 270d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "5":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 144d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 216d, 0d),
                                        new NewEngineWizardState.CylinderLyout(4, 288d, 0d),
                                        new NewEngineWizardState.CylinderLyout(5, 72d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "6":
                                    #region
                                    switch (_strings[4]) //SingleFiring, DoubleFiring
                                    {
                                        case "SingleFiring":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                                new NewEngineWizardState.CylinderLyout(2, 120d, 0d),
                                                new NewEngineWizardState.CylinderLyout(3, 240d, 0d),
                                                new NewEngineWizardState.CylinderLyout(4, 180d, 0d),
                                                new NewEngineWizardState.CylinderLyout(5, 300d, 0d),
                                                new NewEngineWizardState.CylinderLyout(6, 60d, 0d)
                                            };
                                            #endregion
                                            break;

                                        case "DoubleFiring":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                                new NewEngineWizardState.CylinderLyout(2, 240d, 0d),
                                                new NewEngineWizardState.CylinderLyout(3, 120d, 0d),
                                                new NewEngineWizardState.CylinderLyout(4, 120d, 0d),
                                                new NewEngineWizardState.CylinderLyout(5, 240d, 0d),
                                                new NewEngineWizardState.CylinderLyout(6, 0d, 0d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "8":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 90d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 270d, 0d),
                                        new NewEngineWizardState.CylinderLyout(4, 180d, 0d),
                                        new NewEngineWizardState.CylinderLyout(5, 225d, 0d),
                                        new NewEngineWizardState.CylinderLyout(6, 135d, 0d),
                                        new NewEngineWizardState.CylinderLyout(7, 315d, 0d),
                                        new NewEngineWizardState.CylinderLyout(8, 45d, 0d),
                                    };
                                    #endregion
                                    break;


                                default:
                                    throw new NotSupportedException();
                            }
                            #endregion
                            break;

                        case "Vee":
                            #region
                            switch (_strings[3]) //Number
                            {
                                case "2":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -45d),
                                        new NewEngineWizardState.CylinderLyout(2, 90d, 45d)
                                    };
                                    #endregion
                                    break;

                                case "4":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -45d),
                                        new NewEngineWizardState.CylinderLyout(2, 90d, 45d),
                                        new NewEngineWizardState.CylinderLyout(3, 180d, -45d),
                                        new NewEngineWizardState.CylinderLyout(4, 270d, 45d)
                                    };
                                    #endregion
                                    break;

                                case "6":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -30d),
                                        new NewEngineWizardState.CylinderLyout(2, 60d, 30d),
                                        new NewEngineWizardState.CylinderLyout(3, 120d, -30d),
                                        new NewEngineWizardState.CylinderLyout(4, 180d, 30d),
                                        new NewEngineWizardState.CylinderLyout(5, 240d, -30d),
                                        new NewEngineWizardState.CylinderLyout(6, 300d, 30d)
                                    };
                                    #endregion
                                    break;

                                case "8":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0, -22.5d),
                                        new NewEngineWizardState.CylinderLyout(2, 45d, 22.5d),
                                        new NewEngineWizardState.CylinderLyout(3, 180d, -22.5d),
                                        new NewEngineWizardState.CylinderLyout(4, 225d, 22.5d),
                                        new NewEngineWizardState.CylinderLyout(5, 90d, -22.5d),
                                        new NewEngineWizardState.CylinderLyout(6, 135d, 22.5d),
                                        new NewEngineWizardState.CylinderLyout(7, 270d, -22.5d),
                                        new NewEngineWizardState.CylinderLyout(8, 315d, 22.5d)
                                    };
                                    #endregion
                                    break;
                            }
                            #endregion
                            break;

                        case "Boxer":
                            #region
                            switch (_strings[3]) //Number
                            {
                                case "2":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -90d),
                                        new NewEngineWizardState.CylinderLyout(2, 0d, 90d)
                                    };
                                    #endregion
                                    break;

                                case "4":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -90d),
                                        new NewEngineWizardState.CylinderLyout(2, 0d, 90d),
                                        new NewEngineWizardState.CylinderLyout(3, 180d, -90d),
                                        new NewEngineWizardState.CylinderLyout(4, 180d, 90d)
                                    };
                                    #endregion
                                    break;


                                default:
                                    throw new NotSupportedException();
                            }
                            #endregion
                            break;
                    }
                    #endregion
                    break;

                case "FourStroke":
                    #region
                    switch (_strings[2]) //Single, Inline, Vee, Boxer
                    {
                        case "Single":
                            #region
                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                            {
                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d)
                            };
                            #endregion
                            break;

                        case "Inline":
                            #region
                            switch (_strings[3]) //Number
                            {
                                case "2":
                                    #region
                                    switch (_strings[4]) //360, 270
                                    {
                                        case "360":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                                new NewEngineWizardState.CylinderLyout(2, 360d, 0d)
                                            };
                                            #endregion
                                            break;

                                        case "270":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                                new NewEngineWizardState.CylinderLyout(2, 270d, 0d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "3":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 480d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 240d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "4":
                                    #region
                                    switch (_strings[4]) //FlatPlane, CrossPlane
                                    {
                                        case "FlatPlane":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                                new NewEngineWizardState.CylinderLyout(2, 180d, 0d),
                                                new NewEngineWizardState.CylinderLyout(3, 540d, 0d),
                                                new NewEngineWizardState.CylinderLyout(4, 360d, 0d)
                                            };
                                            #endregion
                                            break;

                                        case "CrossPlane":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                                new NewEngineWizardState.CylinderLyout(2, 450d, 0d),
                                                new NewEngineWizardState.CylinderLyout(3, 270d, 0d),
                                                new NewEngineWizardState.CylinderLyout(4, 540d, 0d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "5":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 144d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 576d, 0d),
                                        new NewEngineWizardState.CylinderLyout(4, 288d, 0d),
                                        new NewEngineWizardState.CylinderLyout(5, 432d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "6":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 480d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 240d, 0d),
                                        new NewEngineWizardState.CylinderLyout(4, 600d, 0d),
                                        new NewEngineWizardState.CylinderLyout(5, 120d, 0d),
                                        new NewEngineWizardState.CylinderLyout(6, 360d, 0d)
                                    };
                                    #endregion
                                    break;

                                case "8":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, 0d),
                                        new NewEngineWizardState.CylinderLyout(2, 180d, 0d),
                                        new NewEngineWizardState.CylinderLyout(3, 450d, 0d),
                                        new NewEngineWizardState.CylinderLyout(4, 630d, 0d),
                                        new NewEngineWizardState.CylinderLyout(5, 270d, 0d),
                                        new NewEngineWizardState.CylinderLyout(6, 90d, 0d),
                                        new NewEngineWizardState.CylinderLyout(7, 540d, 0d),
                                        new NewEngineWizardState.CylinderLyout(8, 360d, 0d)
                                    };
                                    #endregion
                                    break;


                                default:
                                    throw new NotSupportedException();
                            }
                            #endregion
                            break;

                        case "Vee":
                            #region
                            switch (_strings[3]) //Number
                            {
                                case "2":
                                    #region
                                    switch (_strings[4]) //Vee angle
                                    {
                                        case "45":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(2, 405d, 22.5d)
                                            };
                                            #endregion
                                            break;

                                        case "60":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -30d),
                                                new NewEngineWizardState.CylinderLyout(2, 420d, 30d)
                                            };
                                            #endregion
                                            break;

                                        case "90":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -45d),
                                                new NewEngineWizardState.CylinderLyout(2, 450d, 45d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "4":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -45d),
                                        new NewEngineWizardState.CylinderLyout(2, 90d, 45d),
                                        new NewEngineWizardState.CylinderLyout(3, 180d, -45d),
                                        new NewEngineWizardState.CylinderLyout(4, 270d , 45d)
                                    };
                                    #endregion
                                    break;

                                case "6":
                                    #region
                                    switch (_strings[4]) //Vee angle
                                    {
                                        case "60":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -30d),
                                                new NewEngineWizardState.CylinderLyout(2, 120d, 30d),
                                                new NewEngineWizardState.CylinderLyout(3, 240d, -30d),
                                                new NewEngineWizardState.CylinderLyout(4, 360d, 30d),
                                                new NewEngineWizardState.CylinderLyout(5, 480d, -30d),
                                                new NewEngineWizardState.CylinderLyout(6, 600d, 30d)
                                            };
                                            #endregion
                                            break;

                                        case "90":
                                            #region
                                            switch (_strings[5]) //SharedCrankpin, SplitCrankpin
                                            {
                                                case "SharedCrankpin":
                                                    #region
                                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                                    {
                                                        new NewEngineWizardState.CylinderLyout(1, 0, -45d),
                                                        new NewEngineWizardState.CylinderLyout(2, 90d, 45d),
                                                        new NewEngineWizardState.CylinderLyout(3, 240d, -45d),
                                                        new NewEngineWizardState.CylinderLyout(4, 330d, 45d),
                                                        new NewEngineWizardState.CylinderLyout(5, 480d, -45d),
                                                        new NewEngineWizardState.CylinderLyout(6, 570d, 45d)
                                                    };
                                                    #endregion
                                                    break;

                                                case "SplitCrankpin":
                                                    #region
                                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                                    {
                                                        new NewEngineWizardState.CylinderLyout(1, 0d, -45d),
                                                        new NewEngineWizardState.CylinderLyout(2, 120d, 45d),
                                                        new NewEngineWizardState.CylinderLyout(3, 240d, -45d),
                                                        new NewEngineWizardState.CylinderLyout(4, 360d, 45d),
                                                        new NewEngineWizardState.CylinderLyout(5, 480d, -45d),
                                                        new NewEngineWizardState.CylinderLyout(6, 600d, 45d)
                                                    };
                                                    #endregion
                                                    break;


                                                default:
                                                    throw new NotSupportedException();
                                            }
                                            #endregion
                                            break;

                                        case "120":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -60d),
                                                new NewEngineWizardState.CylinderLyout(2, 120d, 60d),
                                                new NewEngineWizardState.CylinderLyout(3, 240d, -60d),
                                                new NewEngineWizardState.CylinderLyout(4, 360d, 60d),
                                                new NewEngineWizardState.CylinderLyout(5, 480d, -60d),
                                                new NewEngineWizardState.CylinderLyout(6, 600d, 60d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "8":
                                    #region
                                    switch (_strings[5]) //CrossPlane, FlatPlane
                                    {
                                        case "CrossPlane":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0, -45d),
                                                new NewEngineWizardState.CylinderLyout(2, 90d, 45d),
                                                new NewEngineWizardState.CylinderLyout(3, 270d, -45d),
                                                new NewEngineWizardState.CylinderLyout(4, 360d, 45d),
                                                new NewEngineWizardState.CylinderLyout(5, 450d, -45d),
                                                new NewEngineWizardState.CylinderLyout(6, 540d, 45d),
                                                new NewEngineWizardState.CylinderLyout(7, 180d, -45d),
                                                new NewEngineWizardState.CylinderLyout(8, 630d, 45d)
                                            };
                                            #endregion
                                            break;

                                        case "FlatPlane":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0, -45d),
                                                new NewEngineWizardState.CylinderLyout(2, 90d, 45d),
                                                new NewEngineWizardState.CylinderLyout(3, 180d, -45d),
                                                new NewEngineWizardState.CylinderLyout(4, 270d, 45d),
                                                new NewEngineWizardState.CylinderLyout(5, 540d, -45d),
                                                new NewEngineWizardState.CylinderLyout(6, 630d, 45d),
                                                new NewEngineWizardState.CylinderLyout(7, 360d, -45d),
                                                new NewEngineWizardState.CylinderLyout(8, 450d, 45d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "10":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -36d),
                                        new NewEngineWizardState.CylinderLyout(2, 72d, 36d),
                                        new NewEngineWizardState.CylinderLyout(3, 144d, -36d),
                                        new NewEngineWizardState.CylinderLyout(4, 216d, 36d),
                                        new NewEngineWizardState.CylinderLyout(5, 576d, -36d),
                                        new NewEngineWizardState.CylinderLyout(6, 648d, 36d),
                                        new NewEngineWizardState.CylinderLyout(7, 288d, -36d),
                                        new NewEngineWizardState.CylinderLyout(8, 360d, 36d),
                                        new NewEngineWizardState.CylinderLyout(9, 432d, -36d),
                                        new NewEngineWizardState.CylinderLyout(10, 504d, 36d)
                                    };
                                    #endregion
                                    break;

                                case "12":
                                    #region
                                    switch (_strings[4]) //60, 65
                                    {
                                        case "60":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -30d),
                                                new NewEngineWizardState.CylinderLyout(2, 60d, 30d),
                                                new NewEngineWizardState.CylinderLyout(3, 480d, -30d),
                                                new NewEngineWizardState.CylinderLyout(4, 540d, 30d),
                                                new NewEngineWizardState.CylinderLyout(5, 240d, -30d),
                                                new NewEngineWizardState.CylinderLyout(6, 300d, 30d),
                                                new NewEngineWizardState.CylinderLyout(7, 600d, -30d),
                                                new NewEngineWizardState.CylinderLyout(8, 660d, 30d),
                                                new NewEngineWizardState.CylinderLyout(9, 120d, -30d),
                                                new NewEngineWizardState.CylinderLyout(10, 180d, 30d),
                                                new NewEngineWizardState.CylinderLyout(11, 360d, -30d),
                                                new NewEngineWizardState.CylinderLyout(12, 420d, 30d)
                                            };
                                            #endregion
                                            break;

                                        case "65":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -32.5d),
                                                new NewEngineWizardState.CylinderLyout(2, 65d, 32.5d),
                                                new NewEngineWizardState.CylinderLyout(3, 480d, -32.5d),
                                                new NewEngineWizardState.CylinderLyout(4, 545d, 32.5d),
                                                new NewEngineWizardState.CylinderLyout(5, 240d, -32.5d),
                                                new NewEngineWizardState.CylinderLyout(6, 305d, 32.5d),
                                                new NewEngineWizardState.CylinderLyout(7, 600d, -32.5d),
                                                new NewEngineWizardState.CylinderLyout(8, 665d, 32.5d),
                                                new NewEngineWizardState.CylinderLyout(9, 120d, -32.5d),
                                                new NewEngineWizardState.CylinderLyout(10, 185d, 32.5d),
                                                new NewEngineWizardState.CylinderLyout(11, 360d, -32.5d),
                                                new NewEngineWizardState.CylinderLyout(12, 425d, 32.5d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;

                                case "16":
                                    #region
                                    switch (_strings[4]) //45, 135
                                    {
                                        //16-4-15-3-10-6-9-2-13
                                        case "45":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(2, 405d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(3, 180d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(4, 585d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(5, 90d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(6, 495d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(7, 270d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(8, 675d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(9, 630d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(10, 315d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(11, 450d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(12, 135d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(13, 540d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(14, 225d, 22.5d),
                                                new NewEngineWizardState.CylinderLyout(15, 360d, -22.5d),
                                                new NewEngineWizardState.CylinderLyout(16, 45d, 22.5d)
                                            };
                                            #endregion
                                            break;

                                        case "135":
                                            #region
                                            ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                            {
                                                new NewEngineWizardState.CylinderLyout(1, 0d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(2, 495d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(3, 180d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(4, 675d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(5, 90d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(6, 585d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(7, 270d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(8, 45d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(9, 630d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(10, 405d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(11, 450d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(12, 225d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(13, 540d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(14, 315d, 67.5d),
                                                new NewEngineWizardState.CylinderLyout(15, 360d, -67.5d),
                                                new NewEngineWizardState.CylinderLyout(16, 135d, 67.5d)
                                            };
                                            #endregion
                                            break;


                                        default:
                                            throw new NotSupportedException();
                                    }
                                    #endregion
                                    break;


                                default:
                                    throw new NotSupportedException();
                            }
                            #endregion
                            break;

                        case "Boxer":
                            #region
                            switch (_strings[3]) //Number
                            {
                                case "2":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -90d),
                                        new NewEngineWizardState.CylinderLyout(2, 360d, 90d)
                                    };
                                    #endregion
                                    break;

                                case "4":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -90d),
                                        new NewEngineWizardState.CylinderLyout(2, 540d, 90d),
                                        new NewEngineWizardState.CylinderLyout(3, 360d, -90d),
                                        new NewEngineWizardState.CylinderLyout(4, 180d, 90d)
                                    };
                                    #endregion
                                    break;

                                case "6":
                                    #region
                                    ((NewEngineWizardState)base.State).CylinderLyouts = new NewEngineWizardState.CylinderLyout[]
                                    {
                                        new NewEngineWizardState.CylinderLyout(1, 0d, -90d),
                                        new NewEngineWizardState.CylinderLyout(2, 360d, 90d),
                                        new NewEngineWizardState.CylinderLyout(3, 120d, -90d),
                                        new NewEngineWizardState.CylinderLyout(4, 480d, 90d),
                                        new NewEngineWizardState.CylinderLyout(5, 240d, -90d),
                                        new NewEngineWizardState.CylinderLyout(6, 600d, 90d)
                                    };
                                    #endregion
                                    break;


                                default:
                                    throw new NotSupportedException();
                            }
                            #endregion
                            break;


                        default:
                            throw new NotSupportedException();
                    }
                    #endregion
                    break;


                default:
                    throw new NotSupportedException();
            }


            ((NewEngineWizardState)base.State).SelectedEngineLayoutNode = this.treeView1.SelectedNode.Name;

            //to je samo za opis potem
            ((NewEngineWizardState)base.State).EngineType = _strings[2];

            if (_strings[1] == "TwoStroke")
            {
                if ((_strings[2] == "Inline")
                    && (_strings[3] == "6")
                    && (_strings[4] == "DoubleFiring"))
                {
                    ((NewEngineWizardState)base.State).EngineParticularity = "Double firing";
                }
                else if ((_strings[2] == "Boxer")
                    && ((_strings[3] == "2") || (_strings[3] == "4"))
                    && (_strings[4] == "DoubleFiring"))
                {
                    ((NewEngineWizardState)base.State).EngineParticularity = "Double firing";
                }
            }
            else if (_strings[1] == "FourStroke")
            {
                if ((_strings[2] == "Inline")
                    && (_strings[3] == "4")
                    && (_strings[4] == "CrossPlane"))
                {
                    ((NewEngineWizardState)base.State).EngineParticularity = "Cross plane";
                }
                else if ((_strings[2] == "Vee")
                    && (_strings[3] == "8"))
                {
                    if (_strings[5] == "FlatPlane")
                    {
                        ((NewEngineWizardState)base.State).EngineParticularity = "Flat plane";
                    }
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

    }
}
