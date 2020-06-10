using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Request.Portfolio
{
    //{"name":"subscribeMessage","request_id":"s_5","msg":{"name":"portfolio.order-changed","version":"1.0","params":{"routingFilters":{"user_id":xxxx,"instrument_type":"forex"}}}}
    internal class SubscribePortFolioOrderChangedRequestBody
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
                [JsonProperty("instrument_type")] public string InstrumentType { get; set; }
            }
        }
    }

    internal class SubscribePortfolioOrderChangedRequest : WsMessageBase<SubscribePortFolioOrderChangedRequestBody>
    {
        public override string Name => MessageType.SubscribeMessage;
        public SubscribePortfolioOrderChangedRequest(long userId, InstrumentType instrumentType)
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
                instrumentTypeName = "binary";
            if (instrumentType == InstrumentType.TurboOption)
                instrumentTypeName = "turbo";
            
            //msg
            base.Message = new SubscribePortFolioOrderChangedRequestBody
            {
                // name
                Name = "portfolio.order-changed",
                //version
                Version = "1.0",
                //params
                Parameters = new SubscribePortFolioOrderChangedRequestBody.InstrumentRoutingFilterParameters
                {
                    //routingFilters
                    Filter = new SubscribePortFolioOrderChangedRequestBody.InstrumentRoutingFilterParameters.RequestFilter
                    {
                        //user_id
                        UserId = userId,
                        
                        //instrument_type
                        InstrumentType = instrumentTypeName
                    }
                }
            };
        }
    }
}
    