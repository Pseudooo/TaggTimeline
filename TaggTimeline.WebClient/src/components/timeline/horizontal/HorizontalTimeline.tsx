import { Grid, Paper } from "@mui/material";
import { FunctionComponent, useState } from "react";
import { TaggPreviewModel } from "../../../api/generated";
import { useAPI } from "../../../contexts/API";
import { SelectTaggsDropdown } from "../../io/custom/SelectTaggsDropdown";
import { DateRangePicker } from "../../io/DateRangePicker";

export const HorizontalTimeline: FunctionComponent = () => {
  const { taggs } = useAPI();
  const [chosenTaggs, setChosenTaggs] = useState<TaggPreviewModel[]>([]);
  const [startDate, setStartDate] = useState<Date | null>(new Date());
  const [endDate, setEndDate] = useState<Date | null>(new Date());

  const handleDateChange = (newStart: Date | null, newEnd: Date | null) => {
    setStartDate(newStart);
    setEndDate(newEnd);
  };

  return (
    <Paper>
      <Grid container>
        <Grid item xs={3} padding={1}>
          <SelectTaggsDropdown
            taggs={taggs.value ?? []}
            selected={chosenTaggs}
            onChange={(taggs) => setChosenTaggs(taggs)}
          />
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
