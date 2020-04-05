namespace IqOptionApi.ws.request
{
    internal class UnSubscribeMessageRequest<T> : WsSendMessageBase<T> where T : class
    {
        public override string Name => "unsubscribeMessage";


    }
}