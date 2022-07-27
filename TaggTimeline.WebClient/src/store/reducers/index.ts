import { combineReducers } from "@reduxjs/toolkit";
import authReducer from "./auth";
import apiReducer from "./api";

/**
 * This combines all reducers into a single reducer to use for the store.
 * This should make adding reducers later easier.
 */
const rootReducer = combineReducers({
  auth: authReducer,
  api: apiReducer,
});

export default rootReducer;
