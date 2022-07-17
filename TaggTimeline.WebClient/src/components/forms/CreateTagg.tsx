import { LoadingButton } from "@mui/lab";
import { Box, FormControl } from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";
import { Tagg } from "../../api/generated";
import { createTagg } from "../../api/wrapped";
import TextField from "../io/TextField";

export interface CreateTaggFormProps {
  placeholder?: string;
  onSuccess?: (tagg: Tagg) => void;
  onCancel?: () => void;
}

/**
 * A form for creating a new Tagg
 * @param props The porps for this component
 */
export const CreateTaggForm: FunctionComponent<CreateTaggFormProps> = ({
  placeholder = "",
  onSuccess,
}) => {
  const [taggName, setTaggName] = useState(placeholder);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleTaggNameChange = (value: string) => {
    setTaggName(value);
  };

  const tryCreateTagg = (name: string) => {
    setLoading(true);
    createTagg(name)
      .then((tagg) => () => {
        // Tagg was created successfully
        setLoading(false);
        if (onSuccess) {
          onSuccess(tagg);
        }
      })
      .catch((err) => {
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
    <Box display="flex" flexDirection="column">
      <FormControl sx={{ padding: 1 }}>
        <TextField
          label="Name"
          value={taggName}
          onChange={handleTaggNameChange}
          error={error.length > 0}
          helperText={error}
          disabled={loading}
        />
      </FormControl>
      <LoadingButton
        loading={loading}
        disabled={error.length > 0}
        onClick={() => tryCreateTagg(taggName)}
      >
        Create
      </LoadingButton>
    </Box>
  );
};
