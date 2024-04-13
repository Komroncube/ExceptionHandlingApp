using System.Net;
using School.API.ExceptionHandling.Data.Responses;

namespace School.API.ExceptionHandling.Exceptions;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new ErrorResponseData()
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = ex.Message,
            Path = context.Request.Path
        }.ToString();
        await context.Response.WriteAsync(errorResponse);
    }
}
