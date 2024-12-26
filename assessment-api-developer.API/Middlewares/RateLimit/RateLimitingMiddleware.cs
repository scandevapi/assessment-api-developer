// Purpose: Middleware to limit the number of requests per minute for a client.
using System.Collections.Concurrent;

namespace assessment_api_developer.API.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, RateLimitInfo> _clients = new ConcurrentDictionary<string, RateLimitInfo>();
        private readonly int _requestsPerMinute;
        private readonly TimeSpan _timeSpan = TimeSpan.FromMinutes(1);

        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(RequestDelegate next, int requestsPerMinute, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _requestsPerMinute = requestsPerMinute;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            if (clientIp == null)
            {
                await _next(context);
                return;
            }

            var rateLimitInfo = _clients.GetOrAdd(clientIp, new RateLimitInfo { LastRequestTime = DateTime.UtcNow, RequestCount = 0 });

            if (DateTime.UtcNow - rateLimitInfo.LastRequestTime > _timeSpan)
            {
                rateLimitInfo.LastRequestTime = DateTime.UtcNow;
                rateLimitInfo.RequestCount = 0;
            }

            rateLimitInfo.RequestCount++;

            if (rateLimitInfo.RequestCount > _requestsPerMinute)
            {
                _logger.LogWarning("Rate limit exceeded for IP: {ClientIp}", clientIp);

                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            await _next(context);
        }
    }
}
