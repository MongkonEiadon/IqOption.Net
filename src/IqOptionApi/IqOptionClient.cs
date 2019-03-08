using System;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IqOptionApi.Exceptions;
using IqOptionApi.Extensions;
using IqOptionApi.http;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using IqOptionApi.ws;
using IqOptionApi.ws.Request;

[assembly:InternalsVisibleTo("IqOptionApi.Tests")]
namespace IqOptionApi {
    public class IqOptionApi : IIqOptionApi {
        private readonly ILog _logger = LogProvider.GetLogger("[ API ]");

        /// <summary>
        ///     Client to connect to IqOption.com
        /// </summary>
        /// <param name="email">Username</param>
        /// <param name="password">Password</param>
        public IqOptionApi(string email, string password) {
           _lazyHttp = new Lazy<IIqHttpClient>(() =>  new IqHttpClient(email, password));
           _lazyWs = new Lazy<IIqWsClient>(() => new IqWsClient());
        }

        internal Lazy<IIqHttpClient> _lazyHttp;

        /// <inheritdoc cref="IIqOptionApi.HttpClient"/>
        public IIqHttpClient HttpClient {
            get => _lazyHttp.Value;
            set => _lazyHttp = new Lazy<IIqHttpClient>(() => value);
        }

        internal Lazy<IIqWsClient> _lazyWs;

        /// <inheritdoc cref="IIqOptionApi.HttpClient"/>
        public IIqWsClient WsClient {
            get => _lazyWs.Value;
            internal set => _lazyWs = new Lazy<IIqWsClient>(() => value);
        }


        public void Dispose() {
            HttpClient?.Dispose();
            WsClient?.Dispose();
        }


        private string L(string topic, string msg) {
            return $"{topic.PadRight(13).Substring(0, 13)} | {msg}";
        }


        /*
       #--------------------------------------------------------------------------------
       #-------------------------------Commands-----------------------------------------
       #--------------------------------------------------------------------------------
        */

        #region [Commands]

        /// <inheritdoc />
        public async Task<bool> ConnectAsync() {
            _logger.Info(L("Connect", "--------------------------------------------------------------------------------"));
            _logger.Info(L("Connect", "-----------------------Tried to connect to server.------------------------------"));
            _logger.Info(L("Connect", $"-----------------------------<{this.HttpClient.LoginModel.Email.PadRight(15).Substring(0,15)}>----------------------------------"));
            var result = await HttpClient.LoginAsync();

            if (result.IsSuccessful) {
                WsClient.OpenSecuredConnection(result.Data.Ssid);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public Task BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset)) {

            _logger.Info(L("buyAsync", $"Open {direction} {pair}, lot {size, 5} @Exp {expiration.ToLocalTime()}"));
            return WsClient.BuyAsync(pair, size, direction, expiration);
        }

        /// <inheritdoc />
        public async Task<Profile> GetProfileAsync()
        {
            try
            {
                _logger.Info(L("getprofile", ""));
                var profile = await HttpClient.GetProfileAsync();

                return profile;
            }
            catch (Exception ex)
            {
                _logger.Error(L("getprofile", ex.Message));
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<bool> ChangeBalanceAsync(long balanceId)
        {
            try
            {
                _logger.Info(L("changebalance", $"Change to balance id: {balanceId}"));
                await HttpClient.ChangeBalance(balanceId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(L("changebalance", $"{ex.Message}"), ex);
                return false;
            }

        }

        #endregion

        /*
       #--------------------------------------------------------------------------------
       #--------------------------subscribe&unsubscribe---------------------------------
       #--------------------------------------------------------------------------------
        */

        #region [Subscribe&Unsubscribe]

        /// <inheritdoc />
        public IObservable<CurrentCandle> CandleInfo { get; }


        /// <inheritdoc />
        public Task SubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return ((IqWsClient) WsClient).SendMessageAsync(new SubscribeMessageRequest(pair, timeFrame));
        }

        /// <inheritdoc />
        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return ((IqWsClient) WsClient).SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }
        
        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame timeFrame, int count, DateTimeOffset to) {
            return WsClient.GetCandlesAsync(pair, timeFrame, count, to);
        }

        #endregion


    }
}