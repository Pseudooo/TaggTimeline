/**
 * Fakes a fetch request by returning the given data after a set delay
 * @param data The data to return
 * @param delay An optional delay to immitate network delay
 * @returns The data supplied after delay
 */
export async function fakeFetch<T>(data: T, delay?: number): Promise<T> {
  return new Promise((resolve) => {
    window.setTimeout(() => {
      resolve(data);
    }, delay ?? 0);
  });
}
