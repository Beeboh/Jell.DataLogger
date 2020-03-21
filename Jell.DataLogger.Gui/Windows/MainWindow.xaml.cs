using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO.Ports;
using Excel = Microsoft.Office.Interop.Excel;
using Jell.DataLogger.Gui.ViewModels;
using Jell.DataLogger.Gui.Services;
using Jell.DataLogger.Gui.Formatters;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Core.Exceptions;
using Jell.DataLogger.Testing; //remove eventually
using System.Xml;
using System.Xml.Linq;
using Jell.DataLogger.Gui.Interfaces;
using Jell.DataLogger.Gui.Models;
using Jell.DataLogger.Gui.Adapters;

namespace Jell.DataLogger.Gui.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _connectedmode;

        private MenuItem[] ConnectedMenuItems { get; }
        private MenuItem[] DisconnectedMenuItems { get; }
        private CsvExportService CsvExporter { get; } = new CsvExportService();
        private LoggerCommandService CommandService { get; set; }
        private ViewableParAdapter ViewableParAdapter { get; set; } = new ViewableParAdapter();

        private LoggerInfo LoggerInfo { get; set; }
        private ReadOnlyCollection<ViewableParData> ViewableParData { get; set; }

        private GraphViewModel GraphView { get; set; }
        private DataTableViewModel DataTableView { get; set; }
        private DisconnectedViewModel DisconnectedView { get; } = new DisconnectedViewModel();
        private bool ConnectedMode
        {
            get { return _connectedmode; }
            set
            {
                foreach (MenuItem connectedmenuitem in ConnectedMenuItems)
                {
                    connectedmenuitem.IsEnabled = value;
                }
                foreach (MenuItem disconnectedmenuitem in DisconnectedMenuItems)
                {
                    disconnectedmenuitem.IsEnabled = !value;
                }
                _connectedmode = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            ConnectedMenuItems = GetConnectedMenuItems();
            DisconnectedMenuItems = GetDisconnectedMenuItems();
            Disconnect();
        }
        private bool Connect(string portName)
        {
            CommandService = new LoggerCommandService(portName);
            try
            {
                //Data Generator//
                //LoggerInfoGenerator DataGenerator = new LoggerInfoGenerator();
                //LoggerInfo = DataGenerator.Generate(DateTime.Now, 10, 10);

                //USB//
                LoggerInfo = CommandService.RequestLoggerInfo();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            try
            {
                ViewableParData = ViewableParAdapter.GetViewableParDataList(LoggerInfo.ParData);
                DataTableView = new DataTableViewModel(ViewableParData);
                GraphView = new GraphViewModel(ViewableParData);
                SetView(DataTableView);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data views.\n\nError message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        private void Disconnect()
        {
            CommandService = null;
            LoggerInfo = null;
            ViewableParData = null;
            GraphView = null;
            DataTableView = null;
            ConnectedMode = false;
            SetView(DisconnectedView);
        }
        private void SetView(IViewModel viewmodel)
        {
            DataContext = viewmodel;
            if (viewmodel is DataTableViewModel)
            {
                ConnectedMode = true;
                menuGraphView.IsEnabled = true;
                menuTableView.IsEnabled = false;
            }
            else if (viewmodel is GraphViewModel)
            {
                ConnectedMode = true;
                menuTableView.IsEnabled = true;
                menuGraphView.IsEnabled = false;
            }
            else if(viewmodel is DisconnectedViewModel)
            {
                ConnectedMode = false;
            }
        }
        

        private MenuItem[] GetConnectedMenuItems()
        {
            return new MenuItem[] { menuDisconnect, menuRecordNewData, menuExportToCsv, menuTableView, menuGraphView };
        }
        private MenuItem[] GetDisconnectedMenuItems()
        {
            return new MenuItem[] { menuConnect };
        }
        private IViewModel[] GetViewModels()
        {
            return new IViewModel[] { DisconnectedView, DataTableView, GraphView };
        }

        //Buttons
        private void menuConnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectionWindow connectionWindow = new ConnectionWindow();
            connectionWindow.ShowDialog();
            if (connectionWindow.WasCompleted)
            {
                Connect(connectionWindow.PortName);
            }
                
            
        }
        private void menuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }
        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuRecordNewData_Click(object sender, RoutedEventArgs e)
        {
            RecordNewData RecordDataWindow = new RecordNewData(LoggerInfo.BatteryVoltage, LoggerInfo.BatteryPercent);
            RecordDataWindow.ShowDialog();
            if (RecordDataWindow.ParametersCompleted)
            {
                LoggerParameters LoggerParameters = RecordDataWindow.LoggerParameters;
                try
                {
                    CommandService.SetParameters(LoggerParameters.StartDateTime, LoggerParameters.EndDateTime, LoggerParameters.SamplingRate);
                    MessageBox.Show($"               Parameters Set!\n\n Start Time: {LoggerParameters.StartDateTime.ToString("yyyy-MM-dd HH:mm tt")}\n End Time: {LoggerParameters.EndDateTime.ToString("yyyy-MM-dd HH:mm tt")}\n Sampling Rate: Δt = {LoggerParameters.SamplingRate}s");
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Failed to program device.\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void menuExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            CsvExporter.Export(ViewableParData);
        }

        private void menuTableView_Click(object sender, RoutedEventArgs e)
        {
            SetView(DataTableView);
        }

        private void menuGraphView_Click(object sender, RoutedEventArgs e)
        {
            SetView(GraphView);
        }
    }
}
