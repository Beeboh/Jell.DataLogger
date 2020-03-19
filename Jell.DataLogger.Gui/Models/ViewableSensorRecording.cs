using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Shared.Utilities;

namespace Jell.DataLogger.Gui.Models
{
    public class ViewableSensorRecording
    {
        public ViewableSensorRecording(int? ADCvalue)
        {
            ADC = ADCvalue;
            if (ADCvalue.HasValue)
            {
                Voltage = SensorConversion.ADCtoV(ADCvalue.Value);
                ParValue = SensorConversion.VtoPAR(Voltage.Value);
            }
            else
            {
                Voltage = null;
                ParValue = null;
            }
            
        }
        public int? ADC { get; }
        public double? Voltage { get; }
        public double? ParValue { get; }
    }
}
