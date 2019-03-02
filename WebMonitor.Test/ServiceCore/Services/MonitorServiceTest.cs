using System;
using System.Collections.Generic;
using System.Text;
using WebMonitor.ServiceCore.Services;
using WebMonitor.Test.Common;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;

namespace WebMonitor.Test.ServiceCore.Services
{
    public class MonitorServiceTest
    {

        private IMonitorService monitorService;

        public MonitorServiceTest()
        {
            var serviceProvider = CommonUtils.GetServiceProvider();
            this.monitorService = serviceProvider.GetService<IMonitorService>();
        }

        [Fact]
        public async Task PerformMonitorAsyncTest()
        {
            var delay = await this.monitorService.PerformMonitorAsync();

            Assert.Equal(5000, delay);
        }

    }
}
