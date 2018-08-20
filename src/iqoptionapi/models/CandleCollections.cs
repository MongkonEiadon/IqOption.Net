using iqoptionapi.ws;
using Newtonsoft.Json;

namespace iqoptionapi.models {

    public class CandleCollections {

        [JsonProperty("candles")]
        public CandleInfo[] Infos { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonIgnore]
        public int Count => Infos?.Length ?? 0;
    }
}