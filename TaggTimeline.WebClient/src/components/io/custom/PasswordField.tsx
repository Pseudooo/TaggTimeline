import { Visibility, VisibilityOff } from "@mui/icons-material";
import {
  Box,
  CircularProgress,
  IconButton,
  InputAdornment,
} from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";
import { TextField, TextFieldProps } from "../TextField";

interface PasswordFieldProps extends Omit<TextFieldProps, "type"> {
  type?: "password" | "text";
}

export const PasswordField: FunctionComponent<PasswordFieldProps> = ({
  label = "Password",
  type = "password",
  ...props
}) => {
  const visibleDurationMs = 5000;
  const [fieldType, setFieldType] = useState<typeof type>(type);
  const [timeLeft, setTimeLeft] = useState(0);
  const [isHovering, setIsHovering] = useState(false);

  useEffect(() => {
    if (!timeLeft) {
      setFieldType("password");
      return;
    }

    const intervalId = setInterval(() => {
      if (!isHovering) {
        setTimeLeft(timeLeft - 100);
      }
    }, 100);

    return () => {
      clearInterval(intervalId);
    };
  }, [timeLeft, isHovering]);

  const toggleFieldType = () => {
    const newFieldType = fieldType === "password" ? "text" : "password";
    setFieldType(newFieldType);

    if (newFieldType === "text") {
      setTimeLeft(visibleDurationMs);
    } else if (newFieldType === "password") {
      setTimeLeft(0);
    }
  };

  return (
    <TextField
      type={fieldType}
      label={label}
      onMouseEnter={() => setIsHovering(true)}
      onMouseLeave={() => setIsHovering(false)}
      {...props}
      InputProps={{
        endAdornment: (
          <InputAdornment position="end">
            <Box sx={{ position: "relative", display: "inline-flex" }}>
              <CircularProgress
                color="inherit"
                variant="determinate"
                value={(timeLeft / visibleDurationMs) * 100}
              />

              <IconButton
                sx={{
                  top: 0,
                  left: 0,
                  bottom: 0,
                  right: 0,
                  position: "absolute",
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                }}
                onClick={() => toggleFieldType()}
              >
                {fieldType === "password" ? <VisibilityOff /> : <Visibility />}
              </IconButton>
            </Box>
          </InputAdornment>
        ),
      }}
    />
  );
};
