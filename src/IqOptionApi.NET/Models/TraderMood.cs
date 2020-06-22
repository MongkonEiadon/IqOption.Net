using IqOptionApi.Utilities;
using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class TraderMood
    {
        [JsonProperty("instrument")]
        internal string Instrument { get; set; }

        [JsonProperty("asset_id")]
        internal ActivePair ActiveId { get; set; }

        [JsonProperty("value")]
        internal double Value { get; set; }

        [JsonIgnore]
        public InstrumentType InstrumentType => InstrumentTypeUtilities.GetInstrumentTypeFromFullName(Instrument);

        public ActivePair ActivePair => (ActivePair)ActiveId;

        [JsonIgnore]
        public double Higher => Value;

        [JsonIgnore]
        public double Lower => 1 - Value;
    }
}