import { FunctionComponent, useState } from "react";
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
import {
  required,
  validatePassword,
  ValidationResponse,
} from "../../validation/rules";
import { useAuth } from "../../contexts/Auth";
import { Link, useNavigate } from "react-router-dom";
import Person from "@mui/icons-material/Person";
import { HttpResponse } from "../../api/generated";

export const RegisterForm: FunctionComponent = () => {
  const { register } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmedPassword, setConfirmedPassword] = useState("");
  const [loading, setLoading] = useState(false);

  // @TODO: Workout how to have a ValidationProvider manage these, they're awful here
  const [usernameErrors, setUsernameErrors] = useState<ValidationResponse>([]);
  const [passwordErrors, setPasswordErrors] = useState<ValidationResponse>([]);
  const [confirmedPasswordErrors, setConfirmedPasswordErrors] =
    useState<ValidationResponse>([]);
  const [generalError, setGeneralErrors] = useState<ValidationResponse>([]);

  const handleUsernameChange = (newUsername: string) => {
    setUsernameErrors(required(newUsername));
    setUsername(newUsername);
  };

  const handlePasswordChange = (newPassword: string) => {
    setPasswordErrors(validatePassword(newPassword));
    setPassword(newPassword);
  };

  const handleConfirmedPasswordChange = (newPassword: string) => {
    setConfirmedPasswordErrors(
      newPassword !== password ? ["Passwords do not match"] : []
    );
    setConfirmedPassword(newPassword);
  };

  const tryProcessUser = async () => {
    try {
      setLoading(true);
      await register(username, password);
      navigate("/");
    } catch (e: unknown) {
      if (e instanceof Response) {
        const { error } = e as HttpResponse<unknown, unknown>;
        if (error === "There is already a user with that username") {
          setUsernameErrors([error]);
        }
      }
    } finally {
      setLoading(false);
    }
  };

  const errorsExist = () => {
    return (
      usernameErrors.length > 0 ||
      passwordErrors.length > 0 ||
      confirmedPasswordErrors.length > 0 ||
      generalError.length > 0
    );
  };

  return (
    <Card sx={{ width: 600 }}>
      <CardHeader
        avatar={<Person />}
        title="Register an account"
        subheader={generalError}
        subheaderTypographyProps={{ color: "red" }}
      />
      <CardContent>
        <FormControl fullWidth sx={{ paddingY: 1 }}>
          <TextField
            label="Username"
            value={username}
            onChange={handleUsernameChange}
            error={usernameErrors.length > 0}
            helperText={usernameErrors[0]}
            autoFocus
            required
          />
        </FormControl>
        <FormControl fullWidth sx={{ paddingY: 1 }}>
          <TextField
            label="Password"
            type="password"
            value={password}
            onChange={handlePasswordChange}
            error={passwordErrors.length > 0}
            helperText={passwordErrors[0]}
            disabled={loading}
            required
          />
        </FormControl>
        <FormControl fullWidth sx={{ paddingY: 1 }}>
          <TextField
            label="Confirm Password"
            type="password"
            value={confirmedPassword}
            onChange={handleConfirmedPasswordChange}
            error={confirmedPasswordErrors.length > 0}
            helperText={confirmedPasswordErrors[0]}
            disabled={loading}
            required
          />
        </FormControl>
        <Typography variant="caption" sx={{ paddingY: 1 }}>
          Already registered? <Link to="/login">Login here</Link>
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
          Register
        </LoadingButton>
      </CardActions>
    </Card>
  );
};

export default RegisterForm;
