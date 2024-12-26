//Requaired Nuget Packages
//Logging: Microsoft.Extensions.Logging, Microsoft.Extensions.Logging.Console
//Logging in file: Serilog.AspNetCore, Serilog.Sinks.File

using Newtonsoft.Json;

namespace assessment_api_developer.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the request.");

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }


        //// Detailed Error Response
        //// Instead of a generic error message, you might want to return a more detailed error response
        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An unexpected error occurred while processing the request.");

        //        context.Response.StatusCode = 500;
        //        context.Response.ContentType = "application/json";
        //        var errorResponse = new { message = "An unexpected error occurred.", detail = ex.Message };
        //        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        //    }
        //}


        //// Exception Handling for Specific Exceptions
        //// You can handle specific exceptions differently to provide more appropriate responses/
        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (CustomException ex)
        //    {
        //        _logger.LogError(ex, "A custom error occurred.");

        //        context.Response.StatusCode = 400;
        //        context.Response.ContentType = "application/json";
        //        var errorResponse = new { message = ex.Message };
        //        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An unexpected error occurred while processing the request.");

        //        context.Response.StatusCode = 500;
        //        context.Response.ContentType = "application/json";
        //        var errorResponse = new { message = "An unexpected error occurred.", detail = ex.Message };
        //        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        //    }
        //}


        //// Logging Additional Context:
        //// Including additional context in your logs can be helpful for debugging
        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        var requestPath = context.Request.Path;
        //        _logger.LogError(ex, "An unexpected error occurred while processing the request at {Path}.", requestPath);

        //        context.Response.StatusCode = 500;
        //        context.Response.ContentType = "application/json";
        //        var errorResponse = new { message = "An unexpected error occurred.", detail = ex.Message };
        //        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        //    }
        //}
    }
}
