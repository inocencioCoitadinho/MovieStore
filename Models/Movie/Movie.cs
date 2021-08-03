using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MovieStore.Data;
using MovieStore.DB;
using MovieStore.Models.Genres;
using MovieStore.Models.MovieVideos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace MovieStore.Models.Movie
{
    public class Movie
    {

        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string? OriginalTitle { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Runtime { get; set; }
        public string? Synopsis { get; set; }
        public string? PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }

        public DateTime? InitRent { get; set; }

        public DateTime? EndRent { get; set; }
        public string? ApiId { get; set; }
        public Guid UserId { get; set; }
        public string OriginalLanguage { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Popularity { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }


        public static Movie MovieJsonToMovie(MovieJson movieJson, ConfigurationJson configJson)
        {
            Movie movie = new Movie();

            if (movieJson.title != null) movie.Title = movieJson.title;
            movie.Runtime = movieJson.runtime;
            movie.ApiId = movieJson.id.ToString();
            if (movieJson.release_date != string.Empty ) movie.ReleaseDate = DateTime.Parse(movieJson.release_date);
            if (movieJson.overview != null) movie.Synopsis = movieJson.overview;
            if (movieJson.poster_path != null && configJson.images.base_url != null) 
                movie.PosterPath = configJson.images.base_url + "w500"+ movieJson.poster_path;
            if (movieJson.original_language != null) movie.OriginalLanguage = movieJson.original_language;
            else movie.OriginalLanguage = "Not Defined";
            if (movieJson.original_title != null) movie.OriginalTitle = movieJson.original_title;
            movie.Popularity = (decimal)movieJson.popularity;

            return movie;
        }

        
        public static void InsertMovie(Movie movie, DateTime endDate, DateTime startDate, Guid userId)
        {
            using (MovieStoreContext dbContext = new MovieStoreContext())
            {
                movie.MovieId = Guid.NewGuid();
                movie.InitRent = startDate;
                movie.EndRent = endDate;
                movie.UserId = userId;
                dbContext.Add(movie);

                List<Genre> dbGenres = dbContext.Genre.ToList();

                List<MovieJson.Genre> movieGenres = JSONMethods.GetMovieGenres(movie.ApiId);
                foreach (var movieGenre in movieGenres)
                {
                    var x = dbGenres.Find(dbGenre => dbGenre.JsonGenreId == movieGenre.id.ToString());
                    if (x is null)
                    {
                        Genre insertGenre = new Genre();
                        insertGenre.GenreId = Guid.NewGuid();
                        insertGenre.JsonGenreId = movieGenre.id.ToString();
                        insertGenre.Name = movieGenre.name;
                        dbContext.Add(insertGenre);

                        MovieGenre insertedMovieGenre = new MovieGenre();
                        insertedMovieGenre.GenreId = insertGenre.GenreId;
                        insertedMovieGenre.MovieId = movie.MovieId;
                        dbContext.Add(insertedMovieGenre);
                    }
                    else
                    {
                        MovieGenre insertedMovieGenre = new MovieGenre();
                        insertedMovieGenre.GenreId = x.GenreId;
                        insertedMovieGenre.MovieId = movie.MovieId;
                        dbContext.Add(insertedMovieGenre);
                    }

                }
                dbContext.SaveChanges();
            }
        }


        public static List<Movie> GetMovieByUserId(string userId)
        {
            using (var context = new MovieStoreContext())
            {

                return context.Movie.Where(u => u.UserId == Guid.Parse(userId)).ToList();
            }
        }

        public static Movie GetMovieByJsonMovieId(string movieId)
        {
            using (var context = new MovieStoreContext())
            {

                return context.Movie.Where(u => u.ApiId == movieId).FirstOrDefault();
            }
        }

    }

    }
