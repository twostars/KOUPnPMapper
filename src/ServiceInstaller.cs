using System;
using System.ServiceProcess;
using System.Collections;
using System.Configuration.Install;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KOUPnPMapper
{
    public static class ServiceInstaller
    {
        public static bool IsInstalled(string serviceName)
        {
            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool IsRunning(string serviceName)
        {
            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                if (!IsInstalled(serviceName)) 
                    return false;

                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        private static AssemblyInstaller GetInstaller()
        {
            AssemblyInstaller installer = new AssemblyInstaller(
                System.Reflection.Assembly.GetExecutingAssembly(), null);
            installer.UseNewContext = true;
            return installer;
        }

        public static void InstallAndStartService(string serviceName)
        {
            InstallService(serviceName);
            StartService(serviceName);
        }

        private static void InstallService(string serviceName)
        {
            if (IsInstalled(serviceName)) 
                return;

            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void UninstallService(string serviceName)
        {
            if (!IsInstalled(serviceName))
                return;

            // Stop service in case it's running.
            StopService(serviceName);

            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Uninstall(state);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static void StartService(string serviceName)
        {
            if (!IsInstalled(serviceName)) 
                return;

            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public static void StopService(string serviceName)
        {
            if (!IsInstalled(serviceName)) 
                return;

            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped,
                             TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
