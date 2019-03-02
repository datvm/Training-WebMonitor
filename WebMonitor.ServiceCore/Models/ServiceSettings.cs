using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebMonitor.ServiceCore.Models
{

    public class ServiceSettings
    {

        public EmailSettings Email { get; set; }
        public CheckListSettings[] CheckList { get; set; }
        public int CheckingInterval { get; set; }

        public class CheckListSettings
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public class EmailSettings
        {
            public string MailjetUsername { get; set; }
            public string MailjetPassword { get; set; }
            public EmailInfo From { get; set; }
            public IEnumerable<EmailInfo> To { get; set; }
        }

    }

    public class EmailInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
