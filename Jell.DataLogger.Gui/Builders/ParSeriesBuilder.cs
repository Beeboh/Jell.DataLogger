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

namespace Jell.DataLogger.Gui.Builders
{
    internal class ParSeriesBuilder
    {
        public SeriesCollection Generate(IEnumerable<ViewableParData> datacollection, CartesianMapper<ParSeriesPoint> mapper)
        {
            LineSeries Sensor1 = new LineSeries() { Values = new ChartValues<ParSeriesPoint>(), Title = "S1", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor2 = new LineSeries() { Values = new ChartValues<ParSeriesPoint>(), Title = "S2", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor3 = new LineSeries() { Values = new ChartValues<ParSeriesPoint>(), Title = "S3", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor4 = new LineSeries() { Values = new ChartValues<ParSeriesPoint>(), Title = "S4", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor5 = new LineSeries() { Values = new ChartValues<ParSeriesPoint>(), Title = "S5", PointGeometry = null, Fill = Brushes.Transparent };
            LineSeries Sensor6 = new LineSeries() { Values = new ChartValues<ParSeriesPoint>(), Title = "S6", PointGeometry = null, Fill = Brushes.Transparent };
            foreach (ViewableParData data in datacollection)
            {
                if (data.Sensor1.ParValue.HasValue)
                {
                    Sensor1.Values.Add(new ParSeriesPoint(data.Time, data.Sensor1.ParValue.Value));
                }
                if (data.Sensor2.ParValue.HasValue)
                {
                    Sensor2.Values.Add(new ParSeriesPoint(data.Time, data.Sensor2.ParValue.Value));
                }
                if (data.Sensor3.ParValue.HasValue)
                {
                    Sensor3.Values.Add(new ParSeriesPoint(data.Time, data.Sensor3.ParValue.Value));
                }
                if (data.Sensor4.ParValue.HasValue)
                {
                    Sensor4.Values.Add(new ParSeriesPoint(data.Time, data.Sensor4.ParValue.Value));
                }
                if (data.Sensor5.ParValue.HasValue)
                {
                    Sensor5.Values.Add(new ParSeriesPoint(data.Time, data.Sensor5.ParValue.Value));
                }
                if (data.Sensor6.ParValue.HasValue)
                {
                    Sensor6.Values.Add(new ParSeriesPoint(data.Time, data.Sensor6.ParValue.Value));
                }          
            }
            return new SeriesCollection(mapper) { Sensor1, Sensor2, Sensor3, Sensor4, Sensor5, Sensor6 };
        }
    }
}
