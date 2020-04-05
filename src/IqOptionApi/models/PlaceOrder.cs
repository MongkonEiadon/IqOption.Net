using Newtonsoft.Json;

namespace iqoptionapi.models
{
    public class PlaceOrder
    {
        [JsonProperty("user_balance_id")]
        public long user_balance_id { get; set; }
        [JsonProperty("client_platform_id")]
        public int client_platform_id { get; set; }
        [JsonProperty("instrument_type")]
        public string instrument_type { get; set; }
        [JsonProperty("instrument_id")]
        public string instrument_id { get; set; }
        [JsonProperty("side")]
        public string side { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("amount")]
        public string amount { get; set; }
        [JsonProperty("leverage")]
        public int leverage { get; set; }
        [JsonProperty("limit_price")]
        public decimal limit_price { get; set; }
        [JsonProperty("stop_price")]
        public decimal stop_price { get; set; }
        [JsonProperty("use_token_for_commission")]
        public bool use_token_for_commission { get; set; }
        [JsonProperty("auto_margin_call")]
        public bool auto_margin_call { get; set; }
        [JsonProperty("use_trail_stop")]
        public bool use_trail_stop { get; set; }
        [JsonProperty("take_profit_value")]
        public float take_profit_value { get; set; }
        [JsonProperty("take_profit_kind")]
        public string take_profit_kind { get; set; }
        [JsonProperty("stop_lose_value")]
        public float stop_lose_value { get; set; }
        [JsonProperty("stop_lose_kind")]
        public string stop_lose_kind { get; set; }
    }
}
