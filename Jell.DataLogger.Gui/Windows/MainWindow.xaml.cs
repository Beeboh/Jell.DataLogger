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
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Testing; //remove eventually
using System.Xml;
using System.Xml.Linq;


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
        private CsvExporter CsvExporter { get; } = new CsvExporter();
        private ParameterStringFormatter ParameterStringFormatter { get; } = new ParameterStringFormatter();

        private string portName { get; set; }
        private LoggerInfo LoggerInfo { get; set; }
        private GraphViewModel GraphView { get; set; }
        private DataTableViewModel DataTableView { get; set; }
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
        private void Connect(string portName)
        {
            try
            {
                SerialPort port = new SerialPort(portName, 9600);
                port.DtrEnable = true;
                port.ReadTimeout = 10000;
                port.WriteTimeout = 10000;
                port.Open();
                port.WriteLine("0001");
                string datastring = null;
                try
                {

                    //DataGenerator//
                    //LoggerInfoGenerator DataGenerator = new LoggerInfoGenerator();
                    //datastring = DataGenerator.GenerateDataString(DateTime.Now, 1000, 3600);

                    //USB
                    datastring = port.ReadLine();
                    port.Close();
                    DataParser dataParser = new DataParser();
                    try
                    {
                        LoggerInfo = dataParser.Parse(datastring);
                        DataTableView = new DataTableViewModel(LoggerInfo);
                        GraphView = new GraphViewModel(LoggerInfo);
                        DataContext = DataTableView;
                        ConnectedMode = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing device data.\n\n Message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (TimeoutException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Disconnect()
        {
            portName = null;
            LoggerInfo = null;
            GraphView = null;
            DataTableView = null;
            ConnectedMode = false;
            DataContext = new DisconnectedViewModel();
        }
        private void SendParameters(string portName, DateTime starttime, DateTime endtime, int samplerate)
        {
            SerialPort port = new SerialPort(portName, 9600);
            port.DtrEnable = true;
            port.ReadTimeout = 5000;
            port.WriteTimeout = 5000;
            string parameterstring = ParameterStringFormatter.GetParameterString(starttime, endtime, samplerate);
            port.Open();
            port.WriteLine(parameterstring);
            port.Close();
        }

        private MenuItem[] GetConnectedMenuItems()
        {
            return new MenuItem[] { menuDisconnect, menuRecordNewData, menuExportToCsv };
        }
        private MenuItem[] GetDisconnectedMenuItems()
        {
            return new MenuItem[] { menuConnect };
        }
        [Obsolete]
        private void ExportToExcel(IList<ParData> pardata)
        {
            Excel.Application ExcelApp;
            try
            {
                ExcelApp = new Excel.Application();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            string[] Headers = new string[] { "Date", "Time", "S1", "S2", "S3", "S4", "S5", "S6" };
            for (int i = 0; i < Headers.Length; i++)
            {
                ExcelApp.Cells[1, i + 1] = Headers[i];
            }
            for (int i = 0; i < pardata.Count; i++)
            {
                ExcelApp.Cells[i + 2, 1] = pardata[i].Time.ToString("yyyy-MM-dd");
                ExcelApp.Cells[i + 2, 2] = pardata[i].Time.ToString("hh:mm:ss");
                ExcelApp.Cells[i + 2, 3] = pardata[i].Sensor1.ParValue;
                ExcelApp.Cells[i + 2, 4] = pardata[i].Sensor2.ParValue;
                ExcelApp.Cells[i + 2, 5] = pardata[i].Sensor3.ParValue;
                ExcelApp.Cells[i + 2, 6] = pardata[i].Sensor4.ParValue;
                ExcelApp.Cells[i + 2, 7] = pardata[i].Sensor5.ParValue;
                ExcelApp.Cells[i + 2, 8] = pardata[i].Sensor6.ParValue;
            }
            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;

        }

        //Buttons
        private void menuConnect_Click(object sender, RoutedEventArgs e)
        {
            ConnectionWindow connectionWindow = new ConnectionWindow();
            connectionWindow.ShowDialog();
            if (connectionWindow.WasCompleted)
            {
                portName = connectionWindow.PortName;
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
            RecordNewData RecordDataWindow = new RecordNewData();
            RecordDataWindow.ShowDialog();
            if (RecordDataWindow.ParametersCompleted)
            {
                LoggerParameters LoggerParameters = RecordDataWindow.LoggerParameters;
                try
                {
                    SendParameters(portName, LoggerParameters.StartDateTime, LoggerParameters.EndDateTime, LoggerParameters.SamplingRate);
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
            CsvExporter.Export(LoggerInfo.ParData);
        }
    }
}
