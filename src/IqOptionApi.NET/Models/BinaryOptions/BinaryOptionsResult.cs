using System;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models.BinaryOptions
{
    public class BinaryOptionsResult
    {
        [JsonProperty("user_id")] public long UserId { get; set; }
        
        [JsonProperty("refund_value")] public long RefundValue { get; set; }
        
        [JsonProperty("price")] public long Amount { get; set; }
        
        [JsonProperty("exp")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Exp { get; set; }        
        
        [JsonProperty("created")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Created { get; set; }
        
        [JsonProperty("id")] public long? PositionId { get; set; }
        
        [JsonProperty("type")] public string Type { get; set; }
        
        [JsonProperty("act")] public ActivePair ActivePair { get; set; }
        
        [JsonProperty("user_balance_id")] public long UserBalanceId { get; set; }
        
        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))] public OrderDirection OrderDirection { get; set; }

        [JsonProperty("client_platform_id")] public long ClientPlatformId { get; set; }
        [JsonProperty("message")] public string ErrorMessage { get; set; }

        public bool IsError() => !string.IsNullOrEmpty(ErrorMessage);
    }
}