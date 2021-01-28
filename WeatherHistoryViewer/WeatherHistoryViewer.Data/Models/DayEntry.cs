using System;

namespace WeatherHistoryViewer.Data.Models
{
    public class DayEntry
    {
        public DateTime Date { get; set; }
        public double AverageTemperature { get; set; }
        public double AverageHumidity { get; set; }
        public double AverageMoldPercentage { get; set; }
    }
}