using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class SubscribeTradersMoodChangedRequest : WsMessageBase<dynamic>
    {
        public SubscribeTradersMoodChangedRequest(InstrumentType instrumentType, ActivePair activePair)
        {
            base.Message = new
            {
                Name = "subscribeMessage",
                Msg =
                    new
                    {
                        Name = "traders-mood-changed",
                        Params =
                            new
                            {
                                routingFilters =
                                    new
                                    {
                                        instrument = InstrumentTypeUtilities.GetInstrumentTypeFullName(instrumentType),
                                        asset_id = (int)activePair
                                    }
                            }
                    }
            };
        }
    }
}