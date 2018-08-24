using System;
using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace IqOptionApi.Models {
    public class ServerTime : WsMessageBase<long>, IResponseMessage {

        [JsonProperty("msg")]
        public override long Message { get; set; }

        public DateTimeOffset ServerDateTime => DateTimeOffset.FromUnixTimeMilliseconds(Message).ToLocalTime();
    }
}