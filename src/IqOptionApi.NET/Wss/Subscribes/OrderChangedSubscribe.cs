using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using IqOptionApi.Ws.Request.Portfolio;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<OrderChanged> _orderChangedSubject = new Subject<OrderChanged>();
        public IObservable<OrderChanged> OrderChangedObservable() => _orderChangedSubject.AsObservable();

        [SubscribeForTopicName(MessageType.SubscribeOrderChanged, typeof(OrderChanged))]
        public void Subscribe(OrderChanged orderChanged)
        {
            _orderChangedSubject.OnNext(orderChanged);
        }

        /// <summary>
        /// To subscribe the order changed support for "Forex", "Digital-Option"
        /// </summary>
        /// <param name="instrumentType"></param>
        /// <returns></returns>
        public async Task SubscribeOrderChanged(InstrumentType instrumentType)
        {
            if (Profile == null)
            {
                await SendMessageAsync(new SsidWsMessageBase(SecureToken)).ConfigureAwait(false);
                await Task.Delay(500);
            }
            
            await SendMessageAsync(new SubscribePortfolioOrderChangedRequest(Profile.UserId, instrumentType))
                .ConfigureAwait(false);
        }
    }
}