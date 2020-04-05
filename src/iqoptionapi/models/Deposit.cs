using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class Deposit
    {
        [JsonProperty("min")] public long Min { get; set; }

        [JsonProperty("max")] public long Max { get; set; }
    }
}