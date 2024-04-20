using MedicXR_API.Context;
using MedicXR_API.Services.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MedicXR_API.Services
{
    public class AuthenticationService
	{
		private MedicXRContext _ctx;
		private IConfiguration _config;
		private static AuthToken _authToken;
		public AuthenticationService(IConfiguration config, MedicXRContext ctx)
		{
			_config = config;
			
			_ctx = ctx;
		}
		

		private AuthToken generateAuthToken(string apiKey)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(apiKey);
			DateTime validFrom = DateTime.UtcNow;

			SigningCredentials signingCredentials = new(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new(notBefore:validFrom, expires:validFrom.AddHours(1), signingCredentials:signingCredentials);

			return new AuthToken
			{
				AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
				TokenType = "Authorization",
				ValidFrom = validFrom,
				ExpiresIn = "3600"
			};
		}
	}
}
