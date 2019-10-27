using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.Model
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public string Queue { get; set; }
    }
}
