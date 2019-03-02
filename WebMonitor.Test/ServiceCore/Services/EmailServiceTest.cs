using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMonitor.ServiceCore.Services;
using WebMonitor.Test.Common;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace WebMonitor.Test.ServiceCore.Services
{
    public class EmailServiceTest
    {

        IEmailService emailService;

        public EmailServiceTest()
        {
            var serviceProvider = CommonUtils.GetServiceProviderWithRealEmail();

            this.emailService = serviceProvider.GetService<IEmailService>();
        }

        [Fact]
        public async Task SendEmailAsyncTest()
        {
            await this.emailService.SendEmailAsync(
                "Testing Email",
                "Email content <p style='red'>Abc</p>");
        }

    }
}
