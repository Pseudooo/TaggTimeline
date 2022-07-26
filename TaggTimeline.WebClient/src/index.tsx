import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { CssBaseline } from "@mui/material";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter } from "react-router-dom";
import { Provider as StoreProvider } from "react-redux";
import store from "./store";
import { AuthProvider } from "./contexts/Auth";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment";
import { APIProvider } from "./contexts/API";
import { ToasterProvider } from "./contexts/Toaster";

// It's fair to say this nesting is disgusting.
// TODO: Investigate a better way of doing this
const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    {/* Provides ability to create toaster notifications, top most so all can access */}
    <ToasterProvider>
      {/* Create the router for the app */}
      <BrowserRouter>
        {/* Reset CSS across the board */}
        <CssBaseline />
        {/* Provide the store for the app */}
        <StoreProvider store={store}>
          {/* Localization needed for date/time pickers */}
          <LocalizationProvider dateAdapter={AdapterMoment}>
            {/* Needed for auth across the app */}
            <AuthProvider>
              {/* Needed for calling the API */}
              <APIProvider>
                {/* The main app */}
                <App />
              </APIProvider>
            </AuthProvider>
          </LocalizationProvider>
        </StoreProvider>
      </BrowserRouter>
    </ToasterProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
