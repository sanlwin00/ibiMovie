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
        public string? Director { get; set; }
        public string? Producer { get; set; }

        private DateTime? _released_date;
        public DateTime? Released_Date
        {
            get => _released_date;
            set
            {
                ValidateReleaseYear(value);
                _released_date = value;
            }
        }        
        private void ValidateReleaseYear(DateTime? released_date)
        {
            if (released_date.HasValue)
            {
                if (released_date.Value.Year < 1800)
                    throw new ArgumentException("Movie released year must be later than 1800.");
            }
        }
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }

}
