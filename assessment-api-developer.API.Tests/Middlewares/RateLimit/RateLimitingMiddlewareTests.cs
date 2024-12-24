using assessment_api_developer.API.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Net;
using Moq;

namespace assessment_api_developer.API.Tests.Middlewares
{
    public class RateLimitingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _mockNext;
        private readonly RateLimitingMiddleware _middleware;
        private readonly int _requestsPerMinute = 5;

        public RateLimitingMiddlewareTests()
        {
            _mockNext = new Mock<RequestDelegate>();
            _middleware = new RateLimitingMiddleware(_mockNext.Object, _requestsPerMinute);
        }

        //[Fact]
        //public async Task InvokeAsync_AllowsRequest_WhenUnderLimit()
        //{
        //    // Arrange
        //    var context = new DefaultHttpContext();
        //    context.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");

        //    // Act
        //    await _middleware.InvokeAsync(context);

        //    // Assert
        //    _mockNext.Verify(next => next(context), Times.Once);
        //}

        [Fact]
        public async Task InvokeAsync_BlocksRequest_WhenOverLimit()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");

            for (int i = 0; i < _requestsPerMinute; i++)
            {
                await _middleware.InvokeAsync(context);
            }

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            Assert.Equal("Rate limit exceeded. Try again later.", responseBody);
        }
    }
}
