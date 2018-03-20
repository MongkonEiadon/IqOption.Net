namespace iqoptionapi.ws {
    public class SsidWsMessage : WsMessageBase<string> {
        public override string Name => "ssid";

        public SsidWsMessage(string ssid) {
            base.Message = ssid;
        }

    
    }
}