using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WebMonitor.Demo.Services;

namespace WebMonitor.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IBusinessService, BusinessService>();
            serviceCollection.AddTransient(provider => DateTime.Now.ToString());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("1st:");
            using (var serviceScope = serviceProvider.CreateScope())
            {
                DoSomething(serviceScope);
            }
            
            Task.Delay(1000).Wait();

            Console.WriteLine("2nd:");
            using (var serviceScope = serviceProvider.CreateScope())
            {
                DoSomething(serviceScope);
            }
        }

        private static void DoSomething(IServiceScope serviceScope)
        {
            for (int i = 0; i < 2; i++)
            {
                var service1 = serviceScope.ServiceProvider.GetService<ISingletonCounterService>();
                var service2 = serviceScope.ServiceProvider.GetService<IScopedCounterService>();
                var service3 = serviceScope.ServiceProvider.GetService<ITransientCounterService>();

                Console.WriteLine(service1.Next());
                Console.WriteLine(service2.Next());
                Console.WriteLine(service3.Next());
            }

            Console.WriteLine();
        }
    }
}
