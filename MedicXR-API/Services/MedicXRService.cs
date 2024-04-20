using IdentityModel.Client;
using MedicXR_API.Context;
using MedicXR_API.Context.Models;
using MedicXR_API.Globals.Models;
using MedicXR_API.Services.Utils;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MedicXR_API.Services
{
    public class MedicXRService
	{
		private IConfiguration _config;
		private TokenResponse _icdAuthToken;
		protected internal MedicXRContext Ctx { get; set; }
        private HttpClient _http { get; set; }
		
		public MedicXRService(IConfiguration config)
		{
			_config = config;

			Ctx = new MedicXRContext(config.GetConnectionString(Constants.Database));
		}

		private async Task<IEnumerable<Client>> authenticateClient(string clientId, string clientSecret) => await Ctx.AuthenticateClient(clientId, clientSecret);
		private async Task<IEnumerable<User>> getUsers(string clientId, string userIds) => await Ctx.GetUsers(clientId, userIds);
		private async Task<IEnumerable<Illness>> getIllnesses(string clientId) => await Ctx.GetIllnesses(clientId);

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
				var client = await authenticateClient(clientId, clientSecret);

				//if (client.Active != true && client.Registered == true)
				//	throw new Exception("Client not active");

				//if (clientIp != client.Location)
				//	throw new Exception("Client moved");

				//client.Users = await getUsers(clientId, client.UserIds);

				return client.FirstOrDefault();
			}
			catch(Exception ex)
			{
				throw;
			}
		}
		/// <summary>
		/// Retrieves the list of common illnesses
		/// </summary>
		/// <param name="clientId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Illness>> GetIllnesses(string clientId)
		{
			try
			{
				var illnesses = await getIllnesses(clientId);
				return null;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
