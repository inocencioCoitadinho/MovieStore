using MovieStore.Models.MovieListJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Models.MovieVideos;
using static MovieStore.Models.MoviesSearchListJson;
using MovieStore.DB;
using Newtonsoft.Json;

namespace MovieStore.Models.Movie
{
    public class MovieSearchJsonView
    {
        public Result Movie { get; set; }
        public MovieVideo YoutubeVideo { get; set; }

        //Creates a List of Movies with Youtube links for a view
        public static List<MovieSearchJsonView> Create(MoviesSearchListJson list)
        {
            List<MovieSearchJsonView> returnList = new List<MovieSearchJsonView>();

            //jsonConfig for poster path
            string jsonStringConfig = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/configuration?api_key=5933922b6587d2d506362381025ef410");
            ConfigurationJson configJson = JsonConvert.DeserializeObject<ConfigurationJson>(jsonStringConfig);

            foreach (var movie in list.Results)
            {
                string jsonStringVideo = JSONMethods.JsonApiRequest
                    ("https://api.themoviedb.org/3/movie/"+ movie.id + "/videos?api_key=5933922b6587d2d506362381025ef410&language=en-US");

                //list of Videos for each movie
                MovieVideoJson videoList = JsonConvert.DeserializeObject<MovieVideoJson>(jsonStringVideo);
                //select the principal Youtube Link to show
                List<MovieVideoJson.Result> SortedVideoList = videoList.results.OrderBy(o => o.published_at).ToList();
                MovieVideoJson.Result video = SortedVideoList.Find(o => o.type == "Trailer");

                MovieSearchJsonView add = new MovieSearchJsonView();
                add.Movie = movie;
                add.Movie.poster_path = configJson.images.base_url + "w154" + add.Movie.poster_path;
                if(video != null)
                    add.YoutubeVideo = new MovieVideo(video.name, "https://www.youtube.com/embed/" + video.key+ "?autoplay=0");

                returnList.Add(add);
            }

            return returnList;

        }
    }
}
