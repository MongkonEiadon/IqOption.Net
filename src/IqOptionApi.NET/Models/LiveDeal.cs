using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Utilities;
using Newtonsoft.Json;
using System;

namespace IqOptionApi.Models
{
    public class LiveDeal
    {

        [JsonProperty("amount_enrolled")]
        public double AmountEnrolled { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("expiration_type")]
        public string ExpirationType { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("instrument_active_id")]
        public ActivePair ActiveId { get; set; }

        [JsonProperty("instrument_dir")]
        public string InstrumentDir { get; set; }

        [JsonProperty("instrument_expiration")]
        public string InstrumentExpiration { get; set; }

        [JsonProperty("is_big")]
        public string IsBig { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position_id")]
        public long PositionId { get; set; }

        [JsonProperty("user_id")] 
        public long UserId { get; set; }

        [JsonProperty("brand_id")]
        public long BrandId { get; set; }

    }
}