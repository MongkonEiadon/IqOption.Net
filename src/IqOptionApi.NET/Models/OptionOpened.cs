using System;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class OptionOpened
    {
        [JsonProperty("index")] public long Index { get; set; }

        [JsonProperty("value")] public double Value { get; set; }

        [JsonProperty("active")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair Active { get; set; }

        [JsonProperty("aff_id")] public long AffId { get; set; }

        [JsonProperty("amount")] public double Amount { get; set; }

        [JsonProperty("params")] public object Params { get; set; }

        [JsonProperty("balance")] public long Balance { get; set; }

        [JsonProperty("is_demo")] public bool IsDemo { get; set; }

        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("robot_id")] public object RobotId { get; set; }

        [JsonProperty("active_id")] public long ActiveId { get; set; }

        [JsonProperty("aff_track")] public string AffTrack { get; set; }

        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction { get; set; }

        [JsonProperty("open_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset OpenTime { get; set; }

        [JsonProperty("option_id")] public long OptionId { get; set; }

        [JsonProperty("balance_id")] public long BalanceId { get; set; }

        [JsonProperty("bonus_rate")] public long BonusRate { get; set; }

        [JsonProperty("country_id")] public long CountryId { get; set; }

        [JsonProperty("inout_diff")] public long InoutDiff { get; set; }

        [JsonProperty("option_type")] public string OptionType { get; set; }

        [JsonProperty("platform_id")] public long PlatformId { get; set; }

        [JsonProperty("is_can_trade")] public bool IsCanTrade { get; set; }

        [JsonProperty("currency_mask")] public string CurrencyMask { get; set; }

        [JsonProperty("profit_amount")] public long ProfitAmount { get; set; }

        [JsonProperty("tournament_id")] public object TournamentId { get; set; }

        [JsonProperty("user_group_id")] public long UserGroupId { get; set; }

        [JsonProperty("option_type_id")] public long OptionTypeId { get; set; }

        [JsonProperty("profit_percent")] public long ProfitPercent { get; set; }

        [JsonProperty("balance_type_id")] public long BalanceTypeId { get; set; }

        [JsonProperty("enrolled_amount")] public double EnrolledAmount { get; set; }

        [JsonProperty("expiration_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset ExpirationTime { get; set; }

        [JsonProperty("client_platform_id")] public long ClientPlatformId { get; set; }

        [JsonProperty("open_time_millisecond")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset OpenTimeMillisecond { get; set; }
    }
}