using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Jell.DataLogger.Gui.Commands;
using Jell.DataLogger.Core.Models;

namespace Jell.DataLogger.Gui.ViewModels
{
    public class DataViewModel
    {
        public object DataContext { get; private set; }
        public SwitchDataViewCommand SwitchDataViewCommand { get; private set; }
        public LoggerInfo LoggerInfo { get; }
        private DataTableViewModel DataTable { get; }
        private GraphViewModel Graph { get; }
        public DataViewModel(LoggerInfo loggerinfo)
        {
            SwitchDataViewCommand = new SwitchDataViewCommand(this);
            DataTable = new DataTableViewModel(loggerinfo);
            Graph = new GraphViewModel(loggerinfo);
            DataContext = DataTable;
        }
        public void SwitchView()
        {
            if (DataContext == Graph)
            {
                DataContext = DataTable;
            }
            else if(DataContext == DataTable)
            {
                DataContext = Graph;
            }
        }
    }
}
