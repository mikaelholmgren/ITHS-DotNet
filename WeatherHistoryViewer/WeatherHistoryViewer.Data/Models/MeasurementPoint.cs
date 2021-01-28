using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherHistoryViewer.Data.Models
{
    public class MeasurementPoint
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public double Degrees { get; set; }
        public int Humidity { get; set; }
        public Locations Location { get; set; }
    }
}
