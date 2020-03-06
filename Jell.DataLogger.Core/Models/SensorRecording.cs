using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Core.Models
{
    public class SensorRecording
    {
        public SensorRecording(int ADCvalue)
        {
            Voltage = ADCVoltageConverter.ADCtoV(ADCvalue);
            ParValue = VoltagePARConverter.VtoPAR(Voltage);
        }
        public double Voltage { get; }
        public double ParValue { get; }
        
        private static class VoltagePARConverter
        {
            public static double mVtoPAR(double mV)
            {
                return mV * 5;
            }
            public static double VtoPAR(double V)
            {
                return V * 5000;
            }
            public static double PARtoV(double PAR)
            {
                return PAR / 5000;
            }
            public static double PARtomV(double PAR)
            {
                return PAR / 5;
            }
            
        }
        private static class ADCVoltageConverter
        {
            public static double ADCtoV(int ADCvalue)
            {
                return ADCvalue / 4095;
            }
            public static double ADCtoMV(int ADCvalue)
            {
                return ADCvalue / 4095 * 1000;
            }
        }
        

    }
}
