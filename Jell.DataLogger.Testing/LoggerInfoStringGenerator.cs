using System;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Testing
{
    public class LoggerInfoStringGenerator
    {
        private LoggerInfoGenerator DataGenerator { get; } = new LoggerInfoGenerator();
        private LoggerInfoFormatter StringFormatter { get; } = new LoggerInfoFormatter();
        public string GenerateDataString(DateTime starttime, int numberofpoints, int secondsinterval)
        {
            LoggerInfo loggerInfo = DataGenerator.Generate(starttime, numberofpoints, secondsinterval);
            string datastring = StringFormatter.GetString(loggerInfo);

            return datastring;
        }
    }
}
