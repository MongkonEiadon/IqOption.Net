namespace iqoptionapi.ws.request {
    internal class SsidWsRequestMessageBase : WsRequestMessageBase<string> {
        public SsidWsRequestMessageBase(string ssid) {
            base.Message = ssid;
        }

        public override string Name => "ssid";
    }
}