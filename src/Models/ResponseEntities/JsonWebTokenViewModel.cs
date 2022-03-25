using System.Text.Json.Serialization;

namespace BackendToyo.Models.ResponseEntities
{
    public class JsonWebTokenViewModel
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in_minutes")]
        public int ExpiresInMinutes { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}