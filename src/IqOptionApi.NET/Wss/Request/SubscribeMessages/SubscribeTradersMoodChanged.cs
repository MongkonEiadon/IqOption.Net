using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class SubscribeTradersMoodChangedRequest : WsMessageBase<dynamic>
    {
        public override string Name => "subscribeMessage";

        public SubscribeTradersMoodChangedRequest(InstrumentType instrumentType, ActivePair activePair)
        {
            base.Message = new
            {
                name = "traders-mood-changed",
                @params =
                    new {
                        routingFilters =
                            new
                            {
                                instrument = InstrumentTypeUtilities.GetInstrumentTypeFullName(instrumentType),
                                asset_id = (int) activePair
                            }
                    }
            };
        }
    }
}