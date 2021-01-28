using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeatherHistoryViewer.Data;
using WeatherHistoryViewer.Data.Models;

namespace WeatherHistoryViewer.Web.Pages
{
    public class AvgTempModel : PageModel
    {
        private readonly IWeatherService _ws;
        public double? AvgForDate { get; set; }
        public string FirstDate { get; set; }
        public string LastDate { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime? SelectedDate { get; set; }
        [BindProperty]
        public Locations SelectedLocation { get; set; }
        [BindProperty]
        public List<SelectListItem> ValidDates { get; set; }

        public AvgTempModel(IWeatherService ws)
        {
            _ws = ws;
//            (FirstDate, LastDate) = _ws.GetDateRange();
            
        }
        public void OnGet()
        {
            ValidDates = _ws.GetValidDates();
        }
        public void OnPost()
        {
            ValidDates = _ws.GetValidDates();
            AvgForDate = _ws.GetAvgTempByDate(SelectedDate.Value, SelectedLocation);
            if (AvgForDate != null) AvgForDate = Math.Round(AvgForDate.Value, 2);
        }

    }
}
