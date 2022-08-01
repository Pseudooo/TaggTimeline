import { FunctionComponent, useEffect, useState } from "react";
import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardHeader,
  FormControl,
  Typography,
} from "@mui/material";
import TextField from "../io/TextField";
import { LoadingButton } from "@mui/lab";
import { required, ValidationResponse } from "../../validation/rules";
import { useAuth } from "../../contexts/Auth";
import { Link, useNavigate } from "react-router-dom";
import Person from "@mui/icons-material/Person";
import { HttpResponse } from "../../api/generated";
import { PasswordField } from "../io/custom/PasswordField";

export const UserAccountForm: FunctionComponent = () => {
  const { login } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  // @TODO: Workout how to have a ValidationProvider manage these, they're awful here
  const [usernameErrors, setUsernameErrors] = useState<ValidationResponse>([]);
  const [generalError, setGeneralErrors] = useState<ValidationResponse>([]);

  const handleUsernameChange = (username: string) => {
    setUsernameErrors(required(username));
    setUsername(username);
  };

  const handlePasswordChange = (password: string) => {
    setPassword(password);
  };

  // Reset our errors at the point of changing
  useEffect(() => {
    setGeneralErrors([]);
    setUsernameErrors([]);
  }, [username, password]);

  const tryProcessUser = async () => {
    if (errorsExist()) {
      return;
    }
    try {
      setLoading(true);
      await login(username, password);
      navigate("/");
    } catch (e: unknown) {
      if (e instanceof Response) {
        const { error } = e as HttpResponse<unknown, unknown>;
        if (error === "Couldn't find user with that username") {
          setUsernameErrors([error]);
        } else if (error === "Invalid username/password") {
          setGeneralErrors([error]);
        } else {
          setGeneralErrors([error as string]);
        }
      }
    } finally {
      setLoading(false);
    }
  };

  const errorsExist = () => {
    return usernameErrors.length > 0 || generalError.length > 0;
  };

  return (
    <Card sx={{ width: 600 }}>
      <CardHeader
        avatar={<Person />}
        title="Login"
        subheader={generalError}
        subheaderTypographyProps={{ color: "red" }}
      />
      <CardContent>
        <FormControl fullWidth sx={{ paddingY: 1 }}>
          <TextField
            label="Username"
            value={username}
            onChange={handleUsernameChange}
            onEnter={() => tryProcessUser()}
            error={usernameErrors.length > 0}
            helperText={usernameErrors[0]}
            autoFocus
            required
          />
        </FormControl>
        <FormControl fullWidth sx={{ paddingY: 1 }}>
          <PasswordField
            value={password}
            onChange={handlePasswordChange}
            onEnter={() => tryProcessUser()}
            disabled={loading}
            required
          />
        </FormControl>
        <Typography variant="caption" sx={{ paddingY: 1 }}>
          Don&apos;t have an account? <Link to="/register">Register here</Link>
        </Typography>
      </CardContent>
      <CardActions
        sx={{ display: "flex", flex: "1", justifyContent: "space-between" }}
      >
        {/* Redunant currently, but keeps spacing nice. @TODO: Configure for when in/outside dialog */}
        <Button>Cancel</Button>
        <LoadingButton
          loading={loading}
          disabled={errorsExist()}
          onClick={() => tryProcessUser()}
          variant="contained"
        >
          Login
        </LoadingButton>
      </CardActions>
    </Card>
  );
};

export default UserAccountForm;
