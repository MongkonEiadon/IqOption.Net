using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Wss.Request.SubscribeMessages;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<LiveDeal> _livedealSubject = new Subject<LiveDeal>();
        public IObservable<LiveDeal> LiveDealObservable() => _livedealSubject.AsObservable();

        [SubscribeForTopicName(MessageType.LiveDealDigitalOption, typeof(LiveDeal))]
        public void Subscribe(LiveDeal value)
        {
            _livedealSubject.OnNext(value);
        }
        
        /// <summary>
        /// Subscribe to Traders mood changed
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pair"></param>
        public void SubscribeLiveDeal(string message, ActivePair pair, DigitalOptionsExpiryType duration)
        {
            SendMessageAsync(new SubscribeLiveDealRequest(message, pair, duration), "s_").ConfigureAwait(false);
        }

        /// <summary>
        /// UnSubscribe to Traders mood changed
        /// </summary>
        /// <param name="type">The Instrument type.</param>
        /// <param name="pair">The Active pair</param>
        public void UnSubscribeLiveDeal(string message, ActivePair pair, DigitalOptionsExpiryType duration)
        {
            SendMessageAsync(new UnsubscribeLiveDealRequest(message, pair, duration), "s_").ConfigureAwait(false);
        }
    }
}