using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherHistoryViewer.Data.Models;

namespace WeatherHistoryViewer.Data
{
    public class DataSeeder
    {
        // Seed the database with the data from .csv file
        public static bool SeedFromCsvFile(string fileName, WeatherContext ctx)
        {
            // If database isn't empty we should return without doing anything
            Console.WriteLine("DataSeeder: init.");
            if (ctx.Measurements.Any()) return false;
            if (!File.Exists(fileName)) return false;
            var lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                var chunks = line.Split(',');
                MeasurementPoint p = new();
                p.Time = DateTime.Parse(chunks[0]);
                switch (chunks[1])
                {
                    case "Inne":
                        p.Location = Locations.Indoor;
                        break;
                    case "Ute":
                        p.Location = Locations.Outdoor;
                        break;

                    default:
                        Console.WriteLine("Seeder: Invalid location in source file.");
                        break;
                }
                p.Degrees = double.Parse(chunks[2], CultureInfo.InvariantCulture);
                p.Humidity = int.Parse(chunks[3]);
                ctx.Measurements.Add(p);
            }
            ctx.SaveChanges();
            Console.WriteLine("DataSeeder: Database Seeded.");
            return true;
        }
    }
}
