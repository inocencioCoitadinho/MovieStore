using MovieStore.DB;
using MovieStore.Models.Cast;
using MovieStore.Models.Genres;
using MovieStore.Models.MovieVideos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models.Movie
{
    public class MovieView
    {
        public Movie Movie { get; set; }

        public CastByMovieJson Cast { get; set; }

        public MovieVideo YoutubeVideo { get; set; }

        public List<MovieJson.Genre> Genres { get; set; }


        public MovieView(Movie movie)
        {
            Movie = movie;


            #region Trailer
            //Youtbe trailer
            string jsonStringVideo = JSONMethods.JsonApiRequest
                        ("https://api.themoviedb.org/3/movie/" + movie.ApiId + "/videos?api_key=5933922b6587d2d506362381025ef410&language=en-US");

            //list of Videos for each movie
            MovieVideoJson videoList = JsonConvert.DeserializeObject<MovieVideoJson>(jsonStringVideo);

            //select the principal Youtube Link to show
            List<MovieVideoJson.Result> SortedVideoList = videoList.results.OrderBy(o => o.published_at).ToList();
            MovieVideoJson.Result video = SortedVideoList.Find(o => o.type == "Trailer");

            YoutubeVideo = new MovieVideo(video.name, "https://www.youtube.com/embed/" + video.key + "?autoplay=0");
            #endregion

            //Genres
            Genres = JSONMethods.GetMovieGenres(movie.ApiId);

            //Cast
            Cast = JSONMethods.GetCastByMovieId(movie.ApiId);

        }
    }
}
