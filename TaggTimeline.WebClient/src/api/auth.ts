import { fakeFetch } from "../util/placeholder";

export type AuthUser = {
  name: string;
};

/**
 * Logs a user in
 * @param name The name of the user
 * @returns The user information if successful
 */
export async function login(name: string) {
  const user = await fakeFetch({ name } as AuthUser, 500);
  return user;
}

/**
 * Logs a user out
 * @returns If the user was successfully logged out
 */
export async function logout() {
  const success = await fakeFetch(true, 200);
  return success;
}
