using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
        public string JsonGenreId { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
