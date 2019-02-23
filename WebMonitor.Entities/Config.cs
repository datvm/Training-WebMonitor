using System;
using System.Collections.Generic;
using System.Text;

namespace WebMonitor.Entities
{

    public class Config
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }

}
