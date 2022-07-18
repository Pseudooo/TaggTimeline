import {
  Checkbox as MuiCheckbox,
  CheckboxProps as MuiCheckboxProps,
} from "@mui/material";
import { FunctionComponent } from "react";

/**
 * Creates a checkbox
 * @param props The props for this element
 */
export const Checkbox: FunctionComponent<MuiCheckboxProps> = (props) => {
  return <MuiCheckbox {...props}></MuiCheckbox>;
};
