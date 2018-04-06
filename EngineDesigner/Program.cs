using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EngineDesigner;

//conditional compilation symbols: //TEST_WINDOWS; //TEST_CONSOLES; SHOW_CONSOLE
namespace EngineDesigner
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


#if SHOW_CONSOLE || TEST_CONSOLE
            Program.SetConsoleWindowVisibility(true);
#else
            Program.SetConsoleWindowVisibility(false);
#endif


#if TEST_CONSOLES
            //CHOOSE CONSOLE HERE!

            EngineDesigner.TestConsoles.TestConsole_MathParser.Start();

            string _string = "---  Press any key to exit  ---";
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + _string.Length / 2) + "}", _string);
            Console.ReadKey(true);
#elif TEST_WINDOWS
            //CHOOSE WINDOW HERE!

            //Application.Run(new EngineDesigner.TestForms.CoordinateSystemFormD3D());
            //Application.Run(new EngineDesigner.TestForms.TestForm_AsyncFunctionComputing());
            //Application.Run(new EngineDesigner.TestForms.TestForm_Functions());
            //Application.Run(new EngineDesigner.TestForms.TestForm_IPart());
            //Application.Run(new EngineDesigner.TestForms.TestForm_Sound());
            //Application.Run(new EngineDesigner.TestForms.TestForm_ChartSerialization());
            Application.Run(new EngineDesigner.TestForms.Form1());
#else
            EngineDesigner.Environment.Main.Start<EngineDesigner.MainForms.Form_Main>();
#endif

        }



        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        private static void SetConsoleWindowVisibility(bool _visible)
        {
            IntPtr _intPtr = Program.GetConsoleWindow();

            if (_intPtr != IntPtr.Zero)
            {
                if (_visible)
                {
                    Program.ShowWindow(_intPtr, 1); // 1 = SW_SHOW
                }
                else
                {
                    Program.ShowWindow(_intPtr, 0); // 0 = SW_HIDE
                }
            }
        }

    }
}
