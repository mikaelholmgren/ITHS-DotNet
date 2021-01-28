using System.ComponentModel.DataAnnotations;

namespace WeatherHistoryViewer.Data.Models
{
    public enum Locations
    {
        [Display(Name = "Inomhus")]
        Indoor,
        [Display(Name = "Utomhus")]
        Outdoor
    }
}
