import React, { Component } from "react";
import "./App.css";
import { Route, Redirect, Switch } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import jwtDecode from "jwt-decode";
import Movie from "./components/movie";
import MovieForm from "./components/movieForm";
import Actor from "./components/actor";
import ActorForm from "./components/actorForm";

import NotFound from "./components/notFound";
import NavBar from "./components/navbar";
import ProtectedRoute from "./components/common/ProtectedRoute";
import auth from "./services/authService";

import "react-toastify/dist/ReactToastify.css";

class App extends Component {
  state = {};
  componentDidMount() {}

  render() {
    return (
      <>
        <ToastContainer />
        <NavBar />
        <main className="container">
          <Switch>
            <Route path="/movies/:id" component={MovieForm} />
            <Route path="/movies" render={(props) => <Movie {...props} />} />
            <Route path="/actors/:id" component={ActorForm} />

            <Route path="/actors" component={Actor} />

            <Route path="/not-found" component={NotFound} />

            <Redirect from="/" to="/movies" exact />
            <Redirect to="/not-found" />
          </Switch>
        </main>
      </>
    );
  }
}

export default App;
