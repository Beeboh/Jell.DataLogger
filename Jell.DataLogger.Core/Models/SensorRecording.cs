using Jell.DataLogger.Shared.Utilities;

namespace Jell.DataLogger.Core.Models
{
    public class SensorRecording
    {
        public SensorRecording(int ADCvalue)
        {
            ADC = ADCvalue;
            Voltage = SensorConversion.ADCtoV(ADCvalue);
            ParValue = SensorConversion.VtoPAR(Voltage);
        }
        public int ADC { get; }
        public double Voltage { get; }
        public double ParValue { get; }
    }
}
