using System;
using IqOptionApi.Converters.JsonConverters;
using Newtonsoft.Json;

namespace IqOptionApi.Models
{
    /*
    public class BuyResult
    {
        [JsonProperty("user_id")] public long UserId { get; set; }

        [JsonProperty("refund_value")] public long RefundValue { get; set; }

        [JsonProperty("price")] public long Price { get; set; }

        [JsonProperty("exp")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Exp { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("time_rate")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset TimeRate { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("act")] public long Act { get; set; }

        [JsonProperty("direction")] public string Direction { get; set; }

        [JsonProperty("exp_value")] public long ExpValue { get; set; }

        [JsonProperty("value")] public double Value { get; set; }

        [JsonProperty("profit_income")] public long ProfitIncome { get; set; }

        [JsonProperty("profit_return")] public long ProfitReturn { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("robot_id")] public object RobotId { get; set; }

        [JsonProperty("request_id")] public string RequestId { get; set; }

        [JsonProperty("client_platform_id")] public long ClientPlatformId { get; set; }


        public object[] ErrorMessage { get; set; }

        public static BuyResult BuyResultError(object[] msg)
        {
            return new BuyResult {ErrorMessage = msg};
        }
    }
    */
}