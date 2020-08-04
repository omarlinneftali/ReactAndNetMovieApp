using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoviesApi.Models;

namespace MoviesApi.DTO
{
    public class ActorDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

     public DateTime? BirthDate { get; set; }

        public int SexID { get; set; }

        public string SexName
        {
            get
            {

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
        public virtual ICollection<MovieDTO> Movies { get; set; }

    }
}