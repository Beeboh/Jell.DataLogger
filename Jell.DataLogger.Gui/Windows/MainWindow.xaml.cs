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
using Excel = Microsoft.Office.Interop.Excel;
using Jell.DataLogger.Gui.ViewModels;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Testing; //remove eventually

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
            //Edit this
            ParDataGenerator DataGenerator = new ParDataGenerator();
            ReadOnlyCollection<ParData> DataCollection = DataGenerator.Generate(DateTime.Now, 1000, 5);
            DataContext = new DataTableViewModel(DataCollection);
            ParData = DataCollection;
            ConnectedMode = true;

        }
        private void Disconnect()
        {
            DataContext = new DisconnectedViewModel();
            ParData = null;
            ConnectedMode = false;
        }
        private void ExportToExcel(IList<ParData> pardata)
        {
            Excel.Application ExcelApp;
            try
            {
                ExcelApp = new Excel.Application();
            }
            catch(Exception ex)
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

        private void menuExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(ParData);
        }
    }
}
