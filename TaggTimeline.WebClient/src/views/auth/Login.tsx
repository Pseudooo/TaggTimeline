import {
  Button,
  Card,
  CardActions,
  CardContent,
  Typography,
} from "@mui/material";
import { FunctionComponent, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../contexts/Auth";

/**
 * Page for logging in
 */
export const AuthLoginPage: FunctionComponent = () => {
  const { login } = useAuth();
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  async function doLogin() {
    setLoading(true);
    await login("test_username");
    setLoading(false);
    navigate("/");
  }

  return (
    <Card>
      <CardContent>
        <Typography>
          Clicking login simulates the process of logging in.
        </Typography>
        <Typography>
          Later on, forms can be added to this page to actually log the user in.
        </Typography>
      </CardContent>
      <CardActions>
        <Button onClick={() => doLogin()} disabled={loading}>
          Log in
        </Button>
      </CardActions>
    </Card>
  );
};

export default AuthLoginPage;
