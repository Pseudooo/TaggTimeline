import { PushPin } from "@mui/icons-material";
import { Avatar, SxProps, Tooltip, Typography } from "@mui/material";
import moment from "moment";
import { FunctionComponent } from "react";
import { InstanceModel, TaggModel } from "../../api/generated";

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
      <Avatar sx={{ transformOrigin: "center", ...sx }}>
        <PushPin />
      </Avatar>
    </Tooltip>
  );
};
