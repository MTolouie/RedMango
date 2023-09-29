export default interface ApiResponseModel {
  httpStatusCode: number;
  isSuccessful: boolean;
  errorMessage: string | null | Error;
  results: any | null;
}