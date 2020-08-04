using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesApi.Models
{
    public class MovieGenre
    {
        public int ID { get; set; }

        [Required]
        public  string Name { get; set; }

    }
}