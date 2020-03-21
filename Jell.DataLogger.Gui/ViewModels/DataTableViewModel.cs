using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jell.DataLogger.Gui.Models;
using Jell.DataLogger.Gui.Interfaces;

namespace Jell.DataLogger.Gui.ViewModels
{
    public class DataTableViewModel : IViewModel
    {
        public ReadOnlyCollection<ViewableParData> ParData { get; private set; }

        //Delete these if can't get data binding to work
        public string DateColumnHeader => "Date";
        public string TimeColumnHeader => "Time";
        public string Sensor1ColumnHeader => "S1";
        public string Sensor2ColumnHeader => "S2";
        public string Sensor3ColumnHeader => "S3";
        public string Sensor4ColumnHeader => "S4";
        public string Sensor5ColumnHeader => "S5";
        public string Sensor6ColumnHeader => "S6";

        public DataTableViewModel(IEnumerable<ViewableParData> data)
        {
            ParData = new List<ViewableParData>(data).AsReadOnly();
        }
    }
}
