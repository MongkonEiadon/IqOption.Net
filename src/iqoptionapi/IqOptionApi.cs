using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.http;
using iqoptionapi.models;
using iqoptionapi.ws;
using Microsoft.Extensions.Logging;

namespace iqoptionapi {

    public interface IIqOptionApi {
        IqOptionWebSocketClient WsClient { get;  }
        IqOptionHttpClient HttpClient { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        Profile Profile { get; }
    }


    public class IqOptionApi : IIqOptionApi {
        private readonly IqOptionConfiguration _configuration;
        private readonly ILogger _logger;

        public IqOptionHttpClient HttpClient { get; }
        public IqOptionWebSocketClient WsClient { get; private set; }

        public Profile Profile { get; private set; }

        public IDictionary<InstrumentType, Instrument[]> Instruments { get; private set; }

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


        public async Task<bool> ConnectAsync() {
            var result = await HttpClient.LoginAsync();

            if (result.StatusCode == HttpStatusCode.OK) {
                WsClient = new IqOptionWebSocketClient(HttpClient.SecuredToken, _configuration.Host);

                if (await WsClient.OpenWebSocketAsync())
                    SubscriptWebSocket();
                    _logger.LogInformation("WebSocket Connected!");

                return true;
            }

            return false;
        }

        public async Task<Profile> GetProfileAsync() {
            var result = await HttpClient.GetProfileAsync();
            var profile = result.Content.JsonAs<IqHttpResult<models.Profile>>()?.Result;
            _logger.LogInformation($"Get Profile!: \t{profile}");

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
                    _logger.LogInformation($"Profile Updated : {x?.ToString()}");
                    this.Profile = x;
                });

            WsClient.InstrumentResultSetObservable
                .Subscribe(x => {
                    _logger.LogInformation($"Instrument Updated!");
                    this.Instruments = x;
                });


        }

        
    }
        public enum OrderDirection {

            [EnumMember(Value = "put")]
            Put = 1,

            [EnumMember(Value = "call")]
            Call = 2
        }
}