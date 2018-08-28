using System;
using iqoptionapi.ws.@base;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace IqOptionApi.Models {
    public class ServerTime : WsMessageBase<DateTimeOffset>, IResponseMessage {

        [JsonProperty("msg")]
        [JsonConverter(typeof(UnixDateTimeJsonConverter))]
        public override DateTimeOffset Message { get; set; }
        
    }
}