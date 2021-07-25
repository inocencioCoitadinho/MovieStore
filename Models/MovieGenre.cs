using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Models
{
    public class MovieGenre
    {
        [Key, Column(Order = 0)]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        [Key, Column(Order = 1)]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public MovieGenre()
        {
        }

        public MovieGenre(Guid movieId, Guid genreId)
        {
            MovieId = movieId;
            GenreId = genreId;
        }

    }
}
