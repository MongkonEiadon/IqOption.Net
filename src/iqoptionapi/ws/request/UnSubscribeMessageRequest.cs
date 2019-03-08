using IqOptionApi.Models;

namespace IqOptionApi.ws.Request {
    internal class UnSubscribeMessageRequest : SubscribeMessageRequest {
        public UnSubscribeMessageRequest(ActivePair pair, TimeFrame timeFrame) : base(pair, timeFrame) { }

        public override string Name => "unsubscribeMessage";
    }
}