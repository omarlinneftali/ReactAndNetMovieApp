import React, { Component } from "react";
import Like from "./common/like";
import Table from "./common/table";
import { Link } from "react-router-dom";
import ModalBox from "./modal";

import auth from "../services/authService";

class MovieTable extends Component {
  deleteColumn = {
    key: "delete",
    content: (movie) => (
      <i
        className="fa fa-trash fa-2x text-danger"
        style={{ cursor: "pointer" }}
        onClick={() => this.props.onDeleteMovie(movie)}
        aria-hidden="true"
      ></i>
    ),
  };
  columns = [
    {
      path: "title",
      label: "Titulo",
      content: (movie) => (
        <Link to={`/movies/${movie.id}`}> {movie.title}</Link>
      ),
    },

    { path: "releaseDate", label: "Fecha de Lanzamiento" },
    { path: "genre.name", label: "Genero" },

    {
      key: "like",
      content: (movie) => (
        <Like
          liked={movie.liked}
          onLiked={() => this.props.onLikedMovie(movie)}
        />
      ),
    },
    {
      path: "details",
      label: "detalles",
      content: (movie) => (
        <ModalBox
          options={movie.actors}
          title="Detalles"
          nameProperty="name"
          header="Actores que participan"
        />
      ),
    },
    this.deleteColumn,
  ];

  constructor() {
    super();
  }

  render() {
    const { movies, onSortMovie, sortColumn } = this.props;

    return (
      <Table
        columns={this.columns}
        sortColumn={sortColumn}
        onSort={onSortMovie}
        data={movies}
      />
    );
  }
}

export default MovieTable;
