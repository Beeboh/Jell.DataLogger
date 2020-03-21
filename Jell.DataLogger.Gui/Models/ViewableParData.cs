using System;

namespace Jell.DataLogger.Gui.Models
{
    public class ViewableParData
    {
        public ViewableParData(DateTime time, ViewableSensorRecording sensor1, ViewableSensorRecording sensor2, ViewableSensorRecording sensor3, ViewableSensorRecording sensor4, ViewableSensorRecording sensor5, ViewableSensorRecording sensor6)
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
        public ViewableSensorRecording Sensor1 { get; }
        public ViewableSensorRecording Sensor2 { get; }
        public ViewableSensorRecording Sensor3 { get; }
        public ViewableSensorRecording Sensor4 { get; }
        public ViewableSensorRecording Sensor5 { get; }
        public ViewableSensorRecording Sensor6 { get; }
    }
}
