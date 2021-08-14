using MovieStore.DB;
using MovieStore.Models.Actor;
using MovieStore.Models.Cast;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models.Actor
{
    public class PersonView
    {
        public PersonJson PersonJson { get; set; }

        public List<CreditDetailsJson> MovieCredits { get; set; } = new List<CreditDetailsJson>();

        public string AlsoKnowAs { get; set; } = "";

        public PersonView(string personId)
        {
            int i = 0;

            string jsonStringConfig = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/configuration?api_key=5933922b6587d2d506362381025ef410");
            ConfigurationJson configJson = JsonConvert.DeserializeObject<ConfigurationJson>(jsonStringConfig);

            string actorJson = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/person/" + personId +
                "?api_key=5933922b6587d2d506362381025ef410&language=en-US");
            PersonJson = JsonConvert.DeserializeObject<PersonJson>(actorJson);

            //add correct profile picture path
            if (string.IsNullOrEmpty(PersonJson.profile_path))
                PersonJson.profile_path = "https://public.slidesharecdn.com/v2/images/user-48x48.png";
            else
                PersonJson.profile_path = configJson.images.base_url + "h632" + PersonJson.profile_path;

            //calculate age 
            PersonJson.birthday = PersonJson.birthday + "  (" + (int)((DateTime.Now - DateTime.Parse(PersonJson.birthday)).TotalDays / 365.2425) + " Years Old)";

            //Build the string of alternative names.

            for (; i < PersonJson.also_known_as.Count - 1; i++)
            {
                AlsoKnowAs += PersonJson.also_known_as[i] + ", ";
            }
            AlsoKnowAs += PersonJson.also_known_as[i] + ".\n\n";

            //get list with movie credits for this actor
            string movieCredits = JSONMethods.JsonApiRequest(" https://api.themoviedb.org/3/person/" + personId + "" +
                "/movie_credits?api_key=5933922b6587d2d506362381025ef410&language=en-US");

            CastByMovieJson list = JsonConvert.DeserializeObject<CastByMovieJson>(movieCredits);

            //get detailed credits for each credit for this actor
            foreach (CastByMovieJson.Cast cast in list.cast)
            {
                string movieJson = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/credit/" + cast.credit_id +
                    "?api_key=5933922b6587d2d506362381025ef410");
                var movie = JsonConvert.DeserializeObject<CreditDetailsJson>(movieJson);
                movie.media.poster_path = configJson.images.base_url + "w92" + movie.media.poster_path;
                MovieCredits.Add(movie);
            }
        }
    }
}
