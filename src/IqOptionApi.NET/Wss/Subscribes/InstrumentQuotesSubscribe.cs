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
        private readonly Subject<InstrumentQuotes> _instrumentQuotesSubject = new Subject<InstrumentQuotes>();
        public IObservable<InstrumentQuotes> InstrumentQuotesObservable() => _instrumentQuotesSubject.AsObservable();

        [SubscribeForTopicName(MessageType.InstrumentQuotes, typeof(InstrumentQuotes))]
        public void Subscribe(InstrumentQuotes value)
        {
            _instrumentQuotesSubject.OnNext(value);
        }

        /// <summary>
        /// Subscribe to Traders mood changed
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pair"></param>
        public void SubscribeInstrumentQuotes(ActivePair activePair, int ExpTime, InstrumentType Type)
        {
            SendMessageAsync(new SubscribeInstrumentQuotes(activePair, ExpTime, Type), "s_").ConfigureAwait(false);
        }

        /// <summary>
        /// UnSubscribe to Traders mood changed
        /// </summary>
        /// <param name="type">The Instrument type.</param>
        /// <param name="pair">The Active pair</param>
        public void UnsubscribeInstrumentQuotes(ActivePair activePair, int ExpTime, InstrumentType Type)
        {
            SendMessageAsync(new UnsubscribeInstrumentQuotes(activePair, ExpTime, Type), "s_").ConfigureAwait(false);
        }
    }
}
