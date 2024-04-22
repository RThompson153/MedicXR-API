using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Athena.Models.Providers
{
	public class ProviderList
	{
		[JsonPropertyName("providers")]
		public IEnumerable<Provider> Providers { get; set; }
	}

	public class Provider
	{
		[JsonPropertyName("providerid")]
		public int Id { get; set; }
		[JsonPropertyName("firstname")]
		public string FirstName { get; set; }
		[JsonPropertyName("lastname")]
		public string LastName { get; set; }
	}
}
