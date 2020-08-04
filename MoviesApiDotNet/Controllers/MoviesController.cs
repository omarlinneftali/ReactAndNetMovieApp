using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using MoviesApi.DTO;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private DBContex _contex;

        public MoviesController()
        {
            _contex = new DBContex();
        }
        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            var MoviesDTO = _contex.Movies.Include("Genre").Select(Mapper.Map<Movie, MovieDTO>).ToList();

            if (MoviesDTO.Count == 0)
            {
                return NotFound();
            }


            return Ok(MoviesDTO);


        }
        [HttpGet]

        public IHttpActionResult GetMovie(int id)
        {
            var Movie = _contex.Movies.Include("Genre").SingleOrDefault(a => a.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }


            var MovieDTO = Mapper.Map<Movie, MovieDTO>(Movie);


            return Ok(MovieDTO);



        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDTO MovieDTO)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Valores Invalidos");
            }


            var movieWithTitleRepeted = _contex.Movies.Where(m => m.Title == MovieDTO.Title).ToList();

            if (movieWithTitleRepeted.Count != 0)
            {
                return BadRequest("Una pelicula con este titulo ya existe, No pueden existir dos peliculas con el mismo titulo");
            }

            var movieActorsDTOId = MovieDTO.actors.GroupBy(a => a.ID).Where(a => a.Count() > 1).ToList();

            if (movieActorsDTOId.Count != 0)
            {
                return BadRequest("Ha intentado ingresar un actor ya existente, El mismo Actor no puede existir mas de una vez en la misma pelicula");
            }








            var Movies = _contex.Movies.ToList();


            var MovieGenre = _contex.MovieGenres.SingleOrDefault(m => m.ID == MovieDTO.MovieGenreID);


            if (MovieGenre == null)
            {
                return BadRequest();
            }


            var Movie = Mapper.Map<MovieDTO, Movie>(MovieDTO);

            var actors = MovieDTO.actors.Select(a => a.ID);

            var Actors = _contex.Actors.Where(a => actors.Contains(a.ID)).ToList();

            Movie.Genre = MovieGenre;
            Movie.Actors = Actors;


            _contex.Movies.Add(Movie);



            _contex.SaveChanges();



            var movieDto = Mapper.Map<Movie, MovieDTO>(Movie);



            movieDto.ID = Movie.ID;

            return Created(new Uri(Request.RequestUri + "" + Movie.ID), movieDto);

        }


        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDTO MovieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieActorsDTOId = MovieDTO.actors.GroupBy(a => a.ID).Where(a => a.Count() > 1).ToList();

            if (movieActorsDTOId.Count != 0)
            {
                return BadRequest("El mismo Actor no puede existir mas de una vez en la misma pelicula");
            }



            var Movie = _contex.Movies.SingleOrDefault(a => a.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            var MovieGenre = _contex.MovieGenres.SingleOrDefault(m => m.ID == MovieDTO.MovieGenreID);

            if (MovieGenre == null)
            {
                return BadRequest();
            }











            Mapper.Map<MovieDTO, Movie>(MovieDTO, Movie);


            Movie.Genre = MovieGenre;


            var actors = MovieDTO.actors.Select(a => a.ID);

            var Actors = _contex.Actors.Where(a => actors.Contains(a.ID)).ToList();

            // Movie.Genre = MovieGenre;


            _contex.SaveChanges();

            MovieDTO.ID = Movie.ID;

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var Movie = _contex.Movies.SingleOrDefault(a => a.ID == id);

            if (Movie == null)
            {
                return NotFound();

            }

            _contex.Movies.Remove(Movie);

            _contex.SaveChanges();

            return Ok();
        }

    }

}
