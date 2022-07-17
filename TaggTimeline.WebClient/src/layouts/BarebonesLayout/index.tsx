import { Box, Container, SxProps } from "@mui/material";
import { Theme } from "@mui/system";
import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";
import Navbar from "../../components/navigation/Navbar";

interface BarebonesLayoutProps {
  center?: boolean;
}

/**
 * Barebones layout element.
 * Effectively a blank page with just a navbar back to the homepage.
 * Use for thinks like auth login, where nothing else is needed
 */
export const BarebonesLayout: FunctionComponent<BarebonesLayoutProps> = ({
  center = false,
}) => {
  const sxProps: SxProps<Theme> = center
    ? { display: "flex", alignItems: "center", justifyContent: "center" }
    : {};

  return (
    <Box height="100vh" display="flex" flexDirection="column">
      <Navbar />
      <Container sx={{ flex: 1, ...sxProps }}>
        <Outlet />
      </Container>
    </Box>
  );
};

export default BarebonesLayout;
