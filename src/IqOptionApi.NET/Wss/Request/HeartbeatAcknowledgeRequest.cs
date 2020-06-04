using System;
using IqOptionApi.Converters.JsonConverters;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Request
{
    internal class HeartbeatAcknowledgeMessageRequest
    {
        [JsonProperty("userTime")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset UserTime { get; set; }

        [JsonProperty("heartbeatTime")]
        [JsonConverter(typeof(UnixSecondsDateTimeJsonConverter))]
        public DateTimeOffset HeartbeatTime { get; set; }
    }

    /*
     * Thanks for "https://github.com/mndflr" help this 
     * {"name":"heartbeat","request_id":"284","msg":{"userTime":"1590994438773","heartbeatTime":"1590994438758"}}
     */
    internal sealed class HeartbeatAcknowledgeRequest : WsMessageBase<HeartbeatAcknowledgeMessageRequest>
    {
        public HeartbeatAcknowledgeRequest(DateTimeOffset serverHeartbeat)
        {
            Name = "heartbeat";
            Message = new HeartbeatAcknowledgeMessageRequest
            {
                UserTime = DateTimeOffset.Now.UtcDateTime,
                HeartbeatTime = serverHeartbeat
            };
        }
    }
}