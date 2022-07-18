import { CalendarMonth } from "@mui/icons-material";
import { SpeedDial, SpeedDialAction, SpeedDialIcon } from "@mui/material";
import { FunctionComponent, useState } from "react";
import { CreateTaggInstanceDialog } from "../dialogs/CreateTaggInstance";

export const TimelineSpeedDial: FunctionComponent = () => {
  const [showCreateTagg, setShowCreateTagg] = useState(false);

  return (
    <>
      <SpeedDial
        ariaLabel="Timeline Actions"
        icon={<SpeedDialIcon />}
        sx={{ position: "absolute", bottom: 16, right: 16 }}
      >
        <SpeedDialAction
          icon={<CalendarMonth />}
          tooltipTitle="Create Tagg"
          onClick={() => setShowCreateTagg(true)}
        />
      </SpeedDial>
      <CreateTaggInstanceDialog
        open={showCreateTagg}
        onClose={() => setShowCreateTagg(false)}
      />
    </>
  );
};
