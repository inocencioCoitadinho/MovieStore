using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class MovieLanguage
    {
        public Guid MovieLanguageId { get; set; }
        public string Name { get; set; }
        public string NameJson { get; set; }


        public static List<MovieLanguage> GetMovieLanguages()
        {
            using (var context = new MovieStoreContext())
            {

                return context.MovieLanguage.ToList();
            }
        }

    }
}
