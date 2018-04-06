using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;

namespace EngineDesigner.Environment
{
    public static class Main
    {
        private static List<Form_MainBase> activeForms = new List<Form_MainBase>();
        internal static ICollection<Form_MainBase> ActiveForms
        {
            get { return Main.activeForms; }
        }



        public static void Start<T>() where T : Form_MainBase, new()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Main.ObtainNewForm(new T());

            Application.Run();
        }



        internal static Form_MainBase ObtainNewForm(Form_MainBase _form_MainBase)
        {
            _form_MainBase.FormClosed
                += new FormClosedEventHandler(_form_MainBase_FormClosed);
            Main.activeForms.Add(_form_MainBase);
            _form_MainBase.Show();

            return _form_MainBase;
        }
        internal static void CloseAll()
        {
            List<Form_MainBase> _formsTmp = new List<Form_MainBase>();
            _formsTmp.AddRange(Main.activeForms);

            foreach (Form_MainBase _form_MainBase in _formsTmp)
            {
                _form_MainBase.Close();
            }
        }



        private static void _form_MainBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main.activeForms.Remove((Form_MainBase)sender);

            if (Main.activeForms.Count == 0)
            {
                Application.Exit();
            }
        }

    }
}
