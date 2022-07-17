import { Typography } from "@mui/material";
import { FunctionComponent, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../contexts/Auth";

/**
 * Page for logging out
 * NOTE: Ideally, logging out shouldn't be done by redirecting to this
 */
export const AuthLogoutPage: FunctionComponent = () => {
  const { logout } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    logout().then(() => {
      navigate("/");
    });
  });

  return <Typography>Logging out...</Typography>;
};

export default AuthLogoutPage;
