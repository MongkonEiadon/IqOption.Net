using System;
using IqOptionApi.ws.@base;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.ws.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models {
    public class HeartBeat  : WsMessageBase<DateTimeOffset>, IResponseMessage {

        [JsonProperty("msg")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public override DateTimeOffset Message { get; set; }

    }
}