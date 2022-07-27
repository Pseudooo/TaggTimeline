import { Button } from "@mui/material";
import { FunctionComponent } from "react";
import { Link } from "react-router-dom";
import { useAuth } from "../../contexts/Auth";

export interface LoginButtonProps {
  /* If the button should hide when the user is logged in */
  autoHide?: boolean;
}

/**
 * Creates a login button that redirects to the login page.
 * @param props The props for this component
 */
export const LoginButton: FunctionComponent<LoginButtonProps> = ({
  autoHide = true,
}) => {
  const { token } = useAuth();

  if (token && autoHide) {
    return null;
  }

  return (
    <Button component={Link} to="/login" color="inherit">
      Login
    </Button>
  );
};

export default LoginButton;
