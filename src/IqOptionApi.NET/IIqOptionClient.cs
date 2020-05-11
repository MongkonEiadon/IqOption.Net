﻿using System;
using System.Threading.Tasks;
using IqOptionApi.Http;
using IqOptionApi.Models;
using IqOptionApi.Ws;

namespace IqOptionApi
{
    public interface IIqOptionClient : IDisposable
    {
        IqOptionWebSocketClient WsClient { get; }
        IqOptionHttpClient HttpClient { get; }
        IObservable<Profile> ProfileObservable { get; }
        IObservable<InfoData[]> InfoDatasObservable { get; }
        Profile Profile { get; }
        bool IsConnected { get; }
        IObservable<bool> ConnectedObservable { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction, DateTimeOffset expiration);
        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);
        Task<IObservable<CurrentCandle>> SubscribeRealtimeQuoteAsync(ActivePair pair, TimeFrame tf);
        Task UnSubscribeRealtimeData(ActivePair pair, TimeFrame tf);
    }
}