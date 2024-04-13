using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using School.API.ExceptionHandling.Data.Responses;

namespace School.API.ExceptionHandling.Exceptions;

public static class ExceptionMiddlewareExtensions
{
    
    //built-in exception handler
    public static void ConfigureBuiltInExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextErrorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var contextRequestFeature = context.Features.Get<IHttpRequestFeature>();

                if (contextErrorFeature != null)
                {
                    var response = new ErrorResponseData
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = contextErrorFeature.Error.Message,
                        Path = contextRequestFeature.Path
                    };
                    //context.Response.StatusCode = response.StatusCode;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(response.ToString());
                }

            });
        });
    }

    //custom exception handler
    public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandler>();
    }
}
