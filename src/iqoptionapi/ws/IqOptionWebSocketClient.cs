using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using WebSocket4Net;
using Microsoft.Extensions.Logging;

namespace iqoptionapi.ws {
    public class IqOptionWebSocketClient : IDisposable {

        //privates
        private readonly ILogger _logger;


        #region [Public's]
        public IObservable<string> MessageReceivedObservable { get; }
        public IObservable<object> DataReceivedObservable { get; }

        #endregion  
       

        public string SecureToken { get; set; }

        public WebSocket Client { get; }
        public DateTime TimeSync { get; private set; }
        public Profile Profile { get; private set; }

        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com") {

            Client = new WebSocket(uri: $"wss://{host}/echo/websocket");
            this.SecureToken = secureToken;

            _logger = new LoggerFactory().CreateLogger(nameof(IqOptionWebSocketClient));

            this.MessageReceivedObservable =
                Observable.Using(
                        () => Client,
                        _ => Observable
                            .FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                handler => _.MessageReceived += handler,
                                handler => _.MessageReceived -= handler))
                    .Select(x => x.EventArgs.Message);


            MessageReceivedObservable.Subscribe(x => {

                _logger.LogTrace(x);
                var a = x.JsonAs<WsMessageBase<object>>();

                switch (a.Name?.ToLower()) {

                    case "timesync": {
                        this.TimeSync = a.Message.FromUnixToDateTime();
                        break;
                    }
                    case "profile": {
                        this.Profile = x.JsonAs<WsMessageBase<Profile>>().Message;
                        break;
                    }
                }
            });

            //send ssid message
            OpenSecuredSocketAsync();
        }

        

      

        public async Task SendMessageAsync(IWsIqOptionMessage message) {
            if (await OpenWebSocketAsync()) {
                Client.Send(message.CreateIqOptionMessage());
            }
        }

        protected Task OpenSecuredSocketAsync() {
            return SendMessageAsync(new SsidWsMessage(SecureToken));
        }

        public Task<bool> OpenWebSocketAsync() {
            return Client.OpenAsync();
        }


        public void Dispose() {
            Client?.Dispose();
        }
    }
}