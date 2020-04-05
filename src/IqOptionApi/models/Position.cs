using Newtonsoft.Json;
//^\s+(public\s.\w*\s(.*)\s{ get; set; })
//[JsonProperty("$2")]\r\n$1
namespace iqoptionapi.models
{
    public class Position
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("user_balance_id")]
        public int user_balance_id { get; set; }
        [JsonProperty("platform_id")]
        public int platform_id { get; set; }
        [JsonProperty("external_id")]
        public int external_id { get; set; }
        [JsonProperty("active_id")]
        public int active_id { get; set; }
        [JsonProperty("instrument_id")]
        public string instrument_id { get; set; }
        [JsonProperty("source")]
        public string source { get; set; }
        [JsonProperty("instrument_type")]
        public string instrument_type { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("open_time")]
        public long open_time { get; set; }
        [JsonProperty("open_quote")]
        public float open_quote { get; set; }
        [JsonProperty("invest")]
        public float invest { get; set; }
        [JsonProperty("invest_enrolled")]
        public float invest_enrolled { get; set; }
        [JsonProperty("sell_profit")]
        public float sell_profit { get; set; }
        [JsonProperty("sell_profit_enrolled")]
        public float sell_profit_enrolled { get; set; }
        [JsonProperty("expected_profit")]
        public int expected_profit { get; set; }
        [JsonProperty("expected_profit_enrolled")]
        public int expected_profit_enrolled { get; set; }
        [JsonProperty("pnl")]
        public float pnl { get; set; }
        [JsonProperty("take_profit_price")]
        public float? take_profit_price { get; set; }
        [JsonProperty("take_profit_percent")]
        public float take_profit_percent { get; set; }
        [JsonProperty("stop_lose_price")]
        public float? stop_lose_price { get; set; }
        [JsonProperty("stop_lose_percent")]
        public float stop_lose_percent { get; set; }
        [JsonProperty("current_price")]
        public float? current_price { get; set; }
        [JsonProperty("quote_timestamp")]
        public long quote_timestamp { get; set; }
        [JsonProperty("raw_event")]
        public PositionRawEvent raw_event { get; set; }
    }

    public class PositionRawEvent
    {
        [JsonProperty("close_at")]
        public int? close_at { get; set; }
        public int[] order_ids { get; set; }
        [JsonProperty("currency_rate")]
        public int currency_rate { get; set; }
        [JsonProperty("commission_enrolled")]
        public int commission_enrolled { get; set; }
        [JsonProperty("pnl_realized_enrolled")]
        public int pnl_realized_enrolled { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("buy_avg_price_enrolled")]
        public float? buy_avg_price_enrolled { get; set; }
        [JsonProperty("open_client_platform_id")]
        public int open_client_platform_id { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("instrument_type")]
        public string instrument_type { get; set; }
        [JsonProperty("create_at")]
        public long create_at { get; set; }
        [JsonProperty("close_underlying_price")]
        public float? close_underlying_price { get; set; }
        [JsonProperty("commission_amount")]
        public float? commission_amount { get; set; }
        [JsonProperty("instrument_id")]
        public string instrument_id { get; set; }
        [JsonProperty("instrument_underlying")]
        public string instrument_underlying { get; set; }
        [JsonProperty("instrument_strike_value")]
        public object instrument_strike_value { get; set; }
        [JsonProperty("update_at")]
        public long update_at { get; set; }
        [JsonProperty("swap_enrolled")]
        public float swap_enrolled { get; set; }
        [JsonProperty("count")]
        public float count { get; set; }
        [JsonProperty("sell_amount")]
        public float? sell_amount { get; set; }
        [JsonProperty("charge_enrolled")]
        public float? charge_enrolled { get; set; }
        [JsonProperty("instrument_percent")]
        public float? instrument_percent { get; set; }
        [JsonProperty("last_change_reason")]
        public string last_change_reason { get; set; }
        [JsonProperty("close_effect_amount_enrolled")]
        public float? close_effect_amount_enrolled { get; set; }
        [JsonProperty("extra_data")]
        public PositionExtra_Data extra_data { get; set; }
        [JsonProperty("buy_amount")]
        public float? buy_amount { get; set; }
        [JsonProperty("tpsl_extra")]
        public object tpsl_extra { get; set; }
        [JsonProperty("margin_call")]
        public int margin_call { get; set; }
        [JsonProperty("custodial_enrolled")]
        public int custodial_enrolled { get; set; }
        [JsonProperty("stop_lose_order_id")]
        public int stop_lose_order_id { get; set; }
        [JsonProperty("take_profit_order_id")]
        public int take_profit_order_id { get; set; }
        public object[] orders { get; set; }
        [JsonProperty("currency_unit")]
        public int currency_unit { get; set; }
        [JsonProperty("sell_avg_price")]
        public float? sell_avg_price { get; set; }
        [JsonProperty("buy_amount_enrolled")]
        public float? buy_amount_enrolled { get; set; }
        [JsonProperty("leverage")]
        public int leverage { get; set; }
        [JsonProperty("margin_call_enrolled")]
        public float? margin_call_enrolled { get; set; }
        [JsonProperty("instrument_period")]
        public object instrument_period { get; set; }
        [JsonProperty("sell_amount_enrolled")]
        public float? sell_amount_enrolled { get; set; }
        [JsonProperty("pnl_realized")]
        public int pnl_realized { get; set; }
        [JsonProperty("custodial_last_age")]
        public int custodial_last_age { get; set; }
        [JsonProperty("instrument_active_id")]
        public int instrument_active_id { get; set; }
        [JsonProperty("instrument_expiration")]
        public object instrument_expiration { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("instrument_dir")]
        public object instrument_dir { get; set; }
        [JsonProperty("user_balance_type")]
        public int user_balance_type { get; set; }
        [JsonProperty("instrument_id_escape")]
        public string instrument_id_escape { get; set; }
        [JsonProperty("open_underlying_price")]
        public float? open_underlying_price { get; set; }
        [JsonProperty("count_realized")]
        public int count_realized { get; set; }
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("close_reason")]
        public object close_reason { get; set; }
        [JsonProperty("buy_avg_price")]
        public float? buy_avg_price { get; set; }
        [JsonProperty("user_group_id")]
        public int user_group_id { get; set; }
        [JsonProperty("instrument_strike")]
        public object instrument_strike { get; set; }
        [JsonProperty("charge")]
        public int charge { get; set; }
        [JsonProperty("margin")]
        public float margin { get; set; }
        [JsonProperty("commission")]
        public int commission { get; set; }
        [JsonProperty("last_index")]
        public long last_index { get; set; }
        [JsonProperty("user_balance_id")]
        public int user_balance_id { get; set; }
        [JsonProperty("close_effect_amount")]
        public float? close_effect_amount { get; set; }
        [JsonProperty("index")]
        public long index { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("custodial")]
        public int custodial { get; set; }
        [JsonProperty("sell_avg_price_enrolled")]
        public float? sell_avg_price_enrolled { get; set; }
        [JsonProperty("swap")]
        public float swap { get; set; }
    }

    public class PositionExtra_Data
    {
        [JsonProperty("amount")]
        public float? amount { get; set; }
        [JsonProperty("auto_margin_call")]
        public bool auto_margin_call { get; set; }
        [JsonProperty("init_time")]
        public long init_time { get; set; }
        [JsonProperty("last_change_reason")]
        public string last_change_reason { get; set; }
        [JsonProperty("stop_lose_kind")]
        public string stop_lose_kind { get; set; }
        [JsonProperty("stop_lose_price")]
        public float? stop_lose_price { get; set; }
        [JsonProperty("stop_lose_value")]
        public float? stop_lose_value { get; set; }
        [JsonProperty("stop_out_threshold")]
        public float? stop_out_threshold { get; set; }
        [JsonProperty("take_profit_kind")]
        public string take_profit_kind { get; set; }
        [JsonProperty("take_profit_price")]
        public float? take_profit_price { get; set; }
        [JsonProperty("take_profit_value")]
        public float? take_profit_value { get; set; }
        [JsonProperty("use_token_for_commission")]
        public bool use_token_for_commission { get; set; }
        [JsonProperty("use_trail_stop")]
        public bool use_trail_stop { get; set; }
        [JsonProperty("version")]
        public string version { get; set; }
    }
}
