import { Add } from "@mui/icons-material";
import {
  Divider,
  FormControl,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { FunctionComponent, useState } from "react";
import TextField from "../io/TextField";

const TaggListItem: FunctionComponent = () => {
  return (
    <ListItem disablePadding>
      <ListItemButton>
        <ListItemText primary="Name of Tagg" secondary="## created" />
      </ListItemButton>
    </ListItem>
  );
};

export const CreateTaggInstanceForm: FunctionComponent = () => {
  const [searchTerm, setSearchTerm] = useState("");

  const handleSearchChange = () => {
    console.log(1);
  };

  return (
    <>
      <FormControl fullWidth sx={{ padding: 1 }}>
        <TextField
          label="Filter..."
          value={searchTerm}
          onChange={handleSearchChange}
        ></TextField>
      </FormControl>
      <List>
        <TaggListItem />
        <TaggListItem />
        <Divider />
        <ListItem disablePadding>
          <ListItemButton>
            <ListItemIcon>
              <Add />
            </ListItemIcon>
            <ListItemText>Create new Tagg...</ListItemText>
          </ListItemButton>
        </ListItem>
      </List>
    </>
  );
};
