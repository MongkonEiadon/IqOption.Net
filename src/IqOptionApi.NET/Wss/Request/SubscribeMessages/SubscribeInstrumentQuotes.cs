using IqOptionApi.Models;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Base;
using System;

namespace IqOptionApi.Wss.Request.SubscribeMessages
{
    internal class SubscribeInstrumentQuotes : WsMessageBase<dynamic>
    {
        public override string Name => "subscribeMessage";

        public SubscribeInstrumentQuotes(ActivePair activePair, int ExpTime, InstrumentType Type)
        {
            //reduce second to 00s
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset exp = DateTimeOffset.Now;
            if (now.Second >= 0 && now.Millisecond >= 0)
            {
                DateTimeOffset RawExp = DateTimeOffset.Now.AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
                if (now.Second >= 30)
                {
                    exp = RawExp.AddMinutes(ExpTime + 1);
                }
                else
                {
                    exp = RawExp.AddMinutes(ExpTime);
                }
            }

            base.Message = new
            {
                name = "instrument-quotes-generated",
                @params =
                    new
                    {
                        routingFilters =
                            new
                            {
                                active = (int)activePair,
                                expiration_period = ExpTime*60,
                                expiration_timestamp = (exp).ToUnixTimeSeconds(),
                                kind = InstrumentTypeUtilities.GetInstrumentTypeFullName(Type)
                            }
                    }
            };
        }
    }
}
