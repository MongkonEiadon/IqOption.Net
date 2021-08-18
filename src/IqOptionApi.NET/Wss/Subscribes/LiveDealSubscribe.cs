using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Wss.Request.SubscribeMessages;
using IqOptionApi.Utilities;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<LiveDeal> _liveDealSubject = new Subject<LiveDeal>();
        public IObservable<LiveDeal> LiveDealObservable() => _liveDealSubject.AsObservable();

        [SubscribeForTopicName(MessageType.LiveDealPlaced, typeof(LiveDeal))]
        public void Subscribe(LiveDeal value)
        {
            _liveDealSubject.OnNext(value);
        }

        /// <summary>
        /// Subscribe to Traders mood changed
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pair"></param>
        public void SubscribeLiveDealPlaced(InstrumentType type, ActivePair pair)
        {
            SendMessageAsync(new SubscribeLiveDeal(type, pair), "s_").ConfigureAwait(false);
        }

        /// <summary>
        /// UnSubscribe to Traders mood changed
        /// </summary>
        /// <param name="type">The Instrument type.</param>
        /// <param name="pair">The Active pair</param>
        public void UnSubscribeLiveDealPlaced(InstrumentType type, ActivePair pair)
        {
            SendMessageAsync(new UnsubscribeLiveDeal(type, pair), "s_").ConfigureAwait(false);
        }
    }
}
