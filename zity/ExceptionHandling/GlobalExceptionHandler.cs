using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using zity.ExceptionHandling.Exceptions;
using ValidationException = zity.ExceptionHandling.Exceptions.ValidationException;

namespace zity.ExceptionHandling
{
    public class GlobalExceptionHandler(IHostEnvironment env) : IExceptionHandler
    {
        private readonly IHostEnvironment _env = env;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            var problemDetails = exception switch
            {
                // Custom application error
                AppError appError => CreateProblemDetails(
                    httpContext,
                    appError.StatusCode,
                    "Application Error",
                    appError.Message,
                    "https://api.yourapp.com/errors/application-error",
                    appError.ErrorCode),

                // Entity not found error
                EntityNotFoundException notFoundEx => CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status404NotFound,
                    "Resource Not Found",
                    notFoundEx.Message,
                    "https://api.yourapp.com/errors/not-found",
                    "ENTITY_NOT_FOUND"),

                // Validation error
                ValidationException validationEx => CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status400BadRequest,
                    "Validation Error",
                    "One or more validation errors occurred",
                    "https://api.yourapp.com/errors/validation",
                    "VALIDATION_ERROR",
                    new Dictionary<string, object?>
                    {
                        ["errors"] = validationEx.Errors
                    }),

                // Unauthorized access
                UnauthorizedAccessException unauthorizedEx => CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    unauthorizedEx.Message,
                    "https://api.yourapp.com/errors/unauthorized",
                    "UNAUTHORIZED"),

                // Forbidden access
                ForbiddenException forbiddenEx => CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status403Forbidden,
                    "Forbidden",
                    forbiddenEx.Message,
                    "https://api.yourapp.com/errors/forbidden",
                    "FORBIDDEN"),

                // Concurrency error
                ConcurrencyException concurrencyEx => CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status409Conflict,
                    "Concurrency Conflict",
                    concurrencyEx.Message,
                    "https://api.yourapp.com/errors/concurrency",
                    "CONCURRENCY_CONFLICT"),

                // Default server error
                _ => CreateProblemDetails(
                    httpContext,
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error",
                    "An unexpected error occurred while processing your request",
                    "https://api.yourapp.com/errors/internal",
                    "INTERNAL_SERVER_ERROR")
            };

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private ProblemDetails CreateProblemDetails(
            HttpContext context,
            int statusCode,
            string title,
            string detail,
            string type,
            string errorCode,
            IDictionary<string, object?>? additionalData = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Type = type,
                Instance = context.Request.Path
            };

            // Add common extensions
            problemDetails.Extensions["errorCode"] = errorCode;
            problemDetails.Extensions["timestamp"] = DateTime.UtcNow;
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;

            // Add environment-specific information
            if (_env.IsDevelopment())
            {
                problemDetails.Extensions["stackTrace"] = Environment.StackTrace;
            }

            // Add additional data if provided
            if (additionalData != null)
            {
                foreach (var (key, value) in additionalData)
                {
                    problemDetails.Extensions[key] = value;
                }
            }

            return problemDetails;
        }
    }
}