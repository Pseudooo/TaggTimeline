import {
  Box,
  Checkbox,
  Chip,
  FormControl,
  InputLabel,
  ListItemText,
  MenuItem,
  OutlinedInput,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { FunctionComponent } from "react";
import { TaggPreviewModel } from "../../../api/generated";

export interface SelectTaggsDropdownProps {
  taggs: TaggPreviewModel[];
  selected: TaggPreviewModel[];
  label?: string;
  onChange?(selected: TaggPreviewModel[]): void;
}

/**
 * A dropdown for selecting from a list of taggs
 * @param props The props for this component
 * @returns A dropdown for selecting taggs
 */
export const SelectTaggsDropdown: FunctionComponent<
  SelectTaggsDropdownProps
> = ({ taggs, selected, label = "Taggs", onChange }) => {
  const handleChange = (event: SelectChangeEvent<string[]>) => {
    const value = event.target.value;
    const ids = typeof value === "string" ? value.split(",") : value;
    const selectedTaggs: TaggPreviewModel[] = [];
    ids.forEach((id) => {
      const tagg = taggs.find((tagg) => tagg.id === id);
      if (tagg) {
        selectedTaggs.push(tagg);
      }
    });
    if (onChange) {
      onChange(selectedTaggs);
    }
  };

  return (
    <FormControl fullWidth>
      <InputLabel>{label}</InputLabel>
      <Select
        multiple
        value={selected.map((i) => i.id)}
        onChange={handleChange}
        input={<OutlinedInput label={label} />}
        renderValue={(selected) => (
          <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
            {selected.map((value) => (
              <Chip
                key={value}
                label={taggs.find((i) => i.id === value)?.key}
              ></Chip>
            ))}
          </Box>
        )}
      >
        {taggs.map((tagg) => (
          <MenuItem key={tagg.id} value={tagg.id}>
            <Checkbox
              checked={selected.find((i) => i.id === tagg.id) !== undefined}
            />
            <ListItemText primary={tagg.key} />
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};
