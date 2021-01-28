using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherHistoryViewer.Data.Models
{
    public enum SortColumns
    {
        Date,
        Temperature,
        Humidity,
        MoldRisk
    }
   public enum SortDirection
    {
        Ascending,
        Descending
    }
}
