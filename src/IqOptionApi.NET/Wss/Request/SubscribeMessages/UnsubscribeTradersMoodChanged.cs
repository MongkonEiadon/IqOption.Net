using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class UnsubscribeTradersMoodChanged : WsMessageBase<dynamic>
    {
        public UnsubscribeTradersMoodChanged(InstrumentType instrumentType, ActivePair active)
        {
            base.Message = new
            {
                Name = "unsubscribeMessage",
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
                                        asset_id = (int) active
                                    }
                            }
                    }
            };
        }
    }
}