using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using zity.ExceptionHandling;

namespace zity.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IHostEnvironment _env;

        // Constructor injection for IHostEnvironment
        public GlobalExceptionHandler(IHostEnvironment env)
        {
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is AppError)
            {
                httpContext.Response.StatusCode = (exception as AppError).StatusCode;
            }
            // Set the HTTP status code to 500 (Internal Server Error) by default
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            var details = new ProblemDetails
            {
                Instance = httpContext.Request.Path,
                Status = httpContext.Response.StatusCode,
                Title = "An unexpected error occurred.",
                Detail = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7807"
            };

            // Provide detailed information in development environment
            if (_env.IsDevelopment())
            {
                details.Title = exception.Message;
                details.Detail = exception.ToString(); // Include stack trace and more info
                details.Extensions["traceId"] = httpContext.TraceIdentifier;
                details.Extensions["data"] = exception.Data;
            }
            else
            {
                // Log the exception or perform other actions in production
                details.Title = "An unexpected error occurred.";
                details.Detail = "An error occurred while processing your request.";
            }

            // Set the content type and write the problem details to the response
            httpContext.Response.ContentType = "application/problem+json";
            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken: cancellationToken);

            return true;
        }
    }
}
