using System;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class ServerTime : WsMessageBase<DateTimeOffset>
    {
        [JsonProperty("msg")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public override DateTimeOffset Message { get; set; }
    }
}