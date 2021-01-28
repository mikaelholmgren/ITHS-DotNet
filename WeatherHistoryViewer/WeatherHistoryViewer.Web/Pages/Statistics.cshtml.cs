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
    public class StatisticsModel : PageModel
    {
        private readonly IWeatherService _ws;
        public StatisticsModel(IWeatherService ws)
        {
            _ws = ws;
            PageSize = 20;
            PageIndex = 1;
            TotalEntries = _ws.GetTotalDays();
            NumPages = (TotalEntries +(PageSize -1)) / PageSize;
            
        }

        private void SetDisplayedBoundaries()
        {
            if (PageIndex == 1) FirstDisplayed = 1;
            else FirstDisplayed = ((PageIndex - 1) * PageSize) + (PageIndex - 1);
            LastDisplayed = ((PageIndex) * PageSize) + (PageIndex - 1);
            if (LastDisplayed > TotalEntries) LastDisplayed = TotalEntries;


        }

        [BindProperty(SupportsGet = true)]
        public Locations? Location { get; set; }
        public IEnumerable<DayEntry> Data { get; set; }
        [BindProperty(SupportsGet = true)]
        public SortColumns? SortBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public SortDirection? SortDir { get; set; }
        public int PageSize { get; set; }
        public int NumPages { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }
        public int TotalEntries { get; set; }
        public int FirstDisplayed { get; set; }
        public int LastDisplayed { get; set; }
        public void OnGet()
        {
            if (Location == null)
                return;
            SetDisplayedBoundaries();
            if (SortBy == null)
                SortBy = SortColumns.Temperature;
            switch (SortBy)
            {
                case SortColumns.Date:
                    if (SortDir == null) SortDir = SortDirection.Ascending;

                    break;
                case SortColumns.Temperature:
                    if (SortDir == null) SortDir = SortDirection.Descending;

                    break;
                case SortColumns.Humidity:
                    if (SortDir == null) SortDir = SortDirection.Ascending;

                    break;
                case SortColumns.MoldRisk:
                    if (SortDir == null) SortDir = SortDirection.Ascending;

                    break;
                default:
                    break;
            }
            Data = _ws.GetAllAveragessForLocation(Location.Value, SortBy.Value, SortDir.Value)
                .Skip((PageIndex -1) * PageSize).Take(PageSize);
            
        }
        public SortDirection GetSortDirection(SortColumns curCol)
        {
            SortDirection result = default;
            switch (curCol)
            {
                case SortColumns.Date:
                    result = SortDirection.Ascending;
                    break;
                case SortColumns.Temperature:
                    result = SortDirection.Descending;
                    break;
                case SortColumns.Humidity:
                    result = SortDirection.Ascending;
                    break;
                case SortColumns.MoldRisk:
                    result = SortDirection.Ascending;
                    break;
                default:
                    break;
            }
            if (curCol ==SortBy)
            {
                result = SortDir == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
            }
            return result;
        }
    }
}
