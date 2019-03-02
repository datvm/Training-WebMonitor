using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using WebMonitor.ServiceCore.Services;
using WebMonitor.Test.TestImplementations;

namespace WebMonitor.Test.Common
{

    internal static class CommonUtils
    {

        private static IServiceProvider serviceProvider;
        private static IServiceProvider serviceProviderWithRealEmail;

        static CommonUtils()
        {
            var services = new ServiceCollection();
            services.AddWebMonitorServices();

            serviceProviderWithRealEmail = services.BuildServiceProvider();

            services.RemoveAll<IEmailService>();
            services.AddSingleton<IEmailService, TestEmailService>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static IServiceProvider GetServiceProvider()
        {
            return serviceProvider.CreateScope().ServiceProvider;
        }

        public static IServiceProvider GetServiceProviderWithRealEmail()
        {
            return serviceProviderWithRealEmail.CreateScope().ServiceProvider;
        }

    }

}
