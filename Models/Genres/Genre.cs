using MovieStore.Data;
using MovieStore.DB;
using MovieStore.Models.MovieVideos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models.Genres
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
        public string JsonGenreId { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }

        public static List<Genre> GetGenres()
        {
            using (var context = new MovieStoreContext())
            {

                return context.Genre.ToList();
            }
        }

    }
}
