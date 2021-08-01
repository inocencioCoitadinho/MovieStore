using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models.MovieVideos
{
    public class MovieVideo
    {
        public string Name { get; set; }
        public string YoutubeLink { get; set; }

        public MovieVideo(string name, string youtubeLink)
        {
            Name = name;
            YoutubeLink = youtubeLink;
        }
    }
}
