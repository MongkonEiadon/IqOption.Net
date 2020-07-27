using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class UnsubscribeLiveDealRequest : WsMessageBase<dynamic>
    {
        public override string Name => "unsubscribeMessage";

        public UnsubscribeLiveDealRequest(string message, ActivePair pair, DigitalOptionsExpiryType duration)
        {
            
            base.Message = new
            {
                name = message,
                @params =
                    new
                    {
                        routingFilters =
                            new
                            {
                                instrument_active_id = (int) pair,
                                expiration_type = duration
                            }
                    }
            };
        }
    }
}