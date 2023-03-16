using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PylonC.NET;

namespace PU1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "1000" /*ms*/);
                Pylon.Initialize();

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                frmLoad OWindow = new frmLoad();
                if (OWindow.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }

            
              //  Application.Run(new frmMain());
            }
            catch (Exception Ex)
            { 
                CError.Throw(Ex);
            }
        }
    }
}