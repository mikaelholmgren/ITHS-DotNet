using System;
using System.Collections.Generic;
using WeatherHistoryViewer.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WeatherHistoryViewer.Data
{
    public interface IWeatherService
    {
        WeatherSumary GetSumary();
        double? GetAvgTempByDate(DateTime date, Locations location);
        List<SelectListItem> GetValidDates();
        List<DayEntry> GetAllAveragessForLocation(Locations location, SortColumns SortBy = SortColumns.Date, SortDirection SortDir = SortDirection.Ascending);
        List<BalconyDoorEntry> FindBalconyDoorOpenSlots();
        int GetTotalDays();
    }
}