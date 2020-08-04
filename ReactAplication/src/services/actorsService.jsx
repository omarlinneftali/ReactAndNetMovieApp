import http from "./httpService";
import auth from "./authService";
import { apiUrl } from "../config.json";

const apiEndpoint = apiUrl + "/actors";
function actorUrl(actorId) {
  return `${apiEndpoint}/${actorId}`;
}

export function getActors() {
  return http.get(apiEndpoint);
}

export function getActor(actorId) {
  return http.get(actorUrl(actorId));
}
export function saveActor(actor) {
  if (actor.id) {
    const body = { ...actor };
    delete body.id;
    return http.put(actorUrl(actor.id), body);
  }
  return http.post(apiEndpoint, actor);
}

export function deleteActor(actorId) {
  return http.delete(actorUrl(actorId));
}
