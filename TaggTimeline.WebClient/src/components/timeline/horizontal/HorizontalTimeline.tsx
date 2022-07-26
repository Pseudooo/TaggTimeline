import { Grid, Paper } from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";
import { TaggModel, TaggPreviewModel } from "../../../api/generated";
import { useAPI } from "../../../contexts/API";
import { DataWrapper } from "../../../store/reducers/api";
import { SelectTaggsDropdown } from "../../io/custom/SelectTaggsDropdown";
import { DateRangePicker } from "../../io/DateRangePicker";

export const HorizontalTimeline: FunctionComponent = () => {
  const { useTaggs, useTaggDetails, initTaggDetails } = useAPI();
  const taggs = useTaggs();
  const taggDetails = useTaggDetails();
  const [chosenTaggs, setChosenTaggs] = useState<TaggPreviewModel[]>([]);
  const [chosenTaggDetails, setChosenTaggDetails] = useState<
    DataWrapper<TaggModel>[]
  >([]);
  const [startDate, setStartDate] = useState<Date | null>(new Date());
  const [endDate, setEndDate] = useState<Date | null>(new Date());

  const handleDateChange = (newStart: Date | null, newEnd: Date | null) => {
    setStartDate(newStart);
    setEndDate(newEnd);
  };

  useEffect(() => {
    chosenTaggs.forEach((tagg) => initTaggDetails(tagg.id));
    setChosenTaggDetails([...chosenTaggs.map((tagg) => taggDetails[tagg.id])]);
  }, [chosenTaggs, taggDetails]);

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
            {chosenTaggDetails.map((tagg, i) => (
              <p key={i}>{`${JSON.stringify(tagg)}`}</p>
            ))}
          </Grid>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default HorizontalTimeline;
