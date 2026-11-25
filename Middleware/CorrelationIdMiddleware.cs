namespace AddisEats.Api.Middleware;
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var CorrelationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();
       
       if(string.IsNullOrWhiteSpace(CorrelationId))
        {
            CorrelationId = Guid.NewGuid().ToString();
            context.Request.Headers["X-Correlation-ID"] = CorrelationId;
        }
            context.TraceIdentifier = CorrelationId;
            context.Response.Headers["X-Correlation-ID"] = CorrelationId;

        using(_logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", CorrelationId } }))
        {
            await _next(context);
        }   
        }
}