using assessment_api_developer.API.Controllers;
using assessment_api_developer.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace assessment_api_developer.API.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly TokenService _tokenService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.SetupGet(config => config["Jwt:Key"]).Returns("S3cr3tK3yF0rJWT@1234567890!@#$%^&*()");
            _mockConfiguration.SetupGet(config => config["Jwt:Issuer"]).Returns("https://localhost:7015");
            _mockConfiguration.SetupGet(config => config["Jwt:Audience"]).Returns("https://localhost:7212");

            _tokenService = new TokenService(_mockConfiguration.Object);
            _authController = new AuthController(_tokenService);
        }

        [Fact]
        public void GenerateToken_ReturnsOkResult_WithToken()
        {
            var result = _authController.GenerateToken() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);

            var tokenResponse = result.Value as TokenResponse;
            Assert.NotNull(tokenResponse);
            Assert.NotEmpty(tokenResponse.Token);
        }
    }
}
