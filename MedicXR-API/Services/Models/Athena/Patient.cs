using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Models.Athena
{
	public class Patient
	{
		[JsonPropertyName("patientid")]
		public string Id { get; set; }
		[JsonPropertyName("firstname")]
		public string FirstName { get; set; }
		[JsonPropertyName("middlename")]
		public string MiddleName { get; set; }
		[JsonPropertyName("lastname")]
		public string LastName { get; set; }
		[JsonPropertyName("dob")]
		public string DateOfBirth { get; set; }
		[JsonPropertyName("sex")]
		public string Sex { get; set; }
	}
}
