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
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
