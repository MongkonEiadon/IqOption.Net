using System;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IqOptionApi.Models
{
    public class HeartBeat : WsMessageBase<long>
    {
        [JsonProperty("msg")]
        public override long Message { get; set; }
        
        public DateTimeOffset Hearbeat => DateTimeOffset.FromUnixTimeMilliseconds(Message);
    }
}