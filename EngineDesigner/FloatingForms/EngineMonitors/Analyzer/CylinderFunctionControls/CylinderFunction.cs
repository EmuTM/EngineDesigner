using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using EngineDesigner.Machine;

namespace EngineDesigner.FloatingForms.EngineMonitors.Analyzer
{
    internal partial class CylinderFunction : UserControl
    {
        public event EventHandler<FunctionEventArgs> FunctionChanged;
        public event EventHandler<CylinderEventArgs> CylinderChanged;
        public event EventHandler<HarmonicOrderEventArgs> HarmonicOrderChanged;



        private PositionedCylinder[] availablePositionedCylinders;
        [DefaultValue(null)]
        [Browsable(false)]
        public PositionedCylinder[] AvailablePositionedCylinders
        {
            get { return availablePositionedCylinders; }

            set
            {
                availablePositionedCylinders = value;


                this.comboBox_Cylinder.Items.Clear();
                if ((this.availablePositionedCylinders != null)
                    && (this.availablePositionedCylinders.Length > 0))
                {
                    this.label_Cylinder.Enabled = true;
                    this.comboBox_Cylinder.Enabled = true;

                    this.comboBox_Cylinder.Items.AddRange(this.availablePositionedCylinders);
                    this.comboBox_Cylinder.SelectedItem = this.comboBox_Cylinder.Items[0];
                }
                else
                {
                    this.label_Cylinder.Enabled = false;
                    this.comboBox_Cylinder.Enabled = false;
                }
            }
        }

        private FunctionInfoBase[] availableFunctions;
        [DefaultValue(null)]
        [Browsable(false)]
        public FunctionInfoBase[] AvailableFunctions
        {
            get { return availableFunctions; }

            set
            {
                availableFunctions = value;


                this.comboBox_Function.Items.Clear();
                if ((this.availableFunctions != null)
                    && (this.availableFunctions.Length > 0))
                {
                    this.label_Function.Enabled = true;
                    this.comboBox_Function.Enabled = true;

                    this.comboBox_Function.Items.AddRange(this.availableFunctions);
                    this.comboBox_Function.SelectedItem = this.comboBox_Function.Items[0];
                }
                else
                {
                    this.label_Function.Enabled = false;
                    this.comboBox_Function.Enabled = false;
                }
            }
        }

        private HarmonicOrderInfo[] availableHarmonicOrders;
        [DefaultValue(null)]
        [Browsable(false)]
        public HarmonicOrderInfo[] AvailableHarmonicOrders
        {
            get { return availableHarmonicOrders; }

            set
            {
                availableHarmonicOrders = value;


                this.comboBox_HarmonicOrder.Items.Clear();
                if ((this.availableHarmonicOrders != null)
                    && (this.availableHarmonicOrders.Length > 0))
                {
                    this.label_HarmonicOrder.Enabled = true;
                    this.comboBox_HarmonicOrder.Enabled = true;

                    this.comboBox_HarmonicOrder.Items.AddRange(this.availableHarmonicOrders);
                    this.comboBox_HarmonicOrder.SelectedItem = this.comboBox_HarmonicOrder.Items[0];
                }
                else
                {
                    this.label_HarmonicOrder.Enabled = false;
                    this.comboBox_HarmonicOrder.Enabled = false;
                }
            }
        }

        [DefaultValue(null)]
        [Browsable(false)]
        public FunctionInfoBase SelectedFunction
        {
            get { return (FunctionInfoBase)this.comboBox_Function.SelectedItem; }
        }
        [DefaultValue(null)]
        [Browsable(false)]
        public PositionedCylinder SelectedPositionedCylinder
        {
            get { return (PositionedCylinder)this.comboBox_Cylinder.SelectedItem; }
        }
        [DefaultValue(null)]
        [Browsable(false)]
        public HarmonicOrderInfo SelectedHarmonicOrder
        {
            get { return (HarmonicOrderInfo)this.comboBox_HarmonicOrder.SelectedItem; }
        }



        public CylinderFunction()
            : this(null, null, null)
        {
        }
        public CylinderFunction(FunctionInfoBase[] _availableFunctions, PositionedCylinder[] _availablePositionedCylinders, HarmonicOrderInfo[] _availableHarmonicOrders)
        {
            InitializeComponent();


            this.availableFunctions = _availableFunctions;
            this.availablePositionedCylinders = _availablePositionedCylinders;
            this.availableHarmonicOrders = _availableHarmonicOrders;
        }



