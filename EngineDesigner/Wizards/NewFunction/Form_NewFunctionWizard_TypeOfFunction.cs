using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;

namespace EngineDesigner.Wizards.NewFunction
{
    internal partial class Form_NewFunctionWizard_TypeOfFunction : Form_NewFunctionWizardBase
    {
        public Form_NewFunctionWizard_TypeOfFunction()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_TypeOfFunction(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[1];

            base.Next = new Form_NewFunctionWizard_MinMaxValues_IndicatorDiagram_GasPressureVsFiringAngle(this);
            base.NextEnabled = false;

            //če hočemo, da je gumb finish viden, mora imet neko ciljno formo!
            base.Finish = new Form_NewFunctionWizard_Finish(this);
            //ga pa moramo disablat, dokler ni primeren
            base.FinishEnabled = false;


            //selektamo pravo
            if (!string.IsNullOrEmpty(((NewFunctionWizardState)base.State).SelectedFunctionTypeNode))
            {
                TreeNode[] _treeNodes = this.treeView1.Nodes.Find(((NewFunctionWizardState)base.State).SelectedFunctionTypeNode, true);
                this.treeView1.SelectedNode = _treeNodes[0];
            }
            else
            {
                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            }

            this.treeView1_AfterSelect(this.treeView1, new TreeViewEventArgs(this.treeView1.SelectedNode));


            //in expandamo
            this.treeView1.SelectedNode.Expand();
        }

        protected override bool OnNext()
        {
            this.SetFunctionTypeToState();

            return base.OnNext();
        }
        protected override bool OnFinish()
        {
            this.SetFunctionTypeToState();

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


        private void SetFunctionTypeToState()
        {
            string[] _strings = this.treeView1.SelectedNode.Name.Split('_');

            //_strings[1] = IndicatorDiagram
            //_strings[2] = GasPressureVsFiringAngle
            //_strings[3] = TwoStroke, FourStroke

            switch (_strings[1]) //IndicatorDiagram
            {
                case "IndicatorDiagram":
                    #region
                    switch (_strings[2]) //GasPressureVsFiringAngle
                    {
                        case "GasPressureVsFiringAngle":
                            #region
                            switch (_strings[3]) //TwoStroke, FourStroke
                            {
                                case "TwoStroke":
                                    #region
                                    ((NewFunctionWizardState)base.State).SelectedFunctionType = NewFunctionWizardState.FunctionType.IndicatorDiagram_GasPressureVsFiringAngle_TwoStroke;
                                    #endregion
                                    break;

                                case "FourStroke":
                                    #region
                                    ((NewFunctionWizardState)base.State).SelectedFunctionType = NewFunctionWizardState.FunctionType.IndicatorDiagram_GasPressureVsFiringAngle_FourStroke;
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


            ((NewFunctionWizardState)base.State).SelectedFunctionTypeNode = this.treeView1.SelectedNode.Name;


            //to je samo za opis potem
            switch (_strings[2]) //GasPressureVsFiringAngle
            {
                case "GasPressureVsFiringAngle":
                    ((NewFunctionWizardState)base.State).FunctionTypeName = "Gas pressure Vs. Firing angle";
                    break;


                default:
                    throw new NotSupportedException();
            }
        }

    }
}
