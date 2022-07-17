import { RouteObject } from "react-router-dom";
import BarebonesLayout from "../layouts/BarebonesLayout";
import AuthLoginPage from "../views/auth/Login";
import AuthLogoutPage from "../views/auth/Logout";

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
      element: <AuthLoginPage />,
    },
    {
      path: "/logout",
      element: <AuthLogoutPage />,
    },
  ],
};

export default AuthRoutes;
