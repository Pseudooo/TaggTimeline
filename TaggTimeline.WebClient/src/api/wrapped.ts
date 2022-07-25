/* eslint-disable @typescript-eslint/no-explicit-any */
import { Api, HttpResponse, RequestParams } from "./generated";

const apiInstace = new Api();

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
      apiInstace.tagg.taggCreate({ key: name }, params)
    )
  );
}

/**
 * Gets all the Taggs
 * @returns A list of Taggs
 */
export async function getAllTaggs() {
  return handleResponse(
    await wrappedFetch((params) => apiInstace.tagg.getTagg(params))
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
      apiInstace.tagg.instanceCreate(
        { taggId, occuranceDate: occuranceDate?.toJSON() },
        params
      )
    )
  );
}

/**
 * Gets a tagg by ID from the API
 * @param taggId The ID of the tagg
 * @returns The detailed Tagg
 */
export async function getTagg(taggId: string) {
  return handleResponse(
    await wrappedFetch((params) => apiInstace.tagg.taggDetail(taggId, params))
  );
}
