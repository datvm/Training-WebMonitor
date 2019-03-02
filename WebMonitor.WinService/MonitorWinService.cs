using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebMonitor.ServiceCore.Services;

namespace WebMonitor.WinService
{
    public partial class MonitorWinService : ServiceBase
    {
        private CancellationTokenSource cts;
        private Stream traceStream;
        private TraceListener customTraceListener;

        public MonitorWinService()
        {
            this.traceStream = File.Open(@"log.log", FileMode.Append);
            this.customTraceListener = new TextWriterTraceListener(this.traceStream);

            Trace.Listeners.Add(this.customTraceListener);

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                var services = new ServiceCollection();
                services.AddWebMonitorServices();

                var provider = services.BuildServiceProvider();

                while (!this.cts.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = provider.CreateScope())
                        {
                            var monitorService = scope.ServiceProvider.GetService<IMonitorService>();
                            var delay = await monitorService.PerformMonitorAsync();

                            await Task.Delay(delay);
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.ToString());

                        await Task.Delay(5000);
                    }
                }
            });
        }

        protected override void OnStop()
        {
            this.cts.Cancel();

            this.customTraceListener.Dispose();
            this.traceStream.Dispose();
        }

        [Conditional("DEBUG")]
        public void TestOnStart(string[] args)
        {
            this.OnStart(args);
        }

        [Conditional("DEBUG")]
        public void TestOnStop()
        {
            this.OnStop();
        }

    }
}
