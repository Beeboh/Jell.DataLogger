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
    /// Interaction logic for RecordData.xaml
    /// </summary>
    ///
    public class LoggerParameters
    {
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public int samplingRate { get; }

        public LoggerParameters(DateTime _StartDateTime, DateTime _EndDateTime, int _SamplingRate)
        {
            StartDateTime = _StartDateTime;
            EndDateTime = _EndDateTime;
            samplingRate = _SamplingRate;
        }

    }
    public partial class RecordNewData : Window
    {
        private double batteryVoltage { get; }
        private string displayVoltage => $"{batteryVoltage}V";
        private string batteryLife { get; set; }
        private int samplingRate { get; set; }


        private DateTime StartDateTime { get; set; }
        private DateTime StartDate { get; set; }
        private int sdtYear { get; set; }
        private int sdtMonth { get; set; }
        private int sdtDay { get; set; }
        private int sdtHour { get; set; }
        private int sdtMin { get; set; }
        private string sdtAMPM { get; set; }


        private DateTime EndDateTime { get; set; }
        private DateTime EndDate { get; set; }
        private int edtYear { get; set; }
        private int edtMonth { get; set; }
        private int edtDay { get; set; }
        private int edtHour { get; set; }
        private int edtMin { get; set; }
        private string edtAMPM { get; set; }


        public LoggerParameters LoggerParameters { get; private set; }
        public bool ParametersCompleted { get; private set; } = false;
        public RecordNewData()
        {
            InitializeComponent();

            DateTime CurrentTime = DateTime.Now;
            sdtYear = CurrentTime.Year;
            sdtMonth = CurrentTime.Month;
            sdtDay = CurrentTime.Day;
            sdtHour = 0;
            sdtMin = 0;
            sdtAMPM = "AM";
            StartDateTime = new DateTime(sdtYear, sdtMonth, sdtDay, sdtHour, sdtMin, 0);

            edtYear = CurrentTime.Year;
            edtMonth = CurrentTime.Month;
            edtDay = CurrentTime.Day;
            edtHour = 0;
            edtMin = 0;
            edtAMPM = "PM";
            EndDateTime = new DateTime(edtYear, edtMonth, edtDay, edtHour, edtMin, 0);
        }
        private void cmbSamplingRate_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbSamplngRate.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "5 sec":
                    samplingRate = 5;
                    batteryLife = "f(Δt,1)";
                    break;
                case "10 sec":
                    samplingRate = 10;
                    batteryLife = "f(Δt,2)";
                    break;
                case "30 sec":
                    samplingRate = 30;
                    batteryLife = "f(Δt,3)";
                    break;
                case "1 min":
                    samplingRate = 60;
                    batteryLife = "f(Δt,4)";
                    break;
                case "5 min":
                    samplingRate = 300;
                    batteryLife = "f(Δt,5)";
                    break;
                case "10 min":
                    samplingRate = 600;
                    batteryLife = "f(Δt,6)";
                    break;
                case "15 min":
                    samplingRate = 900;
                    batteryLife = "f(Δt,7)";
                    break;
                case "30 min":
                    samplingRate = 1800;
                    batteryLife = "f(Δt,8)";
                    break;
                case "1 hour":
                    samplingRate = 3600;
                    batteryLife = "f(Δt,9)";
                    break;
            }
        }
        private void calendarStartDate_Changed(object sender, RoutedEventArgs e)
        {
            StartDate = calendarStartDate.SelectedDate.Value;
            sdtYear = StartDate.Year;
            sdtMonth = StartDate.Month;
            sdtDay = StartDate.Day;
        }
        private void cmbStartTimeHour_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbStartTimeHour.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "1:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 1; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 13; }
                    break;
                case "2:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 2; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 14; }
                    break;
                case "3:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 3; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 15; }
                    break;
                case "4:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 4; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 16; }
                    break;
                case "5:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 5; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 17; }
                    break;
                case "6:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 6; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 18; }
                    break;
                case "7:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 7; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 19; }
                    break;
                case "8:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 8; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 20; }
                    break;
                case "9:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 9; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 21; }
                    break;
                case "10:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 10; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 22; }
                    break;
                case "11:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 11; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 23; }
                    break;
                case "12:00":
                    if (sdtAMPM == "AM")
                    { sdtHour = 0; }
                    else if (sdtAMPM == "PM")
                    { sdtHour = 12; }
                    break;
            }
        }
        private void cmbStartTimeMinute_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbStartTimeMinute.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "00:00":
                    sdtMin = 0;
                    break;
                case "00:05":
                    sdtMin = 5;
                    break;
                case "00:10":
                    sdtMin = 10;
                    break;
                case "00:15":
                    sdtMin = 15;
                    break;
                case "00:20":
                    sdtMin = 20;
                    break;
                case "00:25":
                    sdtMin = 25;
                    break;
                case "00:30":
                    sdtMin = 30;
                    break;
                case "00:35":
                    sdtMin = 35;
                    break;
                case "00:40":
                    sdtMin = 40;
                    break;
                case "00:45":
                    sdtMin = 45;
                    break;
                case "00:50":
                    sdtMin = 50;
                    break;
                case "00:55":
                    sdtMin = 55;
                    break;
            }
        }
        private void cmbStartTimeAMPM_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbStartTimeAMPM.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "AM":
                    sdtAMPM = "AM";
                    break;
                case "PM":
                    sdtAMPM = "PM";
                    break;
            }
            // cmbStartTimeHour_Selected();
        }
        private void calendarEndDate_Changed(object sender, RoutedEventArgs e)
        {
            EndDate = calendarEndDate.SelectedDate.Value;
            edtYear = EndDate.Year;
            edtMonth = EndDate.Month;
            edtDay = EndDate.Day;
            EndDateTime = new DateTime(edtYear, edtMonth, edtDay, edtHour, edtMin, 0);
        }
        private void cmbEndTimeHour_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbEndTimeHour.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "1:00":
                    if (edtAMPM == "AM")
                    { edtHour = 1; }
                    else if (edtAMPM == "PM")
                    { edtHour = 13; }
                    break;
                case "2:00":
                    if (edtAMPM == "AM")
                    { edtHour = 2; }
                    else if (edtAMPM == "PM")
                    { edtHour = 14; }
                    break;
                case "3:00":
                    if (edtAMPM == "AM")
                    { edtHour = 3; }
                    else if (edtAMPM == "PM")
                    { edtHour = 15; }
                    break;
                case "4:00":
                    if (edtAMPM == "AM")
                    { edtHour = 4; }
                    else if (edtAMPM == "PM")
                    { edtHour = 16; }
                    break;
                case "5:00":
                    if (edtAMPM == "AM")
                    { edtHour = 5; }
                    else if (edtAMPM == "PM")
                    { edtHour = 17; }
                    break;
                case "6:00":
                    if (edtAMPM == "AM")
                    { edtHour = 6; }
                    else if (edtAMPM == "PM")
                    { edtHour = 18; }
                    break;
                case "7:00":
                    if (edtAMPM == "AM")
                    { edtHour = 7; }
                    else if (edtAMPM == "PM")
                    { edtHour = 19; }
                    break;
                case "8:00":
                    if (edtAMPM == "AM")
                    { edtHour = 8; }
                    else if (edtAMPM == "PM")
                    { edtHour = 20; }
                    break;
                case "9:00":
                    if (edtAMPM == "AM")
                    { edtHour = 9; }
                    else if (edtAMPM == "PM")
                    { edtHour = 21; }
                    break;
                case "10:00":
                    if (edtAMPM == "AM")
                    { edtHour = 10; }
                    else if (edtAMPM == "PM")
                    { edtHour = 22; }
                    break;
                case "11:00":
                    if (edtAMPM == "AM")
                    { edtHour = 11; }
                    else if (edtAMPM == "PM")
                    { edtHour = 23; }
                    break;
                case "12:00":
                    if (edtAMPM == "AM")
                    { edtHour = 0; }
                    else if (edtAMPM == "PM")
                    { edtHour = 12; }
                    break;
            }
        }
        private void cmbEndTimeMinute_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbEndTimeMinute.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "00:00":
                    edtMin = 0;
                    break;
                case "00:05":
                    edtMin = 5;
                    break;
                case "00:10":
                    edtMin = 10;
                    break;
                case "00:15":
                    edtMin = 15;
                    break;
                case "00:20":
                    edtMin = 20;
                    break;
                case "00:25":
                    edtMin = 25;
                    break;
                case "00:30":
                    edtMin = 30;
                    break;
                case "00:35":
                    edtMin = 35;
                    break;
                case "00:40":
                    edtMin = 40;
                    break;
                case "00:45":
                    edtMin = 45;
                    break;
                case "00:50":
                    edtMin = 50;
                    break;
                case "00:55":
                    edtMin = 55;
                    break;
            }
        }
        private void cmbEndTimeAMPM_Selected(object sender, RoutedEventArgs e)
        {
            switch (cmbEndTimeAMPM.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "AM":
                    edtAMPM = "AM";
                    break;
                case "PM":
                    edtAMPM = "PM";
                    break;
            }
            // cmbEndTimeHour_Selected();
        }
        private void btnSetParameters(object sender, RoutedEventArgs e)
        {
            StartDateTime = new DateTime(sdtYear, sdtMonth, sdtDay, sdtHour, sdtMin, 0);
            EndDateTime = new DateTime(edtYear, edtMonth, edtDay, edtHour, edtMin, 0);
            MessageBoxResult result = MessageBox.Show("All data on the logger will be deleted when parameters are set.\n\n                             Do you want to continue?", "Set Parameters", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    LoggerParameters = new LoggerParameters(StartDateTime, EndDateTime, samplingRate);
                    ParametersCompleted = true;
                    MessageBox.Show($"               Parameters Set!\n\n Start Time: {StartDateTime.ToShortDateString()} {StartDateTime.ToShortTimeString()}\n End Time: {EndDateTime.ToShortDateString()} {EndDateTime.ToShortTimeString()}\n Sampling Rate: Δt = {samplingRate}s");
                    break;
                case MessageBoxResult.Cancel:
                    break;

            }
            Close();
        }
        private void btnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}