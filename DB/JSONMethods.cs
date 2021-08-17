using MovieStore.Models;
using MovieStore.Models.Cast;
using MovieStore.Models.Movie;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MovieStore.DB
{
    public class JSONMethods
    {
        public static string JsonApiRequest(string path)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create
                (string.Format(path));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }
            return jsonString;
        }

        public static Movie GetMovie(string jsonId)
        {
            string jsonStringMovie = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/movie/" +
                jsonId + "?api_key=5933922b6587d2d506362381025ef410");
            MovieJson movieJson = JsonConvert.DeserializeObject<MovieJson>(jsonStringMovie);

            string jsonStringConfig = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/configuration?api_key=5933922b6587d2d506362381025ef410");
            ConfigurationJson configJson = JsonConvert.DeserializeObject<ConfigurationJson>(jsonStringConfig);

            return Movie.MovieJsonToMovie(movieJson, configJson);
        }

        public static List<MovieJson.Genre> GetMovieGenres(string jsonId)
        {

            string jsonStringMovie = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/movie/" +
                jsonId + "?api_key=5933922b6587d2d506362381025ef410");
            MovieJson movieJson = JsonConvert.DeserializeObject<MovieJson>(jsonStringMovie);

            return movieJson.genres;
        }

        public static CastByMovieJson GetCastByMovieId(string jsonId)
        {

            string jsonStringMovie = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/movie/" +
                jsonId + "/credits?api_key=5933922b6587d2d506362381025ef410&language=en-US");
            CastByMovieJson castJson = JsonConvert.DeserializeObject<CastByMovieJson>(jsonStringMovie);

            string jsonStringConfig = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/configuration?api_key=5933922b6587d2d506362381025ef410");
            ConfigurationJson configJson = JsonConvert.DeserializeObject<ConfigurationJson>(jsonStringConfig);


            castJson.cast = castJson.cast.Take(15).ToList();
            //set up correct profile path, and give a generic one if it does not exist
            foreach (var a in castJson.cast)
            {
                if (string.IsNullOrEmpty(a.profile_path))
                    a.profile_path = "https://public.slidesharecdn.com/v2/images/user-48x48.png";
                else
                    a.profile_path = configJson.images.base_url + "w45" + a.profile_path;
            }

            return castJson;
        }

        public static string BuildSearchString(string searchString, string language, int page)
        {
            string queryFinal = "&query=" + searchString;
            if (language != null)
            {
                queryFinal += "&language=" + language;
            }
            if (page != 0)
            {
                queryFinal += "&page=" + page;
            }
            return queryFinal;
        }

    }
}
