using MovieStore.Models;
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


    }
}
