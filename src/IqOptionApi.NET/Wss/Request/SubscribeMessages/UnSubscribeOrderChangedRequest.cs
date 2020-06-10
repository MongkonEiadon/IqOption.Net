using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request.Portfolio;

namespace IqOptionApi.Ws.Request
{
    internal class UnSubscribeOrderChangedRequest : SubscribePortfolioOrderChangedRequest
    {
        public UnSubscribeOrderChangedRequest(long userId, InstrumentType instrumentType):base(userId, instrumentType)
        {
            
        }
        
        public override string Name => MessageType.UnsubscribeMessage;
    }
}