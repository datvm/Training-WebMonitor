using System;
using System.Collections.Generic;
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

        ICheckingService checkingService;
        IEmailService emailService;
        WebMonitorContext dbContext;
        public MonitorService(
            ICheckingService checkingService,
            IEmailService emailService,
            WebMonitorContext dbContext)
        {
            this.checkingService = checkingService;
            this.emailService = emailService;
            this.dbContext = dbContext;
        }

        public Task<int> PerformMonitorAsync()
        {
            throw new NotImplementedException();
        }
    }

}
