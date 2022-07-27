import moment from "moment";
import { Moment } from "moment";
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
  const [autoDate, setAutoDate] = useState(true);
  const [startDate, setStartDate] = useState<Moment | null>(moment());
  const [endDate, setEndDate] = useState<Moment | null>(moment());

  const handleDateChange = (newStart: Moment | null, newEnd: Moment | null) => {
    setStartDate(newStart);
    setEndDate(newEnd);
  };

  useEffect(() => {
    chosenTaggs.forEach((tagg) => initTaggDetails(tagg.id));
    setChosenTaggDetails([...chosenTaggs.map((tagg) => taggDetails[tagg.id])]);
  }, [chosenTaggs, taggDetails]);

  useEffect(() => {
    if (autoDate) {
      let newStartDate: Moment | undefined = undefined;
      let newEndDate: Moment | undefined = undefined;
      chosenTaggDetails.forEach((taggDetails) => {
        if (!taggDetails.value || !taggDetails.value.instances) {
          return;
        }
        taggDetails.value.instances.forEach((instance) => {
          const date = moment(instance.occuranceDate);
          if (!newStartDate || date < newStartDate) {
            newStartDate = date;
          }
          if (!newEndDate || date > newEndDate) {
            newEndDate = date;
          }
        });
      });
      setStartDate(newStartDate ?? moment());
      setEndDate(newEndDate ?? moment());
    }
  }, [autoDate, chosenTaggDetails]);

  return (
    <Paper>
      <Grid container>
        <Grid item xs={12} padding={1} gap={1} display="flex">
          <SelectTaggsDropdown
            taggs={taggs.value ?? []}
            selected={chosenTaggs}
            onChange={(taggs) => setChosenTaggs(taggs)}
          />
          <FormControlLabel
            control={
              <Switch
                checked={autoDate}
                onChange={(e) => setAutoDate(e.target.checked)}
              />
            }
            label="Auto"
            labelPlacement="end"
          ></FormControlLabel>
          <DateRangePicker
            startDate={startDate}
            endDate={endDate}
            onChange={handleDateChange}
            disabled={autoDate}
          />
        </Grid>
        <Grid item xs={12}>
          Main content
          {chosenTaggDetails.map((tagg, i) => (
            <p key={i}>{`${JSON.stringify(tagg)}`}</p>
          ))}
        </Grid>
      </Grid>
    </Paper>
  );
};

export default HorizontalTimeline;
