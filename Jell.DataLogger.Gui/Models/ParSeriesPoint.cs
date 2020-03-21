using System;

namespace Jell.DataLogger.Gui.Models
{
    public class ParSeriesPoint
    {
        public ParSeriesPoint(DateTime time, double par)
        {
            Time = time;
            ParValue = par;
        }
        public DateTime Time { get; }
        public double ParValue { get; }
    }
}
