using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using IqOptionApi.Extensions;
using IqOptionApi.ws.request;
using IqOptionApi.http;
using IqOptionApi.Models;
using IqOptionApi.ws;
using Serilog;

namespace IqOptionApi {
    public class IqOptionApi : IIqOptionApi {

        #region [Privates]

        private readonly ILogger _logger = IqOptionLoggerFactory.CreateLogger();
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();

        private readonly Subject<bool> connectedSubject = new Subject<bool>();
        private Profile _profile;

        #endregion

        #region [Publics]

        public string Username { get; }
        public string Password { get; }
        public IDictionary<InstrumentType, Instrument[]> Instruments { get; private set; }
        public Profile Profile {
            get => _profile;
            private set {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }
        public bool IsConnected { get; private set; }

        //clients
        public IqHttpClient HttpClient { get; }
        public IqOptionWebSocketClient WebSocketClient { get; private set; }
        public IIqWsClient IqWsClient { get; }

        //obs
        public IObservable<InfoData[]> InfoDatasObservable => WebSocketClient?.InfoDataObservable;
        public IObservable<Profile> ProfileObservable => _profileSubject.Publish().RefCount();
        public IObservable<bool> IsConnectedObservable => connectedSubject.Publish().RefCount();
        public IObservable<BuyResult> BuyResultObservable => WebSocketClient?.BuyResultObservable;

        #endregion
               
        public Task<bool> ConnectAsync() {
            connectedSubject.OnNext(false);
            IsConnected = false;

            var tcs = new TaskCompletionSource<bool>();
            try {
                this.HttpClient
                    .LoginAsync()
                    .ContinueWith(t => {
                        if (t.Result != null && t.Result.IsSuccessful) {
                           
                            _logger.Information($"{Username} logged in success!");

                            WebSocketClient.OpenSecuredSocketAsync(t.Result.Data.Ssid);

                            SubscribeWebSocket();

                            IsConnected = true;
                            connectedSubject.OnNext(true);
                            tcs.TrySetResult(true);
                            return;
                        }

                        _logger.Information($"{Username} logged in failed due to {t.Result?.Errors?.GetErrorMessage()}");
                        tcs.TrySetResult(false);
                    });
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public async Task<Profile> GetProfileAsync() {
            var result = await HttpClient.GetProfileAsync();
            return result.Result;
        }

        public async Task<bool> ChangeBalanceAsync(long balanceId) {
            var result = await HttpClient.ChangeBalanceAsync(balanceId);

            if (result?.Message == null && !result.IsSuccessful) {
                _logger.Error($"Change balance ({balanceId}) error : {result.Message}");
                return false;
            }

            return true;
        }

        public Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset)) {
            
            return WebSocketClient?.BuyAsync(pair, size, direction, expiration);
        }


        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame timeFrame, int count, DateTimeOffset to) {
            return WebSocketClient?.GetCandlesAsync(pair, timeFrame, count, to);
        }

        public Task<IObservable<CurrentCandle>> SubscribeRealtimeDataAsync(ActivePair pair, TimeFrame tf) {

            WebSocketClient?.SubscribeCandlesAsync(pair, tf).ConfigureAwait(false);

            var stream = WebSocketClient?
                .RealTimeCandleInfoObservable
                .Where(x => x.ActivePair == pair && x.TimeFrame == tf);
            

            return Task.FromResult(stream);
        }

        public Task UnSubscribeRealtimeData(ActivePair pair, TimeFrame tf) {
            return WebSocketClient?.UnsubscribeCandlesAsync(pair, tf);
        }

        public void Dispose() {
            connectedSubject?.Dispose();
            WebSocketClient?.Dispose();
        }


      

        /// <summary>
        /// listen to all obs, for make all properties updated
        /// </summary>
        private void SubscribeWebSocket() {

            //subscribe profile
            WebSocketClient.ProfileObservable
                .Merge(HttpClient.ProfileObservable())
                .DistinctUntilChanged()
                .Where(x => x!=null)
                .Subscribe(x => Profile = x);

            //subscribe for instrument updated
            WebSocketClient.InstrumentResultSetObservable
                .Subscribe(x => Instruments = x);


        }

        #region [Ctor]

        /// <summary>
        /// This is for unit testable
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="iqWsClient"></param>
        internal IqOptionApi(IqHttpClient httpClient, IIqWsClient iqWsClient) {
            HttpClient = httpClient;
            IqWsClient = iqWsClient;
        }

        public IqOptionApi(string username, string password, string host = "iqoption.com")
        {
            Username = username;
            Password = password;

            //set up client
            HttpClient = new IqHttpClient(username, password);
            //WebSocketClient = new IqOptionWebSocketClient("");
            IqWsClient = new IqWsClient();
        }

        #endregion
    }

    public enum OrderDirection {
        [EnumMember(Value = "put")] Put = 1,

        [EnumMember(Value = "call")] Call = 2
    }


    public class IqOptionApiGetProfileFailedException : Exception {
        public IqOptionApiGetProfileFailedException(object receivedContent) : base(
            $"received incorrect content : {receivedContent}") {
        }
    }
}