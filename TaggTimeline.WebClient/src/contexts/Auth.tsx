import {
  createContext,
  FunctionComponent,
  PropsWithChildren,
  useContext,
  useMemo,
  useState,
} from "react";
import { AuthenticationResultModel } from "../api/generated";
import { registerUser, loginUser } from "../api/wrapped";
import { fakeFetch } from "../util/placeholder";
import { useToaster } from "./Toaster";

interface AuthContextType {
  token: string | null;
  register: typeof registerUser;
  login: typeof loginUser;
  logout: () => Promise<boolean>;
}

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

/**
 * Creates a provider to use for auth functionality
 * @param props The props for this component
 */
export const AuthProvider: FunctionComponent<PropsWithChildren> = ({
  children,
}) => {
  const { createToaster } = useToaster();
  const [token, setToken] = useState<string | null>(null);

  /**
   * Registers an account, and returns a token
   * @param username The username of the user to create
   * @param password The password to secure the new account
   * @returns An authorization token
   */
  const register: AuthContextType["register"] = async (...args) => {
    setToken(null);
    const response = await registerUser(...args);
    setToken(response.token);
    createToaster({
      severity: "success",
      message: "Account created successfully",
    });

    return { token } as AuthenticationResultModel;
  };

  /**
   * Logs an account in, and returns a token
   * @param username The username of the user to login
   * @param password The password to the account
   * @returns An authorization token
   */
  const login: AuthContextType["login"] = async (...args) => {
    setToken(null);
    const response = await loginUser(...args);
    setToken(response.token);
    createToaster({ severity: "success", message: "Login successful" });
    return { token } as AuthenticationResultModel;
  };

  /**
   * Attempts to log out the current user
   * @returns If the user successfully logged out
   */
  async function logout() {
    await fakeFetch({}, 1000);
    setToken(null);
    createToaster({ severity: "success", message: "Logout successful" });
    return true;
  }

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValue = useMemo<AuthContextType>(
    () => ({
      token,
      register,
      login,
      logout,
    }),
    [token]
  );

  return (
    <AuthContext.Provider value={memoedValue}>{children}</AuthContext.Provider>
  );
};

/**
 * Helper function for getting the app's auth context
 */
export function useAuth() {
  return useContext(AuthContext);
}