        public void EnableCylinder()
        {
            this.comboBox_Cylinder.Enabled = true;
        }
        public void DisableCylinder()
        {
            this.comboBox_Cylinder.Enabled = false;
        }
        public void DeselectCylinder()
        {
            this.comboBox_Cylinder.SelectedIndex = -1;
            this.comboBox_Cylinder.SelectedItem = null;
            this.comboBox_Cylinder.SelectedValue = null;
        }
        public void ReselectCylinder()
        {
            if (this.comboBox_Cylinder.Items.Count > 0)
            {
                this.comboBox_Cylinder.SelectedIndex = 0;
                this.comboBox_Cylinder.SelectedItem = this.comboBox_Cylinder.Items[0];
                this.comboBox_Cylinder.SelectedValue = this.comboBox_Cylinder.Items[0];
            }
        }
        public void EnableFunction()
        {
            this.comboBox_Function.Enabled = true;
        }
        public void DisableFunction()
        {
            this.comboBox_Function.Enabled = false;
        }
        public void DeselectFunction()
        {
            this.comboBox_Function.SelectedIndex = -1;
            this.comboBox_Function.SelectedItem = null;
            this.comboBox_Function.SelectedValue = null;
        }
        public void ReselectFunction()
        {
            if (this.comboBox_Function.Items.Count > 0)
            {
                this.comboBox_Function.SelectedIndex = 0;
                this.comboBox_Function.SelectedItem = this.comboBox_Function.Items[0];
                this.comboBox_Function.SelectedValue = this.comboBox_Function.Items[0];
            }
        }
        public void EnableHarmonicOrder()
        {
            this.comboBox_HarmonicOrder.Enabled = true;
        }
        public void DisableHarmonicOrder()
        {
            this.comboBox_HarmonicOrder.Enabled = false;
        }
        public void DeselectHarmonicOrder()
        {
            this.comboBox_HarmonicOrder.SelectedIndex = -1;
            this.comboBox_HarmonicOrder.SelectedItem = null;
            this.comboBox_HarmonicOrder.SelectedValue = null;
        }
        public void ReselectHarmonicOrder()
        {
            if (this.comboBox_HarmonicOrder.Items.Count > 0)
            {
                this.comboBox_HarmonicOrder.SelectedIndex = 0;
                this.comboBox_HarmonicOrder.SelectedItem = this.comboBox_HarmonicOrder.Items[0];
                this.comboBox_HarmonicOrder.SelectedValue = this.comboBox_HarmonicOrder.Items[0];
            }
        }



        private void comboBox_Function_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox = (ComboBox)sender;
            FunctionInfoBase _functionInfoBase = (FunctionInfoBase)_comboBox.SelectedItem;

            this.OnFunctionChanged(_functionInfoBase);
        }
        private void comboBox_Cylinder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox = (ComboBox)sender;
            Cylinder _cylinder = (Cylinder)_comboBox.SelectedItem;

            this.OnCylinderChanged(_cylinder);
        }
        private void comboBox_HarmonicOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox _comboBox = (ComboBox)sender;
            HarmonicOrderInfo _harmonicOrderInfo = (HarmonicOrderInfo)_comboBox.SelectedItem;

            this.OnHarmonicOrderChanged(_harmonicOrderInfo);
        }



        protected virtual void OnFunctionChanged(FunctionInfoBase _functionInfoBase)
        {
            if (this.FunctionChanged != null)
            {
                this.FunctionChanged(this,
                    new FunctionEventArgs(_functionInfoBase));
            }
        }
        protected virtual void OnCylinderChanged(Cylinder _cylinder)
        {
            if (this.CylinderChanged != null)
            {
                this.CylinderChanged(this,
                    new CylinderEventArgs(_cylinder));
            }
        }
        protected virtual void OnHarmonicOrderChanged(HarmonicOrderInfo _harmonicOrderInfo)
        {
            if (this.HarmonicOrderChanged != null)
            {
                this.HarmonicOrderChanged(this,
                    new HarmonicOrderEventArgs(_harmonicOrderInfo));
            }
        }

    }


    internal class CylinderEventArgs : EventArgs
    {
        public CylinderEventArgs(Cylinder _cylinder)
        {
            this.cylinder = _cylinder;
        }


        private Cylinder cylinder;
        public Cylinder @Cylinder
        {
            get { return cylinder; }
        }
    }


    internal class FunctionEventArgs : EventArgs
    {
        public FunctionEventArgs(FunctionInfoBase _functionInfoBase)
        {
            this.functionInfoBase = _functionInfoBase;
        }


        private FunctionInfoBase functionInfoBase;
        public FunctionInfoBase @FunctionInfoBase
        {
            get { return functionInfoBase; }
        }
    }


    internal class HarmonicOrderEventArgs : EventArgs
    {
        public HarmonicOrderEventArgs(HarmonicOrderInfo _harmonicOrderInfo)
        {
            this.harmonicOrderInfo = _harmonicOrderInfo;
        }


        private HarmonicOrderInfo harmonicOrderInfo;
        public HarmonicOrderInfo @HarmonicOrderInfo
        {
            get { return harmonicOrderInfo; }
        }
    }

}
