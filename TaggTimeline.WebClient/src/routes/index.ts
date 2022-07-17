import { useRoutes } from "react-router-dom";
import AuthRoutes from "./AuthRoutes";
import MainRoutes from "./MainRoutes";

/**
 * Returns all the route-level routes for this app
 * @returns All routes for this app
 */
export default function Routes() {
  return useRoutes([MainRoutes, AuthRoutes]);
}
