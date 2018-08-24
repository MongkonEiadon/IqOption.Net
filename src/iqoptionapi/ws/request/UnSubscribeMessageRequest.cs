using IqOptionApi.Models;

namespace IqOptionApi.ws.request {
    internal class UnSubscribeMessageRequest : SubscribeMessageRequest {
        public override string Name => "unsubscribeMessage";

        public UnSubscribeMessageRequest(ActivePair pair, TimeFrame timeFrame) : base(pair, timeFrame) {
        }
    }
}