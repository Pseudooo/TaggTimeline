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

export const UserAccountForm: FunctionComponent = () => {
  const { login } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  // @TODO: Workout how to have a ValidationProvider manage these, they're awful here
  const [usernameErrors, setUsernameErrors] = useState<ValidationResponse>([]);
  const [passwordErrors, setPasswordErrors] = useState<ValidationResponse>([]);

  const handleUsernameChange = (username: string) => {
    setUsernameErrors(required(username));
    setUsername(username);
  };

  const handlePasswordChange = (password: string) => {
    setPasswordErrors(validatePassword(password));
    setPassword(password);
  };

  const tryProcessUser = async () => {
    try {
      setLoading(true);
      await login(username, password);
      navigate("/");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Card sx={{ width: 600 }}>
      <CardHeader avatar={<Person />} title="Login" />
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
          disabled={passwordErrors.length > 0 || usernameErrors.length > 0}
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
