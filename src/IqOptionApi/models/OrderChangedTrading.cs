using Newtonsoft.Json;

namespace iqoptionapi.models
{
    public class OrderChangedTrading
    {

        [JsonProperty("instrument_id_escape")]
        public string instrument_id_escape { get; set; }
        [JsonProperty("basic_stoplimit_amount")]
        public object basic_stoplimit_amount { get; set; }
        [JsonProperty("take_profit_price")]
        public object take_profit_price { get; set; }
        [JsonProperty("stop_lose_price")]
        public object stop_lose_price { get; set; }
        [JsonProperty("tpsl_extra")]
        public object tpsl_extra { get; set; }
        [JsonProperty("instrument_strike_value")]
        public object instrument_strike_value { get; set; }
        [JsonProperty("instrument_type")]
        public string instrument_type { get; set; }
        [JsonProperty("instrument_id")]
        public string instrument_id { get; set; }
        [JsonProperty("instrument_underlying")]
        public string instrument_underlying { get; set; }
        [JsonProperty("instrument_active_id")]
        public int instrument_active_id { get; set; }
        [JsonProperty("instrument_expiration")]
        public object instrument_expiration { get; set; }
        [JsonProperty("instrument_strike")]
        public object instrument_strike { get; set; }
        [JsonProperty("instrument_dir")]
        public object instrument_dir { get; set; }
        [JsonProperty("instrument_period")]
        public object instrument_period { get; set; }
        [JsonProperty("instrument_percent")]
        public object instrument_percent { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_balance_id")]
        public int user_balance_id { get; set; }
        [JsonProperty("user_group_id")]
        public int user_group_id { get; set; }
        [JsonProperty("user_balance_type")]
        public int user_balance_type { get; set; }
        [JsonProperty("position_id")]
        public int PositionId { get; set; }
        [JsonProperty("create_at")]
        public long create_at { get; set; }
        [JsonProperty("update_at")]
        public long update_at { get; set; }
        [JsonProperty("execute_at")]
        public long? execute_at { get; set; }
        [JsonProperty("side")]
        public string side { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("execute_status")]
        public string execute_status { get; set; }
        [JsonProperty("count")]
        public float count { get; set; }
        [JsonProperty("leverage")]
        public int leverage { get; set; }
        [JsonProperty("underlying_price")]
        public float? underlying_price { get; set; }
        [JsonProperty("avg_price")]
        public float? avg_price { get; set; }
        [JsonProperty("avg_price_enrolled")]
        public float? avg_price_enrolled { get; set; }
        [JsonProperty("client_platform_id")]
        public int client_platform_id { get; set; }
        [JsonProperty("limit_price")]
        public float? limit_price { get; set; }
        [JsonProperty("stop_price")]
        public float? stop_price { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("margin")]
        public float? margin { get; set; }
        [JsonProperty("spread")]
        public float? spread { get; set; }
        [JsonProperty("commission_amount")]
        public float? commission_amount { get; set; }
        [JsonProperty("commission_amount_enrolled")]
        public float? commission_amount_enrolled { get; set; }
        [JsonProperty("extra_data")]
        public ExtraData extra_data { get; set; }
        [JsonProperty("time_in_force")]
        public string time_in_force { get; set; }
        [JsonProperty("time_in_force_date")]
        public object time_in_force_date { get; set; }
        [JsonProperty("last_index")]
        public object last_index { get; set; }
        [JsonProperty("index")]
        public long index { get; set; }
    }

    public class ExtraData
    {
        [JsonProperty("version")]
        public string version { get; set; }
        [JsonProperty("init_time")]
        public long init_time { get; set; }
        [JsonProperty("use_trail_stop")]
        public bool use_trail_stop { get; set; }
        [JsonProperty("auto_margin_call")]
        public bool auto_margin_call { get; set; }
        [JsonProperty("paid_for_commission")]
        public int paid_for_commission { get; set; }
        [JsonProperty("use_token_for_commission")]
        public bool use_token_for_commission { get; set; }
        [JsonProperty("paid_for_commission_enrolled")]
        public int paid_for_commission_enrolled { get; set; }
    }

}
