/* eslint-disable @typescript-eslint/no-explicit-any */
import { Moment } from "moment";
import { Api, HttpResponse, RequestParams } from "./generated";

const apiInstance = new Api();

export type UserToken = string | null;

/**
 * Wrapper function for API calls. Handles injecting things like auth headers
 * @param fn The handler function, responsible for appending params to the API call
 * @returns The response from the API call, if successful
 */
async function wrappedFetch<T extends (params: RequestParams) => any>(
  fn: T,
  token?: UserToken
): Promise<ReturnType<T>> {
  const params: RequestParams = {};
  if (token) {
    params.headers = {
      Authorization: `Bearer ${token}`,
    };
  }
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
 * @param colour The colour of the tagg
 * @returns The Tagg, if created
 */
export async function createTagg(
  this: UserToken,
  name: string,
  colour: string
) {
  return handleResponse(
    await wrappedFetch(
      (params) => apiInstance.tagg.taggCreate({ key: name, colour }, params),
      this
    )
  );
}

/**
 * Gets all the Taggs
 * @returns A list of Taggs
 */
export async function getAllTaggs(this: UserToken) {
  return handleResponse(
    await wrappedFetch((params) => apiInstance.tagg.getTagg(params), this)
  );
}

/**
 * Creates an instance of a tag
 * @param taggId The id of the tag
 * @param occuranceDate The occurance of the tag
 * @returns The created instance
 */
export async function createTaggInstance(
  this: UserToken,
  taggId: string,
  occuranceDate: Moment
) {
  return handleResponse(
    await wrappedFetch(
      (params) =>
        apiInstance.tagg.instanceCreate(
          { taggId, occuranceDate: occuranceDate?.toJSON() },
          params
        ),
      this
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

/**
 * Gets a tagg by ID from the API
 * @param taggId The ID of the tagg
 * @returns The detailed Tagg
 */
export async function getTagg(this: UserToken, taggId: string) {
  return handleResponse(
    await wrappedFetch(
      (params) => apiInstance.tagg.taggDetail(taggId, params),
      this
    )
  );
}
