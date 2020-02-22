using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Jell.DataLogger.Gui.Windows
{
    /// <summary>
    /// Interaction logic for ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public bool WasCompleted { get; private set; }
        public string PortName { get; private set; }
        public ConnectionWindow()
        {
            InitializeComponent();
            WasCompleted = false;
        }
        

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            PortName = ddlPorts.SelectedValue as string;
            WasCompleted = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            WasCompleted = false;
            Close();
        }
    }
}
