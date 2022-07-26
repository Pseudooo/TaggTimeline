import { RouteObject } from "react-router-dom";
import BarebonesLayout from "../layouts/BarebonesLayout";
import { Home } from "../views/Home";

/**
 * Routes used in the main app.
 * All routes here will fit nicely into the main layout automatically.
 */
const MainRoutes: RouteObject = {
  path: "/",
  element: <BarebonesLayout center />,
  children: [
    {
      path: "/",
      element: <Home />,
    },
  ],
};

export default MainRoutes;
