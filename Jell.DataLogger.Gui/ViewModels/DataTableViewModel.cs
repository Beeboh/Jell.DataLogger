using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Gui.ViewModels
{
    public class DataTableViewModel
    {
        public ReadOnlyCollection<ParData> ParData { get; private set; }

        //Delete these if can't get data binding to work
        public string DateColumnHeader => "Date";
        public string TimeColumnHeader => "Time";
        public string Sensor1ColumnHeader => "S1";
        public string Sensor2ColumnHeader => "S2";
        public string Sensor3ColumnHeader => "S3";
        public string Sensor4ColumnHeader => "S4";
        public string Sensor5ColumnHeader => "S5";
        public string Sensor6ColumnHeader => "S6";

        public DataTableViewModel(IEnumerable<ParData> pardata)
        {
            ParData = new List<ParData>(pardata).AsReadOnly();
        }
    }
}
