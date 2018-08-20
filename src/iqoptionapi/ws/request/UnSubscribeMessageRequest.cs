using iqoptionapi.models;

namespace iqoptionapi.ws.request {
    internal class UnSubscribeMessageRequest : SubscribeMessageRequest {
        public override string Name => "unsubscribeMessage";

        public UnSubscribeMessageRequest(ActivePair pair, TimeFrame timeFrame) : base(pair, timeFrame) {
        }
    }
}