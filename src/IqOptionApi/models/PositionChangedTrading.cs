using Newtonsoft.Json;

namespace iqoptionapi.models
{
    public class PositionChangedTrading
    {

        [JsonProperty("last_change_reason")]
        public string last_change_reason { get; set; }
        [JsonProperty("instrument_id_escape")]
        public string instrument_id_escape { get; set; }
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
        public float? instrument_percent { get; set; }
        [JsonProperty("id")]
        public long id { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_balance_id")]
        public int user_balance_id { get; set; }
        [JsonProperty("user_balance_type")]
        public int user_balance_type { get; set; }
        [JsonProperty("create_at")]
        public long create_at { get; set; }
        [JsonProperty("update_at")]
        public long update_at { get; set; }
        [JsonProperty("close_at")]
        public long? close_at { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("leverage")]
        public int leverage { get; set; }
        [JsonProperty("count")]
        public float count { get; set; }
        [JsonProperty("count_realized")]
        public float count_realized { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("buy_amount")]
        public float buy_amount { get; set; }
        [JsonProperty("sell_amount")]
        public float sell_amount { get; set; }
        [JsonProperty("buy_amount_enrolled")]
        public float buy_amount_enrolled { get; set; }
        [JsonProperty("sell_amount_enrolled")]
        public float sell_amount_enrolled { get; set; }
        [JsonProperty("commission_amount")]
        public float commission_amount { get; set; }
        [JsonProperty("commission")]
        public float commission { get; set; }
        [JsonProperty("commission_enrolled")]
        public float commission_enrolled { get; set; }
        [JsonProperty("close_effect_amount")]
        public float? close_effect_amount { get; set; }
        [JsonProperty("close_effect_amount_enrolled")]
        public float? close_effect_amount_enrolled { get; set; }
        [JsonProperty("close_reason")]
        public string close_reason { get; set; }
        [JsonProperty("close_underlying_price")]
        public float? close_underlying_price { get; set; }
        [JsonProperty("open_underlying_price")]
        public float? open_underlying_price { get; set; }
        [JsonProperty("pnl_realized")]
        public float pnl_realized { get; set; }
        [JsonProperty("pnl_realized_enrolled")]
        public float pnl_realized_enrolled { get; set; }
        [JsonProperty("buy_avg_price")]
        public float buy_avg_price { get; set; }
        [JsonProperty("buy_avg_price_enrolled")]
        public float buy_avg_price_enrolled { get; set; }
        [JsonProperty("sell_avg_price")]
        public float sell_avg_price { get; set; }
        [JsonProperty("sell_avg_price_enrolled")]
        public float sell_avg_price_enrolled { get; set; }
        [JsonProperty("stop_lose_order_id")]
        public int? stop_lose_order_id { get; set; }
        [JsonProperty("take_profit_order_id")]
        public int? take_profit_order_id { get; set; }
        [JsonProperty("tpsl_extra")]
        public object tpsl_extra { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("margin")]
        public float margin { get; set; }
        [JsonProperty("margin_call")]
        public float margin_call { get; set; }
        [JsonProperty("margin_call_enrolled")]
        public float margin_call_enrolled { get; set; }
        [JsonProperty("swap")]
        public float swap { get; set; }
        [JsonProperty("swap_enrolled")]
        public float swap_enrolled { get; set; }
        [JsonProperty("custodial")]
        public float custodial { get; set; }
        [JsonProperty("custodial_enrolled")]
        public float custodial_enrolled { get; set; }
        [JsonProperty("custodial_last_age")]
        public int custodial_last_age { get; set; }
        [JsonProperty("user_group_id")]
        public int user_group_id { get; set; }
        [JsonProperty("charge")]
        public float charge { get; set; }
        [JsonProperty("charge_enrolled")]
        public float charge_enrolled { get; set; }
        public object[] orders { get; set; }
        [JsonProperty("currency_unit")]
        public int currency_unit { get; set; }
        [JsonProperty("currency_rate")]
        public float currency_rate { get; set; }
        [JsonProperty("open_client_platform_id")]
        public int open_client_platform_id { get; set; }
        [JsonProperty("extra_data")]
        public Extra_Data extra_data { get; set; }
        public int[] order_ids { get; set; }
        [JsonProperty("last_index")]
        public long last_index { get; set; }
        [JsonProperty("index")]
        public long index { get; set; }
    }

    public class Extra_Data
    {
        [JsonProperty("amount")]
        public int amount { get; set; }
        [JsonProperty("version")]
        public string version { get; set; }
        [JsonProperty("init_time")]
        public string init_time { get; set; }
        [JsonProperty("stop_lose_kind")]
        public string stop_lose_kind { get; set; }
        [JsonProperty("use_trail_stop")]
        public bool use_trail_stop { get; set; }
        [JsonProperty("stop_lose_price")]
        public float stop_lose_price { get; set; }
        [JsonProperty("stop_lose_value")]
        public float stop_lose_value { get; set; }
        [JsonProperty("auto_margin_call")]
        public bool auto_margin_call { get; set; }
        [JsonProperty("take_profit_kind")]
        public string take_profit_kind { get; set; }
        [JsonProperty("take_profit_price")]
        public float take_profit_price { get; set; }
        [JsonProperty("take_profit_value")]
        public float take_profit_value { get; set; }
        [JsonProperty("last_change_reason")]
        public string last_change_reason { get; set; }
        [JsonProperty("stop_out_threshold")]
        public int stop_out_threshold { get; set; }
        [JsonProperty("use_token_for_commission")]
        public bool use_token_for_commission { get; set; }
    }
}
