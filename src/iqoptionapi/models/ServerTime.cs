using System;
using iqoptionapi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.models {
    public class ServerTime : WsMessageBase<long>, IResponseMessage {

        [JsonProperty("msg")]
        public override long Message { get; set; }

        public DateTimeOffset ServerDateTime => DateTimeOffset.FromUnixTimeMilliseconds(Message).ToLocalTime();
    }
}