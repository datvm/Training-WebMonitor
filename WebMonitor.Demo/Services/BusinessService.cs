using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.Demo.Services
{

    public interface IBusinessService
    {

        int GetRandom(int min, int max);
        DateTime GetOneMoreDay();
        Task UploadFileAndCleanup();
        string GetMagicString();

    }

    public class BusinessService : IBusinessService
    {

        string magicString;
        public BusinessService(string magicString)
        {
            this.magicString = magicString;
        }

        public string GetMagicString()
        {
            return this.magicString;
        }

        public DateTime GetOneMoreDay()
        {
            return DateTime.Now.AddDays(1);
        }

        public int GetRandom(int min, int max)
        {
            return new Random().Next(min, max);
        }

        public async Task UploadFileAndCleanup()
        {
            var tempFile = Path.GetTempFileName();

            using (var stream = File.Create(tempFile))
            {
                // Assume upload
                await Task.Delay(10);
            }

            File.Delete(tempFile);
        }
    }
}
