import http from "./httpService";
import auth from "./authService";
import { apiUrl } from "../config.json";

const apiEndpoint = apiUrl + "/movies";
function movieUrl(movieId) {
  return `${apiEndpoint}/${movieId}`;
}

export function getMovies() {
  return http.get(apiEndpoint);
}

export function getMovie(movieId) {
  return http.get(movieUrl(movieId));
}
export function saveMovie(movie) {
  if (movie.id) {
    const body = { ...movie };
    delete body.id;
    return http.put(movieUrl(movie.id), body);
  }
  return http.post(apiEndpoint, movie);
}

export function deleteMovie(movieId) {
  return http.delete(movieUrl(movieId));
}
