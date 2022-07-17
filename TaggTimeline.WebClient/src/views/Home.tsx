import { Box, Card, Typography } from "@mui/material";
import { FunctionComponent } from "react";
import { CreateTaggForm } from "../components/forms/CreateTagg";
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
      <Card>
        <CreateTaggInstanceForm />
      </Card>
      <Card>
        <CreateTaggForm />
      </Card>
    </Box>
  );
};
