using MedicXR_API.Libraries;
using MedicXR_API.Services.Athena.Constants;
using MedicXR_API.Services.Athena.Models.Patients;
using MedicXR_API.Services.Athena.Utils.Mappings;
using MedicXR_API.Services.Models;

namespace MedicXR_API.Services.Athena
{
	public class PatientService : AthenaService
	{
		private string _endpoint
		{
			get => Services.GetValue<string>(AthenaConstants.PatientsEndpoint);
		}

		public PatientService(IConfiguration config, HttpLibrary httpClient) : base(config, httpClient)
		{

		}

		/// <summary>
        /// Retrieves basic patient info for a provided patient id
        /// </summary>
        /// <param name="practiceId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<MedicXRPatient?> GetPatient(int practiceId, int patientId)
        {
            await Authenticate();

            string endpoint = _endpoint.Replace(AthenaConstants.PracticeId, practiceId.ToString());

            try
            {
                IEnumerable<Patient> result = await HttpLibrary.GetAsync<IEnumerable<Patient>>($"{BaseUrl}{endpoint}/{patientId}", AddAuthenticationHeader());

                return result?.FirstOrDefault()?.MapToMedicXRPatient();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
	}
}
