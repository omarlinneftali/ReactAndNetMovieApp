using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoviesApi.Models
{
    public class Movie
    {
        public  int ID { get; set; }

        [Required]
        public  string Title { get; set; }


        [ForeignKey("Genre")]

        public int MovieGenreId { get; set; }

        public MovieGenre Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        







    }
}