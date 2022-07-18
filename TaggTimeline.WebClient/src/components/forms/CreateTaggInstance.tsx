import { Typography } from "@mui/material";
import { FunctionComponent, useState } from "react";
import { Tagg } from "../../api/generated";
import { SelectTaggForm } from "./SelectTagg";

/**
 * Form for creating a new Tagg instance
 */
export const CreateTaggInstanceForm: FunctionComponent = () => {
  const [tagg, setTagg] = useState<Tagg>();

  const selectTagg = (tagg: Tagg) => {
    setTagg(tagg);
  };

  return !tagg ? (
    <SelectTaggForm onSelected={selectTagg} />
  ) : (
    <Typography>The selected tag is {tagg.key}</Typography>
  );
};
