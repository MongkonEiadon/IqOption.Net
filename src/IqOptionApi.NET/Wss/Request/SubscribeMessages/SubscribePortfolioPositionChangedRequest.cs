using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Request.Portfolio
{
    //{"name":"subscribeMessage","request_id":"s_5","msg":{"name":"portfolio.order-changed","version":"1.0","params":{"routingFilters":{"user_id":xxxx,"instrument_type":"forex"}}}}

    internal class SubscribePortfolioPositionChangedRequest : WsMessageBase<dynamic>
    {
        public override string Name => MessageType.SubscribeMessage;

        public SubscribePortfolioPositionChangedRequest(long userId, long userBalanceId, InstrumentType instrumentType)
        {
            var instrumentTypeName = "";
            if (instrumentType == InstrumentType.Forex)
                instrumentTypeName = "forex";
            else if (instrumentType == InstrumentType.CFD)
                instrumentTypeName = "cfd";
            else if (instrumentType == InstrumentType.Crypto)
                instrumentTypeName = "crypto";
            else if (instrumentType == InstrumentType.DigitalOption)
                instrumentTypeName = "digital-option";
            else if (instrumentType == InstrumentType.BinaryOption)
                instrumentTypeName = "binary-option";
            else if (instrumentType == InstrumentType.TurboOption)
                instrumentTypeName = "turbo-option";
            else if (instrumentType == InstrumentType.FxOption)
                instrumentTypeName = "fx-option";
            else
                return;


            base.Message = new
            {
                name = "portfolio.position-changed",
                version = "3.0",
                @params =
                    new
                    {
                        routingFilters =
                            new
                            {
                                user_id = userId,
                                user_balance_id = userBalanceId,
                                instrument_type = instrumentTypeName
                            }
                    }
            };
        }
    }
}
    