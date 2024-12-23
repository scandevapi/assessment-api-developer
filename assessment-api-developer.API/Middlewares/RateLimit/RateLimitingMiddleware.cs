using System.Collections.Concurrent;

namespace assessment_api_developer.API.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, RateLimitInfo> _clients = new ConcurrentDictionary<string, RateLimitInfo>();
        private readonly int _requestsPerMinute;
        private readonly TimeSpan _timeSpan = TimeSpan.FromMinutes(1);

        public RateLimitingMiddleware(RequestDelegate next, int requestsPerMinute)
        {
            _next = next;
            _requestsPerMinute = requestsPerMinute;
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
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            await _next(context);
        }
    }
}
