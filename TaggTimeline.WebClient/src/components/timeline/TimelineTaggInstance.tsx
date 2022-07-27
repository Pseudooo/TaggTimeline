import { PushPin } from "@mui/icons-material";
import { Avatar, SxProps } from "@mui/material";
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
    <Avatar sx={{ transformOrigin: "center", ...sx }}>
      <PushPin />
    </Avatar>
  );
};
