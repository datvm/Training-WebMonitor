using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WebMonitor.ServiceCore.Services;

namespace WebMonitor.ServiceCore
{

    public class WebMonitorService : IDisposable
    {

        private CancellationTokenSource cts;
        
        public void Start()
        {
            this.cts = new CancellationTokenSource();
            Task.Run(DoServiceWork);
        }

        private async Task DoServiceWork()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    IMonitorService monitorService = null;

                    var delay = await monitorService.PerformMonitorAsync();
                    await Task.Delay(delay);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                    await Task.Delay(1000);
                }
            }
        }

        public void Dispose()
        {
            this.cts.Cancel();
        }
    }

}
