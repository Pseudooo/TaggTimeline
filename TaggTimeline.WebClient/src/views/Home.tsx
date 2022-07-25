import { Box, Typography } from "@mui/material";
import { FunctionComponent } from "react";
import HorizontalTimeline from "../components/timeline/horizontal/HorizontalTimeline";
import { TimelineSpeedDial } from "../components/timeline/TimelineSpeedDial";

/**
 * Homepage view
 */
export const Home: FunctionComponent = () => {
  return (
    <Box display="flex" flexDirection="column" gap={1}>
      <Typography>Home page</Typography>
      {/* <HorizontalTimeline /> */}
      {/* <CreateTaggInstanceForm /> */}
      <HorizontalTimeline />
      <TimelineSpeedDial />
    </Box>
  );
};
