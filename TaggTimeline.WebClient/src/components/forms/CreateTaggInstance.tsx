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
import { FunctionComponent, useEffect, useState } from "react";
import { Tagg } from "../../api/generated";
import { useAPI } from "../../contexts/API";
import TextField from "../io/TextField";

const TaggListItem: FunctionComponent<{ tagg: Tagg }> = ({ tagg }) => {
  return (
    <ListItem disablePadding>
      <ListItemButton>
        <ListItemText primary={tagg.key} secondary={tagg.id} />
      </ListItemButton>
    </ListItem>
  );
};

/**
 * Form for creating a new Tagg instance
 */
export const CreateTaggInstanceForm: FunctionComponent = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const { taggs } = useAPI();
  const [filteredTaggs, setFilteredTaggs] = useState<Tagg[]>(taggs ?? []);

  const handleSearchChange = (value: string) => {
    setSearchTerm(value);
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
          tagg.key.toLowerCase().includes(searchTerm.toLowerCase())
        ),
      ]);
    }
  }, [taggs, searchTerm]);

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
        {filteredTaggs.map((tagg) => (
          <TaggListItem tagg={tagg} key={tagg.id} />
        ))}
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
