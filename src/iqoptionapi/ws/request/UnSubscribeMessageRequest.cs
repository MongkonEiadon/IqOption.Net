using IqOptionApi.Models;

using iqoptionapi.ws.@base;

namespace IqOptionApi.ws.request {
    internal class UnSubscribeMessageRequest : SubscribeMessageRequest {

        public override string Name => MessageType.UnsubscribeMessage;

        public UnSubscribeMessageRequest(ActivePair pair, TimeFrame timeFrame) : base(pair, timeFrame) {
        }
    }
}