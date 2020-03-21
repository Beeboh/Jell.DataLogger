using System;

namespace Jell.DataLogger.Core.Models
{
    public class ParData
    {
        public ParData(DateTime time, SensorRecording sensor1, SensorRecording sensor2, SensorRecording sensor3, SensorRecording sensor4, SensorRecording sensor5, SensorRecording sensor6)
        {
            Time = time;
            Sensor1 = sensor1;
            Sensor2 = sensor2;
            Sensor3 = sensor3;
            Sensor4 = sensor4;
            Sensor5 = sensor5;
            Sensor6 = sensor6;
        }
        public DateTime Time { get; }
        public SensorRecording Sensor1 { get; }
        public SensorRecording Sensor2 { get; }
        public SensorRecording Sensor3 { get; }
        public SensorRecording Sensor4 { get; }
        public SensorRecording Sensor5 { get; }
        public SensorRecording Sensor6 { get; }

    }
}
