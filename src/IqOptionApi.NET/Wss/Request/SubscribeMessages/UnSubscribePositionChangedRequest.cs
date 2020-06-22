using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request.Portfolio;

namespace IqOptionApi.Ws.Request
{
    internal class UnSubscribePositionChangedRequest : SubscribePortfolioPositionChangedRequest
    {
        public UnSubscribePositionChangedRequest(long userId, long balanceId, InstrumentType instrumentType):base(userId, balanceId, instrumentType)
        {
            
        }
        
        public override string Name => MessageType.UnsubscribeMessage;
    }
}