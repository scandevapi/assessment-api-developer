using assessment_api_developer.API.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Moq;

namespace assessment_api_developer.API.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.SetupGet(config => config["Jwt:Key"]).Returns("S3cr3tK3yF0rJWT@1234567890!@#$%^&*()");
            _mockConfiguration.SetupGet(config => config["Jwt:Issuer"]).Returns("https://localhost:7015");
            _mockConfiguration.SetupGet(config => config["Jwt:Audience"]).Returns("https://localhost:7212");

            _tokenService = new TokenService(_mockConfiguration.Object);
        }

        [Fact]
        public void GenerateToken_ReturnsValidToken()
        {
            var token = _tokenService.GenerateToken();

            Assert.NotNull(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("S3cr3tK3yF0rJWT@1234567890!@#$%^&*()"));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:7015",
                ValidAudience = "https://localhost:7212",
                IssuerSigningKey = securityKey
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            Assert.NotNull(validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
            Assert.NotNull(jwtToken);
            Assert.Equal("https://localhost:7015", jwtToken.Issuer);
            Assert.Contains("https://localhost:7212", jwtToken.Audiences);
        }
    }
}
