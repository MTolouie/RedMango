using System.Net;

namespace RedMango_API.Models;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public List<string> ErrorMessages { get; set; }
    public object Results { get; set; }
}
