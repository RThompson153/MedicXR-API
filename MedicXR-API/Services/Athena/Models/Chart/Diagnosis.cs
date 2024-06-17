using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Athena.Models.Chart
{
	public class DiagnosisList
	{
		[JsonPropertyName("diagnoses")]
		public IEnumerable<Diagnosis> Diagnoses { get; set; }
	}

	public class Diagnosis
	{
		[JsonPropertyName("code")]
		public string Code { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("codeset")]
		public string CodeSet { get; set; }
	}
}
