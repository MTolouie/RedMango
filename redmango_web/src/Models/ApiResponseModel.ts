export default interface ApiResponseModel {
  httpStatusCode: number;
  isSuccessful: boolean;
  errorMessage: string | null;
  results: any | null;
}