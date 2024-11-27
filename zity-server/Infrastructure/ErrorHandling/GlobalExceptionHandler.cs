using Application.Core.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ErrorHandling;

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

        var problemDetails = CreateProblemDetails(httpContext, exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase },
            cancellationToken
        );

        return true;
    }

    private ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
    {
        ProblemDetails problemDetails = exception switch
        {
            EntityNotFoundException ex => CreateNotFoundProblemDetails(httpContext, ex),
            ValidationException ex => CreateValidationProblemDetails(httpContext, ex),
            BusinessRuleException ex => CreateBusinessRuleProblemDetails(httpContext, ex),
            _ => CreateDefaultProblemDetails(httpContext, exception)
        };

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

        return problemDetails;
    }

    private ProblemDetails CreateNotFoundProblemDetails(HttpContext httpContext, EntityNotFoundException ex)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "Resource Not Found",
            Detail = ex.Message,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions["code"] = ex.Code;
        return problemDetails;
    }

    private ProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ValidationException ex)
    {
        var validationProblemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Validation Error",
            Detail = "One or more validation errors occurred",
            Instance = httpContext.Request.Path
        };

        // Add validation errors to both Errors and Extensions for maximum compatibility
        foreach (var error in ex.Errors)
        {
            validationProblemDetails.Errors.Add(error.Key, error.Value);
        }
        validationProblemDetails.Extensions["errors"] = ex.Errors;

        return validationProblemDetails;
    }

    private ProblemDetails CreateBusinessRuleProblemDetails(HttpContext httpContext, BusinessRuleException ex)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Business Rule Violation",
            Detail = ex.Message,
            Instance = httpContext.Request.Path
        };
        problemDetails.Extensions["code"] = ex.Code;
        return problemDetails;
    }

    private ProblemDetails CreateDefaultProblemDetails(HttpContext httpContext, Exception exception)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Detail = _env.IsDevelopment() ? exception.Message : "An unexpected error occurred",
            Instance = httpContext.Request.Path
        };
    }
}