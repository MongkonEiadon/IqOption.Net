using System;
using System.Threading.Tasks;
using IqOptionApi.http;
using IqOptionApi.Models;
using IqOptionApi.ws;

namespace IqOptionApi {
    public interface IIqOptionApi : IDisposable {
        [Obsolete]
        IqOptionWebSocketClient WebSocketClient { get; }
        
        IIqWsClient IqWsClient { get; }

        IqHttpClient HttpClient { get; }
        IObservable<Profile> ProfileObservable { get; }
        IObservable<InfoData[]> InfoDatasObservable { get; }
        Profile Profile { get; }
        bool IsConnected { get; }
        IObservable<bool> IsConnectedObservable { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction, DateTimeOffset expiration = default(DateTimeOffset));
        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);
        Task<IObservable<CurrentCandle>> SubscribeRealtimeDataAsync(ActivePair pair, TimeFrame tf);
        Task UnSubscribeRealtimeData(ActivePair pair, TimeFrame tf);

    }
}