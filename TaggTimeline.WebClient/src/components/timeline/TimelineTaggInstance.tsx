import { Box, SxProps, Tooltip, Typography } from "@mui/material";
import moment from "moment";
import { FunctionComponent } from "react";
import { InstanceModel, TaggModel } from "../../api/generated";
import { stringToColour } from "../../util";

export interface TimelineTaggInstanceProps {
  tagg: TaggModel;
  taggInstance: InstanceModel;
  sx?: SxProps;
}

export const TimelineTaggInstance: FunctionComponent<
  TimelineTaggInstanceProps
> = ({ tagg, taggInstance, sx }) => {
  return (
    <Tooltip
      title={
        <>
          <Typography fontWeight="bold">{tagg.key}</Typography>
          <Typography>
            Occured {moment(taggInstance.occuranceDate).format("Do MMMM YYYY")}
          </Typography>
        </>
      }
    >
      <Box
        sx={{
          transformOrigin: "center",
          transform: "translate(-50%, -50%)",
          bgcolor: stringToColour(tagg.id),
          borderRadius: "100000px",
          width: "1rem",
          height: "1rem",
          ...sx,
        }}
      ></Box>
    </Tooltip>
  );
};
