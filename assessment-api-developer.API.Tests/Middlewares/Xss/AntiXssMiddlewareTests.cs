using assessment_api_developer.API.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Text;
using Moq;

namespace assessment_api_developer.API.Tests.Middlewares
{
    public class AntiXssMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _mockNext;
        private readonly AntiXssMiddleware _middleware;

        public AntiXssMiddlewareTests()
        {
            _mockNext = new Mock<RequestDelegate>();
            _middleware = new AntiXssMiddleware(_mockNext.Object);
        }

        //[Fact]
        //public async Task InvokeAsync_SanitizesJsonRequestBody()
        //{
        //    // Arrange
        //    var context = new DefaultHttpContext();
        //    context.Request.ContentType = "application/json";
        //    var originalBody = "{\"name\":\"<script>alert('xss')</script>\"}";
        //    var sanitizedBody = "{\"name\":\"alert('xss')\"}";

        //    context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(originalBody));
        //    context.Request.Body.Position = 0;

        //    // Act
        //    await _middleware.InvokeAsync(context);

        //    // Assert
        //    context.Request.Body.Position = 0;
        //    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
        //    {
        //        var body = await reader.ReadToEndAsync();
        //        Assert.Equal(sanitizedBody, body);
        //    }

        //    _mockNext.Verify(next => next(context), Times.Once);
        //}

        [Fact]
        public async Task InvokeAsync_DoesNotSanitizeNonJsonRequestBody()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.ContentType = "text/plain";
            var originalBody = "This is a plain text body.";

            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(originalBody));
            context.Request.Body.Position = 0;

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Request.Body.Position = 0;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                var body = await reader.ReadToEndAsync();
                Assert.Equal(originalBody, body);
            }

            _mockNext.Verify(next => next(context), Times.Once);
        }
    }
}
