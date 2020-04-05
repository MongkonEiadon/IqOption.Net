using iqoptionapi.models;
using iqoptionapi.ws.@base;
using Newtonsoft.Json;

namespace iqoptionapi.ws.result
{
    public class PositionReceivedResultMessage
    {
        [JsonProperty("positions")]
        public Position[] positions { get; set; }
        [JsonProperty("total")]
        public int total { get; set; }
        [JsonProperty("limit")]
        public int limit { get; set; }

    }
    public class PositionsReceivedResult : WsMessageBase<PositionReceivedResultMessage>
    {

    }
}
