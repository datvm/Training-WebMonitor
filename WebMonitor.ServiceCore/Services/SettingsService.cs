using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebMonitor.ServiceCore.Models;

namespace WebMonitor.ServiceCore.Services
{

    public interface ISettingsService
    {
        ServiceSettings Get();
    }

    public class SettingsService : ISettingsService
    {
        const string FilePath = "appsettings.json";

        public ServiceSettings Get()
        {
            return JsonConvert.DeserializeObject<ServiceSettings>(
                File.ReadAllText(FilePath));
        }

    }

}
