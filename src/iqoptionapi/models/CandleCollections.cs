using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class CandleCollections
    {
        [JsonProperty("candles")] public CandleInfo[] Infos { get; set; }

        [JsonProperty("status")] public int Status { get; set; }

        [JsonIgnore] public int Count => Infos?.Length ?? 0;
    }
}