using System.Net;
using Newtonsoft.Json;
using stationeryManagement.Service.Exceptions;

namespace stationeryManagement.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<dynamic> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<dynamic> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = new ErrorDetails();
        switch (exception)
        {
            case NotFoundException:
                result.StatusCode = HttpStatusCode.NotFound;
                result.Message = exception.Message;
                break;
            case UnauthorizedException:
                result.StatusCode = HttpStatusCode.Unauthorized;
                result.Message = exception.Message;
                break;
            case BadRequestException:
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Message = exception.Message;
                break;
            default:
                result.StatusCode = HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)result.StatusCode;
        return context.Response.WriteAsync(result.ToString());
    }

}