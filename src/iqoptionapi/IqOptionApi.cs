using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.http;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;

namespace iqoptionapi {

    public interface IIqOptionApi  : IDisposable {
        IqOptionWebSocketClient WsClient { get;  }
        IqOptionHttpClient HttpClient { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        IObservable<Profile> ProfileObservable { get; }
        IObservable<InfoData[]> InfoDatasObservable { get; }

        Profile Profile { get; }

        bool IsConnected { get; }

        IObservable<bool> IsConnectedObservable { get; }

        Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTime expiration = default(DateTime));


    }


    public class IqOptionApi : IIqOptionApi {
        private readonly IqOptionConfiguration _configuration;
        private readonly ILogger _logger;

        public IObservable<InfoData[]> InfoDatasObservable { get; private set; }
        public IqOptionHttpClient HttpClient { get; }
        public IqOptionWebSocketClient WsClient { get; private set; }

        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();
        private Profile _profile;
        public Profile Profile {
            get => _profile;
            private set {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }

        public IObservable<Profile> ProfileObservable => _profileSubject;

        public IDictionary<InstrumentType, Instrument[]> Instruments { get; private set; }
        public bool IsConnected { get; private set; }

        public IObservable<bool> IsConnectedObservable => connectedSubject;

        #region [Ctor]

        public IqOptionApi(string username, string password, string host = "iqoption.com")
            : this(new IqOptionConfiguration {Email = username, Password = password, Host = host}) {
        }

        public IqOptionApi(IqOptionConfiguration configuration) {
            _configuration = configuration;

            //set up client
            HttpClient = new IqOptionHttpClient(configuration.Email, configuration.Password);

            //set log
            _logger = IqOptionLoggerFactory.CreateLogger();
        }

        #endregion

        private Subject<bool> connectedSubject = new Subject<bool>();
        public async Task<bool> ConnectAsync() {

            connectedSubject.OnNext(false);
            IsConnected = false;

            _logger.LogInformation("Begin Connect to IqOption.com");
            var result = await HttpClient.LoginAsync();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"{_configuration.Email} logged in to {_configuration.Host} success!");
                WsClient = new IqOptionWebSocketClient(HttpClient.SecuredToken, _configuration.Host);

                if (await WsClient.OpenWebSocketAsync())
                    SubscriptWebSocket();

                    var profile = await GetProfileAsync();
                    _logger.LogInformation($"WebSocket for {profile.Email}({profile.UserId}) Connected!");

                IsConnected = true;
                connectedSubject.OnNext(true);
            }

            return IsConnected;
        }

        public async Task<Profile> GetProfileAsync() {
            var result = await HttpClient.GetProfileAsync();
            var profile = result.Content.JsonAs<IqHttpResult<models.Profile>>()?.Result;
            _logger.LogTrace($"Get Profile!: \t{profile}");

            return profile;
        }

        public async Task<bool> ChangeBalanceAsync(long balanceId) {
            var result = await HttpClient.ChangeBalanceAsync(balanceId);

            if (result?.Message == null && !result.IsSuccessful) {
                _logger.LogError($"Change balance ({balanceId}) error : {result.Message}");
                return false;
            }

            return true;
        }


        public Task<InstrumentResultSet> GetInstrumentsAsync() => WsClient.SendInstrumentsRequestAsync();

        public async Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction, DateTime expiration = default (DateTime)) {
            return await WsClient.BuyAsync(pair, size, direction, expiration);
            
        }



        private void SubscriptWebSocket() {
            Contract.Requires(WsClient != null);
            Contract.Requires(HttpClient != null);

            //subscribe profile
            WsClient.ProfileObservable
                .Merge(HttpClient.ProfileObservable())
                .DistinctUntilChanged()
                .Subscribe(x => {
                    _logger.LogDebug($"Profile Updated : {x?.ToString()}");
                    this.Profile = x;
                });

            WsClient.InstrumentResultSetObservable
                .Subscribe(x => {
                    _logger.LogDebug($"Instrument Updated!");
                    this.Instruments = x;
                });

            this.InfoDatasObservable = WsClient.InfoDataObservable;


        }


        public void Dispose() {
            connectedSubject?.Dispose();
            WsClient?.Dispose();
        }
    }
        public enum OrderDirection {

            [EnumMember(Value = "put")]
            Put = 1,

            [EnumMember(Value = "call")]
            Call = 2
        }
}