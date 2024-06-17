using MedicXR_API.Services;
using MedicXR_API.Services.Athena;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MedicXR_API.Controllers
{
    [ApiController]
	[Route("/")]
	public class MedicXRController : ControllerBase
	{
		private MedicXRService _svc;
		private AthenaService _athena;
		protected internal IConfiguration Config;
		protected internal readonly string Salt;
		
		public MedicXRController(IConfiguration config, MedicXRService svc, AthenaService athena)
		{
			Salt = Convert.ToBase64String(Encoding.UTF8.GetBytes("medar")).Replace("=", "");
			Config = config;
			_athena = athena;
			_svc = svc;
		}

		private string decode(string source) => Encoding.UTF8.GetString(Convert.FromBase64String(source));

		[Route("authenticateclient")]
		[HttpGet]
		public async Task<string> AuthenticateClient()
		{
			try
			{
				if (!Request.Headers.ContainsKey("ApiKey"))
					throw new Exception("API Key not provided");
				if (!Request.Headers.ContainsKey("Authorization"))
					throw new Exception("Authorization not provided");
				if (!Request.Headers.ContainsKey("ClientIp"))
					throw new Exception("Client IP not provided");

				string decoded = decode(Request.Headers["Authorization"]);

				List<string> authorizationHeaders = decoded.Split(Salt).Where(d => !string.IsNullOrEmpty(d)).ToList();

				_svc.ValidateApiKey(decode(Request.Headers["ApiKey"]));

				return JsonSerializer.Serialize(await _svc.AuthenticateClient(decode(authorizationHeaders.FirstOrDefault()), decode(authorizationHeaders.LastOrDefault()), decode(Request.Headers["ClientIp"])));
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		[Route("updateclient")]
		[HttpPut]
		public async Task UpdateClient()
		{
			
		}
	}
}
