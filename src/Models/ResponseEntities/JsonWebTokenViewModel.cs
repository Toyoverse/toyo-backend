using Newtonsoft.Json;

namespace BackendToyo.Models.ResponseEntities
{
    public class JsonWebTokenViewModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in_minutes")]
        public int ExpiresInMinutes { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}