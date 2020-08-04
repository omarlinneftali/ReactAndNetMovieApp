import http from "./httpService";
import { apiUrl } from "../config.json";

const apiEndpoint = apiUrl + "/moviesgenre";

export function getGenres() {
  return http.get(apiEndpoint);
}
