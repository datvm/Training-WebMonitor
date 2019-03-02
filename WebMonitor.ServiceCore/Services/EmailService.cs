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
        Task SendEmailAsync(string from, string[] to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        
        public async Task SendEmailAsync(string from, string[] to, string subject, string html)
        {
            var request = WebRequest.CreateHttp("https://api.mailjet.com/v3.1/send");
            request.Method = "POST";
            request.ContentType = "application/json";

            var username = "2414c889fc40419c209edfe5c158e335";
            var password = "bd6e704c1b9ee8c975c1b821bd104db4";
            var authKey = Convert.ToBase64String(
                Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + authKey);

            using (var requestBody = await request.GetRequestStreamAsync())
            {
                using (var streamWriter = new StreamWriter(requestBody))
                {
                    await streamWriter.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        Messages = new object[] {
                            new {
                                From = new
                                {
                                    Email = from,
                                    Name = from,
                                },
                                To = to.Select(q => new
                                {
                                    Email = q,
                                    Name = q,
                                }).ToList(),
                                Subject = subject,
                                HtmlPart = html,
                            }
                        },
                    }));
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
