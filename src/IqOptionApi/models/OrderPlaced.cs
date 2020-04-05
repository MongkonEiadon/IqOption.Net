using Newtonsoft.Json;

namespace iqoptionapi.models
{
    public class OrderPlaced
    {
        [JsonProperty("id")]
        public string PositionId { get; set; }
    }
}
