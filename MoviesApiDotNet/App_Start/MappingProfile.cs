using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MoviesApi.DTO;
using MoviesApi.Models;

namespace MoviesApi.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()

        {
            // domain to Dto
            Mapper.CreateMap<Movie, MovieDTO>();
            Mapper.CreateMap<Actor, ActorDTO>();
            Mapper.CreateMap<MovieGenre, MovieGenreDTO>();



            //Dto to Domain

            Mapper.CreateMap<MovieDTO, Movie>().ForMember(m => m.ID, opt => opt.Ignore()); 
            Mapper.CreateMap<ActorDTO, Actor>().ForMember(m => m.ID, opt => opt.Ignore());
            Mapper.CreateMap<Actor, MovieActorDTO>();

            Mapper.CreateMap<MovieGenreDTO, MovieGenre>().ForMember(m => m.ID, opt => opt.Ignore());

        }

    }


}