import {
  Box,
  ClickAwayListener,
  InputLabel,
  OutlinedInput,
} from "@mui/material";
import { FunctionComponent, useState } from "react";
import { ChromePicker } from "react-color";

interface ColourPickerProps {
  label?: string;
  value: string;
  onChange: (newValue: string) => void;
  disabled?: boolean;
}

export const ColourPicker: FunctionComponent<ColourPickerProps> = ({
  label = "Colour",
  value,
  onChange,
  disabled = false,
}) => {
  const [showPicker, setShowPicker] = useState(false);

  return (
    <>
      <InputLabel htmlFor="">{label}</InputLabel>
      {/* 
        A bit lazy setting the width to 0, but it keeps styling inline with other inputs
        without having to be manual about it
      */}
      <OutlinedInput
        label={label}
        onClick={() => setShowPicker(true)}
        onChange={(value) => onChange(value.target.value)}
        disabled={disabled}
        inputProps={{ style: { width: "0" } }}
        sx={{ cursor: "pointer" }}
        startAdornment={
          <Box
            sx={{
              bgcolor: value,
              borderRadius: "4px",
              width: "100%",
              height: "2rem",
              minWidth: "2rem",
            }}
          />
        }
        readOnly
      />
      {showPicker && (
        <ClickAwayListener onClickAway={() => setShowPicker(false)}>
          <div style={{ position: "relative" }}>
            <div style={{ position: "fixed", zIndex: "2", userSelect: "none" }}>
              <ChromePicker
                color={value}
                onChange={(value) => onChange(value.hex)}
                disableAlpha
              />
            </div>
          </div>
        </ClickAwayListener>
      )}
    </>
  );
};
