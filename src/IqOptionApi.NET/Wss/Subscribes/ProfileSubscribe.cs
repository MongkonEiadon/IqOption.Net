using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request.Portfolio;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        public Profile Profile { get; private set; }
        public IObservable<Profile> ProfileObservable => _profileSubject.AsObservable();

        [SubscribeForTopicName(MessageType.Profile, typeof(Profile))]
        public void Subscribe(Profile type)
        {
            Profile = type;
            _profileSubject.OnNext(type);
            
            //invoke subscribe portfolio changed
            foreach (var instru in new[]
            {
                //InstrumentType.Forex,
                //InstrumentType.CFD,
                //InstrumentType.Crypto,
                InstrumentType.DigitalOption,
                //InstrumentType.TurboOption,
                //InstrumentType.BinaryOption,
                //InstrumentType.FxOption
            })
            {
                SubscribePositionChanged(instru).ConfigureAwait(false);
            }
        }

        [Predisposable]
        private void OnProfileDisposal()
        {
            _profileSubject.OnCompleted();
        }
    }
}