import { Grid, Paper } from "@mui/material";
import { FunctionComponent, useState } from "react";
import { DateRangePicker } from "../../io/DateRangePicker";

export const HorizontalTimeline: FunctionComponent = () => {
  const [startDate, setStartDate] = useState<Date | null>(new Date());
  const [endDate, setEndDate] = useState<Date | null>(new Date());

  const handleDateChange = (newStart: Date | null, newEnd: Date | null) => {
    setStartDate(newStart);
    setEndDate(newEnd);
  };

  return (
    <Paper>
      <Grid container>
        <Grid item xs={3}>
          Left column
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
