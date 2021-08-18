using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class UnsubscribeLiveDeal : WsMessageBase<dynamic>
    {
        public override string Name => "unsubscribeMessage";

        public UnsubscribeLiveDeal(InstrumentType instrumentType, ActivePair active)
        {
            base.Message = new
            {
                name = "live-deal-binary-option-placed",
                version = "2.0",
                @params =
                    new
                    {
                        routingFilters =
                            new
                            {
                                option_type = InstrumentTypeUtilities.GetInstrumentTypeShortName(instrumentType),
                                active_id = (int)active
                            }
                    }
            };
        }
    }
}
