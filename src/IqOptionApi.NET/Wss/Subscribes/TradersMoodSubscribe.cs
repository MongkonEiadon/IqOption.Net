using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Wss.Request.SubscribeMessages;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<TraderMood> _traderMoodSubject = new Subject<TraderMood>();
        public IObservable<TraderMood> TradersMoodObservable() => _traderMoodSubject.AsObservable();

        [SubscribeForTopicName(MessageType.TraderMoodChanged, typeof(TraderMood))]
        public void Subscribe(TraderMood value)
        {
            _traderMoodSubject.OnNext(value);
        }
        
        /// <summary>
        /// Subscribe to Traders mood changed
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pair"></param>
        public void SubscribeTradersMoodChanged(InstrumentType type, ActivePair pair)
        {
            SendMessageAsync(new SubscribeTradersMoodChangedRequest(type, pair), "s_").ConfigureAwait(false);
        }

        /// <summary>
        /// UnSubscribe to Traders mood changed
        /// </summary>
        /// <param name="type">The Instrument type.</param>
        /// <param name="pair">The Active pair</param>
        public void UnSubscribeTradersMoodChanged(InstrumentType type, ActivePair pair)
        {
            SendMessageAsync(new UnsubscribeTradersMoodChanged(type, pair), "s_").ConfigureAwait(false);
        }
    }
}