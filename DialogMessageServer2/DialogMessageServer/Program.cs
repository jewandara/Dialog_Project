using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dialog.MessageServer;
using System.IO;

namespace DialogMessageServer
{
    static class Program
    {
        [STAThread]            

   
        static void Main()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("DialogCustomerMessageServer", Application.ExecutablePath.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DialogSplashWindow());

            PortConfigClass callServer = new PortConfigClass();
            if (callServer.dialogServer())
            {
                Application.Run(new DialogNotificationWindow(callServer._truePortName));
            }
            else
            {
                MessageBox.Show(callServer._PortMessage, "Message Server Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }

        }
    }
}
