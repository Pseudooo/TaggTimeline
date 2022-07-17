import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AuthUser } from "../../api/auth";

export enum AuthStatus {
  LOGGED_OUT,
  LOGGED_IN,
  LOGGING_IN,
}

export interface AuthState {
  loggedIn: boolean;
  status: AuthStatus;
  user?: AuthUser;
}

// Initial state of the auth store
const initialState: AuthState = {
  loggedIn: false,
  status: AuthStatus.LOGGED_OUT,
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
    updateAuthStatus(state, action: PayloadAction<AuthStatus>) {
      state.status = action.payload;
      state.loggedIn = action.payload === AuthStatus.LOGGED_IN;
    },
  },
});

export const { updateAuthStatus } = authSlice.actions;

export default authSlice.reducer;
