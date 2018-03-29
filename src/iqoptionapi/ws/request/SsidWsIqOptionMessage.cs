namespace iqoptionapi.ws.request {
    internal class SsidWsRequestMessageBase : WsRequestMessageBase<string> {
        public override string Name => "ssid";

        public SsidWsRequestMessageBase(string ssid) {
            base.Message = ssid;
        }
    }
}