using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models.MovieVideos
{
    public class MovieVideos
    {
        public Guid MovieVideoId { get; set; }
        public string Name { get; set; }
        public string YoutubeLink { get; set; }

    }
}
