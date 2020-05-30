using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using Newtonsoft.Json;

namespace IqOptionApi.Wss.Request.DigitalOptions
{
    internal class GetPositionByPositionIdRequestBody
    {
        [JsonProperty("position_id")]
        public long PositionId { get; set; }
    }

    sealed class GetPositionsByPositionIdRequest : WsSendMessageBase<GetPositionByPositionIdRequestBody>
    {
        public GetPositionsByPositionIdRequest(long positionId)
        {
            Message = new RequestBody<GetPositionByPositionIdRequestBody>
            {
                RequestBodyType = "get-position",
                Body = new GetPositionByPositionIdRequestBody
                {
                    PositionId = positionId
                }
            };
        }
    }
}