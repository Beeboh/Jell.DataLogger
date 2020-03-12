using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Testing
{
    public class LoggerInfoGenerator
    {
        private ParDataGenerator DataGenerator { get; } = new ParDataGenerator();
        private LoggerInfoFormatter StringFormatter { get; } = new LoggerInfoFormatter();
        public string GenerateDataString(DateTime starttime, int numberofpoints, int secondsinterval)
        {
            LoggerInfo loggerInfo = DataGenerator.Generate(starttime, numberofpoints, secondsinterval);
            string datastring = StringFormatter.GetString(loggerInfo);

            return datastring;
        }
    }
}
