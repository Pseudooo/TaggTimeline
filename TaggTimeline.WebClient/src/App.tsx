import { AppBar, Box, IconButton, Toolbar, Typography } from "@mui/material";
import { Menu } from "@mui/icons-material";

function App() {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar variant="dense">
          <IconButton
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
            <Menu />
          </IconButton>
          <Typography variant="h6" color="inherit" component="div">
            TaggTimeline
          </Typography>
        </Toolbar>
      </AppBar>
    </Box>
  );
}

export default App;
