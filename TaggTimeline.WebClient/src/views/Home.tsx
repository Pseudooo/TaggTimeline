import { Box } from "@mui/material";
import { FunctionComponent } from "react";
import RegisterForm from "../components/forms/RegisterForm";
import HorizontalTimeline from "../components/timeline/horizontal/HorizontalTimeline";
import { TimelineSpeedDial } from "../components/timeline/TimelineSpeedDial";
import { useAuth } from "../contexts/Auth";

/**
 * Homepage view
 */
export const Home: FunctionComponent = () => {
  const { token } = useAuth();
  return (
    <Box display="flex" flexDirection="column" gap={1}>
      {/* @TODO: Should probably investigate into route guards for react */}
      {token ? (
        <>
          <HorizontalTimeline />
          <TimelineSpeedDial />
        </>
      ) : (
        <RegisterForm />
      )}
    </Box>
  );
};
