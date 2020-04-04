using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using IqOptionApi.Models;
using IqOptionApi.utilities;

using iqoptionapi.ws.@base;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    
    public partial class IqOptionWebSocketClient  {
        
        public Balance CurrentBalance { get; private set; }

        [SubscribeForTopicName(MessageType.BalanceChanged, typeof(BalanceChanged))]
        public void Subscribe(BalanceChanged type) {
            CurrentBalance = type.CurrentBalance;
        }

    }
}