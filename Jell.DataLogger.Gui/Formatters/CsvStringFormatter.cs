using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Gui.Models;

namespace Jell.DataLogger.Gui.Formatters
{
    internal class CsvStringFormatter
    {
        public CsvStringFormatter()
        {
            
        }
        public string GetString(IEnumerable<ViewableParData> ParData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Date,Time,S1,S2,S3,S4,S5,S6\n");
            foreach (ViewableParData ParDataPoint in ParData)
            {
                sb.Append($"{ParDataPoint.Time.ToString("yyyy-MM-dd")},");
                sb.Append($"{ParDataPoint.Time.ToString("HH:mm:ss")},");
                sb.Append($"{FormattedPar(ParDataPoint.Sensor1.ParValue)},");
                sb.Append($"{FormattedPar(ParDataPoint.Sensor2.ParValue)},");
                sb.Append($"{FormattedPar(ParDataPoint.Sensor3.ParValue)},");
                sb.Append($"{FormattedPar(ParDataPoint.Sensor4.ParValue)},");
                sb.Append($"{FormattedPar(ParDataPoint.Sensor5.ParValue)},");
                sb.Append($"{FormattedPar(ParDataPoint.Sensor6.ParValue)},");
                sb.Append("\n");
            }
            return sb.ToString();
        }
        private string FormattedPar(double? par)
        {
            if (par.HasValue)
            {
                return par.Value.ToString("0");
            }
            else
            {
                return "";
            }
        }
    }
}
