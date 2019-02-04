using IqOptionApi.ws.@base;

namespace IqOptionApi.ws.request {
    internal class SsidWsMessageBase : WsMessageBase<string> {
        public SsidWsMessageBase(string ssid) {
            base.Message = ssid;
        }

        public override string Name => "ssid";
    }
}