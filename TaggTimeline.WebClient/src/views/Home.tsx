import { Box, Typography } from "@mui/material";
import { FunctionComponent } from "react";
import { CreateTaggInstanceForm } from "../components/forms/CreateTaggInstance";
import HorizontalTimeline from "../components/timeline/horizontal/HorizontalTimeline";

/**
 * Homepage view
 */
export const Home: FunctionComponent = () => {
  return (
    <Box display="flex" flexDirection="column" gap={1}>
      <Typography>Home page</Typography>
      <HorizontalTimeline />
      <CreateTaggInstanceForm />
    </Box>
  );
};
