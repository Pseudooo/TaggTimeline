import { fakeFetch } from "../util/placeholder";
import { registerUser, loginUser } from "./wrapped";

export type AuthUser = {
  username: string;
};

/**
 * Registers an account, and returns a token
 * @param username The username of the user to create
 * @param password The password to secure the new account
 * @returns An authorization token
 */
export async function register(username: string, password: string) {
  const response = await registerUser(username, password);
  return response.token;
}

/**
 * Logs an account in, and returns a token
 * @param username The username of the user to login
 * @param password The password to the account
 * @returns An authorization token
 */
export async function login(username: string, password: string) {
  const response = await loginUser(username, password);
  return response.token;
}

/**
 * Logs a user out
 * @returns If the user was successfully logged out
 */
export async function logout() {
  const success = await fakeFetch(true, 2000);
  return success;
}
