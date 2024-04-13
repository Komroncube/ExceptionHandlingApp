using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using School.API.ExceptionHandling.Data.Responses;

namespace School.API.ExceptionHandling.Exceptions.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            StudentNameException => (int)HttpStatusCode.BadRequest,
            StudentAgeException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new ErrorResponseData
        {
            StatusCode = statusCode,
            Message = context.Exception.Message,
            Path = context.Exception.StackTrace
            //Path = context.HttpContext.Request.Path
        };
        //context.HttpContext.Response.ContentType = "application/json";
        //context.HttpContext.Response.StatusCode = response.StatusCode;
        
        context.Result = new JsonResult(response)
        {
            ContentType = "application/json",
            StatusCode = response.StatusCode
        };
    }
}
