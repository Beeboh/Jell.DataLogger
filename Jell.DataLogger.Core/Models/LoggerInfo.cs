using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Core.Models
{
    public class LoggerInfo
    {
        public LoggerInfo(IEnumerable<ParData> data, double batteryVoltage, double batteryPercentage)
        {
            ParData = new List<ParData>(data).AsReadOnly();
            BatteryVoltage = batteryVoltage;
            BatteryPercent = batteryPercentage;
        }
        public ReadOnlyCollection<ParData> ParData { get; }
        public double BatteryVoltage { get; }
        public double BatteryPercent { get; }
    }
}
