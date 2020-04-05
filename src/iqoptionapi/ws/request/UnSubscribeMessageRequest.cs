using IqOptionApi.Models;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws.Request
{
    internal class UnSubscribeMessageRequest : SubscribeMessageRequest
    {
        public UnSubscribeMessageRequest(ActivePair pair, TimeFrame timeFrame) : base(pair, timeFrame)
        {
        }

        public override string Name => MessageType.UnsubscribeMessage;
    }
}