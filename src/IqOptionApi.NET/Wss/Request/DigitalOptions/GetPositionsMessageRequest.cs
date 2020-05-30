using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using Newtonsoft.Json;

namespace IqOptionApi.Wss.Request.DigitalOptions
{
    internal class GetPositionRequestBody
    {
        [JsonProperty("user_balance_id")]
        public int UserBalanceId { get; set; }
        
        [JsonProperty("instrument_type")]
        public string InstrumentsType { get; set; }
    }
    
    sealed class GetPositionsMessageRequest : WsSendMessageBase<GetPositionRequestBody>
    {
        internal GetPositionsMessageRequest(long balanceId)
        {
            Message = new RequestBody<GetPositionRequestBody>
            {
                RequestBodyType = "digital-options.get-positions",
                Body = new GetPositionRequestBody
                {
                    InstrumentsType = "digital-option",
                    UserBalanceId =  (int)balanceId
                }
            };
        }
        
        internal GetPositionsMessageRequest(InstrumentType instrumentType, long balanceId)
        {
            var name = "get-positions";
            if (instrumentType == InstrumentType.Forex)
                name = "trading-fx-option.get-positions";
            if (instrumentType == InstrumentType.CFD)
                name = "digital-options.get-positions";
            
            Message = new RequestBody<GetPositionRequestBody>
            {
                RequestBodyType = "digital-options.get-positions",
                Body = new GetPositionRequestBody
                {
                    InstrumentsType = name,
                    UserBalanceId =  (int)balanceId
                }
            };
        }
    }
    
}