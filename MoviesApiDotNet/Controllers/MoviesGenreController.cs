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

    public class MoviesGenreController : ApiController
    {
        private DBContex _contex;

        public MoviesGenreController()
        {
            _contex = new DBContex();
        }
        [HttpGet]
        public IHttpActionResult GetMoviesGenres()
        {
            var moviesGenresDTO = _contex.MovieGenres.Select(Mapper.Map<MovieGenre, MovieGenreDTO>).ToList();

            if (moviesGenresDTO.Count == 0)
            {
                return NotFound();
            }


            return Ok(moviesGenresDTO);


        }
        [HttpGet]

        public IHttpActionResult GetMoviesGenres(int id)
        {
            var movieGenre = _contex.MovieGenres.SingleOrDefault(a => a.ID == id);

            if (movieGenre == null)
            {
                return NotFound();
            }


            var MovieGenreDTO = Mapper.Map<MovieGenre, MovieGenreDTO>(movieGenre);


            return Ok(MovieGenreDTO);



        }
        [HttpPost]
        public IHttpActionResult CreateMovieGenres(MovieGenreDTO movieGenreDTO)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var movieGenre = Mapper.Map<MovieGenreDTO, MovieGenre>(movieGenreDTO);

            _contex.MovieGenres.Add(movieGenre);

            _contex.SaveChanges();

            movieGenreDTO.ID = movieGenre.ID;

            return Created(new Uri(Request.RequestUri + "" + movieGenre.ID), movieGenreDTO);

        }


        [HttpPut]
        public IHttpActionResult UpdateMovieGenres(int id, MovieGenreDTO movieGenreDTO)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }


            var movieGenre = _contex.MovieGenres.SingleOrDefault(a => a.ID == id);

            if (movieGenre == null)
            {
                return NotFound();
            }


            Mapper.Map<MovieGenreDTO, MovieGenre>(movieGenreDTO, movieGenre);


            _contex.SaveChanges();

            movieGenreDTO.ID = movieGenre.ID;

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteMovieGenre(int id)
        {
            var movieGenre = _contex.MovieGenres.SingleOrDefault(a => a.ID == id);

            if (movieGenre == null)
            {
                return NotFound();

            }

            _contex.MovieGenres.Remove(movieGenre);

            _contex.SaveChanges();

            return Ok();
        }

    }
}
