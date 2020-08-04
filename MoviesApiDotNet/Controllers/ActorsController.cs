using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MoviesApi.DTO;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ActorsController : ApiController
    {
        private DBContex _contex;

        public ActorsController()
        {
            _contex = new DBContex();
        }
        [HttpGet]
        public IHttpActionResult GetActors()
        {
            var actorsDTO = _contex.Actors.Select(Mapper.Map<Actor, ActorDTO>).ToList();

            if (actorsDTO.Count == 0)
            {
                return NotFound();
            }


            return Ok(actorsDTO);


        }
        [HttpGet]

        public IHttpActionResult GetActor(int id)
        {
            var actor = _contex.Actors.SingleOrDefault(a => a.ID == id);

            if (actor == null)
            {
                return NotFound();
            }


            var actorDTO = Mapper.Map<Actor, ActorDTO>(actor);


            return Ok(actorDTO);



        }
        [HttpPost]
        public IHttpActionResult CreateActor(ActorDTO actorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Existen Campos  invalidos");
            }





            var movies = actorDTO.Movies.Select(a => a.ID);

            var Movies = _contex.Movies.Where(a => movies.Contains(a.ID)).ToList();


            var actor = Mapper.Map<ActorDTO, Actor>(actorDTO);

            actor.Movies = Movies;

            try
            {
                _contex.Actors.Add(actor);

                _contex.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }


            actorDTO.ID = actor.ID;

            return Created(new Uri(Request.RequestUri + "" + actor.ID), actorDTO);

        }


        [HttpPut]
        public IHttpActionResult UpdateActor(int id, ActorDTO actorDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Existen Campos invalidos");
            }


            var actor = _contex.Actors.SingleOrDefault(a => a.ID == id);

            if (actor == null)
            {
                return NotFound();
            }


            Mapper.Map<ActorDTO, Actor>(actorDTO, actor);


            var movies = actorDTO.Movies.Select(a => a.ID);

            var Movies = _contex.Movies.Where(a => movies.Contains(a.ID)).ToList();


            actor.Movies = Movies;



            _contex.SaveChanges();

            actorDTO.ID = actor.ID;

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteActor(int id)
        {
            var actor = _contex.Actors.SingleOrDefault(a => a.ID == id);

            if (actor == null)
            {
                return NotFound();

            }

            _contex.Actors.Remove(actor);

            _contex.SaveChanges();

            return Ok();
        }

    }
}
