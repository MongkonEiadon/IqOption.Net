using System;
using iqoptionapi.ws.@base;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.ws.request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models {
    public class HeartBeat  : WsMessageBase<DateTimeOffset> {

        [JsonProperty("msg")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public override DateTimeOffset Message { get; set; }

    }
}