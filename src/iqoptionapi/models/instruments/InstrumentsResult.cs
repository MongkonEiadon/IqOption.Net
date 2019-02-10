using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.models.Instruments;
using IqOptionApi.Models;
using Newtonsoft.Json;

namespace IqOptionApi.models.instruments {
    public partial class InstrumentsResult {
        [JsonProperty("user_group_id")]
        public int UserGroupId { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(InstrumentTypeJsonConverter))]
        public InstrumentType Type { get; set; }

        [JsonProperty("instruments")]
        public Instrument[] Instruments { get; set; }
    }
}