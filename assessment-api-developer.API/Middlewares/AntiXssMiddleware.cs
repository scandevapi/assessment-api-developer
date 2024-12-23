//Requaired Nuget Packages
//HtmlSanitizer

using Ganss.Xss;
using System.Text;

namespace assessment_api_developer.API.Middlewares
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HtmlSanitizer _sanitizer;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next;
            _sanitizer = new HtmlSanitizer();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var request = context.Request;

            if (request.ContentType != null && request.ContentType.Contains("application/json"))
            {
                request.Body.Position = 0;
                using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    var body = await reader.ReadToEndAsync();
                    var sanitizedBody = _sanitizer.Sanitize(body);
                    var bytes = Encoding.UTF8.GetBytes(sanitizedBody);
                    request.Body = new MemoryStream(bytes);
                }
            }

            await _next(context);
        }
    }
}
