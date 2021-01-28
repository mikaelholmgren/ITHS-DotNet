using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherHistoryViewer.Data;
using WeatherHistoryViewer.Data.Models;

namespace WeatherHistoryViewer.Web.Pages
{
    public class BalconyDoorModel : PageModel
    {
        private readonly IWeatherService _ws;
        public List<BalconyDoorEntry> BalconyDoorEntries { get; set; }
        public BalconyDoorModel(IWeatherService ws)
        {
            _ws = ws;
        }
        public void OnGet()
        {
            BalconyDoorEntries = _ws.FindBalconyDoorOpenSlots();
        }
    }
}
