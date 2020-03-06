using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Jell.DataLogger.Gui.Services
{
    public class DataParser
    {
        public DataParser()
        {

        }
        public ReadOnlyCollection<ParData> Parse(string datastring)
        {
            string RootTagStart = "<DataPoints>";
            string RootTagEnd = "</DataPoints>";
            
            string XMLdatastring = RootTagStart + datastring + RootTagEnd;
            XElement doc = XElement.Parse(XMLdatastring);
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
            return ParDataPoints.AsReadOnly();
        }
    }
}
