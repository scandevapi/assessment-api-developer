using assessment_api_developer.API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace assessment_api_developer.API.Tests.Middlewares
{
    public class ErrorHandlingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _mockNext;
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _mockLogger;
        private readonly ErrorHandlingMiddleware _middleware;

        public ErrorHandlingMiddlewareTests()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            _middleware = new ErrorHandlingMiddleware(_mockNext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Invoke_NoException_CallsNextDelegate()
        {
            // Arrange
            var context = new DefaultHttpContext();

            // Act
            await _middleware.Invoke(context);

            // Assert
            _mockNext.Verify(next => next(context), Times.Once);
        }

        [Fact]
        public async Task Invoke_ExceptionThrown_LogsErrorAndSetsResponse()
        {
            // Arrange
            var context = new DefaultHttpContext();
            _mockNext.Setup(next => next(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            // Act
            await _middleware.Invoke(context);

            // Assert
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An unexpected error occurred while processing the request.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);

            Assert.Equal(500, context.Response.StatusCode);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            Assert.Equal("An unexpected error occurred.", responseBody);
        }
    }
}
