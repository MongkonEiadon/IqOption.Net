namespace iqoptionapi.ws.request {
    internal class SsidWsMessage : WsMessageBase<string> {
        public override string Name => "ssid";

        public SsidWsMessage(string ssid) {
            base.Message = ssid;
        }
    }
}