using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbiMovie.Core
{
    public class Movie    {
        public int Id { get; set; }
        public string Title { get; set; }        

        private int _releasedYear;
        public int ReleasedYear
        {
            get => _releasedYear;
            set
            {
                ValidateReleaseYear(value);
                _releasedYear = value;
            }
        }        
        private void ValidateReleaseYear(int year)
        {
            if (year < 1800)
            {
                throw new ArgumentException("Movie released year must be later than 1800.");
            }
        }
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }

}
