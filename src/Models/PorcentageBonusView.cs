using Newtonsoft.Json;

namespace BackendToyo.Models
{
    public class PorcentageBonusView
    {
        [JsonProperty("Ym9udXM")]
        public string bonus  { get; set; }
        
        [JsonProperty("dG9rZW5JZA")]
        public string tokenId  { get; set; }
        
        public string wallet { get; set; }
    }
}