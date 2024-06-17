using MedicXR_API.Libraries;
using MedicXR_API.Services.Athena.Constants;
using MedicXR_API.Services.Athena.Models.Appointments;

namespace MedicXR_API.Services.Athena
{
	public class AppointmentService : AthenaService
	{
		private string _endpoint
		{
			get => Services.GetValue<string>(AthenaConstants.AppointmentsEndpoint);
		}
		public AppointmentService(IConfiguration config, HttpLibrary httpClient) : base(config, httpClient)
		{

		}

		/// <summary>
		/// Retrieves list of open appointments for the provided provider
		/// </summary>
		/// <param name="practiceId"></param>
		/// <param name="departmentId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<Appointment>?> GetAppointments(int practiceId, int departmentId, int providerId)
        {
            await Authenticate();

            string startdate = DateTime.Now.ToShortDateString();
            string enddate = DateTime.Now.AddDays(1).ToShortDateString();

            string endpoint = _endpoint.Replace(AthenaConstants.PracticeId, practiceId.ToString()).Replace(AthenaConstants.DepartmentId, departmentId.ToString()).Replace(AthenaConstants.ProviderId, providerId.ToString()).Replace(AthenaConstants.StartDate, startdate).Replace(AthenaConstants.EndDate, enddate);

            try
            {
                AppointmentList result = await HttpLibrary.GetAsync<AppointmentList>($"{BaseUrl}{endpoint}", AddAuthenticationHeader());

                return result?.Appointments.OrderBy(a => a.StartTime);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
	}
}
