using System;
using iqoptionapi.ws.request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iqoptionapi.models {
    public class HeartBeat  : WsMessageBase<long>, IResponseMessage {

        [JsonProperty("msg")]
        public override long Message { get; set; }

        public DateTimeOffset HearBeatDateTime => DateTimeOffset.FromUnixTimeMilliseconds(Message).ToLocalTime();

    }
}