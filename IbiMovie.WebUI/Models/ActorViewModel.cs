using IbiMovie.WebUI.Shared;
using System.Text.Json;

namespace IbiMovie.WebUI.Models
{
    public class ActorViewModel
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Mass { get; set; }
        public string Hair_Color { get; set; }
        public string Skin_Color { get; set; }
        public string Eye_Color { get; set; }
        public string Birth_Year { get; set; }
        public string Gender { get; set; }        
        public List<MovieViewModel> Movies { get; set; }
        public ActorViewModel(JsonElement jsonObject)
        {
            Name = Utility.GetPropertyCaseInsensitive(jsonObject, "name");
            Height = int.TryParse(Utility.GetPropertyCaseInsensitive(jsonObject, "height"), out int height) ? height : 0;
            Mass = int.TryParse(Utility.GetPropertyCaseInsensitive(jsonObject, "mass"), out int mass) ? mass : 0;
            Hair_Color = Utility.GetPropertyCaseInsensitive(jsonObject,"hair_color");
            Skin_Color = Utility.GetPropertyCaseInsensitive(jsonObject,"skin_color");
            Eye_Color = Utility.GetPropertyCaseInsensitive(jsonObject,"eye_color");
            Birth_Year = Utility.GetPropertyCaseInsensitive(jsonObject,"birth_year");
            Gender = Utility.GetPropertyCaseInsensitive(jsonObject, "gender");
        }       
    }
}
