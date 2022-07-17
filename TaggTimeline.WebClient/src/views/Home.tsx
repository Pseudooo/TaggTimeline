import { Typography } from "@mui/material";
import { FunctionComponent } from "react";
import HorizontalTimeline from "../components/timeline/horizontal/HorizontalTimeline";

/**
 * Homepage view
 */
export const Home: FunctionComponent = () => {
  return (
    <>
      <Typography>Home page</Typography>
      <HorizontalTimeline />
    </>
  );
};
