import { Dialog, DialogProps, DialogTitle } from "@mui/material";
import { FunctionComponent } from "react";
import { CreateTaggInstanceForm } from "../forms/CreateTaggInstance";

/**
 * A dialog popup for creating a new tag instance
 * @param props The props for this component
 */
export const CreateTaggInstanceDialog: FunctionComponent<DialogProps> = (
  props
) => {
  return (
    <Dialog maxWidth="sm" fullWidth {...props}>
      <DialogTitle>Add a Tagg</DialogTitle>
      <CreateTaggInstanceForm />
    </Dialog>
  );
};

export default CreateTaggInstanceDialog;
