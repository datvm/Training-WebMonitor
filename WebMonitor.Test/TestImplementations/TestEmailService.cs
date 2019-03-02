using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebMonitor.ServiceCore.Services;

namespace WebMonitor.Test.TestImplementations
{

    public class TestEmailService : IEmailService
    {
        public Task SendEmailAsync(string subject, string html)
        {
            Debug.WriteLine(subject + ": " + html);

            return Task.CompletedTask;
        }
    }

}
