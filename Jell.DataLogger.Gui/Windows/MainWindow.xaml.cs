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
        private ReadOnlyCollection<ParData> ParData { get; set; }
        private CsvExporter CsvExporter { get; } = new CsvExporter();

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
        private void Connect(string portName)
        {
            //Delete this eventually
            ParDataGenerator DataGenerator = new ParDataGenerator();
            ReadOnlyCollection<ParData> RandomlyGeneratedData = DataGenerator.Generate(DateTime.Now, 1000, 5);

            try
            {
                SerialPort port = new SerialPort(portName, 9600);
                port.DtrEnable = true;
                port.ReadTimeout = 5000;
                port.WriteTimeout = 5000;
                port.Open();
                port.WriteLine("0001");
                string datastring = null;
                try
                {
                    ParXmlFormatter parXmlFormatter = new ParXmlFormatter();
                    datastring = parXmlFormatter.GetString(RandomlyGeneratedData);

                    //datastring = port.ReadLine();
                    port.Close();
                    DataParser dataParser = new DataParser();
                    try
                    {
                        ParData = dataParser.Parse(datastring);
                        DataContext = new DataTableViewModel(ParData);
                        ConnectedMode = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error Parsing device data.\n\n Message: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            DataContext = new DisconnectedViewModel();
            ParData = null;
            ConnectedMode = false;
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
        }

        private void menuExportToCsv_Click(object sender, RoutedEventArgs e)
        {
            CsvExporter.Export(ParData);
        }
    }
}
