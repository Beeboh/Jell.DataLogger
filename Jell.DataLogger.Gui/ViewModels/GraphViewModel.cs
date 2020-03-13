using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Gui.Models;
using System.Windows.Media;

namespace Jell.DataLogger.Gui.ViewModels
{
    public class GraphViewModel
    {
        public SeriesCollection ParSeriesCollection { get; }
        public Func<double, string> Formatter { get; }

        public GraphViewModel(LoggerInfo LoggerInfo)
        {
            Formatter = value => new DateTime((long)(value*TimeSpan.FromHours(1).Ticks)).ToString("t");
            CartesianMapper<ParMeasurement> mapper = Mappers.Xy<ParMeasurement>()
                .X(par => (double)par.Time.Ticks/TimeSpan.FromHours(1).Ticks)
                .Y(par => par.ParValue);
            ParSeriesCollection = GenerateSeriesCollection(LoggerInfo.ParData, mapper);
        }
        private SeriesCollection GenerateSeriesCollection(IList<ParData> datacollection, CartesianMapper<ParMeasurement> mapper)
        {
            LineSeries Sensor1 = new LineSeries() { Values = new ChartValues<ParMeasurement>(), Title = "S1", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor2 = new LineSeries() { Values = new ChartValues<ParMeasurement>(), Title = "S2", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor3 = new LineSeries() { Values = new ChartValues<ParMeasurement>(), Title = "S3", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor4 = new LineSeries() { Values = new ChartValues<ParMeasurement>(), Title = "S4", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor5 = new LineSeries() { Values = new ChartValues<ParMeasurement>(), Title = "S5", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor6 = new LineSeries() { Values = new ChartValues<ParMeasurement>(), Title = "S6", PointGeometry = null, Fill = Brushes.Transparent };
            foreach (ParData data in datacollection)
            {
                Sensor1.Values.Add(new ParMeasurement(data.Time, data.Sensor1.ParValue));
                Sensor2.Values.Add(new ParMeasurement(data.Time, data.Sensor2.ParValue));
                Sensor3.Values.Add(new ParMeasurement(data.Time, data.Sensor3.ParValue));
                Sensor4.Values.Add(new ParMeasurement(data.Time, data.Sensor4.ParValue));
                Sensor5.Values.Add(new ParMeasurement(data.Time, data.Sensor5.ParValue));
                Sensor6.Values.Add(new ParMeasurement(data.Time, data.Sensor6.ParValue));
            }
            return new SeriesCollection(mapper) { Sensor1, Sensor2, Sensor3, Sensor4, Sensor5, Sensor6 };
        }
    }
}
