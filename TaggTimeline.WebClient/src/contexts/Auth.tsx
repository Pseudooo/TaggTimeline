import {
  createContext,
  FunctionComponent,
  PropsWithChildren,
  useContext,
  useMemo,
  useState,
} from "react";
import {
  AuthUser,
  login as authLogin,
  logout as authLogout,
} from "../api/auth";

interface AuthContextType {
  user?: AuthUser;
  login: (name: string) => Promise<AuthUser>;
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
  const [user, setUser] = useState<AuthUser>();

  /**
   * Attempts to log in
   * @param name The name of the user to log in
   */
  async function login(name: string) {
    setUser(undefined);
    const user = await authLogin(name);
    setUser(user);
    return user;
  }

  /**
   * Attempts to log out the current user
   * @returns If the user successfully logged out
   */
  async function logout() {
    const success = await authLogout();
    setUser(undefined);
    return success;
  }

  // Use Memo'd versions to prevent re-rendering unnecessarily
  const memoedValue = useMemo<AuthContextType>(
    () => ({
      user,
      login,
      logout,
    }),
    [user]
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
