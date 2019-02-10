using System;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.models.CFD {

    /// <summary>
    /// Suported for CFD; (Contract-For-Differences) Options, This is kind of Futeres Products
    /// </summary>
    public class DigitalInfoData {

        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DigitalDirection Type { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DigitalStatus Status { get; set; }
        
        [JsonProperty("instrument_underlying")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivePair ActivePair { get; set; }

        [JsonProperty("leverage")]
        public int Leverage { get; set; }

        [JsonProperty("create_at")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("update_at")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset UpdateAt { get; set; }

        [JsonProperty("close_at")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public DateTimeOffset CloseAt { get; set; }

    }
}
