import { LoadingButton } from "@mui/lab";
import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  FormControl,
} from "@mui/material";
import { FunctionComponent, useState } from "react";
import { TaggModel } from "../../api/generated";
import { useAPI } from "../../contexts/API";
import { required, ValidationResponse } from "../../validation/rules";
import { ColourPicker } from "../io/ColourPicker";
import TextField from "../io/TextField";

export interface CreateTaggFormProps {
  placeholder?: string;
  onSuccess?: (tagg: TaggModel) => void;
  onCancel?: () => void;
  cancelText?: string;
}

/**
 * A form for creating a new Tagg
 * @param props The porps for this component
 */
export const CreateTaggForm: FunctionComponent<CreateTaggFormProps> = ({
  placeholder = "",
  onSuccess,
  onCancel,
  cancelText = "Cancel",
}) => {
  const [taggName, setTaggName] = useState(placeholder);
  const [taggColour, setTaggColour] = useState("#000");
  const [error, setError] = useState<ValidationResponse>([]);
  const [loading, setLoading] = useState(false);
  const { createTagg } = useAPI();

  const handleTaggNameChange = (value: string) => {
    setError(required(value));
    setTaggName(value);
  };

  const handleTaggColourChange = (value: string) => {
    setTaggColour(value);
  };

  const tryCreateTagg = (name: string, colour: string) => {
    if (error.length > 0) {
      return;
    }
    setLoading(true);
    createTagg(name, colour)
      .then((tagg) => {
        // Tagg was created successfully
        setLoading(false);
        if (onSuccess) {
          onSuccess(tagg);
        }
      })
      .catch(() => {
        setLoading(false);
        setError(["An error occured"]);
      })
      .finally(() => setLoading(false));
  };

  return (
    <Card>
      <CardContent sx={{ display: "flex" }}>
        <FormControl>
          <ColourPicker value={taggColour} onChange={handleTaggColourChange} />
        </FormControl>
        <FormControl sx={{ flex: "1", marginLeft: "1rem" }}>
          <TextField
            label="Name"
            value={taggName}
            onChange={handleTaggNameChange}
            onEnter={() => tryCreateTagg(taggName, taggColour)}
            error={error.length > 0}
            helperText={error[0]}
            disabled={loading}
            autoFocus
          />
        </FormControl>
      </CardContent>
      <CardActions
        sx={{ display: "flex", flex: 1, flexSpacing: "space-between" }}
      >
        {onCancel && <Button onClick={onCancel}>{cancelText}</Button>}
        <Box sx={{ marginLeft: "auto" }}>
          <LoadingButton
            loading={loading}
            disabled={error.length > 0}
            onClick={() => tryCreateTagg(taggName, taggColour)}
            variant="contained"
          >
            Create
          </LoadingButton>
        </Box>
      </CardActions>
    </Card>
  );
};
