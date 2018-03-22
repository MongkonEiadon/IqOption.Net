using Newtonsoft.Json;

namespace iqoptionapi.models {
    public partial class Deposit
    {
        [JsonProperty("min")]
        public long Min { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }
    }

    public partial class Socials
    {
    }
}