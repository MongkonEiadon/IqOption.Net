using System;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    public class InfoData
    {
        [JsonProperty("amount")] public long Amount { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("refund")] public long Refund { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("currency_char")] public string CurrencyChar { get; set; }

        [JsonProperty("active_id")] public ActivePair ActiveId { get; set; }

        [JsonProperty("active")] public string Active { get; set; }

        [JsonProperty("value")] public double Value { get; set; }

        [JsonProperty("exp_value")] public double ExpValue { get; set; }

        [JsonProperty("dir")] public OrderDirection Direction { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("expired")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Expired { get; set; }

        [JsonProperty("exp_time")] public long ExpTime { get; set; }

        [JsonProperty("type_name")] public string TypeName { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("profit")] public long Profit { get; set; }

        [JsonProperty("profit_amount")] public long ProfitAmount { get; set; }

        [JsonProperty("win_amount")] public double WinAmount { get; set; }

        [JsonProperty("loose_amount")] public long LooseAmount { get; set; }

        [JsonProperty("sum")] public long Sum { get; set; }

        [JsonProperty("win")] public string Win { get; set; }

        [JsonProperty("now")] public long Now { get; set; }

        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("game_state")] public long GameState { get; set; }

        [JsonProperty("profit_income")] public long ProfitIncome { get; set; }

        [JsonProperty("profit_return")] public long ProfitReturn { get; set; }

        [JsonProperty("option_type_id")] public long OptionTypeId { get; set; }

        [JsonProperty("site_id")] public long SiteId { get; set; }

        [JsonProperty("is_demo")] public bool IsDemo { get; set; }

        [JsonProperty("user_balance_id")] public long UserBalanceId { get; set; }

        [JsonProperty("client_platform_id")] public long ClientPlatformId { get; set; }

        [JsonProperty("re_track")] public string ReTrack { get; set; }
    }
}