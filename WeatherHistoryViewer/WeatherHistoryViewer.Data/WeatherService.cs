using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeatherHistoryViewer.Data.Models;
namespace WeatherHistoryViewer.Data
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherContext _ctx;

        public WeatherService(WeatherContext context)
        {
            _ctx = context;
        }

        public double? GetAvgTempByDate(DateTime date, Locations location)
        {
            double? result = null;
            try
            {
                result = _ctx.Measurements.Where(d => d.Location == location && d.Time.Date == date.Date).Average(a => a.Degrees);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public WeatherSumary GetSumary()
        {
            WeatherSumary sumary = new();
            var events = _ctx.Measurements.Where(l => l.Location == Locations.Outdoor).OrderBy(o => o.Time.Date);
            sumary.FromDate = events.First().Time;
            sumary.ToDate = events.Last().Time;
            sumary.DayCount = events.GroupBy(g => g.Time.Date).Select(k => k.Key).Count();

            var outDoorAverages = GetAllAveragessForLocation(Locations.Outdoor);
            sumary.AutumnDate = FindAutumn(outDoorAverages);
            sumary.WinterDate = FindWinter(outDoorAverages);
            return sumary;
        }

        public int GetTotalDays()
        {
            var events = _ctx.Measurements.Where(l => l.Location == Locations.Outdoor).OrderBy(o => o.Time.Date);
            return events.GroupBy(g => g.Time.Date).Select(k => k.Key).Count();
        }
        private DateTime? FindWinter(List<DayEntry> e)
        {
            DateTime? result = null;
            // we filter the dates, since the winter doesn't begin erlier.
            var filteredDays = e.Where(d => d.Date.Month > 9);
            int validTempCount = 0;
            foreach (var day in filteredDays)
            {
                if (validTempCount == 5) break;
                if (Math.Round(day.AverageTemperature, 1) <= 0)
                {
                    validTempCount++;
                }
                else
                {
                    validTempCount = 0;
                    result = null;
                }
                if (validTempCount == 1) result = day.Date; // maybe
            }
            return result;

        }

        public List<DayEntry> GetAllAveragessForLocation(Locations location, SortColumns sortBy = SortColumns.Date, SortDirection sortDir = SortDirection.Ascending)
        {
            List<DayEntry> result = new();
            var groupedByDate = _ctx.Measurements.Where(l => l.Location == location)
                .GroupBy(g => g.Time.Date,
                (key, g) => new DayEntry
                {
                    Date = key,
                    AverageTemperature = g.Average(a => a.Degrees),
                    AverageHumidity = g.Average(a => a.Humidity)
                });

            switch (sortBy)
            {
                case SortColumns.Date:
                    if (sortDir == SortDirection.Ascending)
                        result = groupedByDate.OrderBy(o => o.Date).ToList();
                    else
                        result = groupedByDate.OrderByDescending(o => o.Date).ToList();
                    GetMoldPercentage(result);
                    break;
                case SortColumns.Temperature:
                    if (sortDir == SortDirection.Ascending)
                        result = groupedByDate.OrderBy(o => o.AverageTemperature).ToList();
                    else
                        result = groupedByDate.OrderByDescending(o => o.AverageTemperature).ToList();
                    GetMoldPercentage(result);
                    break;
                case SortColumns.Humidity:
                    if (sortDir == SortDirection.Ascending)
                        result = groupedByDate.OrderBy(o => o.AverageHumidity).ToList();
                    else
                        result = groupedByDate.OrderByDescending(o => o.AverageHumidity).ToList();
                    GetMoldPercentage(result);
                    break;
                case SortColumns.MoldRisk:
                    // We need to get this from database first, so we have to sort by something else first and then sort when moldrisk is calculated

                    result = groupedByDate.OrderBy(o => o.Date).ToList();
                    GetMoldPercentage(result);
                    if (sortDir == SortDirection.Ascending)
                        result = result.OrderBy(o => o.AverageMoldPercentage).ToList();
                    else
                        result = result.OrderByDescending(o => o.AverageMoldPercentage).ToList();
                    break;
                default:
                    break;
            }

            return result;
        }
        private void GetMoldPercentage(IEnumerable<DayEntry> l)
        {
            foreach (var item in l)
            {
                double result = ((item.AverageHumidity - 78) * (item.AverageTemperature / 15)) / 0.22;
                if (result < 0) result = 0;
                else if (result > 100) result = 100;
                item.AverageMoldPercentage = result;
            }



        }
        private DateTime? FindAutumn(List<DayEntry> e)
        {
            DateTime? result = null;
            // we filter the dates, since the autumn doesn't begin erlier.
            var filteredDays = e.Where(d => d.Date.Month > 8);
            int validTempCount = 0;
            foreach (var day in filteredDays)
            {
                if (validTempCount == 5) break;
                if (Math.Round(day.AverageTemperature, 1) < 10)
                {
                    validTempCount++;
                }
                else
                {
                    validTempCount = 0;
                    result = null;
                }
                if (validTempCount == 1) result = day.Date; // maybe
            }
            return result;
        }
        public List<SelectListItem> GetValidDates()
        {
            var q = _ctx.Measurements.GroupBy(g => g.Time.Date).OrderBy(o => o.Key)
                .Select(k => k.Key.ToShortDateString())
            .Select(a =>
                new SelectListItem
                {
                    Value = a,
                    Text = a
                }
                );

            return q.ToList();

        }
        /// <summary>
        /// Tries to find when the door was open, based on looking at decreasing indoor temperatures in pair with increasing outdoor, since the sensor is close
        /// </summary>
        /// <returns>Daily sumaries of when the door could have been open</returns>
        public List<BalconyDoorEntry> FindBalconyDoorOpenSlots()
        {
            List<BalconyDoorEntry> result = new();
            // We need all events anyway, so we grab them down and filter with LINQ
            var allEvents = _ctx.Measurements.ToList().GroupBy(g => g.Time.Date).OrderBy(o => o.Key.Date);
            foreach (var group in allEvents)
            {
                var date = group.Key;
                var indoorEvents = group.Where(l => l.Location == Locations.Indoor).OrderBy(o => o.Time).ToList();
                var outdoorEvents = group.Where(l => l.Location == Locations.Outdoor).OrderBy(o => o.Time).ToList();
                List<(DateTime StartTime, DateTime EndTime)> decreasingIndoor = FindDecreasingTemps(indoorEvents);
                List<(DateTime StartTime, DateTime EndTime)> increasingOutdoor = FindIncreasingTemps(outdoorEvents);
                var doorEntry = FindOpenDoor(date, decreasingIndoor, increasingOutdoor);
                if (doorEntry != null)
                    result.Add(doorEntry);
            }
            return result;
        }
        /// <summary>
        /// This creates one sumary for one day of when the balconydoor was thought to be open
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="decreasingIndoor"></param>
        /// <param name="increasingOutdoor"></param>
        /// <returns>Entry for one day</returns>
        private BalconyDoorEntry? FindOpenDoor(DateTime date, List<(DateTime StartTime, DateTime EndTime)> decreasingIndoor, List<(DateTime StartTime, DateTime EndTime)> increasingOutdoor)
        {
            List<(DateTime StartTime, DateTime EndTime)> entries = new();
            TimeSpan totalTime = new();
            foreach (var item in decreasingIndoor)
            {
                var match = increasingOutdoor.Where(x => x.StartTime >= item.StartTime && x.EndTime <= item.EndTime);
                if (match != null && match.Count() > 0)
                {
                    foreach (var m in match)
                    {



                        entries.Add((m.StartTime, m.EndTime));
                    }
                }
            }
            if (entries.Count() == 0) return null; // if we found no slots we return null
            foreach (var item in entries)
            {
                totalTime += (item.EndTime - item.StartTime);
            }
            return new BalconyDoorEntry()
            {
                Date = date,
                Time = totalTime
            };
        }

        private List<(DateTime StartTime, DateTime EndTime)> FindDecreasingTemps(List<MeasurementPoint> events)
        {
            List<(DateTime StartTime, DateTime EndTime)> ts = new();
            double lastTemp = double.MinValue;
            double tolerance = 0.2;
            double? startTemp = null;
            DateTime? start = null;
            foreach (var e in events)
            {
                if (lastTemp > (e.Degrees + tolerance) && startTemp == null)
                {
                    start = e.Time;
                    startTemp = e.Degrees;
                    lastTemp = e.Degrees;
                }
                else if (startTemp != null && (e.Degrees) > startTemp.Value)
                {
                    ts.Add((start.Value, e.Time));
                    start = null;
                    startTemp = null;
                    lastTemp = e.Degrees;
                }
                else
                {
                    lastTemp = e.Degrees;
                }
            }
            return ts;
        }
        private List<(DateTime StartTime, DateTime EndTime)> FindIncreasingTemps(List<MeasurementPoint> events)
        {
            List<(DateTime StartTime, DateTime EndTime)> ts = new();
            double lastTemp = double.MaxValue;
            double tolerance = 0.2;
            double? startTemp = null;
            DateTime? start = null;
            foreach (var e in events)
            {
                if (lastTemp < (e.Degrees - tolerance) && startTemp == null)
                {
                    start = e.Time;
                    startTemp = e.Degrees;
                    lastTemp = e.Degrees;
                }
                else if (startTemp != null && (e.Degrees) <= startTemp.Value)
                {
                    ts.Add((start.Value, e.Time));
                    start = null;
                    startTemp = null;
                    lastTemp = e.Degrees;
                }
                else
                {
                    lastTemp = e.Degrees;
                }
            }
            return ts;
        }

    }
}
