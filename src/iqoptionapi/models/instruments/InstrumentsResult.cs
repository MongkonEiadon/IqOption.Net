using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.models.Instruments;
using Newtonsoft.Json;

namespace IqOptionApi.models.instruments {
    public class InstrumentsResult {
        [JsonProperty("user_group_id")] public int UserGroupId { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(InstrumentTypeJsonConverter))]
        public InstrumentType Type { get; set; }

        [JsonProperty("instruments")] public Instrument[] Instruments { get; set; }
    }
}