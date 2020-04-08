using System;
using IqOptionApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Ws.Request
{
    public class GetCandlesRequestModel
    {
        [JsonProperty("name")] public string Name => "get-candles";

        [JsonProperty("version")] public string Version => "2.0";

        [JsonProperty("body")] public GetCandlesRequestBody RequestBody { get; set; }

        public class GetCandlesRequestBody
        {
            [JsonProperty("active_id")] public ActivePair ActivePair { get; set; }

            [JsonProperty("size")] public TimeFrame TimeFrame { get; set; }

            [JsonProperty("to")]
            [JsonConverter(typeof(UnixDateTimeConverter))]
            public DateTimeOffset To { get; set; }

            [JsonProperty("count")] public int Count { get; set; }
        }
    }
}