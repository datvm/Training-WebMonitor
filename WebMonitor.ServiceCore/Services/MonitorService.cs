using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebMonitor.Entities;

namespace WebMonitor.ServiceCore.Services
{

    public interface IMonitorService
    {
        Task<int> PerformMonitorAsync();
    }

    public class MonitorService : IMonitorService
    {
        const string EmailSubject = "Website {0} down";
        const string EmailContent = "<p>Website {0} ({1}) down</p>";

        ICheckingService checkingService;
        IEmailService emailService;
        ISettingsService settingsService;
        public MonitorService(
            ICheckingService checkingService,
            IEmailService emailService,
            ISettingsService settingsService)
        {
            this.checkingService = checkingService;
            this.emailService = emailService;
            this.settingsService = settingsService;
        }

        public async Task<int> PerformMonitorAsync()
        {
            var settings = this.settingsService.Get();
            var checkList = settings.CheckList;

            foreach (var checkListItem in checkList)
            {
                try
                {
                    var ok = await this.checkingService.EnsureOkAsync(checkListItem.Url);

                    Trace.WriteLine($"{checkListItem.Name}: {ok}");

                    if (!ok)
                    {
                        await this.emailService.SendEmailAsync(
                            string.Format(EmailSubject, checkListItem.Name, checkListItem.Url),
                            string.Format(EmailContent, checkListItem.Name, checkListItem.Url));
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.ToString());
                }
            }

            return settings.CheckingInterval;
        }
    }

}
