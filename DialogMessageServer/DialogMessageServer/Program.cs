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
            System.Threading.Thread.Sleep(800);
            PortConfigClass callServer = new PortConfigClass();
            String portName = callServer.dialogServer();
            Application.Run(new DialogSplashWindow());
            if (portName != "") { Application.Run(new DialogNotificationWindow(portName)); }
            else { Application.Run(new DialogNotificationWindow("")); }
        }

    }
}
