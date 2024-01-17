using System.Net;
using Newtonsoft.Json;
using stationeryManagement.Service.Exceptions;

namespace stationeryManagement.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            case UnauthorizedException _:
                code = HttpStatusCode.Unauthorized;
                break;
            case BadRequestException _:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
            // Thêm các case cho các loại exception khác nếu cần
        }

        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        // if (code == HttpStatusCode.InternalServerError)
        // {
        //     result = JsonConvert.SerializeObject(new { error = "An error occurred" });
        // }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}