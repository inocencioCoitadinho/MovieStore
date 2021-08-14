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

        public string Director { get; set; } = "Unknown.";

        public string Screenplay { get; set; } = "Unknown.";

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

            if(video != null)
                YoutubeVideo = new MovieVideo(video.name, "https://www.youtube.com/embed/" + video.key + "?autoplay=0");
            #endregion

            //Genres
            Genres = JSONMethods.GetMovieGenres(movie.ApiId);

            //Cast and Director
            Cast = JSONMethods.GetCastByMovieId(movie.ApiId);
            Director = Cast.crew.Find(x => x.department == "Directing" && x.job == "Director").name;

            //writing team
            var writers = Cast.crew.FindAll(x => x.department == "Writing" && x.job == "Story");

            if (writers.Count > 0)
            {
                Screenplay = "Written by : ";
                foreach(var t in writers)
                {
                    Screenplay += t.name + ", ";
                }
                Screenplay = Screenplay.Remove(Screenplay.Length - 2);
            }
            


        }
    }
}
