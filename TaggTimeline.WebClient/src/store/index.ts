import { configureStore } from "@reduxjs/toolkit";
import rootReducer from "./reducers";

// Load any stored data from localStorage
const storedData = localStorage.getItem("reducStorage");
const preloadedState = storedData !== null ? JSON.parse(storedData) : {};

// Configure the store
const store = configureStore({
  reducer: rootReducer,
  preloadedState,
});

export default store;
