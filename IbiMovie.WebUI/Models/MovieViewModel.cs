using IbiMovie.WebUI.Shared;
using System.Text.Json;

namespace IbiMovie.WebUI.Models
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public DateTime? Released_Date { get; set; }

        public MovieViewModel(JsonElement jsonObject)
        {
            Title = jsonObject.GetProperty("title").GetString();
            Released_Date = DateTime.TryParse(Utility.GetPropertyCaseInsensitive(jsonObject, "released_date"), out DateTime releasedDate) ? releasedDate : null;            
            Director = Utility.GetPropertyCaseInsensitive(jsonObject, "director");
            Producer = Utility.GetPropertyCaseInsensitive(jsonObject, "producer");
            
        }
    }
}
