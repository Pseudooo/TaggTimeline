import { Box } from "@mui/material";
import { FunctionComponent } from "react";
import { UserAccountForm } from "../components/forms/UserAccountForm";
import { TimelineSpeedDial } from "../components/timeline/TimelineSpeedDial";
import { useAuth } from "../contexts/Auth";

/**
 * Homepage view
 */
export const Home: FunctionComponent = () => {
  const { token } = useAuth();
  return (
    <Box display="flex" flexDirection="column" gap={1}>
      {token ? <TimelineSpeedDial /> : <UserAccountForm formMode="Register" />}
    </Box>
  );
};
