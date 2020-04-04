using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using IqOptionApi.Models;
using IqOptionApi.utilities;

using iqoptionapi.ws.@base;

namespace IqOptionApi.ws {
    
    public partial class IqOptionWebSocketClient {
        
        private readonly Subject<OptionArchived> _optionArchivedSubject = new Subject<OptionArchived>();
        public IObservable<OptionArchived> OptionArchivedObservable => _optionArchivedSubject.AsObservable();

        [SubscribeForTopicName(MessageType.OptionArchived, typeof(OptionArchived))]
        public void Subscribe(OptionArchived value) {
            _optionArchivedSubject.OnNext(value);
            
        }

        [SubscribeForTopicName(MessageType.UserTournamentPositionChanged, typeof(Object))]
        public void Subscibe(Object value) {
            
        }
        
        [Predisposable]
        private void OnOptionArchivedDisposal() {
            _optionArchivedSubject.OnCompleted();
        }

    }
}