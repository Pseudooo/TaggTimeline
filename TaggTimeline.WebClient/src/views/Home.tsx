import { Box } from "@mui/material";
import { FunctionComponent } from "react";
import RegisterAccountForm from "../components/forms/RegisterAccount";
import { TimelineSpeedDial } from "../components/timeline/TimelineSpeedDial";

/**
 * Homepage view
 */
export const Home: FunctionComponent = () => {
  return (
    <Box display="flex" flexDirection="column" gap={1}>
      <RegisterAccountForm />
      <TimelineSpeedDial />
    </Box>
  );
};
