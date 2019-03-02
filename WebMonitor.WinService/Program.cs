using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            var assemblyLocation = typeof(Program).Assembly.Location;
            var assemblyFolder = Path.GetDirectoryName(assemblyLocation);

            Environment.CurrentDirectory = assemblyFolder;

#if DEBUG
            var service = new MonitorWinService();

            service.TestOnStart(args);

            Console.WriteLine("Service started. Press ENTER to stop...");
            Console.ReadLine();

            Console.WriteLine("Stopping service...");
            service.TestOnStop();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MonitorWinService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
            
        }
    }
}
