using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws.request;
using WebSocket4Net;
using Microsoft.Extensions.Logging;

namespace iqoptionapi.ws {
    public class IqOptionWebSocketClient : ObservableObject, IDisposable {

        //privates
        private readonly ILogger _logger;
        private WebSocket Client { get; }

        #region [Public's]
        public IObservable<string> MessageReceivedObservable { get; }
        public IObservable<object> DataReceivedObservable { get; }
        public string SecureToken { get; set; }

        #endregion


        private DateTime _timeSync;
        public DateTime TimeSync {
            get => _timeSync;
            set {
                this.OnPropertyChanged(nameof(Profile));
                _timeSync = value;
            }
        }

        private Profile _profile;
        public Profile Profile {
            get => _profile;
            set {
                this.OnPropertyChanged(nameof(Profile));
                _profile = value;
            }
        }

        public IObservable<Profile> ProfileObservable() {
            return this.ToObservable(x => x.Profile);
        }

        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com") {

            Client = new WebSocket(uri: $"wss://{host}/echo/websocket");
            _logger = IqOptionLoggerFactory.CreateLogger();
            this.SecureToken = secureToken;

            this.MessageReceivedObservable =
                Observable.Using(
                        () => Client,
                        _ => Observable
                            .FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                handler => _.MessageReceived += handler,
                                handler => _.MessageReceived -= handler))
                    .Select(x => x.EventArgs.Message)
                    .SubscribeOn(new EventLoopScheduler())
                    .Publish()
                    .RefCount();


            MessageReceivedObservable.Subscribe(x => {

                var a = x.JsonAs<WsMessageBase<object>>();

                switch (a.Name?.ToLower()) {

                    case "timesync": {
                        this.TimeSync = a.Message.FromUnixToDateTime();
                        //_logger.LogTrace($"{a.Message}");
                        break;
                    }
                    case "profile": {
                        _logger.LogTrace($"Get Profile!, {a.Message}");
                        this.Profile = x.JsonAs<WsMessageBase<models.Profile>>().Message;
                        break;
                    }
                    case "instruments": {
                        var result = x.JsonAs<WsMessageBase<InstrumentsResult>>().Message;
                        break;
                    }
                }
            }, onError: ex => { _logger.LogCritical(ex.Message); });

            //send ssid message
            OpenSecuredSocketAsync();
        }


        public async Task SendMessageAsync(IWsIqOptionMessage message) {
            if (await OpenWebSocketAsync()) {
                _logger.LogTrace($"send msge => :\t{message.CreateIqOptionMessage()}");
                Client.Send(message.CreateIqOptionMessage());
            }
        }

        public Task SendInstrumentsRequestAsync() {
            return Task.WhenAll(
                SendMessageAsync(GetInstrumentWsMessage.CreateRequest(InstrumentType.Forex)),
                SendMessageAsync(GetInstrumentWsMessage.CreateRequest(InstrumentType.CFD)),
                SendMessageAsync(GetInstrumentWsMessage.CreateRequest(InstrumentType.Crypto))
            );
        }



        protected Task OpenSecuredSocketAsync() {
            return SendMessageAsync(new SsidWsMessage(SecureToken));
        }

        public async Task<bool> OpenWebSocketAsync() {
            if (Client.State == WebSocketState.Open)
                return true;

            return await Client.OpenAsync();
        }


        public void Dispose() {
            Client?.Dispose();
        }
        
        
    }
}