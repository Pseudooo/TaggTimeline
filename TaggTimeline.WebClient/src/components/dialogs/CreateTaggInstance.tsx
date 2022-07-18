import { Close } from "@mui/icons-material";
import { Dialog, DialogProps, DialogTitle, IconButton } from "@mui/material";
import { FunctionComponent } from "react";
import { CreateTaggInstanceForm } from "../forms/CreateTaggInstance";

/**
 * A dialog popup for creating a new tag instance
 * @param props The props for this component
 */
export const CreateTaggInstanceDialog: FunctionComponent<DialogProps> = ({
  onClose,
  ...props
}) => {
  return (
    <Dialog maxWidth="sm" fullWidth onClose={onClose} {...props}>
      <IconButton
        aria-label="close"
        onClick={() => {
          if (onClose) {
            onClose({}, "backdropClick");
          }
        }}
        sx={{ position: "absolute", right: 8, top: 8 }}
      >
        <Close />
      </IconButton>
      <DialogTitle>Add a Tagg</DialogTitle>
      <CreateTaggInstanceForm />
    </Dialog>
  );
};

export default CreateTaggInstanceDialog;
