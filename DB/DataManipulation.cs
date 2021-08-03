using MovieStore.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.DB
{
    public class DataManipulation
    {


        public static int CheckMovieReservationStatus(DateTime InitRent, DateTime endDate, Movie movie)
        {
            //1 if movie can be reserved
            int status = 1;

            
            if (InitRent > endDate)
            {
                status = 2;
            }
                
            if(Movie.GetMovieByJsonMovieId(movie.ApiId) != null)
            {
                status = 3;
            }

            if (movie.Popularity>1 && movie.Popularity<50)
            {
                Random random = new Random();
                int x = random.Next((int)movie.Popularity, 100);
                if (x < 7)
                    status = 4;
            }

            return status;
        }
    }
}
