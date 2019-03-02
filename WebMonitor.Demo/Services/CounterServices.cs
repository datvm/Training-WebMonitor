using System;
using System.Collections.Generic;
using System.Text;

namespace WebMonitor.Demo.Services
{
    public interface ISingletonCounterService
    {
        int Next();
    }

    public interface IScopedCounterService
    {
        int Next();
    }

    public interface ITransientCounterService
    {
        int Next();
    }

    public class CounterSerivce : ISingletonCounterService, IScopedCounterService, ITransientCounterService
    {

        private int counter;
        public CounterSerivce()
        {
            Console.WriteLine("Created new instance of CounterService.");
        }

        public int Next()
        {
            return ++this.counter;
        }
    }
}
