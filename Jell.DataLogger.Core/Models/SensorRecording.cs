using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Core.Models
{
    public class SensorRecording
    {
        public SensorRecording(double voltage, double parvalue)
        {
            Voltage = voltage;
            ParValue = parvalue;
        }
        public double Voltage { get; }
        public double ParValue { get; }
    }
}
