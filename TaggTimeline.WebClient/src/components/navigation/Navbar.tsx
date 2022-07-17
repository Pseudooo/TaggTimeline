import { AppBar, Toolbar, Typography } from "@mui/material";
import { FunctionComponent } from "react";
import { Link } from "react-router-dom";
import LoginButton from "../auth/LoginButton";
import LogoutButton from "../auth/LogoutButton";

/**
 * Main navbar component
 * This should really only be used by layouts
 */
export const Navbar: FunctionComponent = () => {
  return (
    <AppBar position="static">
      <Toolbar variant="dense">
        {/* <IconButton
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
            <Menu />
          </IconButton> */}
        <Typography
          variant="h6"
          color="inherit"
          component={Link}
          to="/"
          sx={{ flexGrow: 1 }}
        >
          TaggTimeline
        </Typography>
        <LoginButton />
        <LogoutButton />
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
