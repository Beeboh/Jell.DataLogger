using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Gui.Models;

namespace Jell.DataLogger.Gui.Adapters
{
    public class ViewableParAdapter
    {
        public ReadOnlyCollection<ViewableParData> GetViewableParDataList(IEnumerable<ParData> pardatalist)
        {
            List<ViewableParData> ViewableParDataList = new List<ViewableParData>();
            foreach(ParData pardata in pardatalist)
            {
                ViewableParData ViewableParData = GetViewableParData(pardata);
                ViewableParDataList.Add(ViewableParData);
            }
            return ViewableParDataList.AsReadOnly();
        }
        public ViewableParData GetViewableParData(ParData pardata)
        {
            ViewableSensorRecording sensor1 = GetViewableSensorRecording(pardata.Sensor1);
            ViewableSensorRecording sensor2 = GetViewableSensorRecording(pardata.Sensor2);
            ViewableSensorRecording sensor3 = GetViewableSensorRecording(pardata.Sensor3);
            ViewableSensorRecording sensor4 = GetViewableSensorRecording(pardata.Sensor4);
            ViewableSensorRecording sensor5 = GetViewableSensorRecording(pardata.Sensor5);
            ViewableSensorRecording sensor6 = GetViewableSensorRecording(pardata.Sensor6);
            return new ViewableParData(pardata.Time, sensor1, sensor2, sensor3, sensor4, sensor5, sensor6);
        }
        public ViewableSensorRecording GetViewableSensorRecording(SensorRecording sensorRecording)
        {
            if (ValidSensorRecording(sensorRecording))
            {
                return new ViewableSensorRecording(sensorRecording.ADC);
            }
            else return new ViewableSensorRecording(null);
        }
        private bool ValidSensorRecording(SensorRecording sensorRecording)
        {
            return sensorRecording.ParValue <= 4999;
        }
    }
}
