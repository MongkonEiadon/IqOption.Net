using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.utilities;
using IqOptionApi.Ws.Base;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {
        public Balance CurrentBalance { get; private set; }

        [SubscribeForTopicName(MessageType.BalanceChanged, typeof(BalanceChanged))]
        public void Subscribe(BalanceChanged type)
        {
            CurrentBalance = type.CurrentBalance;
        }
    }

    public partial class IqOptionWebSocketClient
    {
        private readonly Subject<BinaryOptionsResult> _buyResultSubject = new Subject<BinaryOptionsResult>();

        public IObservable<BinaryOptionsResult> BinaryOptionPlacedResultObservable =>
            _buyResultSubject.AsObservable();

        [SubscribeForTopicName(MessageType.PlacedBinaryOptions, typeof(BinaryOptionsResult))]
        public void Subscribe(BinaryOptionsResult value)
        {
            _buyResultSubject.OnNext(value);
        }

        [Predisposable]
        internal void OnPlacedBinaryOptionsDisposal()
        {
            _buyResultSubject.OnCompleted();
        }
        
    }
}