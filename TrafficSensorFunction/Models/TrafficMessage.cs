using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSensorFunctionApp.Models
{
    public class TrafficMessage
    {
        public string sensorId { get; set; }
        public string location { get; set; }
        public int sensorValue { get; set; }
        public string sensorType { get; set; }
    }

    public class PowerBiRequestPayload
    {
        public List<TrafficMessage> rows { get; set; } = new List<TrafficMessage>();
    }
}
