import { Outlet } from "react-router-dom";
import Routes from "./routes";

/**
 * The main App component
 */
function App() {
  return (
    <>
      <Routes />
      <Outlet />
    </>
  );
}

export default App;
