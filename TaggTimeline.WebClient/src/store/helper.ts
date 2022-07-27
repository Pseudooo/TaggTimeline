import { AnyAction, ThunkAction } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import store from ".";

// The state of the app
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

// Strongly-typed useDispatch hook
export const useAppDispatch = () => useDispatch<AppDispatch>();
// Strongly-typed useSelector hook
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

// Reusable thunk definition
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  AnyAction
>;
