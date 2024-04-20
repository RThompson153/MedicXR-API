using System.Text.Json.Serialization;

namespace MedicXR_API.Services.Models
{
    public class AuthToken
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        public DateTime? ValidFrom { get; set; }
        public bool Expired
        {
            get
            {
                ValidFrom ??= DateTime.UtcNow;

                return DateTime.UtcNow >= ValidFrom.Value.AddSeconds(int.Parse(ExpiresIn));
            }
        }
    }
}
