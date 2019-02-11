using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.Extensions;
using IqOptionApi.http;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using IqOptionApi.ws;
using IqOptionApi.ws.Request;

namespace IqOptionApi {
    public class IqOptionClient : IIqOptionClient {
        private readonly ILog _logger = LogProvider.GetLogger("[ API ]");

        /// <summary>
        ///     Client to connect to IqOption.com
        /// </summary>
        /// <param name="email">Username</param>
        /// <param name="password">Password</param>
        public IqOptionClient(string email, string password) {
            HttpClient = new IqHttpClient(email, password);
            WsClient = new IqWsClient();
        }

        public IObservable<Profile> ProfileUpdated =>
            WsClient.ToObservable(x => x.Profile)
                .Merge(HttpClient.ToObservable(x => x.Profile));

        public IIqHttpClient HttpClient { get; }
        public IIqWsClient WsClient { get; }


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
                await WsClient.OpenSecuredConnectionAsync(result.Data.Ssid);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTimeOffset expiration = default) {
            return WsClient.BuyAsync(pair, size, direction, expiration);
        }

        #endregion

        /*
       #--------------------------------------------------------------------------------
       #--------------------------subscribe&unsubscribe---------------------------------
       #--------------------------------------------------------------------------------
        */

        #region [Subscribe&Unsubscribe]

        /// <inheritdoc />
        public IObservable<CurrentCandle> CandleInfo => WsClient.ToObservable(x => x.CurrentCandle);


        /// <inheritdoc />
        public Task SubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return ((IqWsClient) WsClient).SendMessageAsync(new SubscribeMessageRequest(pair, timeFrame));
        }

        /// <inheritdoc />
        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return ((IqWsClient) WsClient).SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }
        
        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame timeFrame, int count, DateTimeOffset to)
        {
            return WsClient.GetCandlesAsync(pair, timeFrame, count, to);
        }

        #endregion
    }
}