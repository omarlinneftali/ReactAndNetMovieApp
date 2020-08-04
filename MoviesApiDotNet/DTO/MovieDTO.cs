using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoviesApi.Models;

namespace MoviesApi.DTO
{
    public class MovieDTO
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        
        public  MovieGenreDTO Genre { get; set; }

        [Required]
        public int MovieGenreID { get; set; }


        public DateTime ReleaseDate { get; set; }

       
        public virtual ICollection<MovieActorDTO> actors{
            get;
            set;
        }




    }
}