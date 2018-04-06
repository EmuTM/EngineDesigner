using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EngineDesigner.Environment;
using EngineDesigner.Common.Definitions;
using EngineDesigner.Environment.Controls.Charting;

namespace EngineDesigner.Wizards.NewFunction
{
    internal partial class Form_NewFunctionWizard_AlterFunction : Form_NewFunctionWizardBase
    {
        public Form_NewFunctionWizard_AlterFunction()
            : this(null)
        {
        }
        public Form_NewFunctionWizard_AlterFunction(Form_WizardBase _previous)
            : base(_previous)
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            base.tableOfContents1.SelectedBookmark = base.tableOfContents1.Bookmarks[4];
            base.Finish = new Form_NewFunctionWizard_Finish(this);


            if (((NewFunctionWizardState)base.State).Function == null)
            {
                this.function = base.GenerateFunctionFromState((NewFunctionWizardState)base.State);
                this.inputFunctionChart1.DrawFunction(this.function);
            }
            else
            {
                this.inputFunctionChart1.DrawFunction(((NewFunctionWizardState)base.State).Function);
            }
        }


        protected override bool OnFinish()
        {
            ((NewFunctionWizardState)base.State).Function = this.function;

            return base.OnFinish();
        }



        private Function function = null;



        private void inputFunctionChart1_PointAddAllowed(object sender, PointAddAllowedEventArgs e)
        {
            if ((e.NewXY.X <= e.Function.GetMinX())
                || (e.NewXY.X >= e.Function.GetMaxX()))
            {
                e.AddAllowed = false;
            }
        }
        private void inputFunctionChart1_PointDeleteAllowed(object sender, PointDeleteAllowedEventArgs e)
        {
            if ((e.XY.X == e.Function.GetMinX())
                || (e.XY.X == e.Function.GetMaxX()))
            {
                e.DeleteAllowed = false;
            }
        }
        private void inputFunctionChart1_PointMoveAllowed(object sender, PointMoveAllowedEventArgs e)
        {
            if ((e.XY.X == e.Function.GetMinX())
                || (e.XY.X == e.Function.GetMaxX()))
            {
                e.MoveAllowedX = false;
            }
        }
        private void inputFunctionChart1_NewFunctionGenerated(object sender, FunctionGeneratedEventArgs e)
        {
            this.function = e.NewFunction;
        }


    }
}