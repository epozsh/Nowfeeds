using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nowfeeds.Api.Config;
using Nowfeeds.Api.Services;

namespace Nowfeeds.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly JwtTokenService _jwtTokenService;
		private readonly AdminConfiguration _adminConfiguration;

		public AuthController(JwtTokenService jwtTokenService, IOptionsSnapshot<ApiConfiguration> apiConfiguration)
		{
			_jwtTokenService = jwtTokenService;
			_adminConfiguration = apiConfiguration.Value.Admin;
		}

		[HttpPost("token/generate")]
		public IActionResult Token([FromBody] TokenGenerateRequest request)
		{
			// Simple Validation
			if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password) ||
				!(request.Username.Equals(_adminConfiguration.Username) && request.Password.Equals(_adminConfiguration.Password)))
				return Unauthorized();

			var token = _jwtTokenService.GenerateToken(request.Subject);
			return Ok(new { token });
		}
	}

	public class TokenGenerateRequest
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Subject { get; set; }
	}
}
