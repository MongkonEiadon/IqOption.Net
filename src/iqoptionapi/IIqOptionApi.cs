using System;
using System.Threading.Tasks;
using iqoptionapi.http;
using iqoptionapi.models;
using iqoptionapi.ws;

namespace iqoptionapi {
    public interface IIqOptionApi : IDisposable {
        IqOptionWebSocketClient WsClient { get; }
        IqOptionHttpClient HttpClient { get; }
        IObservable<Profile> ProfileObservable { get; }
        IObservable<InfoData[]> InfoDatasObservable { get; }
        Profile Profile { get; }
        bool IsConnected { get; }
        IObservable<bool> IsConnectedObservable { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction, DateTime expiration = default(DateTime));
        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);
        Task<IObservable<CurrentCandle>> GetRealtimeCandlesInfoAsync(ActivePair pair, TimeFrame tf);
        Task UnSubscribeRealtimeData(ActivePair pair, TimeFrame tf);

    }
}