using RedMango_API.Models;
using System.Net;

namespace RedMango_API.Utilities
{
    public static class ApiResponseConfiguration
    {
        public static ApiResponse ConfigureResponse(bool isSuccessful,HttpStatusCode code,string errorMessage,object result)
        {
            
            ApiResponse response = new ApiResponse()
            {
                ErrorMessage = errorMessage,
                IsSuccessful = isSuccessful,
                Results = result,
                StatusCode = code
            };

            return response;
        }
    }
}
