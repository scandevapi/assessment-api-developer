using assessment_api_developer.UI.Services;
using assessment_api_developer.UI.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Net;
using Moq;


namespace assessment_api_developer.UI.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _httpClientMock = new Mock<HttpClient>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _customerService = new CustomerService(_httpClientMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task GetTokenAsync_ReturnsToken()
        {
            // Arrange
            var tokenResponse = new TokenResponse { Token = "test-token" };
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(tokenResponse)
            };
            _httpClientMock.Setup(client => client.GetAsync("api/v1/auth/generate-token"))
                           .ReturnsAsync(httpResponseMessage);

            // Act
            var token = await _customerService.GetTokenAsync();

            // Assert
            Assert.Equal("test-token", token);
        }

        [Fact]
        public void StoreToken_SetsTokenInSession()
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            _httpContextAccessorMock.Setup(accessor => accessor.HttpContext.Session).Returns(sessionMock.Object);

            // Act
            _customerService.StoreToken("test-token");

            // Assert
            sessionMock.Verify(session => session.SetString("JWToken", "test-token"), Times.Once);
        }
    }
}
