using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.ServiceCore.Services
{

    public interface ICheckingService
    {
        Task<bool> EnsureOkAsync(string url);
    }

    public class CheckingService : ICheckingService
    {
        public async Task<bool> EnsureOkAsync(string url)
        {
            var webRequest = WebRequest.CreateHttp(url);

            try
            {
                using (var webResponse = await webRequest.GetResponseAsync()
                    as HttpWebResponse)
                {
                    return webResponse.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException ex)
            {
                Trace.TraceError(ex.ToString());
                return false;
            }
        }
    }
}
