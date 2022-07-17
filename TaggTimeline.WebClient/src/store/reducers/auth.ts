import { createSlice, PayloadAction } from "@reduxjs/toolkit";

// Initial state of the auth store
const initialState = {
  loggedIn: false,
  user: undefined,
};

export const authSlice = createSlice({
  name: "Auth",
  initialState,
  reducers: {
    /**
     * Updates the logged in status of the user in the store
     * @param state The state of the app
     * @param action The action to take on the state
     */
    updateIsLoggedIn(state, action: PayloadAction<boolean>) {
      state.loggedIn = action.payload;
    },
  },
});

export const { updateIsLoggedIn } = authSlice.actions;

export default authSlice.reducer;
