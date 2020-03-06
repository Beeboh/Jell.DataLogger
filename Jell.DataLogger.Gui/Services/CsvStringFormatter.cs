using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Gui.Services
{
    internal class CsvStringFormatter
    {
        public CsvStringFormatter()
        {
            
        }
        public string GetString(IEnumerable<ParData> ParData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Date,Time,S1,S2,S3,S4,S5,S6\n");
            foreach (ParData ParDataPoint in ParData)
            {
                sb.Append($"{ParDataPoint.Time.ToString("yyyy-MM-dd")},");
                sb.Append($"{ParDataPoint.Time.ToString("HH:mm:ss")},");
                sb.Append($"{ParDataPoint.Sensor1.ParValue.ToString("0")},");
                sb.Append($"{ParDataPoint.Sensor2.ParValue.ToString("0")},");
                sb.Append($"{ParDataPoint.Sensor3.ParValue.ToString("0")},");
                sb.Append($"{ParDataPoint.Sensor4.ParValue.ToString("0")},");
                sb.Append($"{ParDataPoint.Sensor5.ParValue.ToString("0")},");
                sb.Append($"{ParDataPoint.Sensor6.ParValue.ToString("0")}");
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
