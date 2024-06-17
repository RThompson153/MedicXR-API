using MedicXR_API.Libraries;
using MedicXR_API.Services.Utils;
using MedicXR_API.Services.Athena.Constants;
using MedicXR_API.Services.Athena.Models.Appointments;
using MedicXR_API.Services.Athena.Models.Patients;
using MedicXR_API.Services.Models;
using System.Net.Http.Headers;
using System.Text;
using MedicXR_API.Services.Athena.Models.Providers;
using System.Text.Json;
using MedicXR_API.Services.Athena.Utils.Mappings;
using static System.Collections.Specialized.BitVector32;

namespace MedicXR_API.Services.Athena
{
	public abstract class AthenaService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _emrSection, _scopesSection;
        private AuthToken _authenticationToken;
        
        private readonly string _authenticationEndpoint, _tokenEndpoint, _chartEndpoint, _clientId, _clientSecret, _grantType, _athenaScope;
        private readonly Dictionary<string, string> _httpHeaders;

        protected readonly string BaseUrl;
        protected readonly IConfigurationSection Section, Services;
        protected readonly HttpLibrary HttpLibrary;
        protected string Scopes;

        public AthenaService(IConfiguration config, HttpLibrary httpClient)
        {
            _config = config;
            HttpLibrary = httpClient;
            Section = config.GetSection(AthenaConstants.AthenaEmr);
            BaseUrl = Section.GetValue<string>(AthenaConstants.BaseUrl);
            Scopes = Section.GetValue<string>(AthenaConstants.Scopes);
            Services = Section.GetSection(AthenaConstants.Services);
            
            _authenticationEndpoint = Services.GetValue<string>(AthenaConstants.AuthorizationEndpoint);
            _tokenEndpoint = Services.GetValue<string>(AthenaConstants.TokenEndpoint);

            _clientId = Section.GetValue<string>(MedicXRConstants.ClientId);
            _clientSecret = Section.GetValue<string>(MedicXRConstants.ClientSecret);
            _grantType = Section.GetValue<string>(MedicXRConstants.GrantType);
            
            _httpHeaders = new()
            {
                { MedicXRConstants.Grant_Type, _grantType },
                { MedicXRConstants.Scope, Scopes },
            };
        }

        #region Private Methods
        protected Dictionary<string, string> AddAuthenticationHeader()
        {
            Dictionary<string, string> headers = _httpHeaders;

            headers.Add(_authenticationToken.TokenType, _authenticationToken.AccessToken);

            return headers;
        }

        protected async Task<AuthToken> Authenticate()
        {
            try
            {
                if (_authenticationToken is not null && !_authenticationToken.Expired)
                    return _authenticationToken;

                Dictionary<string, string> headers = new()
                {
                    { HttpConstants.Basic, Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"))}
                };

                _authenticationToken = await HttpLibrary.PostAsync<AuthToken>($"{BaseUrl}{_authenticationEndpoint}{_tokenEndpoint}", headers, _httpHeaders);

                return _authenticationToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
		#endregion

		#region Public Methods
        #endregion
    }
}
