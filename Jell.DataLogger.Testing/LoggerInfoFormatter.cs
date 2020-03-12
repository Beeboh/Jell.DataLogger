using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Testing
{
    internal class LoggerInfoFormatter
    {
        public string GetString(LoggerInfo loggerinfo)
        {
            StringBuilder sb = new StringBuilder();
            foreach(ParData data in loggerinfo.ParData)
            {
                sb.Append($"<Data year='{data.Time.Year}' month='{data.Time.Month}' day='{data.Time.Day}' hour='{data.Time.Hour}' minute='{data.Time.Minute}' second='{data.Time.Second}'>");
                sb.Append($"<ADC1>{data.Sensor1.ADC}</ADC1>");
                sb.Append($"<ADC2>{data.Sensor2.ADC}</ADC2>");
                sb.Append($"<ADC3>{data.Sensor3.ADC}</ADC3>");
                sb.Append($"<ADC4>{data.Sensor4.ADC}</ADC4>");
                sb.Append($"<ADC5>{data.Sensor5.ADC}</ADC5>");
                sb.Append($"<ADC6>{data.Sensor6.ADC}</ADC6>");
                sb.Append($"</Data>");
            }
            sb.Append($"<BatteryVoltage>{loggerinfo.BatteryVoltage}</BatteryVoltage>");
            sb.Append($"<BatteryPercent>{loggerinfo.BatteryPercent}</BatteryPercent>");
            return sb.ToString();
        }
    }
}
