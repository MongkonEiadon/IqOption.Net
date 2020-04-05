using IqOptionApi.Ws.Request;

namespace IqOptionApi.Ws.Base
{
    internal class WsSendMessageBase<T> : WsMessageBase<RequestBody<T>> where T : class
    {
        public override string Name { get; set; } = MessageType.SendMessage;
    }
}