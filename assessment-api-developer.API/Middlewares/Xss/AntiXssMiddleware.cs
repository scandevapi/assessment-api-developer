//Requaired Nuget Packages
//HtmlSanitizer

// Porpose: This middleware is used to prevent XSS attacks by sanitizing the request body.
using Ganss.Xss;
using System.Text;

namespace assessment_api_developer.API.Middlewares
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HtmlSanitizer _sanitizer;

        private readonly ILogger<AntiXssMiddleware> _logger;

        public AntiXssMiddleware(RequestDelegate next, ILogger<AntiXssMiddleware> logger)
        {
            _next = next;
            _sanitizer = new HtmlSanitizer();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var request = context.Request;

            if (request.ContentType != null && (request.ContentType.Contains("application/json") || request.ContentType.Contains("application/x-www-form-urlencoded")))
            {
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    var body = await reader.ReadToEndAsync();
                    var sanitizedBody = _sanitizer.Sanitize(body);

                    _logger.LogInformation("Sanitized request body for IP: {ClientIp}", context.Connection.RemoteIpAddress);

                    var bytes = Encoding.UTF8.GetBytes(sanitizedBody);
                    request.Body = new MemoryStream(bytes);
                    request.Body.Position = 0; // Reset the position
                }
            }

            await _next(context);
        }
    }
}
