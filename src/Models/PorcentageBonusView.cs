using System.Text.Json.Serialization;

namespace BackendToyo.Models
{
    public class PorcentageBonusView
    {
        [JsonPropertyName("Ym9udXM")]
        public string bonus  { get; set; }
        
        [JsonPropertyName("dG9rZW5JZA")]        
        public string tokenId  { get; set; }
        
        public string wallet { get; set; }
    }
}