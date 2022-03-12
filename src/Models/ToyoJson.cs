using System.Text.Json.Serialization;

namespace BackendToyo.Models
{
    public class ToyoJson
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string animation_url { get; set; }
        public AttributesJson[] attributes { get; set; }
    }
}