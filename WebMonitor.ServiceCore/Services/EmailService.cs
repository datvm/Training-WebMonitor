using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.ServiceCore.Services
{

    public interface IEmailService
    {
        Task SendEmailAsync(string from, string[] to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string from, string[] to, string subject, string html)
        {
            throw new NotImplementedException();
        }
    }
}
