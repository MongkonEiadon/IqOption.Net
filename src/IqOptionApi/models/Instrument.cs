using Newtonsoft.Json;

namespace iqoptionapi.ws.model
{
    public class Instrument
    {
        [JsonProperty("ticker")]
        public string ticker { get; set; }
        [JsonProperty("is_visible")]
        public bool is_visible { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("active_id")]
        public int active_id { get; set; }
        [JsonProperty("active_group_id")]
        public int active_group_id { get; set; }
        [JsonProperty("active_type")]
        public string active_type { get; set; }
        [JsonProperty("underlying")]
        public string underlying { get; set; }
        public Schedule[] schedule { get; set; }
        [JsonProperty("is_enabled")]
        public bool is_enabled { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("localization_key")]
        public string localization_key { get; set; }
        [JsonProperty("image")]
        public string image { get; set; }
        [JsonProperty("image_prefix")]
        public string image_prefix { get; set; }
        [JsonProperty("precision")]
        public int precision { get; set; }
        [JsonProperty("pip_scale")]
        public int pip_scale { get; set; }
        [JsonProperty("start_time")]
        public long start_time { get; set; }
        [JsonProperty("last_ask")]
        public object last_ask { get; set; }
        [JsonProperty("last_bid")]
        public object last_bid { get; set; }
        [JsonProperty("currency_left_side")]
        public string currency_left_side { get; set; }
        [JsonProperty("currency_right_side")]
        public string currency_right_side { get; set; }
        public object[] expirations { get; set; }
        [JsonProperty("tags")]
        public Tags tags { get; set; }
        [JsonProperty("is_suspended")]
        public bool is_suspended { get; set; }
    }
    public class Tags
    {
    }

    public class Schedule
    {
        [JsonProperty("open")]
        public int open { get; set; }
        [JsonProperty("close")]
        public int close { get; set; }
    }
}
