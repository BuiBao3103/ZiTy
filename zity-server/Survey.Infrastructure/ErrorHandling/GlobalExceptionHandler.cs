using Survey.Application.Core.Exceptions;
using Survey.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
namespace Survey.Infrastructure.ErrorHandling;


public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unexpected error occurred");

        ProblemDetails problemDetails;
        int statusCode;

        switch (exception)
        {
            case EntityNotFoundException ex:
                statusCode = StatusCodes.Status404NotFound;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Title = "Resource Not Found",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path
                };
                problemDetails.Extensions["code"] = ex.Code;
                break;

            case ValidationException ex:
                statusCode = StatusCodes.Status400BadRequest;
                var validationProblemDetails = new ValidationProblemDetails
                {
                    Status = statusCode,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Validation Error",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path
                };

                // Add validation errors
                foreach (var error in ex.Errors)
                {
                    validationProblemDetails.Errors.Add(error.Key, error.Value);
                }

                problemDetails = validationProblemDetails;
                break;

            case BusinessRuleException ex:
                statusCode = StatusCodes.Status400BadRequest;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Business Rule Violation",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path
                };
                problemDetails.Extensions["code"] = ex.Code;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Detail = _env.IsDevelopment() ? exception.Message : "An unexpected error occurred",
                    Instance = httpContext.Request.Path
                };
                break;
        }

        // Add common extensions
        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        if (_env.IsDevelopment())
        {
            problemDetails.Extensions["debugInfo"] = new
            {
                exception.StackTrace,
                exception.Source
            };
        }

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
