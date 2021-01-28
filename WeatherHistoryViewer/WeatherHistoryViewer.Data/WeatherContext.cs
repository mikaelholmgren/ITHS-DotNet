using Microsoft.EntityFrameworkCore;
using WeatherHistoryViewer.Data.Models;

namespace WeatherHistoryViewer.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<MeasurementPoint> Measurements { get; set; }
        
    }
}
