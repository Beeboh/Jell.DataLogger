using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Jell.DataLogger.Gui.ViewModels;

namespace Jell.DataLogger.Gui.Commands
{
    public class SwitchDataViewCommand : ICommand
    {
        private DataViewModel DataViewModel { get; }
        public SwitchDataViewCommand(DataViewModel dataviewmodel)
        {
            DataViewModel = dataviewmodel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.DataViewModel.SwitchView();
        }
    }
}
