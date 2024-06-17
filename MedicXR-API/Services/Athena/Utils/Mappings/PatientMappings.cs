using MedicXR_API.Services.Athena.Models.Patients;
using MedicXR_API.Services.Models;

namespace MedicXR_API.Services.Athena.Utils.Mappings
{
	internal static class PatientMappings
	{
		internal static MedicXRPatient MapToMedicXRPatient(this Patient source) => new()
		{
			EmrId = source.Id,
			Name = $"{source.LastName}, {source.FirstName}",
			DateOfBirth = source.DateOfBirth,
			Sex = source.Sex
		};
	}
}
