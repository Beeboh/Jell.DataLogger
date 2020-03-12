using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jell.DataLogger.Gui.Services
{
    public class ParameterStringFormatter
    {
        public string GetParameterString(DateTime starttime, DateTime endtime, int samplerate)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0010");
            sb.Append(starttime.ToString("MMddyyyyHHmmss"));
            sb.Append(endtime.ToString("MMddyyyyHHmmss"));
            sb.Append(samplerate.ToString().PadLeft(4, '0'));
            sb.Append(DateTime.Now.ToString("MMddyyyyHHmmss"));
            return sb.ToString();
        }
    }
}
