using IdentityModel.Client;
using MedicXR_API.Context;
using MedicXR_API.Context.Models;
using MedicXR_API.Globals.Models;
using MedicXR_API.Services.Athena;
using MedicXR_API.Services.Athena.Models.Providers;
using MedicXR_API.Services.Utils;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MedicXR_API.Services
{
    public class MedicXRService
	{
		private IConfiguration _config;
		private readonly MedicXRContext _ctx;
		private ProviderService _athena;
		
		public MedicXRService(IConfiguration config, ProviderService athena)
		{
			_config = config;
			_athena = athena;
			_ctx = new MedicXRContext(config.GetConnectionString(MedicXRConstants.Database));
		}

		private async Task<Client?> authenticateClient(string clientId, string clientSecret) => await _ctx.AuthenticateClient(clientId, clientSecret);

		/// <summary>
		/// Validates the client's API key
		/// </summary>
		/// <param name="apiKey"></param>
		/// <exception cref="Exception"></exception>
		public void ValidateApiKey(string apiKey)
		{
			if (apiKey != _config.GetValue<string>("APIKey"))
				throw new Exception("Invalid API Key");
		}
		/// <summary>
		/// Authenticates the client
		/// </summary>
		/// <param name="clientId"></param>
		/// <param name="clientSecret"></param>
		/// <param name="clientIp"></param>
		/// <returns></returns>
		public async Task<Client> AuthenticateClient(string clientId, string clientSecret, string clientIp)
		{
			try
			{
				Client client = await authenticateClient(clientId, clientSecret);

				client.Providers = await _athena.GetProviders(client.PracticeId);

				//if (client.Active != true && client.Registered == true)
				//	throw new Exception("Client not active");

				//if (clientIp != client.Location)
				//	throw new Exception("Client moved");

				//client.Users = await getUsers(clientId, client.UserIds);

				return client;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
