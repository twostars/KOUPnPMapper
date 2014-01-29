using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace KOUPnPMapper
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        private static readonly string ServiceName = "KOUPnPMapperService";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Install on first run, if not already installed.
                if (!ServiceInstaller.IsInstalled(ServiceName))
                {
                    ServiceInstaller.InstallAndStartService(ServiceName);
                    MessageBox.Show("Installed & started " + ServiceName + " service.", 
                        "Service started.", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    RunService();
                }
                return;
            }

            // Attach console for stdout (since we're in a Windows app, not a console app)
            AttachConsole(ATTACH_PARENT_PROCESS);

            switch (args[0])
            {
                case "-i":
                case "-install":
                case "install":
                    ServiceInstaller.InstallAndStartService(ServiceName);
                    Console.WriteLine("Installed & started {0} service.", ServiceName);
                    break;

                case "-u":
                case "-uninstall":
                case "uninstall":
                    ServiceInstaller.UninstallService(ServiceName);
                    Console.WriteLine("Uninstalled service {0}.", ServiceName);
                    break;

                default:
                    throw new System.NotImplementedException();
            }
        }

        private static void RunService()
        {
            var ServicesToRun = new ServiceBase[] 
            { 
                new KOUPnPMapperService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
