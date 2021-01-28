using System;
using System.Collections.Generic;

namespace WeatherHistoryViewer.Data.Models
{
    public class WeatherSumary
    {
        public WeatherSumary()
        {
            InDoorAverages = new();
            OutDoorAverages = new();
        }
        public DateTime FromDate { get; internal set; }
        public DateTime ToDate { get; internal set; }
        public int DayCount { get; internal set; }
        public List<DayEntry> InDoorAverages { get; set; }
        public List<DayEntry> OutDoorAverages { get; set; }
        public DateTime? AutumnDate { get; internal set; }
        public DateTime? WinterDate { get; internal set; }
    }
}