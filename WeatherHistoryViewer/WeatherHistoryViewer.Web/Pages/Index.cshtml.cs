using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHistoryViewer.Data;
using WeatherHistoryViewer.Data.Models;

namespace WeatherHistoryViewer.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWeatherService _ws;
        public WeatherSumary Sumary { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IWeatherService ws)
        {
            _logger = logger;
            _ws = ws;
        }

        public void OnGet()
        {
            Sumary = _ws.GetSumary();
        }
    }
}
