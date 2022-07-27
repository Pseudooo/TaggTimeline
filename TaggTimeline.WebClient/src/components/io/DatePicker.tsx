import { TextField } from "@mui/material";
import { DesktopDatePicker } from "@mui/x-date-pickers";
import { Moment } from "moment";
import { FunctionComponent, ReactNode } from "react";

interface DatePickerProps {
  label?: ReactNode;
  value: Moment | null;
  // onChange: React.ComponentProps<typeof DesktopDatePicker>["onChange"];
  onChange: (newValue: Moment | null) => void;
  minDate?: Moment;
  maxDate?: Moment;
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
  const handleChange = (newValue: Moment | null) => {
    onChange(newValue);
  };

  return (
    <DesktopDatePicker<Moment>
      label={label}
      disableMaskedInput
      value={value}
      onChange={handleChange}
      minDate={minDate}
      maxDate={maxDate}
      renderInput={(params) => <TextField {...params} />}
      disabled={disabled}
      inputFormat="DD/MM/yyyy"
    />
  );
};

export default DatePicker;
