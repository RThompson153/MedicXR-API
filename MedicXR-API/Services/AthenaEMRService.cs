using MedicXR_API.Libraries;
using MedicXR_API.Services.Models;
using MedicXR_API.Services.Models.Athena;
using MedicXR_API.Services.Utils;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MedicXR_API.Services
{
	public class AthenaEMRService
	{
		private readonly IConfiguration _config;
		private readonly IConfigurationSection _emrSection, _scopesSection;
		private AuthToken _authenticationToken;
		private HttpClient _httpClient;
		private readonly HttpLibrary _httpLibrary;
		private readonly string _baseUrl, _authenticationEndpoint, _tokenEndpoint, _chartEndpoint, _conditionsEndpoint, _clientId, _clientSecret, _grantType, _athenaScope, _conditionsScope;
		private string _appointmentsEndpoint, _patientEndpoint;
		private readonly Dictionary<string, string> _httpHeaders;
		private int _ambulatoryPracticeId = 195900;
		private int _systemPracticeId = 1128700;

		public AthenaEMRService(IConfiguration config, HttpLibrary httpClient)
		{
			_config = config;

			_emrSection = config.GetSection(Constants.AthenaEmr);
			_scopesSection = _emrSection.GetSection(Constants.Scopes);
			
			_baseUrl = _emrSection.GetValue<string>(Constants.BaseUrl);
			_authenticationEndpoint = _emrSection.GetValue<string>(Constants.AuthorizationEndpoint);
			_tokenEndpoint = _emrSection.GetValue<string>(Constants.TokenEndpoint);
			_chartEndpoint = _emrSection.GetValue<string>(Constants.ChartEndpoint);
			_conditionsEndpoint = _emrSection.GetValue<string>(Constants.ConditionsEndpoint);
			_appointmentsEndpoint = _emrSection.GetValue<string>(Constants.AppointmentsEndpoint);
			_patientEndpoint = _emrSection.GetValue<string>(Constants.PatientEndpoint);

			_clientId = _emrSection.GetValue<string>(Constants.ClientId);
			_clientSecret = _emrSection.GetValue<string>(Constants.ClientSecret);
			_grantType = _emrSection.GetValue<string>(Constants.GrantType);
			_athenaScope = _scopesSection.GetValue<string>(Constants.AthenaScope);
			_conditionsScope = _scopesSection.GetValue<string>(Constants.ConditionsScope);

			_httpClient = new HttpClient();
			_httpLibrary = httpClient;
			_httpHeaders = new()
			{
				{ Constants.Grant_Type, _grantType },
				{ Constants.Scope, $"{_athenaScope} {_conditionsScope}" },
			};
		}

		private Dictionary<string, string> addAuthenticationHeader()
		{
			Dictionary<string, string> headers = _httpHeaders;

			headers.Add(_authenticationToken.TokenType, _authenticationToken.AccessToken);

			return headers;
		}

		public async Task<AuthToken> Authenticate()
		{
			try
			{
				Dictionary<string, string> headers = new()
				{
					{ HttpConstants.Basic, Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"))}
				};

				_authenticationToken = await _httpLibrary.PostAsync<AuthToken>($"{_baseUrl}{_authenticationEndpoint}{_tokenEndpoint}", headers, _httpHeaders);

				return _authenticationToken;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		public async Task<IEnumerable<Appointment>?> GetAppointments(int practiceId, int departmentId)
		{
			if (_authenticationToken is null || _authenticationToken.Expired)
				await Authenticate();

			string date = DateTime.Now.ToShortDateString();

			_appointmentsEndpoint = _appointmentsEndpoint.Replace("{practiceId}", practiceId.ToString()).Replace("{departmentId}", departmentId.ToString()).Replace("{startDate}", date).Replace("{endDate}", date);

			try
			{
				AppointmentList result = await _httpLibrary.GetAsync<AppointmentList>($"{_baseUrl}{_appointmentsEndpoint}", addAuthenticationHeader());

				return result?.Appointments;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		public async Task<Patient?> GetPatient(int practiceId, int patientId)
		{
			if (_authenticationToken is null || _authenticationToken.Expired)
				await Authenticate();

			_patientEndpoint = _patientEndpoint.Replace("{practiceId}", practiceId.ToString());

			try
			{
				IEnumerable<Patient> result = await _httpLibrary.GetAsync<IEnumerable<Patient>>($"{_baseUrl}{_patientEndpoint}/{patientId}", addAuthenticationHeader());

				return result?.FirstOrDefault();
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		public async Task GetConditions()
		{
			if (_authenticationToken is null || _authenticationToken.Expired)
				await Authenticate();

			try
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationToken.AccessToken);

				foreach (KeyValuePair<string, string> header in _httpHeaders)
					_httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

				HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}{_conditionsEndpoint}");

				var result = response.Content.ReadAsStringAsync().Result;

				return;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
