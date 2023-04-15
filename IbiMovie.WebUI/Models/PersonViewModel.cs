using System.Text.Json;

namespace IbiMovie.WebUI.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Mass { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string BirthYear { get; set; }
        public string Gender { get; set; }
        public string HomeWorldUrl { get; set; }
        public List<string> Films { get; set; }
        public List<string> Species { get; set; }
        public List<string> Vehicles { get; set; }
        public List<string> Starships { get; set; }

        public PersonViewModel(JsonElement personData)
        {
            Name = personData.GetProperty("name").GetString();
            Height = int.TryParse(personData.GetProperty("height").GetString(), out int height) ? height : 0;
            Mass = int.TryParse(personData.GetProperty("mass").GetString(), out int mass) ? mass : 0;
            HairColor = personData.GetProperty("hair_color").GetString();
            SkinColor = personData.GetProperty("skin_color").GetString();
            EyeColor = personData.GetProperty("eye_color").GetString();
            BirthYear = personData.GetProperty("birth_year").GetString();
            Gender = personData.GetProperty("gender").GetString();
        }
    }
}
