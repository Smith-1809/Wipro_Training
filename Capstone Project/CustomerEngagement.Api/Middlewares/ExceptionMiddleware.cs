using System.Net;
using System.Text.Json;
using CustomerEngagement.Api.Middlewares;

namespace CustomerEngagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResponse
            {
                Success = false,
                Message = ex.Message,
                Details = null
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}