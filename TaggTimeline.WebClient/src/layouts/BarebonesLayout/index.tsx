import { AppBar, Box, Toolbar, Typography } from "@mui/material";
import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";

/**
 * Barebones layout element.
 * Effectively a blank page with just a navbar back to the homepage.
 * Use for thinks like auth login, where nothing else is needed
 */
export const BarebonesLayout: FunctionComponent = () => {
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
        </Toolbar>
      </AppBar>
      <Outlet />
    </Box>
  );
};

export default BarebonesLayout;
