using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Shared.Utilities
{
    public static class SensorConversion
    {
        public static double ADCtoV(int ADCvalue)
        {
            return (double)ADCvalue / 4095;
        }
        public static double ADCtoMV(int ADCvalue)
        {
            return ADCtoV(ADCvalue) * 1000;
        }
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
}
