using System;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;
using IqOptionApi.http;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using IqOptionApi.ws;

namespace IqOptionApi {
    public interface IIqClient : IDisposable {

        IIqHttpClient HttpClient { get; }
        IIqWsClient WsClient { get; }

        Task<bool> ConnectAsync();
    }

    public class IqClient : IIqClient {

        private readonly ILog _logger = LogProvider.GetLogger("[ API ]");

        public IIqHttpClient HttpClient { get; }
        public IIqWsClient WsClient { get; }

        public IObservable<Profile> ProfileUpdated => 
            HttpClient.ProfileUpdated
                .Merge(WsClient.ProfileUpdated);

        public IqClient(string email, string password) {

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

        private string L(string msg) {

            return $"[API]  : {msg}";
        }


        public void Dispose() {
            HttpClient?.Dispose();
            WsClient?.Dispose();
        }

    }
}