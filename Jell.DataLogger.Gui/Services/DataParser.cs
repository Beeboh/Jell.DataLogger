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

                double voltage1 = Convert.ToInt32(datapoint.Element("Voltage1").Value);
                double voltage2 = Convert.ToInt32(datapoint.Element("Voltage2").Value);
                double voltage3 = Convert.ToInt32(datapoint.Element("Voltage3").Value);
                double voltage4 = Convert.ToInt32(datapoint.Element("Voltage4").Value);
                double voltage5 = Convert.ToInt32(datapoint.Element("Voltage5").Value);
                double voltage6 = Convert.ToInt32(datapoint.Element("Voltage6").Value);
                SensorRecording sensor1 = new SensorRecording(voltage1);
                SensorRecording sensor2 = new SensorRecording(voltage2);
                SensorRecording sensor3 = new SensorRecording(voltage3);
                SensorRecording sensor4 = new SensorRecording(voltage4);
                SensorRecording sensor5 = new SensorRecording(voltage5);
                SensorRecording sensor6 = new SensorRecording(voltage6);
                ParData ParDataPoint = new ParData(time, sensor1, sensor2, sensor3, sensor4, sensor5, sensor6);
                ParDataPoints.Add(ParDataPoint);
            }
            return ParDataPoints.AsReadOnly();
        }
    }
}
