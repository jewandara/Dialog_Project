using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DIALOGGSM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DialogMainForm("C8D918DB-296E-4105-A652-87A0B4BA3A65", "94773632687"));
            //Application.Run(new DialogSplashForm());
            //Application.Run(new DialogLoginForm());

        }
    }
}
