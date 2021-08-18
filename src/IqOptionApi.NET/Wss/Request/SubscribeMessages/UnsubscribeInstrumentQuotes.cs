using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;
using System;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class UnsubscribeInstrumentQuotes : WsMessageBase<dynamic>
    {
        public override string Name => "unsubscribeMessage";
        public UnsubscribeInstrumentQuotes(ActivePair activePair, int ExpTime, InstrumentType Type)
        {
            DateTime Now = DateTime.Now;
            if (Now.Second % 60 != 0)
                Now = Now.AddSeconds(60 - Now.Second);

            int TotalMinute = (DateTime.Now.Minute - Now.Minute);
            if (TotalMinute < ExpTime)
            {
                Now.AddMinutes(TotalMinute - ExpTime);
            }

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
                                active = (int)activePair,
                                expiration_period = ExpTime * 60,
                                expiration_timestamp = ((DateTimeOffset)Now).ToUnixTimeSeconds(),
                                kind = InstrumentTypeUtilities.GetInstrumentTypeShortName(Type)
                            }
                    }
            };
        }
    }
}
