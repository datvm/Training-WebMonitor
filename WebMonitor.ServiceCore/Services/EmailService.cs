using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.ServiceCore.Services
{

    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string html);
    }

    public class EmailService : IEmailService
    {

        ISettingsService settingsService;
        public EmailService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }
        
        public async Task SendEmailAsync(string subject, string html)
        {
            var request = WebRequest.CreateHttp("https://api.mailjet.com/v3.1/send");
            request.Method = "POST";
            request.ContentType = "application/json";

            var settings = this.settingsService.Get();
            var emailSettings = settings.Email;

            var authKey = Convert.ToBase64String(
                Encoding.GetEncoding("ISO-8859-1").GetBytes(
                    emailSettings.MailjetUsername + ":" + emailSettings.MailjetPassword));
            request.Headers.Add("Authorization", "Basic " + authKey);

            using (var requestBody = await request.GetRequestStreamAsync())
            {
                using (var streamWriter = new StreamWriter(requestBody))
                {
                    var body = JsonConvert.SerializeObject(new
                    {
                        Messages = new object[] {
                            new {
                                emailSettings.From,
                                emailSettings.To,
                                Subject = subject,
                                HtmlPart = html,
                            }
                        },
                    });


                    await streamWriter.WriteAsync(body);
                }
            }

            try
            {
                using (var response = await request.GetResponseAsync()
                    as HttpWebResponse)
                {
#if DEBUG
                    using (var responseBody = response.GetResponseStream())
                    {
                        using (var streamReader = new StreamReader(responseBody))
                        {
                            var responseText = await streamReader.ReadToEndAsync();
                            Debugger.Break();
                        }
                    }
#endif
                }
            }
            catch (WebException ex)
            {
#if DEBUG
                using (var responseBody = ex.Response.GetResponseStream())
                {
                    using (var streamReader = new StreamReader(responseBody))
                    {
                        var responseText = await streamReader.ReadToEndAsync();
                        Debugger.Break();
                    }
                }
#endif
                throw;
            }

        }

    }
}
