using System;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.ws.@base;
using IqOptionApi.ws.Request;
using Newtonsoft.Json;

namespace IqOptionApi.Models {
    public class HeartBeat : WsMessageBase<DateTimeOffset>, IResponseMessage {
        [JsonProperty("msg")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public override DateTimeOffset Message { get; set; }
    }
}