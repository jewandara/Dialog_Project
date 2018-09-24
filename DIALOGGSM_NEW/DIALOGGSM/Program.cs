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

            //Application.Run(new DialogMainForm(7, 7, "AB76D528-D124-4196-8CE4-CDA5041402DE", "94773339445"));
            Application.Run(new DialogMainForm(8, 8, "4DFDE317-277D-473C-B24D-60A8B6A053B2", "94773632682"));

            //Application.Run(new DialogSplashForm());
            //Application.Run(new DialogLoginForm());
            //Application.Run(new Form2());
            //Application.Run(new DialogExcelForm());
            //Application.Run(new DialogExcelForm());
            //Application.Run(new DialogExcelForm());
            //Application.Run(new DialogUserPassWordForm("34215342563416"));

        }
    }
}
