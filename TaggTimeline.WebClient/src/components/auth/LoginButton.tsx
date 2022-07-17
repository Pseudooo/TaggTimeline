import { Button } from "@mui/material";
import { FunctionComponent } from "react";
import { useAppSelector } from "../../store/helper";

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
  const loggedIn = useAppSelector((state) => state.auth.loggedIn);

  if (loggedIn && autoHide) {
    return null;
  }

  return (
    <Button href="/login" color="inherit">
      Login
    </Button>
  );
};

export default LoginButton;
