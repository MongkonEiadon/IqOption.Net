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
        private readonly Subject<OptionClosed> _closedOptionSubject = new Subject<OptionClosed>();

        private readonly Subject<OptionOpened> _openOptionSubject = new Subject<OptionOpened>();

        public IObservable<OptionOpened> OpenOptionObservable()
        {
            return _openOptionSubject.AsObservable();
        }

        public IObservable<OptionClosed> ClosedOptionObservable()
        {
            return _closedOptionSubject.AsObservable();
        }


        [SubscribeForTopicName(MessageType.OptionOpened, typeof(OptionOpened))]
        public void Subscribe(OptionOpened type)
        {
            _openOptionSubject.OnNext(type);
        }

        [SubscribeForTopicName(MessageType.OptionClosed, typeof(OptionClosed))]
        public void Subscribe(OptionClosed type)
        {
            _closedOptionSubject.OnNext(type);
        }

        [Predisposable]
        private void OnCompletedOptionDisposable()
        {
            _closedOptionSubject.OnCompleted();
            _openOptionSubject.OnCompleted();
        }
    }
}