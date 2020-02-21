﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Testing
{
    /// <summary>
    /// Generates random par data based on the given input parameters. The voltages range 0 and 5V and the PAR values range from 0 to 100
    /// </summary>
    public class ParDataGenerator
    {
        private Random Random { get; }
        public ParDataGenerator()
        {
            Random = new Random();
        }
        public ReadOnlyCollection<ParData> Generate(DateTime starttime, int numberofpoints, int secondsinterval)
        {
            List<ParData> DataCollection = new List<ParData>();
            for (int i = 0; i<numberofpoints; i++)
            {
                SensorRecording sensor1 = new SensorRecording(Random.Next(0, 5), Random.Next(0, 100));
                SensorRecording sensor2 = new SensorRecording(Random.Next(0, 5), Random.Next(0, 100));
                SensorRecording sensor3 = new SensorRecording(Random.Next(0, 5), Random.Next(0, 100));
                SensorRecording sensor4 = new SensorRecording(Random.Next(0, 5), Random.Next(0, 100));
                SensorRecording sensor5 = new SensorRecording(Random.Next(0, 5), Random.Next(0, 100));
                SensorRecording sensor6 = new SensorRecording(Random.Next(0, 5), Random.Next(0, 100));
                DateTime time = starttime.AddSeconds(i * secondsinterval);
                ParData Data = new ParData(time, sensor1, sensor2, sensor3, sensor4, sensor5, sensor6);
                DataCollection.Add(Data);
            }
            return DataCollection.AsReadOnly();
        }
    }
}
