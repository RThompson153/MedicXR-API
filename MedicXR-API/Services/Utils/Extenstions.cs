using System.Text.Json;

namespace MedicXR_API.Services.Utils
{
	internal static class Extenstions
	{
		internal static Dictionary<string, string> ObjectToDictionary<T>(this T source) where T : new()
		{
			string json = JsonSerializer.Serialize(source);

			return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
		}
	}
}
