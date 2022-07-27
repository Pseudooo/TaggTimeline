import { Moment } from "moment";
import { FunctionComponent, useEffect, useState } from "react";
import DatePicker from "./DatePicker";

interface DateRangePickerProps {
  startLabel?: string;
  endLabel?: string;
  startDate: Moment | null;
  endDate: Moment | null;
  onChange: (newStart: Moment | null, newEnd: Moment | null) => void;
  behaviour?: "none" | "limit" | "overwrite";
  disabled?: boolean;
}

/**
 * A date range picker, for choosing a start and end date
 * @param props The props for this component
 */
export const DateRangePicker: FunctionComponent<DateRangePickerProps> = ({
  startLabel = "From",
  endLabel = "To",
  startDate,
  endDate,
  onChange,
  behaviour = "none",
  disabled = false,
}) => {
  const [startDateMin] = useState<Moment>(); // Unused currently
  const [startDateMax, setStartDateMax] = useState<Moment>();
  const [endDateMin, setEndDateMin] = useState<Moment>();
  const [endDateMax] = useState<Moment>(); // Unused currently

  /**
   * Handles what happens when a new date is selected by the user
   * @param newStart The new start date
   * @param newEnd The new end date
   */
  const handleChange = (newStart: Moment | null, newEnd: Moment | null) => {
    onChange(newStart, newEnd);
  };

  useEffect(() => {
    // When behavious is set to "limit", the dates will be prevented from overlapping
    if (behaviour === "limit") {
      setStartDateMax(endDate ?? undefined);
      setEndDateMin(startDate ?? undefined);
    }
  }, [startDate, endDate]);

  return (
    <>
      <DatePicker
        value={startDate}
        onChange={(val) => handleChange(val, endDate)}
        label={startLabel}
        minDate={startDateMin}
        maxDate={startDateMax}
        disabled={disabled}
      ></DatePicker>
      <DatePicker
        value={endDate}
        onChange={(val) => handleChange(startDate, val)}
        label={endLabel}
        minDate={endDateMin}
        maxDate={endDateMax}
        disabled={disabled}
      ></DatePicker>
    </>
  );
};
