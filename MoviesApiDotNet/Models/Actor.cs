using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MoviesApi.Models
{
    public class Actor
    {
        public int ID { get; set; }

        [Required] public string Name { get; set; }


        public DateTime? BirthDate { get; set; }



        public int SexID { get; set; }


        public string SexName
        {
            get {
                
                    if (SexID == 1)
                    {
                        return "Masculino";
                    }
                    else if (SexID == 2)
                    {
                        return "Femenino";
                    }
                    else
                    {
                        return "indefinido";
                    }
                
            }

        }
        public virtual ICollection<Movie> Movies { get; set; }






    }
}