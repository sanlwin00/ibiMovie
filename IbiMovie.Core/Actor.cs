using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbiMovie.Core
{
    public class Actor
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Height { get; set; }
        public int? Mass { get; set; }
        public string? Hair_Color { get; set; }
        public string? Skin_Color { get; set; }
        public string? Eye_Color { get; set; }
        public string? Birth_Year { get; set; }
        public string? Gender { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
