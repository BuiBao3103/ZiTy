using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Infrastructure.Extensions;

public static class ProblemDetailsExtensions
{
    public static ProblemDetails ToProblemDetails(
        this Exception exception,
        HttpContext httpContext,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new ProblemDetails
        {
            Status = (int)statusCode,
            Type = GetProblemType(statusCode),
            Title = GetTitle(statusCode),
            Detail = exception.Message,
            Instance = httpContext.Request.Path,
            Extensions =
        {
            ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier
        }
        };
    }

    private static string GetProblemType(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            HttpStatusCode.BadRequest => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
    }

    private static string GetTitle(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.NotFound => "Resource Not Found",
            HttpStatusCode.BadRequest => "Bad Request",
            _ => "Internal Server Error"
        };
    }
}
