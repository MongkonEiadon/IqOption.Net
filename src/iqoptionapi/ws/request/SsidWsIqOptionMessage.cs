using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws.Request
{
    internal class SsidWsMessageBase : WsMessageBase<string>
    {
        public SsidWsMessageBase(string ssid)
        {
            base.Message = ssid;
        }

        public override string Name => MessageType.Ssid;
    }
}