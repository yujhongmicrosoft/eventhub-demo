using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficManagerCli
{
    public class TrafficMessage
    {
        public string location { get; set; }
        public int sensorValue { get; set; }
        public string sensorId { get; set; }
        public string sensorType { get; set; }
    }
}
