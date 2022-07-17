import { RouteObject } from "react-router-dom";
import MainLayout from "../layouts/MainLayout";
import { Home } from "../views/Home";

/**
 * Routes used in the main app.
 * All routes here will fit nicely into the main layout automatically.
 */
const MainRoutes: RouteObject = {
  path: "/",
  element: <MainLayout />,
  children: [
    {
      path: "/home",
      element: <Home />,
    },
  ],
};

export default MainRoutes;
