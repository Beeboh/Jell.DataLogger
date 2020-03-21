using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Gui.Parsers
{
    internal class DataParser
    {

        public LoggerInfo Parse(string datastring)
        {
            string RootTagStart = "<LoggerInfo>";
            string RootTagEnd = "</LoggerInfo>";
            
            string XMLdatastring = RootTagStart + datastring + RootTagEnd;
            XElement doc = XElement.Parse(XMLdatastring);

            //fix
            //List<XElement> BatteryVoltages = doc.Elements("BatteryVoltage").ToList();
            //List<XElement> BatteryPercentages = doc.Elements("BatteryPercent").ToList();
            
            double batteryvoltage = Convert.ToDouble(doc.Element("BatteryVoltage").Value);
            double batterypercentage = Convert.ToDouble(doc.Element("BatteryPercent").Value);

            IEnumerable<XElement> datapoints = doc.Elements("Data");
            List<ParData> ParDataPoints = new List<ParData>();
            foreach (XElement datapoint in datapoints)
            {
                int year = Convert.ToInt32(datapoint.Attribute("year").Value);
                int month = Convert.ToInt32(datapoint.Attribute("month").Value);
                int day = Convert.ToInt32(datapoint.Attribute("day").Value);
                int hour = Convert.ToInt32(datapoint.Attribute("hour").Value);
                int minute = Convert.ToInt32(datapoint.Attribute("minute").Value);
                int second = Convert.ToInt32(datapoint.Attribute("second").Value);
                DateTime time = new DateTime(year, month, day, hour, minute, second);

                int adc1 = Convert.ToInt32(datapoint.Element("ADC1").Value);
                int adc2 = Convert.ToInt32(datapoint.Element("ADC2").Value);
                int adc3 = Convert.ToInt32(datapoint.Element("ADC3").Value);
                int adc4 = Convert.ToInt32(datapoint.Element("ADC4").Value);
                int adc5 = Convert.ToInt32(datapoint.Element("ADC5").Value);
                int adc6 = Convert.ToInt32(datapoint.Element("ADC6").Value);
                SensorRecording sensor1 = new SensorRecording(adc1);
                SensorRecording sensor2 = new SensorRecording(adc2);
                SensorRecording sensor3 = new SensorRecording(adc3);
                SensorRecording sensor4 = new SensorRecording(adc4);
                SensorRecording sensor5 = new SensorRecording(adc5);
                SensorRecording sensor6 = new SensorRecording(adc6);
                ParData ParDataPoint = new ParData(time, sensor1, sensor2, sensor3, sensor4, sensor5, sensor6);
                ParDataPoints.Add(ParDataPoint);
            }

            return new LoggerInfo(ParDataPoints, batteryvoltage, batterypercentage);
        }
    }
}
