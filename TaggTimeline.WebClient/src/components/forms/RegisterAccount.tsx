import { FunctionComponent, useState } from "react";
import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardHeader,
  FormControl,
} from "@mui/material";
import TextField from "../io/TextField";
import { LoadingButton } from "@mui/lab";
import {
  required,
  validatePassword,
  ValidationResponse,
} from "../../validation/rules";
import { useAuth } from "../../contexts/Auth";
import { useNavigate } from "react-router-dom";

export const RegisterAccountForm: FunctionComponent = () => {
  const { register } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  // @TODO: Workout how to have a ValidationProvide manage these, they're awful here
  const [usernameValidation, setUsernameValidation] =
    useState<ValidationResponse>({ failed: false, errors: [] });

  const [passwordValidation, setPasswordValidation] =
    useState<ValidationResponse>({ failed: false, errors: [] });

  const handleUsernameChange = (username: string) => {
    setUsernameValidation(required(username));
    setUsername(username);
  };

  const handlePasswordChange = (password: string) => {
    setPasswordValidation(validatePassword(password));
    setPassword(password);
  };

  const tryRegisterUser = async () => {
    try {
      setLoading(true);
      await register(username, password);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Card>
      <CardHeader title="Register an account" />
      <CardContent>
        <FormControl fullWidth sx={{ paddingY: 1 }}>
          <TextField
            label="Username"
            value={username}
            onChange={handleUsernameChange}
            error={usernameValidation.failed}
            helperText={usernameValidation.errors[0]}
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
            error={passwordValidation.failed}
            helperText={passwordValidation.errors[0]}
            disabled={loading}
            required
          />
        </FormControl>
      </CardContent>
      <CardActions
        sx={{ display: "flex", flex: "1", justifyContent: "space-between" }}
      >
        {/* Redunant currently, but keeps spacing nice. @TODO: Configure for when in/outside dialog */}
        <Button>Cancel</Button>
        <LoadingButton
          loading={loading}
          disabled={passwordValidation.failed || usernameValidation.failed}
          onClick={() => tryRegisterUser()}
          variant="contained"
        >
          Register
        </LoadingButton>
      </CardActions>
    </Card>
  );
};

export default RegisterAccountForm;
