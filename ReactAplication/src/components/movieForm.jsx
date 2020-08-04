import React from "react";
import Joi from "joi-browser";
import Form from "./common/form";
import { getMovie } from "../services/movieService";
import { getActors } from "../services/actorsService";

import { getGenres } from "../services/genreService";
import { saveMovie } from "../services/movieService";
import { toast } from "react-toastify";
import moment from "moment";

class MovieForm extends Form {
  state = {
    data: {
      id: 0,
      title: "",
      movieGenreId: 0,
      releaseDate: "",
      actors: [],
    },
    actor: 0,
    genres: [],
    actors: [],
    errors: {},
  };
  schema = {
    id: Joi.number(),
    title: Joi.string().required().label("Title"),
    movieGenreId: Joi.number().required().label("Genre"),
    releaseDate: Joi.date().required().label("Realese Date"),
    genre: Joi.label("gender"),
    actors: Joi.label("actors"),
  };

  async populateGenre() {
    const { data: genres } = await getGenres();
    this.setState({ genres });
  }

  async populateActors() {
    const { data: actors } = await getActors();
    this.setState({ actors });

    console.log(actors);
  }

  async populateMovie() {
    try {
      const movieId = this.props.match.params.id;
      if (movieId === "new") return;
      const { data: movie } = await getMovie(movieId);
      this.setState({ data: this.mapToViewModel(movie) });
    } catch (error) {
      if (error.response && error.response.status === 404)
        this.props.history.replace("/not-found");
    }
  }
  async componentDidMount() {
    await this.populateGenre();
    await this.populateMovie();
    await this.populateActors();
  }

  mapToViewModel = (movie) => {
    return {
      id: movie.id,
      title: movie.title,
      genre: movie.genre,
      movieGenreId: movie.genre.id,
      releaseDate: movie.releaseDate,
      actors: movie.actors,
    };
  };

  handleActorChange = ({ currentTarget: input }) => {
    const errors = { ...this.state.errors };
    const data = { ...this.state.data };
    const value = this.state.actors.find((a) => a.id == input.value);
    data["actors"].push({ ...value });

    let actor = input.value;

    let actorIndex = this.state.actors.findIndex((a) => a.id === actor);
    this.state.actors.splice(actorIndex, 1);

    this.setState({ data, errors, actor });
  };

  handleActorDelete = (actor) => {
    const data = { ...this.state.data };
    const filteredActors = data.actors.filter((a) => a.id !== actor.id);
    data.actors = filteredActors;

    this.setState({ data });
  };

  doSubmit = async () => {
    try {
      var result = await saveMovie(this.state.data);
      this.props.history.push("/movies");
    } catch (error) {
      if (error.response && error.response.status === 400)
        toast.error(error.response.data.message);
    }
  };

  render() {
    console.log(this.state.data);
    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          {this.renderInput("title", "Titulo")}
          {this.renderSelect("movieGenreId", "Genero", this.state.genres)}
          {this.renderInputDate("releaseDate", "Fecha de Lanzamiento", "date")}
          {this.renderActorSelect("actor", "Agregar Actor", this.state.actors)}

          {this.renderButton("Save")}
        </form>
        <br />
        Actores que participan
        <ul className="list-group">
          {this.state.data.actors.map((actor) => (
            <li className="list-group-item" key={actor.id}>
              {actor.name}

              <i
                className=" ml-2 fa fa-trash fa-2x text-danger"
                style={{ cursor: "pointer" }}
                onClick={() => this.handleActorDelete(actor)}
                aria-hidden="true"
              ></i>
            </li>
          ))}
        </ul>
      </div>
    );
  }
}

export default MovieForm;
