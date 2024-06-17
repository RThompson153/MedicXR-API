using MedicXR_API.Libraries;
using MedicXR_API.Services.Athena.Constants;
using MedicXR_API.Services.Athena.Models.Providers;

namespace MedicXR_API.Services.Athena
{
	public class ProviderService : AthenaService
	{
        private string _endpoint
		{
			get => Services.GetValue<string>(AthenaConstants.ProvidersEndpoint);
		}

		public ProviderService(IConfiguration config, HttpLibrary httpClient) : base(config, httpClient)
		{

		}

		public async Task<IEnumerable<Provider>?> GetProviders(string practiceId)
        {
            await Authenticate();

            string endpoint = _endpoint.Replace(AthenaConstants.PracticeId, practiceId);

            try
            {
                ProviderList result = await HttpLibrary.GetAsync<ProviderList>($"{BaseUrl}{endpoint}", AddAuthenticationHeader());

                return result.Providers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
	}
}
