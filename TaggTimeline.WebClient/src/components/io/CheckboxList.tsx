import { FormControlLabel, FormGroup } from "@mui/material";
import { FunctionComponent } from "react";
import { Checkbox } from "./Checkbox";

export interface CheckboxDefinition {
  label: string;
  value: string;
  checked: boolean;
}

export interface CheckboxListProps {
  items: CheckboxDefinition[];
  onChange: (value: CheckboxDefinition, checked: boolean) => void;
}

/**
 * A list of checkboxes
 * @param props The props for this component
 */
export const CheckboxList: FunctionComponent<CheckboxListProps> = ({
  items,
  onChange,
}) => {
  const handleOnChange = (value: CheckboxDefinition, checked: boolean) => {
    onChange(value, checked);
  };

  const checkboxes = items.map((item) => {
    return (
      <FormControlLabel
        key={item.value}
        control={<Checkbox></Checkbox>}
        label={item.label}
        checked={item.checked}
        onChange={(_, checked) => handleOnChange(item, checked)}
      ></FormControlLabel>
    );
  });

  return <FormGroup>{checkboxes}</FormGroup>;
};
