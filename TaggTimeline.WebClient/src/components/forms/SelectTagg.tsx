import { Add, Circle } from "@mui/icons-material";
import {
  Alert,
  AlertTitle,
  Card,
  Divider,
  FormControl,
  Link,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { FunctionComponent, useEffect, useState } from "react";
import { TaggPreviewModel } from "../../api/generated";
import { useAPI } from "../../contexts/API";
import { stringToColour } from "../../util";
import TextField from "../io/TextField";
import { CreateTaggForm } from "./CreateTagg";

interface TaggListItemProps {
  tagg: TaggPreviewModel;
  onClick(tagg: TaggPreviewModel): void;
}

const TaggListItem: FunctionComponent<TaggListItemProps> = ({
  tagg,
  onClick,
}) => {
  return (
    <ListItem disablePadding dense>
      <ListItemButton onClick={() => onClick(tagg)}>
        <ListItemIcon>
          <Circle sx={{ color: stringToColour(tagg.id ?? "") }} />
        </ListItemIcon>
        <ListItemText primary={tagg.key} secondary={tagg.id} />
      </ListItemButton>
    </ListItem>
  );
};

export interface SelectTaggFormProps {
  onSelected?(tagg: TaggPreviewModel): void;
}

/**
 * Form for creating a new Tagg instance
 */
export const SelectTaggForm: FunctionComponent<SelectTaggFormProps> = ({
  onSelected,
}) => {
  const { taggs } = useAPI();
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredTaggs, setFilteredTaggs] = useState<TaggPreviewModel[]>(
    taggs ?? []
  );
  const [shouldCreateTagg, setShouldCreateTagg] = useState(false);

  const handleSearchChange = (value: string) => {
    setSearchTerm(value);
  };

  const handleTaggSelected = (tagg: TaggPreviewModel) => {
    if (onSelected) {
      onSelected(tagg);
    }
  };

  const handleCreateTagg = () => {
    setShouldCreateTagg(true);
  };

  const handleCancelCreateTagg = () => {
    setShouldCreateTagg(false);
  };

  /**
   * Update the filtered list of Taggs
   */
  useEffect(() => {
    if (!taggs) {
      setFilteredTaggs([]);
    } else {
      setFilteredTaggs([
        ...taggs.filter((tagg) =>
          (tagg.key ?? "").toLowerCase().includes(searchTerm.toLowerCase())
        ),
      ]);
    }
  }, [taggs, searchTerm]);

  const taggListItems = filteredTaggs.map((tagg) => (
    <TaggListItem tagg={tagg} key={tagg.id} onClick={handleTaggSelected} />
  ));

  return shouldCreateTagg ? (
    <CreateTaggForm
      onSuccess={handleTaggSelected}
      onCancel={handleCancelCreateTagg}
      placeholder={searchTerm}
    />
  ) : (
    <Card sx={{ display: "flex", flexDirection: "column" }}>
      <FormControl fullWidth sx={{ padding: 2, flex: "0 0 auto" }}>
        <TextField
          label="Filter Taggs..."
          value={searchTerm}
          onChange={handleSearchChange}
        ></TextField>
      </FormControl>
      <List disablePadding sx={{ overflow: "auto", flex: "1 1 auto" }}>
        {taggListItems.length > 0 ? (
          taggListItems
        ) : (
          <Alert severity="warning">
            <AlertTitle>No results</AlertTitle>
            Maybe{" "}
            <Link
              component="button"
              onClick={handleCreateTagg}
              underline="hover"
              color="inherit"
              variant="body2"
            >
              create a new tagg?
            </Link>
          </Alert>
        )}
      </List>
      <Divider />
      <ListItem disablePadding>
        <ListItemButton onClick={handleCreateTagg}>
          <ListItemIcon>
            <Add />
          </ListItemIcon>
          <ListItemText>Create new Tagg...</ListItemText>
        </ListItemButton>
      </ListItem>
    </Card>
  );
};
