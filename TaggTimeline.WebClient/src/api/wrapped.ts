/* eslint-disable @typescript-eslint/no-explicit-any */
import { Api, HttpResponse, RequestParams } from "./generated";

const apiInstance = new Api();

/**
 * Wrapper function for API calls. Handles injecting things like auth headers
 * @param fn The handler function, responsible for appending params to the API call
 * @returns The response from the API call, if successful
 */
async function wrappedFetch<T extends (params: RequestParams) => any>(
  fn: T
): Promise<ReturnType<T>> {
  const params = {};
  const result = await fn(params);
  return result;
}

/**
 * Extracts the data from a request, throwing an error if unsuccessful.
 * @param response The response object
 * @returns The data from the request
 */
function handleResponse<T = any, E = any>(response: HttpResponse<T, E>): T {
  if (response.error) {
    throw new Error(`${response.error}`);
  }
  if (!response.ok) {
    throw new Error(`Response not OK`);
  }
  return response.data;
}

/**
 * Creates a tagg
 * @param name The name to use for the tagg
 * @returns The Tagg, if created
 */
export async function createTagg(name: string) {
  return handleResponse(
    await wrappedFetch((params) =>
      apiInstance.tagg.taggCreate({ key: name }, params)
    )
  );
}

/**
 * Gets all the Taggs
 * @returns A list of Taggs
 */
export async function getAllTaggs() {
  return handleResponse(
    await wrappedFetch((params) => apiInstance.tagg.getTagg(params))
  );
}

/**
 * Creates an instance of a tag
 * @param taggId The id of the tag
 * @param occuranceDate The occurance of the tag
 * @returns The created instance
 */
export async function createTaggInstance(taggId: string, occuranceDate: Date) {
  return handleResponse(
    await wrappedFetch((params) =>
      apiInstance.tagg.instanceCreate(
        { taggId, occuranceDate: occuranceDate?.toJSON() },
        params
      )
    )
  );
}

/**
 * Registers an account, and returns a token
 * @param username The username of the user to create
 * @param password The password to secure the new account
 * @returns An authorization token
 */
export async function registerUser(username: string, password: string) {
  return handleResponse(
    await wrappedFetch((params) =>
      apiInstance.identity.registerCreate(
        { userName: username, password },
        params
      )
    )
  );
}

/**
 * Logs an account in, and returns a token
 * @param username The username of the user to login
 * @param password The password to the account
 * @returns An authorization token
 */
export async function loginUser(username: string, password: string) {
  return handleResponse(
    await wrappedFetch((params) =>
      apiInstance.identity.loginCreate({ userName: username, password }, params)
    )
  );
}
