using IqOptionApi.ws.@base;

namespace IqOptionApi.ws.Request {
    internal class SsidWsMessageBase : WsMessageBase<string> {
        public SsidWsMessageBase(string ssid) {
            base.Message = ssid;
        }

        public override string Name => "ssid";
    }
}