namespace IqOptionApi.ws.request
{
    public class SubscribeMessageRequest<T> : WsSendMessageBase<T> where T : class
    {
        public override string Name => "subscribeMessage";

        public SubscribeMessageRequest() : base()
        {
            RequestId = "s_" + RequestId;
        }
    }

}