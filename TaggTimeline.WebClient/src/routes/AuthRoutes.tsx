import { RouteObject } from "react-router-dom";
import BarebonesLayout from "../layouts/BarebonesLayout";
import AuthLogoutPage from "../views/auth/Logout";
import UserAccountForm from "../components/forms/UserAccountForm";

/**
 * Routes related to user login/logout.
 * This has been split so that the auth routes can use a different layout to the rest of the app.
 */
const AuthRoutes: RouteObject = {
  path: "/",
  element: <BarebonesLayout center />,
  children: [
    {
      path: "/login",
      element: <UserAccountForm formMode="Login" />,
    },
    {
      path: "/register",
      element: <UserAccountForm formMode="Register" />,
    },
    {
      path: "/logout",
      element: <AuthLogoutPage />,
    },
  ],
};

export default AuthRoutes;
