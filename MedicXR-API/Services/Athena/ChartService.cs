using MedicXR_API.Libraries;
using MedicXR_API.Services.Athena.Constants;
using MedicXR_API.Services.Athena.Models.Chart;

namespace MedicXR_API.Services.Athena
{
	public class ChartService : AthenaService
	{
		private string _endpoint
		{
			get => Services.GetValue<string>(AthenaConstants.ChartEndpoint);
		}

		public ChartService(IConfiguration config, HttpLibrary httpClient) : base(config, httpClient)
		{

		}

		public async Task<ProblemList> GetProblemList(int practiceId, int departmentId, int patientId)
		{
			await Authenticate();

            string endpoint = _endpoint.Replace(AthenaConstants.PracticeId, practiceId.ToString()).Replace(AthenaConstants.PatientId, patientId.ToString()).Replace(AthenaConstants.DepartmentId, departmentId.ToString());

			try
			{
				ProblemList problems = await HttpLibrary.GetAsync<ProblemList>($"{BaseUrl}{endpoint}", AddAuthenticationHeader());

				return problems;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}
