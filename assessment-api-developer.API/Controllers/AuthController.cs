using assessment_api_developer.API.Services;

using Asp.Versioning;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace assessment_api_developer.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("generate-token")]
        public IActionResult GenerateToken()
        {
            var token = _tokenService.GenerateToken();
            return Ok(new { Token = token });
        }
    }
}
