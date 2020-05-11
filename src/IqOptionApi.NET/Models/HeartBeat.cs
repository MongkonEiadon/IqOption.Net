using System;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class HeartBeat : WsMessageBase<DateTimeOffset>
    {
        [JsonProperty("msg")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public override DateTimeOffset Message { get; set; }
    }
}