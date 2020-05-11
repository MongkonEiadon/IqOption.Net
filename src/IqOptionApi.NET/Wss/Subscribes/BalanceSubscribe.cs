using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        public Balance CurrentBalance { get; private set; }

        [SubscribeForTopicName(MessageType.BalanceChanged, typeof(BalanceChanged))]
        public void Subscribe(BalanceChanged type)
        {
            CurrentBalance = type.CurrentBalance;
        }
    }
}