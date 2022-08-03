import { Circle } from "@mui/icons-material";
import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  FormGroup,
  ListItem,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { FunctionComponent, useState } from "react";
import { InstanceModel, TaggPreviewModel } from "../../api/generated";
import { SelectTaggForm } from "./SelectTagg";
import DatePicker from "../io/DatePicker";
import { LoadingButton } from "@mui/lab";
import { useAPI } from "../../contexts/API";
import { useToaster } from "../../contexts/Toaster";
import moment, { Moment } from "moment";

export interface CreateTaggInstanceForm {
  onCancel?: () => void;
  onSuccess?: (instance: InstanceModel) => void;
}

/**
 * Form for creating a new Tagg instance
 */
export const CreateTaggInstanceForm: FunctionComponent<
  CreateTaggInstanceForm
> = ({ onSuccess }) => {
  const [tagg, setTagg] = useState<TaggPreviewModel>();
  const [loading, setLoading] = useState(false);
  const [complete, setComplete] = useState(false);
  const { createTaggInstance } = useAPI();
  const { createToaster } = useToaster();
  const [date, setDate] = useState(moment());

  const selectTagg = (tagg: TaggPreviewModel) => {
    setTagg(tagg);
  };

  const handleDateChange = (newDate: Moment | null) => {
    if (!newDate) {
      return;
    }
    setDate(newDate);
  };

  const tryCreateTaggInstance = (tagg: TaggPreviewModel) => {
    setLoading(true);
    createTaggInstance(tagg.id, date)
      .then((instance) => {
        // Don't remove loading state, so user can't resubmit
        // TODO: Add a disabled state
        // setLoading(false);
        setComplete(true);
        createToaster({
          severity: "success",
          message: (
            <>
              Created an instance of <b>{tagg.key}</b>
            </>
          ),
        });
        if (onSuccess) {
          onSuccess(instance);
        }
      })
      .catch(() => {
        setLoading(false);
      })
      .finally(() => setLoading(false));
  };

  return !tagg ? (
    <SelectTaggForm onSelected={selectTagg} />
  ) : (
    <Card>
      <ListItem dense>
        <ListItemIcon>
          <Circle sx={{ color: tagg.colour }} />
        </ListItemIcon>
        <ListItemText primary={tagg.key} secondary={tagg.id} />
      </ListItem>
      <CardContent>
        <FormGroup>
          <DatePicker label="Date" value={date} onChange={handleDateChange} />
        </FormGroup>
      </CardContent>
      <CardActions>
        <Button onClick={() => setTagg(undefined)}>Back</Button>
        <Box sx={{ marginLeft: "auto" }}>
          <LoadingButton
            loading={loading}
            disabled={complete}
            onClick={() => tryCreateTaggInstance(tagg)}
            variant="contained"
          >
            Create
          </LoadingButton>
        </Box>
      </CardActions>
    </Card>
  );
};

export default CreateTaggInstanceForm;
