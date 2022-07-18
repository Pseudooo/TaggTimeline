import { Circle } from "@mui/icons-material";
import {
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
import { TaggPreviewModel } from "../../api/generated";
import { SelectTaggForm } from "./SelectTagg";
import { stringToColour } from "../../util";
import DatePicker from "../io/DatePicker";
import { LoadingButton } from "@mui/lab";

export interface CreateTaggInstanceForm {
  onCancel?: () => void;
}

/**
 * Form for creating a new Tagg instance
 */
export const CreateTaggInstanceForm: FunctionComponent<
  CreateTaggInstanceForm
> = ({ onCancel }) => {
  const [tagg, setTagg] = useState<TaggPreviewModel>();
  const [loading, setLoading] = useState(false);

  const selectTagg = (tagg: TaggPreviewModel) => {
    setTagg(tagg);
  };

  const handleDateChange = () => {
    // Do nothing
  };

  const tryCreateTaggInstance = (tagg: TaggPreviewModel) => {
    setLoading(true);
  };

  return !tagg ? (
    <SelectTaggForm onSelected={selectTagg} />
  ) : (
    <Card>
      <ListItem dense>
        <ListItemIcon>
          <Circle sx={{ color: stringToColour(tagg.id ?? "") }} />
        </ListItemIcon>
        <ListItemText primary={tagg.key} secondary={tagg.id} />
      </ListItem>
      <CardContent>
        <FormGroup>
          <DatePicker
            label="Date"
            value={new Date()}
            onChange={handleDateChange}
            disabled
          />
        </FormGroup>
      </CardContent>
      <CardActions>
        <Button onClick={() => setTagg(undefined)}>Back</Button>
        <LoadingButton
          loading={loading}
          onClick={() => tryCreateTaggInstance(tagg)}
          variant="outlined"
        >
          Create
        </LoadingButton>
      </CardActions>
    </Card>
  );
};

export default CreateTaggInstanceForm;
