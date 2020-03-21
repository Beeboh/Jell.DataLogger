using System;
using System.Collections.Generic;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Testing
{
    /// <summary>
    /// Generates random par data based on the given input parameters. The voltages range 0 and 5V and the PAR values range from 0 to 100
    /// </summary>
    public class LoggerInfoGenerator
    {
        private Random Random { get; } = new Random();

        public LoggerInfo Generate(DateTime starttime, int numberofpoints, int secondsinterval)
        {
            List<ParData> DataCollection = new List<ParData>();
            for (int i = 0; i<numberofpoints; i++)
            {
                SensorRecording sensor1 = new SensorRecording(Random.Next(0, 4095));
                SensorRecording sensor2 = new SensorRecording(4095);
                SensorRecording sensor3 = new SensorRecording(Random.Next(0, 4095));
                SensorRecording sensor4 = new SensorRecording(Random.Next(0, 4095));
                SensorRecording sensor5 = new SensorRecording(Random.Next(0, 4095));
                SensorRecording sensor6 = new SensorRecording(Random.Next(0, 4095));
                DateTime time = starttime.AddSeconds(i * secondsinterval);
                ParData Data = new ParData(time, sensor1, sensor2, sensor3, sensor4, sensor5, sensor6);
                DataCollection.Add(Data);
            }
            double batteryVoltage = Random.NextDouble() * 3.2;
            double batteryPercent = batteryVoltage / 3.2 * 100;
            LoggerInfo loggerInfo = new LoggerInfo(DataCollection, batteryVoltage, batteryPercent);
            return loggerInfo;
        }
    }
}
