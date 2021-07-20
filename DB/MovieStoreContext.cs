using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Data
{
    public class MovieStoreContext : IdentityDbContext
    {
        public DbSet<Movie> Movie { get; set; }

        public DbSet<MovieLanguage> MovieLanguage { get; set; }

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
