namespace CustomerEngagement.Api.Middlewares;

public class ApiErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
}