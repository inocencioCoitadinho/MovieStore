using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieStore.Data;
using MovieStore.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace MovieStore.Models
{
    public class Movie
    {

        public Guid MovieId { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Runtime { get; set; }
        public string? Synopsis { get; set; }
        public string? PosterPath { get; set; }
        public DateTime Release_date { get; set; }

        public DateTime? InitRent { get; set; }

        public DateTime? EndRent { get; set; }

        public string? ApiId { get; set; }


        public static Movie MovieJsonToMovie(MovieJson movieJson, ConfigurationJson configJson)
        {
            Movie movie = new Movie();

            if (movieJson.title != null) movie.Title = movieJson.title;
            movie.Runtime = movieJson.runtime;
            if (movieJson.release_date != string.Empty ) movie.Release_date = DateTime.Parse(movieJson.release_date);
            if (movieJson.overview != null) movie.Synopsis = movieJson.overview;
            if (movieJson.poster_path != null && configJson.images.base_url != null) 
                movie.PosterPath = configJson.images.base_url + "w500"+ movieJson.poster_path;
            movie.ApiId = movieJson.id.ToString();

            return movie;
        }

        
        public static void InsertMovie(Movie movie, DateTime endDate, DateTime startDate)
        {

            using (MovieStoreContext dbContext = new MovieStoreContext())
            {
                
                movie.InitRent = startDate;
                movie.EndRent = endDate;
                dbContext.Add(movie);
                dbContext.SaveChanges();
            }
        }
        

    }

    }
