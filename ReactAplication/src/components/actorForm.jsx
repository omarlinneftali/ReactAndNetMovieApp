import React from "react";
import Joi from "joi-browser";
import Form from "./common/form";
import { getMovie } from "../services/actorsService";
import { getActors } from "../services/actorsService";

import { getActor } from "../services/actorsService";
import { saveActor } from "../services/actorsService";
import { getMovies } from "../services/movieService";
import { toast } from "react-toastify";

class ActorForm extends Form {
  state = {
    data: {
      id: 0,
      name: "",
      sexId: 1,
      birthDate: "",
      movies: [],
    },
    sex: [
      { name: "Masculino", id: 1 },
      { name: "Femenino", id: 2 },
    ],
    errors: {},
  };
  schema = {
    id: Joi.number(),
    name: Joi.string().required().label("Name"),
    sexId: Joi.required().label("Sex"),
    birthDate: Joi.date().required().label("Birthdate"),
    movies: Joi.label("actors"),
  };

  async populateMovies() {
    const { data: movies } = await getMovies();
    this.setState({ movies });

    console.log(movies);
  }

  async populateActors() {
    try {
      const actorId = this.props.match.params.id;
      if (actorId === "new") return;
      const { data: actor } = await getActor(actorId);
      this.setState({ data: this.mapToViewModel(actor) });
    } catch (error) {
      if (error.response && error.response.status === 404)
        this.props.history.replace("/not-found");
    }
  }
  async componentDidMount() {
    await this.populateMovies();
    await this.populateActors();
  }

  mapToViewModel = (actor) => {
    return {
      id: actor.id,
      name: actor.name,
      sexId: actor.sexId,
      birthDate: actor.birthDate,
      movies: actor.movies,
    };
  };

  doSubmit = async () => {
    console.log(this.state.data);

    try {
      var result = await saveActor(this.state.data);
      this.props.history.push("/actors");
    } catch (error) {
      if (error.response && error.response.status === 400)
        toast.error(error.response.data.message);
    }
  };

  render() {
    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          {this.renderInput("name", "Name")}
          {this.renderInputDate("birthDate", "Birthdate", "date")}
          {this.renderSelect("sexId", "Sexo", this.state.sex)}

          {this.renderButton("Save")}
        </form>
        <br />
        Peliculas en las que participa
        <ul className="list-group">
          {this.state.data.movies.map((movie) => (
            <li className="list-group-item" key={movie.id}>
              {movie.title}
            </li>
          ))}
        </ul>
      </div>
    );
  }
}

export default ActorForm;
