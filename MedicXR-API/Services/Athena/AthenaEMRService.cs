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

namespace MedicXR_API.Services.Athena
{
	public class AthenaEMRService
    {
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _emrSection, _scopesSection;
        private AuthToken _authenticationToken;
        private HttpClient _httpClient;
        private readonly HttpLibrary _httpLibrary;
        private readonly string _baseUrl, _authenticationEndpoint, _tokenEndpoint, _chartEndpoint, _conditionsEndpoint, _clientId, _clientSecret, _grantType, _athenaScope, _conditionsScope;
        private string _appointmentsEndpoint, _patientEndpoint, _providerEndpoint;
        private readonly Dictionary<string, string> _httpHeaders;

        public AthenaEMRService(IConfiguration config, HttpLibrary httpClient)
        {
            _config = config;

            _emrSection = config.GetSection(AthenaConstants.AthenaEmr);
            _scopesSection = _emrSection.GetSection(MedicXRConstants.Scopes);

            _baseUrl = _emrSection.GetValue<string>(AthenaConstants.BaseUrl);
            _authenticationEndpoint = _emrSection.GetValue<string>(AthenaConstants.AuthorizationEndpoint);
            _tokenEndpoint = _emrSection.GetValue<string>(AthenaConstants.TokenEndpoint);
            _chartEndpoint = _emrSection.GetValue<string>(AthenaConstants.ChartEndpoint);
            _conditionsEndpoint = _emrSection.GetValue<string>(AthenaConstants.ConditionsEndpoint);
            _appointmentsEndpoint = _emrSection.GetValue<string>(AthenaConstants.AppointmentsEndpoint);
            _patientEndpoint = _emrSection.GetValue<string>(AthenaConstants.PatientsEndpoint);
            _providerEndpoint = _emrSection.GetValue<string>(AthenaConstants.ProvidersEndpoint);

            _clientId = _emrSection.GetValue<string>(MedicXRConstants.ClientId);
            _clientSecret = _emrSection.GetValue<string>(MedicXRConstants.ClientSecret);
            _grantType = _emrSection.GetValue<string>(MedicXRConstants.GrantType);
            _athenaScope = _scopesSection.GetValue<string>(AthenaConstants.AthenaScope);
            _conditionsScope = _scopesSection.GetValue<string>(AthenaConstants.ConditionsScope);

            _httpClient = new HttpClient();
            _httpLibrary = httpClient;
            _httpHeaders = new()
            {
                { MedicXRConstants.Grant_Type, _grantType },
                { MedicXRConstants.Scope, $"{_athenaScope} {_conditionsScope}" },
            };
        }

        #region Private Methods
        private Dictionary<string, string> addAuthenticationHeader()
        {
            Dictionary<string, string> headers = _httpHeaders;

            headers.Add(_authenticationToken.TokenType, _authenticationToken.AccessToken);

            return headers;
        }

        private async Task<AuthToken> authenticate()
        {
            try
            {
                if (_authenticationToken is not null && !_authenticationToken.Expired)
                    return _authenticationToken;

                Dictionary<string, string> headers = new()
                {
                    { HttpConstants.Basic, Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"))}
                };

                _authenticationToken = await _httpLibrary.PostAsync<AuthToken>($"{_baseUrl}{_authenticationEndpoint}{_tokenEndpoint}", headers, _httpHeaders);

                return _authenticationToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
		#endregion

		#region Public Methods
		#region Providers
        public async Task<IEnumerable<Provider>?> GetProviders(string practiceId)
        {
            await authenticate();

            string endpoint = _providerEndpoint.Replace("{practiceId}", practiceId);

            try
            {
                ProviderList result = await _httpLibrary.GetAsync<ProviderList>($"{_baseUrl}{endpoint}", addAuthenticationHeader());

                return result.Providers;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
		#region Appointments
		/// <summary>
		/// Retrieves list of open appointments for the provided provider
		/// </summary>
		/// <param name="practiceId"></param>
		/// <param name="departmentId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Appointment>?> GetAppointments(int practiceId, int departmentId, int providerId)
        {
            await authenticate();

            string startdate = DateTime.Now.ToShortDateString();
            string enddate = DateTime.Now.AddDays(1).ToShortDateString();

            string endpoint = _appointmentsEndpoint.Replace("{practiceId}", practiceId.ToString());

            try
            {
                AppointmentList result = await _httpLibrary.GetAsync<AppointmentList>($"{_baseUrl}{endpoint}/booked?departmentid={departmentId}&providerid={providerId}&startdate={startdate}&enddate={enddate}", addAuthenticationHeader());

                return result?.Appointments.OrderBy(a => a.StartTime);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
        #region Patients
        /// <summary>
        /// Retrieves basic patient info for a provided patient id
        /// </summary>
        /// <param name="practiceId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<MedicXRPatient?> GetPatient(int practiceId, int patientId)
        {
            await authenticate();

            string endpoint = _patientEndpoint.Replace("{practiceId}", practiceId.ToString());

            try
            {
                IEnumerable<Patient> result = await _httpLibrary.GetAsync<IEnumerable<Patient>>($"{_baseUrl}{endpoint}/{patientId}", addAuthenticationHeader());

                return result?.FirstOrDefault().MapToMedicXRPatient();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task GetConditions()
        {
            await authenticate();

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationToken.AccessToken);

                foreach (KeyValuePair<string, string> header in _httpHeaders)
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

                HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}{_conditionsEndpoint}");

                var result = response.Content.ReadAsStringAsync().Result;

                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #endregion
    }
}
