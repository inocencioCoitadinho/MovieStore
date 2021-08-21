using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models;
using MovieStore.Models.Genres;
using MovieStore.Models.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.DB
{
    public class MovieStoreContext : IdentityDbContext
    {
        public DbSet<Movie> Movie { get; set; }

        public DbSet<MovieLanguage> MovieLanguage { get; set; }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MovieGenre>().HasKey(table => new {
                table.GenreId,
                table.MovieId
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\;Database=MovieStore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options)
            : base(options)
        {
        }
        public MovieStoreContext()
    : base()
        { }
    }
}
