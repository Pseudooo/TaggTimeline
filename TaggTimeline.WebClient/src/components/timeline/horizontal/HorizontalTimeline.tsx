import {
  Box,
  CircularProgress,
  FormControlLabel,
  Grid,
  Paper,
  Switch,
} from "@mui/material";
import moment from "moment";
import { Moment } from "moment";
import { FunctionComponent, useEffect, useState } from "react";
import { TaggModel, TaggPreviewModel } from "../../../api/generated";
import { useAPI } from "../../../contexts/API";
import { DataWrapper } from "../../../store/reducers/api";
import { SelectTaggsDropdown } from "../../io/custom/SelectTaggsDropdown";
import { DateRangePicker } from "../../io/DateRangePicker";
import { TimelineTaggInstance } from "../TimelineTaggInstance";
import { stringToColour } from "../../../util";

interface HorizontalTimelineRowProps {
  tagg: TaggModel;
  startDate: Moment;
  endDate: Moment;
}

/**
 * Creates a row in the horizontal timeline
 * @param props The props for this component
 */
const HorizontalTimelineRow: FunctionComponent<HorizontalTimelineRowProps> = ({
  tagg,
  startDate,
  endDate,
}) => {
  const startTime = startDate.valueOf();
  const timeRange = endDate.valueOf() - startTime;

  const timelineEntries = tagg.instances?.map((instance) => {
    const occuranceTime = moment(instance.occuranceDate).valueOf();
    const offset = (occuranceTime - startTime) / timeRange;
    return (
      <TimelineTaggInstance
        key={instance.id}
        tagg={tagg}
        taggInstance={instance}
        sx={{ position: "absolute", left: `${offset * 100}%` }}
      />
    );
  });

  return (
    <Box
      sx={{
        position: "relative",
        height: "1rem",
        padding: "1rem",
        "&::before": {
          content: "''",
          position: "absolute",
          left: 0,
          right: 0,
          top: "50%",
          height: "1px",
          bgcolor: stringToColour(tagg.id),
        },
      }}
    >
      <Box
        sx={{ marginLeft: "1rem", marginRight: "1rem", position: "relative" }}
      >
        {timelineEntries}
      </Box>
    </Box>
  );
};

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

  const rows = chosenTaggDetails.map((tagg, i) => {
    return tagg.value ? (
      <HorizontalTimelineRow
        key={tagg.value.id}
        tagg={tagg.value}
        startDate={startDate ?? moment()}
        endDate={endDate ?? moment()}
      />
    ) : (
      <Box key={i} flex={1} textAlign="center">
        <CircularProgress />
      </Box>
    );
  });

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
          />
          <DateRangePicker
            startDate={startDate}
            endDate={endDate}
            onChange={handleDateChange}
            disabled={autoDate}
          />
        </Grid>
        <Grid item xs={12}>
          {rows}
        </Grid>
      </Grid>
    </Paper>
  );
};

export default HorizontalTimeline;
