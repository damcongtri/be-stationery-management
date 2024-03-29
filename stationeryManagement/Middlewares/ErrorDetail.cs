using System.Net;
using System.Text.Json;

namespace stationeryManagement.Middlewares;

public class ErrorDetails
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public string Message { get; set; } = "Internal Server Error";
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}