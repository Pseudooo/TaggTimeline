import { Grid, Paper } from "@mui/material";
import { FunctionComponent, useState } from "react";
import { CheckboxDefinition, CheckboxList } from "../../io/CheckboxList";
import { DateRangePicker } from "../../io/DateRangePicker";

const testItems: CheckboxDefinition[] = [
  { label: "Item 1", value: "id1", checked: false },
  { label: "Item 2", value: "id2", checked: true },
];

export const HorizontalTimeline: FunctionComponent = () => {
  const [tags, setTags] = useState([...testItems]);
  const [startDate, setStartDate] = useState<Date | null>(new Date());
  const [endDate, setEndDate] = useState<Date | null>(new Date());

  const handleDateChange = (newStart: Date | null, newEnd: Date | null) => {
    setStartDate(newStart);
    setEndDate(newEnd);
  };

  const handleTagsChange = (tag: CheckboxDefinition, checked: boolean) => {
    setTags(
      tags.map((item) =>
        item.value === tag.value ? { ...item, checked } : item
      )
    );
  };

  return (
    <Paper>
      <Grid container>
        <Grid item xs={3} padding={1}>
          <CheckboxList items={tags} onChange={handleTagsChange} />
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
