using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models.Cast
{
    public class CreditDetailsJson
    {
        public string credit_type { get; set; }
        public string department { get; set; }
        public string job { get; set; }
        public Media media { get; set; }
        public string media_type { get; set; }
        public string id { get; set; }
        public Person person { get; set; }


        public class Media
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public List<int> genre_ids { get; set; }
            public string original_language { get; set; }
            public string original_title { get; set; }
            public string poster_path { get; set; }
            public bool video { get; set; }
            public double vote_average { get; set; }
            public string title { get; set; }
            public string overview { get; set; }
            public string release_date { get; set; } = "Not Defined";
            public int vote_count { get; set; }
            public int id { get; set; }
            public double popularity { get; set; }
            public string character { get; set; }
        }

        public class KnownFor
        {
            public double id { get; set; }
            public double vote_average { get; set; }
            public double vote_count { get; set; }
            public string first_air_date { get; set; }
            public string media_type { get; set; }
            public double popularity { get; set; }
            public string backdrop_path { get; set; }
            public string original_name { get; set; }
            public List<double> genre_ids { get; set; }
            public string overview { get; set; }
            public string original_language { get; set; }
            public string name { get; set; }
            public string poster_path { get; set; }
            public List<string> origin_country { get; set; }
        }

        public class Person
        {
            public int gender { get; set; }
            public List<KnownFor> known_for { get; set; }
            public string known_for_department { get; set; }
            public string profile_path { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public bool adult { get; set; }
            public double popularity { get; set; }
        }

    }
}
