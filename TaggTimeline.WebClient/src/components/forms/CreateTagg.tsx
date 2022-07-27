import { LoadingButton } from "@mui/lab";
import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  FormControl,
} from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";
import { TaggModel } from "../../api/generated";
import { useAPI } from "../../contexts/API";
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
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const { createTagg } = useAPI();

  const handleTaggNameChange = (value: string) => {
    setTaggName(value);
  };

  const tryCreateTagg = (name: string) => {
    if (error.length > 0) {
      return;
    }
    setLoading(true);
    createTagg(name)
      .then((tagg) => {
        // Tagg was created successfully
        setLoading(false);
        if (onSuccess) {
          onSuccess(tagg);
        }
      })
      .catch(() => {
        setLoading(false);
        setError("An error occured");
      })
      .finally(() => setLoading(false));
  };

  useEffect(() => {
    if (taggName.length === 0) {
      setError("Cannot be blank.");
      return;
    }
    setError("");
  }, [taggName]);

  return (
    <Card>
      <CardContent>
        <FormControl fullWidth>
          <TextField
            label="Name"
            value={taggName}
            onChange={handleTaggNameChange}
            onEnter={() => tryCreateTagg(taggName)}
            error={error.length > 0}
            helperText={error}
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
            onClick={() => tryCreateTagg(taggName)}
            variant="contained"
          >
            Create
          </LoadingButton>
        </Box>
      </CardActions>
    </Card>
  );
};
