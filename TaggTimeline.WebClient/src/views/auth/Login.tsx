import { FunctionComponent } from "react";
import UserAccountForm from "../../components/forms/UserAccountForm";

/**
 * Page for logging in
 */
export const AuthLoginPage: FunctionComponent = () => {
  return <UserAccountForm formMode="Login" />;
};

export default AuthLoginPage;
