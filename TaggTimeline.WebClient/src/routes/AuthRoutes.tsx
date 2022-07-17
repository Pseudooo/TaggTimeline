import { RouteObject } from "react-router-dom";
import BarebonesLayout from "../layouts/BarebonesLayout";

/**
 * Routes related to user login/logout.
 * This has been split so that the auth routes can use a different layout to the rest of the app.
 */
const AuthRoutes: RouteObject = {
  path: "/",
  element: <BarebonesLayout />,
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
