using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Request.Portfolio
{
    //{"name":"subscribeMessage","request_id":"s_5","msg":{"name":"portfolio.order-changed","version":"1.0","params":{"routingFilters":{"user_id":xxxx,"instrument_type":"forex"}}}}
    internal class SubscribePortFolioPositionChangedRequestBody
    {
        [JsonProperty("name")] public string Name { get; set; } = "portfolio.order-changed";

        [JsonProperty("params")] public InstrumentRoutingFilterParameters Parameters { get; set; }

        [JsonProperty("version")] public string Version { get; set; } = "1.0";

        internal class InstrumentRoutingFilterParameters
        {
            [JsonProperty("routingFilters")] public RequestFilter Filter { get; set; }

            internal class RequestFilter
            {
                [JsonProperty("user_id")] public long UserId { get; set; }
                [JsonProperty("user_balance_id")] public long UserBalanceId { get; set; }
                [JsonProperty("instrument_type")] public string InstrumentType { get; set; }
            }
        }
    }

    internal class SubscribePortFolioPositionChangedRequest : WsMessageBase<SubscribePortFolioPositionChangedRequestBody>
    {
        public override string Name => MessageType.SubscribeMessage;
        
        public SubscribePortFolioPositionChangedRequest(long userId, long userBalanceId, InstrumentType instrumentType)
        {
            var instrumentTypeName = "";
            if (instrumentType == InstrumentType.Forex)
                instrumentTypeName = "forex";
            if (instrumentType == InstrumentType.CFD)
                instrumentTypeName = "cfd";
            if (instrumentType == InstrumentType.Crypto)
                instrumentTypeName = "crypto";
            if (instrumentType == InstrumentType.DigitalOption)
                instrumentTypeName = "digital-option";
            if (instrumentType == InstrumentType.BinaryOption)
                instrumentTypeName = "binary-option";
            if (instrumentType == InstrumentType.TurboOption)
                instrumentTypeName = "turbo-option";
            
            //msg
            base.Message = new SubscribePortFolioPositionChangedRequestBody
            {
                // name
                Name = "portfolio.order-changed",
                //version
                Version = "1.0",
                //params
                Parameters = new SubscribePortFolioPositionChangedRequestBody.InstrumentRoutingFilterParameters
                {
                    //routingFilters
                    Filter = new SubscribePortFolioPositionChangedRequestBody.InstrumentRoutingFilterParameters.RequestFilter
                    {
                        //user_id
                        UserId = userId,
                        
                        //user_balance_id
                        UserBalanceId = userBalanceId,
                        
                        //instrument_type
                        InstrumentType = instrumentTypeName
                    }
                }
            };
        }
    }
}
    