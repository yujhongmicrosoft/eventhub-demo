using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSensorApp
{
    public class TrafficMessage
    {
        public string location { get; set; }
        public int averageSpeed { get; set; }
        public int numberofCars { get; set; }
    }

}
