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

        public CastByMovieJson MovieCredits { get; set; }

        public PersonView(string personId)
        {
            string jsonStringConfig = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/configuration?api_key=5933922b6587d2d506362381025ef410");
            ConfigurationJson configJson = JsonConvert.DeserializeObject<ConfigurationJson>(jsonStringConfig);


            string actorJson = JSONMethods.JsonApiRequest("https://api.themoviedb.org/3/person/" + personId +
                "?api_key=5933922b6587d2d506362381025ef410&language=en-US");
            PersonJson = JsonConvert.DeserializeObject<PersonJson>(actorJson);

            //add correct profile picture path
            if (string.IsNullOrEmpty(PersonJson.profile_path))
                PersonJson.profile_path = "https://public.slidesharecdn.com/v2/images/user-48x48.png";
            else
                PersonJson.profile_path = configJson.images.base_url + "w45" + PersonJson.profile_path;


            string movieCredits = JSONMethods.JsonApiRequest(" https://api.themoviedb.org/3/person/" + personId + "" +
                "/movie_credits?api_key=5933922b6587d2d506362381025ef410&language=en-US");

            MovieCredits = JsonConvert.DeserializeObject<CastByMovieJson>(movieCredits);
        }
    }
}
