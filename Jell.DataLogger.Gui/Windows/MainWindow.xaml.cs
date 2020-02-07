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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _connectedmode;
        private bool ConnectedMode
        {
            get { return _connectedmode; }
            set
            {
                foreach (MenuItem connectedmenuitem in ConnectedMenuItems)
                {
                    connectedmenuitem.IsEnabled = value;
                }
                foreach(MenuItem disconnectedmenuitem in DisconnectedMenuItems)
                {
                    disconnectedmenuitem.IsEnabled = !value;
                }
                _connectedmode = value;
            }
        }
        private MenuItem[] ConnectedMenuItems { get; }
        private MenuItem[] DisconnectedMenuItems { get; }
        //{
        //    get { return _connectedmode; }
        //    set
        //    {
        //        _connectedmode = value;
        //        OnPropertyChanged("ConnectedMode");
        //    }
        //}

        public MainWindow()
        {
            InitializeComponent();
            ConnectedMenuItems = GetConnectedMenuItems();
            DisconnectedMenuItems = GetDisconnectedMenuItems();
            Disconnect();
        }
        private void Connect()
        {
            ConnectedMode = true;

        }
        private void Disconnect()
        {
            ConnectedMode = false;
        }
        private MenuItem[] GetConnectedMenuItems()
        {
            return new MenuItem[] { menuDisconnect, menuRecordNewData, menuExportToExcel };
        }
        private MenuItem[] GetDisconnectedMenuItems()
        {
            return new MenuItem[] { menuConnect };
        }


        //Buttons
        private void menuConnect_Click(object sender, RoutedEventArgs e)
        {
            Connect();
            
        }
        private void menuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectedMode = false;
        }
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuRecordNewData_Click(object sender, RoutedEventArgs e)
        {
            RecordNewData RecordDataWindow = new RecordNewData();
            RecordDataWindow.ShowDialog();
        }
    }
}
