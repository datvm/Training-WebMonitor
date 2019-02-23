using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMonitor.ServiceCore.Services;
using Xunit;

namespace WebMonitor.Test.ServiceCore.Services
{
    public class EmailServiceTest
    {

        IEmailService emailService;

        public EmailServiceTest()
        {
            this.emailService = new EmailService();
        }

        [Fact]
        public async Task SendEmailAsyncTest()
        {
            await this.emailService.SendEmailAsync(
                "nazozotole@mail-hub.info",
                new string[] { "nazozotole@mail-hub.info", },
                "Testing Email",
                "Email content <p style='red'>Abc</p>");
        }

    }
}
