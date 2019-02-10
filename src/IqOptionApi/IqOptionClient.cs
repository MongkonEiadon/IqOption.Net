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

        public IIqHttpClient HttpClient { get; }
        public IIqWsClient WsClient { get; }

        public IObservable<Profile> ProfileUpdated =>
            WsClient.ToObservable(x => x.Profile)
                .Merge(HttpClient.ToObservable(x => x.Profile));


        public IqOptionClient(string email, string password) {
            HttpClient = new IqHttpClient(email, password);
            WsClient = new IqWsClient();
        }


        public async Task<bool> ConnectAsync() {
            _logger.Info(L("Tried to connect to server."));
            var result = await HttpClient.LoginAsync();

            if (result.IsSuccessful) {
                await WsClient.OpenSecuredConnectionAsync(result.Data.Ssid);
                return true;
            }

            return false;
        }

        /*
       #--------------------------------------------------------------------------------
       #--------------------------subscribe&unsubscribe---------------------------------
       #--------------------------------------------------------------------------------
        */

        #region [Subscribe&Unsubscribe]

        public IObservable<CurrentCandle> CandleInfo => WsClient.ToObservable(x => x.CurrentCandle);
        public Task SubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return ((IqWsClient) WsClient).SendMessageAsync(new SubscribeMessageRequest(pair, timeFrame));
        }

        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return ((IqWsClient) WsClient).SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }

        #endregion


        private string L(string msg) {
            return $"[API]  : {msg}";
        }


        public void Dispose() {
            HttpClient?.Dispose();
            WsClient?.Dispose();
        }
    }
}