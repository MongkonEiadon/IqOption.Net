using System;
using IqOptionApi.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IqOptionApi.Converters.JsonConverters;

namespace IqOptionApi.Models
{
    public class LiveDeal
    {
        [JsonProperty("avatar")]
        internal string Avatar { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        internal DateTimeOffset Created { get; set; }

        [JsonProperty("flag")]
        internal string Flag { get; set; }

        [JsonProperty("is_big")]
        internal bool IsBig { get; set; }

        [JsonProperty("option_id")]
        internal long OptionID { get; set; }

        [JsonProperty("option_type")]
        internal string OptionType { get; set; }

        [JsonProperty("brand_id")]
        internal int BrandID { get; set; }

        [JsonProperty("active_id")]
        public ActivePair ActiveId { get; set; }

        [JsonProperty("amount_enrolled")]
        public double Amount { get; set; }

        [JsonProperty("country_id")]
        public Country Country { get; set; }

        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderDirection Direction { get; set; }

        [JsonProperty("expiration")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset Expiration { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("user_id")]
        public long UserID { get; set; }

    }
}
