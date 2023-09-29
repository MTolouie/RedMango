import { MenuItemApiPath } from "../Common";
import { ApiResponseModel } from "../Models";

export async function fetchMenuItems(signal: any, action: string) {
  const url = MenuItemApiPath + action;
  const response: Response = await fetch(url, { signal: signal });

  if (!response.ok) {
    // Handle the error here, e.g., by throwing an exception or returning an error message

    throw new Error(`Failed to fetch data. Status code: ${response.status}`);
  }

  const data = await response.json();

  const apiResponse: ApiResponseModel = {
    httpStatusCode: data.StatusCode,
    isSuccessful: data.isSuccessful, // Assuming a successful HTTP response means success
    errorMessage: null, // You can set this based on your error handling logic
    results: data.results,      // Assign the actual data from the response
  };

  return apiResponse.results;
}