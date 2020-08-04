import React, { Component } from "react";
import Like from "./common/like";
import Table from "./common/table";
import { Link } from "react-router-dom";
import auth from "../services/authService";
import ModalBox from "./modal";

class ActorTable extends Component {
  deleteColumn = {
    key: "delete",
    content: (actor) => (
      <i
        className="fa fa-trash fa-2x text-danger"
        style={{ cursor: "pointer" }}
        onClick={() => this.props.onDeleteActor(actor)}
        aria-hidden="true"
      ></i>
    ),
  };
  columns = [
    {
      path: "name",
      label: "Name",
      content: (actor) => <Link to={`/actors/${actor.id}`}> {actor.name}</Link>,
    },

    { path: "birthDate", label: "Fecha de Nacimiento" },
    { path: "sexName", label: "Sexo" },
    {
      path: "details",
      label: "Detalles",
      content: (actor) => (
        <ModalBox
          options={actor.movies}
          title="Detalles"
          nameProperty="title"
          header="Peliculas en las que participa"
        />
      ),
    },

    this.deleteColumn,
  ];

  constructor() {
    super();
  }

  render() {
    const { actors, onSortActor, sortColumn } = this.props;

    return (
      <Table
        columns={this.columns}
        sortColumn={sortColumn}
        onSort={onSortActor}
        data={actors}
      />
    );
  }
}

export default ActorTable;
