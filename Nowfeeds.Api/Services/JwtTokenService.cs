using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nowfeeds.Api.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nowfeeds.Api.Services
{
	public class JwtTokenService
	{
		private readonly JwtConfiguration _jwtConfiguration;

		public JwtTokenService(IOptionsSnapshot<ApiConfiguration> configuration)
		{
			_jwtConfiguration = configuration.Value.Jwt;
		}

		public string GenerateToken(string subject)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, subject),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = new JwtSecurityToken(
				issuer: _jwtConfiguration.Issuer,
				audience: _jwtConfiguration.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtConfiguration.Expires),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
