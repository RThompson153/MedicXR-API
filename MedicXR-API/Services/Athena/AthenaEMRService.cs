using MedicXR_API.Libraries;
using MedicXR_API.Services.Utils;
using MedicXR_API.Services.Athena.Constants;
using MedicXR_API.Services.Athena.Models.Appointments;
using MedicXR_API.Services.Athena.Models.Patients;
using MedicXR_API.Services.Models;
using System.Net.Http.Headers;
using System.Text;

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
        public async Task GetProviders(string practiceId)
        {
            await authenticate();

            string endpoint = _providerEndpoint.Replace("{practiceId}", practiceId);

            try
            {
                var result = await _httpLibrary.GetAsync($"{_baseUrl}{endpoint}", addAuthenticationHeader());

                return;
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

            string date = DateTime.Now.ToShortDateString();

            string endpoint = _appointmentsEndpoint.Replace("{practiceId}", practiceId.ToString());

            try
            {
                AppointmentList result = await _httpLibrary.GetAsync<AppointmentList>($"{_baseUrl}{endpoint}/booked?departmentid={departmentId}&providerid={providerId}&startdate={date}&enddate={date}", addAuthenticationHeader());

                return result?.Appointments;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new appointment
        /// </summary>
        /// <param name="practiceId"></param>
        /// <returns></returns>
        public async Task CreateAppointment(int practiceId)
        {
            await authenticate();

            string endpoint = _appointmentsEndpoint.Replace("{practiceId}", practiceId.ToString());

            AppointmentDto appointment = new()
            {
                AppointmentDate = DateTime.Now.ToString("MM/dd/yyyy"),
                AppointmentTime = DateTime.Now.AddHours(5).ToString("HH:mm"),
                AppointmentTypeId = "103",
                DepartmentId = "1",
                ProviderId = "1",
                ReasonId = "2"
            };

            try
            {
                var meh = appointment.ObjectToDictionary();

                Dictionary<string, string> headers = new()
                {
                    {_authenticationToken.TokenType, _authenticationToken.AccessToken }
                };

                string newAppointment = await _httpLibrary.PostAsync($"{_baseUrl}{endpoint}/open", headers, meh);

                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Patients
        /// <summary>
        /// Retrieves basic patient infor for a provided patient id
        /// </summary>
        /// <param name="practiceId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<Patient?> GetPatient(int practiceId, int patientId)
        {
            await authenticate();

            string endpoint = _patientEndpoint.Replace("{practiceId}", practiceId.ToString());

            try
            {
                IEnumerable<Patient> result = await _httpLibrary.GetAsync<IEnumerable<Patient>>($"{_baseUrl}{endpoint}/{patientId}", addAuthenticationHeader());

                return result?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreatePatient(int practiceId)
        {
            await authenticate();

            var endpoint = _patientEndpoint.Replace("{practiceId}", practiceId.ToString());

            PatientDto patient = new()
            {
                DepartmentId = "1",
                FirstName = "Test",
                LastName = "Patient",
                DOB = "01/12/1984",
                Sex = "M",
                Email = "",
                GuarantorEmail = "",
                SSN = "",
                HomePhone = "",
                MobilePhone = "",
                WorkPhone = "",
                Zip = "32504"
            };

            try
            {
                var meh = patient.ObjectToDictionary();

                Dictionary<string, string> headers = new()
                {
                    {_authenticationToken.TokenType, _authenticationToken.AccessToken }
                };

                string newPatient = await _httpLibrary.PostAsync($"{_baseUrl}{endpoint}", headers, meh);

                return;
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
