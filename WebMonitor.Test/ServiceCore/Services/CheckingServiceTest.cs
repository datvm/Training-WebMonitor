using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMonitor.ServiceCore.Services;
using Xunit;

namespace WebMonitor.Test.ServiceCore.Services
{

    public class CheckingServiceTest
    {

        ICheckingService checkingService;

        public CheckingServiceTest()
        {
            this.checkingService = new CheckingService();
        }

        [Theory]
        [InlineData("https://www.google.com/")]
        [InlineData("https://www.facebook.com/")]
        [InlineData("https://httpstat.us/200")]
        public async Task EnsureOk_OkUrl(string url)
        {
            var result = await this.checkingService.EnsureOkAsync(url);
            Assert.True(result);
        }

        [Theory]
        [InlineData("https://httpstat.us/404")]
        [InlineData("https://httpstat.us/500")]
        public async Task EnsureOk_NotOkUrl(string url)
        {
            var result = await this.checkingService.EnsureOkAsync(url);
            Assert.False(result);
        }

    }

}
