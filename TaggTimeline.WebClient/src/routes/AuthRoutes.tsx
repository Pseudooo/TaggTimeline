import { RouteObject } from "react-router-dom";
import MainLayout from "../layouts/MainLayout";

/**
 * Routes related to user login/logout.
 * This has been split so that the auth routes can use a different layout to the rest of the app.
 */
const AuthRoutes: RouteObject = {
  path: "/",
  element: <MainLayout />,
  children: [
    {
      path: "/login",
      element: null,
    },
    {
      path: "/logout",
      element: null,
    },
  ],
};

export default AuthRoutes;
