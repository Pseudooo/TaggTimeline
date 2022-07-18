import { TextField } from "@mui/material";
import { DesktopDatePicker } from "@mui/x-date-pickers";
import { FunctionComponent, ReactNode } from "react";

interface DatePickerProps {
  label?: ReactNode;
  value: Date | null;
  // onChange: React.ComponentProps<typeof DesktopDatePicker>["onChange"];
  onChange: (newValue: Date | null) => void;
  minDate?: Date;
  maxDate?: Date;
  disabled?: boolean;
}

/**
 * Date picking component
 */
export const DatePicker: FunctionComponent<DatePickerProps> = ({
  label,
  value,
  onChange,
  minDate,
  maxDate,
  disabled = false,
}) => {
  const handleChange = (newValue: Date | null) => {
    onChange(newValue);
  };

  return (
    <DesktopDatePicker
      label={label}
      disableMaskedInput
      value={value}
      onChange={handleChange}
      minDate={minDate}
      maxDate={maxDate}
      renderInput={(params) => <TextField {...params} />}
      disabled={disabled}
    />
  );
};

export default DatePicker;
