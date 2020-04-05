using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<OptionArchived> _optionArchivedSubject = new Subject<OptionArchived>();
        public IObservable<OptionArchived> OptionArchivedObservable => _optionArchivedSubject.AsObservable();

        [SubscribeForTopicName(MessageType.OptionArchived, typeof(OptionArchived))]
        public void Subscribe(OptionArchived value)
        {
            _optionArchivedSubject.OnNext(value);
        }

        [SubscribeForTopicName(MessageType.UserTournamentPositionChanged, typeof(object))]
        public void Subscibe(object value)
        {
        }

        [Predisposable]
        private void OnOptionArchivedDisposal()
        {
            _optionArchivedSubject.OnCompleted();
        }
    }
}