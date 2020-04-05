using Newtonsoft.Json;

namespace iqoptionapi.ws.model
{
    public class InstrumentType
    {
        [JsonProperty("user_group_id")]
        public int user_group_id { get; set; }
        [JsonProperty("is_regulated")]
        public bool is_regulated { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        public Instrument[] Instruments { get; set; }
    }



}
