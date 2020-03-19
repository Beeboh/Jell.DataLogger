using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Jell.DataLogger.Core.Models;
using Jell.DataLogger.Gui.Models;
using Jell.DataLogger.Gui.Builders;
using Jell.DataLogger.Gui.Adapters;
using Jell.DataLogger.Gui.Interfaces;

namespace Jell.DataLogger.Gui.ViewModels
{
    public class GraphViewModel : IViewModel
    {
        public SeriesCollection ParSeriesCollection { get; }

        private ReadOnlyCollection<ViewableParData> ViewableData { get; }

        private ParSeriesBuilder SeriesBuilder { get; } = new ParSeriesBuilder();


        public Func<double, string> Formatter { get; }

        public double StartTime { get; }

        public double EndTime { get; }



        public GraphViewModel(IEnumerable<ViewableParData> data)
        {
            ViewableData = new List<ViewableParData>(data).AsReadOnly();
            
            if (ViewableData.Count > 0)
            {
                StartTime = (double)ViewableData[0].Time.Ticks / TimeSpan.FromHours(1).Ticks;
                EndTime = (double)ViewableData[ViewableData.Count - 1].Time.Ticks / TimeSpan.FromHours(1).Ticks;
            }
            ParSeriesCollection = SeriesBuilder.Generate(ViewableData, GetMapper());
            Formatter = value => new DateTime((long)(value * TimeSpan.FromHours(1).Ticks)).ToString("t");
        }
        private CartesianMapper<ParSeriesPoint> GetMapper()
        {
            return Mappers.Xy<ParSeriesPoint>()
                .X(point => (double)point.Time.Ticks / TimeSpan.FromHours(1).Ticks)
                .Y(point => point.ParValue);
        }
    }
}
