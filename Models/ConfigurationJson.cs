using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class ConfigurationJson
    {
        public Images images { get; set; }
        public IList<string> change_keys { get; set; }

        
        public class Images
        {
            public string base_url { get; set; }
            public string secure_base_url { get; set; }
            public IList<string> backdrop_sizes { get; set; }
            public IList<string> logo_sizes { get; set; }
            public IList<string> poster_sizes { get; set; }
            public IList<string> profile_sizes { get; set; }
            public IList<string> still_sizes { get; set; }

        }
        


    }
}
