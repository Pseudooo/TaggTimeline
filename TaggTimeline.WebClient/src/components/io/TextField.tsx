import {
  TextField as MuiTextField,
  TextFieldProps as MuiTextFieldProps,
} from "@mui/material";
import { ChangeEvent, FunctionComponent, KeyboardEvent } from "react";

export interface TextFieldProps extends Omit<MuiTextFieldProps, "onChange"> {
  onChange?: (value: string) => void;
  onEnter?: () => void;
}

/**
 * Creates a text field
 * @param props The props for this element
 */
export const TextField: FunctionComponent<TextFieldProps> = ({
  onChange,
  onEnter,
  onKeyDown,
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

  /**
   * Handles firing an onEnter event when ENTER is pressed.
   * @param event The keyboard event
   */
  const handleKeyDown = (event: KeyboardEvent<HTMLInputElement>) => {
    if (onKeyDown) {
      onKeyDown(event);
    }
    if (onEnter && event.key === "Enter") {
      onEnter();
    }
  };

  return (
    <MuiTextField
      onChange={handleOnChange}
      onKeyDown={handleKeyDown}
      variant="outlined"
      {...remaining}
    ></MuiTextField>
  );
};

export default TextField;
