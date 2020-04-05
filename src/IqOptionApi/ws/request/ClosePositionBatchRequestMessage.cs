using iqoptionapi.models;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request
{
    internal class ClosePositionBatchRequestMessage
    {
        [JsonProperty("name")]
        public string Name => "close-position-batch";

        [JsonProperty("version")]
        public string Version => "1.0";

        [JsonProperty("body")]
        public ClosePositionBatch Body { get; set; }
    }

    internal class ClosePositionBatchRequest : WsSendMessageBase<ClosePositionBatchRequestMessage>
    {
        public ClosePositionBatchRequest(int[] positionIds)
        {
            Message = new ClosePositionBatchRequestMessage()
            {
                Body = new ClosePositionBatch()
                {
                    PositionIds = positionIds
                }
            };
        }
    }
}