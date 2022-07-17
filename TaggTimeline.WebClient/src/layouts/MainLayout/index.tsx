import { AppBar, Box, Toolbar, Typography } from "@mui/material";
import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";
import LoginButton from "../../components/auth/LoginButton";
import LogoutButton from "../../components/auth/LogoutButton";

/**
 * Main layout element.
 * Represents hwo the app will look to the user most of the itme.
 */
export const MainLayout: FunctionComponent = () => {
  return (
    <Box>
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
            component="a"
            href="/"
            sx={{ flexGrow: 1 }}
          >
            TaggTimeline
          </Typography>
          <LoginButton />
          <LogoutButton />
        </Toolbar>
      </AppBar>
      <Outlet />
    </Box>
  );
};

export default MainLayout;
