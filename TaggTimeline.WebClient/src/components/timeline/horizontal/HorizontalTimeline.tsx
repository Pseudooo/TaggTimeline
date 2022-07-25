import { Grid, Paper } from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";
import { TaggPreviewModel } from "../../../api/generated";
import { useAPI } from "../../../contexts/API";
import { CheckboxDefinition, CheckboxList } from "../../io/CheckboxList";
import { DateRangePicker } from "../../io/DateRangePicker";

export const HorizontalTimeline: FunctionComponent = () => {
  const { taggs } = useAPI();
  const [selectedTaggs, setSelectedTaggs] = useState<Set<TaggPreviewModel>>(
    new Set()
  );
  const [checkboxes, setCheckboxes] = useState<CheckboxDefinition[]>([]);
  const [startDate, setStartDate] = useState<Date | null>(new Date());
  const [endDate, setEndDate] = useState<Date | null>(new Date());

  const handleDateChange = (newStart: Date | null, newEnd: Date | null) => {
    setStartDate(newStart);
    setEndDate(newEnd);
  };

  useEffect(() => {
    // TODO: Make this not loop so many times
    setCheckboxes((prev) => {
      return (taggs.value ?? []).map((tagg) => ({
        label: tagg.key,
        value: tagg.id,
        checked: prev.find((i) => i.value === tagg.id)?.checked ?? false,
      }));
    });
  }, [taggs.value]);

  const handleCheckboxChanged = (
    checkbox: CheckboxDefinition,
    checked: boolean
  ) => {
    const tagg = taggs.value?.find((i) => i.id === checkbox.value);
    setCheckboxes((prev) => {
      return [
        ...prev.map((i) =>
          i.value === checkbox.value ? { ...i, checked } : i
        ),
      ];
    });
    if (!tagg) {
      return;
    }
    if (checked) {
      setSelectedTaggs((prev) => new Set([...prev.values(), tagg]));
    } else {
      setSelectedTaggs(
        (prev) => new Set([...prev].filter((i) => i.id !== checkbox.value))
      );
    }
  };

  return (
    <Paper>
      <Grid container>
        <Grid item xs={3} padding={1}>
          <CheckboxList items={checkboxes} onChange={handleCheckboxChanged} />
        </Grid>
        <Grid item xs={9}>
          <Grid
            item
            xs={12}
            padding={1}
            display="flex"
            justifyContent="flex-end"
          >
            <DateRangePicker
              startDate={startDate}
              endDate={endDate}
              onChange={handleDateChange}
            ></DateRangePicker>
          </Grid>
          <Grid item xs={12}>
            Main content
          </Grid>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default HorizontalTimeline;
