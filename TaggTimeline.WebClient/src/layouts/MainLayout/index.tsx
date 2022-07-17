import { Box, Container } from "@mui/material";
import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";
import Navbar from "../../components/navigation/Navbar";

/**
 * Main layout element.
 * Represents hwo the app will look to the user most of the itme.
 */
export const MainLayout: FunctionComponent = () => {
  return (
    <Box height="100vh" display="flex" flexDirection="column">
      <Navbar />
      <Container>
        <Outlet />
      </Container>
    </Box>
  );
};

export default MainLayout;
