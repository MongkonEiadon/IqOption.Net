using iqoptionapi.models;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request
{
    internal class ClosePositionMessage
    {
        [JsonProperty("name")]
        public string Name => "close-position";

        [JsonProperty("version")]
        public string Version => "1.0";

        [JsonProperty("body")]
        public ClosePosition Body { get; set; }


    }

    internal class ClosePositionRequest : WsSendMessageBase<ClosePositionMessage>
    {
        public ClosePositionRequest(string positionId)
        {
            Message = new ClosePositionMessage()
            {
                Body = new ClosePosition()
                {
                    PositionId = positionId
                }
            };
        }
    }
}