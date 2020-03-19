using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
