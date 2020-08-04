using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesApi.Models
{
    public class DBContex: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Sex> Sexes { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }


        public DBContex()
            :base("InitialDB")
        {
            
        }
    }
}