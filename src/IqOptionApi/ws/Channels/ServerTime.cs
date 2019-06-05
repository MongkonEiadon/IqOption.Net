﻿using IqOptionApi.Models;
using ReactiveUI;

// ReSharper disable once CheckNamespace
namespace IqOptionApi.ws {
    public partial class IqWsClient {
        private ServerTime _serverTime;

        public ServerTime ServerTime {
            get => _serverTime;
            private set =>
                this.RaiseAndSetIfChanged(ref _serverTime, value);
        }
    }
}