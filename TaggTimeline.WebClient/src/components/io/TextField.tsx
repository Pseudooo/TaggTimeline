import {
  TextField as MuiTextField,
  TextFieldProps as MuiTextFieldProps,
} from "@mui/material";
import { ChangeEvent, FunctionComponent } from "react";

export interface TextFieldProps extends Omit<MuiTextFieldProps, "onChange"> {
  onChange?: (value: string) => void;
}

/**
 * Creates a text field
 * @param props The props for this element
 */
export const TextField: FunctionComponent<TextFieldProps> = ({
  onChange,
  ...remaining
}) => {
  /**
   * Handles calling onChange callback when value is updated
   * @param event The change event
   */
  const handleOnChange = (event: ChangeEvent<HTMLInputElement>) => {
    if (onChange) {
      onChange(event.target.value);
    }
  };

  return (
    <MuiTextField
      onChange={handleOnChange}
      variant="outlined"
      {...remaining}
    ></MuiTextField>
  );
};

export default TextField;
