﻿using System;
using System.Text;

namespace Jell.DataLogger.Gui.Formatters
{
    internal class ParameterStringFormatter
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
