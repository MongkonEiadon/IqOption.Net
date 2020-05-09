using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        public Profile Profile { get; private set; }

        public IObservable<Profile> ProfileObservable()
        {
            return _profileSubject.AsObservable();
        }

        [SubscribeForTopicName(MessageType.Profile, typeof(Profile))]
        public void Subscribe(Profile type)
        {
            _logger = IqOptionLoggerFactory.CreateWebSocketLogger(type);
            Profile = type;
            _profileSubject.OnNext(type);
        }

        [Predisposable]
        private void OnProfileDisposal()
        {
            _profileSubject.OnCompleted();
        }
    }
}