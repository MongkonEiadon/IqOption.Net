using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        public IqOptionHttpClient HttpClient { get; }
        public IqOptionWebSocketClient WsClient { get; private set; }

        //obs
        public IObservable<InfoData[]> InfoDatasObservable => WsClient?.InfoDataObservable;
        public IObservable<Profile> ProfileObservable => _profileSubject.Publish().RefCount();
        public IObservable<bool> IsConnectedObservable => connectedSubject.Publish().RefCount();
        
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
                           
                            _logger.LogInformation($"{Username} logged in success!");

                            WsClient.OpenSecuredSocketAsync(t.Result.Data.Ssid);

                            SubscribeWebSocket();

                            IsConnected = true;
                            connectedSubject.OnNext(true);
                            tcs.TrySetResult(true);
                            return;
                        }

                        _logger.LogInformation($"{Username} logged in failed due to {t.Result?.Errors?.GetErrorMessage()}");
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
                _logger.LogError($"Change balance ({balanceId}) error : {result.Message}");
                return false;
            }

            return true;
        }

        public Task<BuyResult> BuyAsync(ActivePair pair, int size, OrderDirection direction,
            DateTime expiration = default(DateTime)) {
            
            return WsClient?.BuyAsync(pair, size, direction, expiration);
        }


        public void Dispose() {
            connectedSubject?.Dispose();
            WsClient?.Dispose();
        }


      


        private void SubscribeWebSocket() {

            //subscribe profile
            WsClient.ProfileObservable
                .Merge(HttpClient.ProfileObservable())
                .DistinctUntilChanged()
                .Where(x => x!=null)
                .Subscribe(x => Profile = x);

            WsClient.InstrumentResultSetObservable
                .Subscribe(x => Instruments = x);

        }

        #region [Ctor]

        public IqOptionApi(string username, string password, string host = "iqoption.com")
        {
            Username = username;
            Password = password;

            //set up client
            HttpClient = new IqOptionHttpClient(username, password);
            WsClient = new IqOptionWebSocketClient("");
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