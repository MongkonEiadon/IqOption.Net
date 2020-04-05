using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class OptionClosed : OptionOpened
    {
        [JsonProperty("result")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderResult Result { get; set; }

        [JsonProperty("actual_expire")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset ActualExpire { get; set; }

        [JsonProperty("expiration_value")] public double ExpirationValue { get; set; }

        [JsonProperty("win_enrolled_amount")] public double WinEnrolledAmount { get; set; }
    }
}