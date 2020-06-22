using System;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class ServerTime : WsMessageBase<long>
    {
        [JsonProperty("msg")]
        public override long Message { get; set; }

        public DateTimeOffset ServerTick => DateTimeOffset.FromUnixTimeMilliseconds(Message);
    }
}