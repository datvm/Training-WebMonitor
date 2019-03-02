using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebMonitor.ServiceCore.Models;
using WebMonitor.ServiceCore.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {

        public static IServiceCollection AddWebMonitorServices(this IServiceCollection services)
        {
            services.AddSingleton<ISettingsService, SettingsService>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICheckingService, CheckingService>();
            services.AddScoped<IMonitorService, MonitorService>();

            return services;
        }

    }
}
