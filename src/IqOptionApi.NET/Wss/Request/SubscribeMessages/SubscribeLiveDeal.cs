using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class SubscribeLiveDeal : WsMessageBase<dynamic>
    {
        public override string Name => "subscribeMessage";
        public override string MicroserviceName => "live-deals";

        public SubscribeLiveDeal(InstrumentType instrumentType, ActivePair activePair)
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
                                active_id = (int)activePair
                            }
                    }
            };
        }
    }
}
