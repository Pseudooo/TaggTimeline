import { Button } from "@mui/material";
import { FunctionComponent } from "react";
import { Link } from "react-router-dom";
import { useAuth } from "../../contexts/Auth";

export interface LogoutButtonProps {
  /* If the button should hide when the user is logged in */
  autoHide?: boolean;
}

/**
 * Creates a logout button that redirects to the logout page.
 * @param props The props for this component
 */
export const LogoutButton: FunctionComponent<LogoutButtonProps> = ({
  autoHide = true,
}) => {
  const { user } = useAuth();

  if (!user && autoHide) {
    return null;
  }

  return (
    <Button component={Link} to="/logout" color="inherit">
      Log Out
    </Button>
  );
};

export default LogoutButton;
