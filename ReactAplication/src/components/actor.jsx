import React, { Component } from "react";
import { getActors } from "../services/actorsService";
import { getGenres } from "../services/genreService";
import ActorTable from "./actorTable";
import Pagination from "./common/pagination";
import ListGroup from "./common/ListGroup";
import { paginate } from "../utils/paginate";
import _ from "lodash";
import { Link } from "react-router-dom";
import SearchBox from "./common/searchBox";
import { deleteActor } from "../services/actorsService";
import { getMovies } from "../services/movieService";

import { toast } from "react-toastify";

class Actor extends Component {
  state = {
    actors: [],

    pageSize: 4,
    currentPage: 1,
    searchQuery: "",
    sortColumn: { path: "name", order: "asc" },
  };

  async componentDidMount() {
    const { data: actors } = await getActors();
    console.log(actors);
    this.setState({ actors });
  }

  handleDelete = async (actor) => {
    const originalActors = this.state.actors;
    const actors = originalActors.filter((a) => a.id !== actor.id);
    this.setState({ actors });

    try {
      await deleteActor(actor.id);
    } catch (error) {
      if (error.response && error.response.status === 404)
        toast.error("this actor has been deleted");

      this.setState({ actors: originalActors });
    }
  };

  handlePageChange = (currentPage) => {
    this.setState({ currentPage });
  };

  handleSearch = (searchQuery) => {
    this.setState({ searchQuery, currentPage: 1 });
  };

  handleSort = (sortColumn) => {
    this.setState({ sortColumn });
  };

  extratMovies(allActors, currentPage, pageSize) {
    let ActorCount = paginate(allActors, currentPage, pageSize).length;

    if (ActorCount <= 0) {
      this.setState({ currentPage: 1, searchQuery: "" });

      return [];
    }
    return paginate(allActors, currentPage, pageSize);
  }
  getPageData() {
    let {
      actors: allActors,
      currentPage,
      pageSize,
      searchQuery,
      sortColumn,
    } = this.state;

    let filteredActors = allActors;
    if (searchQuery)
      filteredActors = allActors.filter((a) =>
        a.name.toLowerCase().startsWith(searchQuery.toLowerCase())
      );

    let sortedActors = _.orderBy(
      filteredActors,
      [sortColumn.path],
      [sortColumn.order]
    );

    let actors = paginate(sortedActors, currentPage, pageSize);

    return { filteredActors, actors };
  }

  render() {
    const { length: count } = this.state.actors;

    let { currentPage, pageSize, sortColumn } = this.state;

    let { filteredActors, actors } = this.getPageData();

    return (
      <div className="row">
        <div className="col">
          <p>
            <span>Hay {filteredActors.length} disponibles</span>
          </p>
          <SearchBox
            value={this.state.searchQuery}
            onChange={this.handleSearch}
          />
          {
            <Link
              to="/actors/new"
              className="btn btn-primary"
              style={{ marginBottom: 20 }}
            >
              New
            </Link>
          }

          <ActorTable
            actors={actors}
            onDeleteActor={this.handleDelete}
            sortColumn={sortColumn}
            onSortActor={this.handleSort}
            options={this.state.movies}
          />

          <Pagination
            totalCount={filteredActors.length}
            pageSize={pageSize}
            onPageChange={this.handlePageChange}
            currentPage={currentPage}
          />
        </div>
      </div>
    );
  }
}

export default Actor;
