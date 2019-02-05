using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoptionapi.ws.@base;
using IqOptionApi.extensions;
using IqOptionApi.Models;
using IqOptionApi.ws.request;
using Websocket.Client;

namespace IqOptionApi.ws
{
    internal class WsClient : IClient<IWebsocketClient>, IDisposable
    {
        public IWebsocketClient Client { get; }


        #region [Subjects and Public Obs.]

        private readonly Subject<ServerTime> _serverTime = new Subject<ServerTime>();
        public IObservable<ServerTime> ServerTime() => _serverTime;

        private readonly Subject<HeartBeat> _heartBeat = new Subject<HeartBeat>();
        public IObservable<HeartBeat> Heartbeat() => _heartBeat;

        private readonly Subject<BuyResult> _buyResult = new Subject<BuyResult>();
        public IObservable<BuyResult> BuyResult() => _buyResult;


        #endregion

        public WsClient(IWebsocketClient customizeClient = null)
        {
            Client = customizeClient ?? new WebsocketClient(new Uri("$wss://iqoption.com/echo/websocket"));

            Client
                .MessageReceived
                .Subscribe(x =>
                {
                    var msg = x.JsonAs<WsMessageBase<object>>();
                    switch (msg.MessageType)
                    {
                        case EnumMessageType.ServerTime:
                            _serverTime.OnNext(x.JsonAs<ServerTime>());
                            break;

                        case EnumMessageType.Heartbeat:
                            _heartBeat.OnNext(x.JsonAs<HeartBeat>());
                            break;

                    }

                });
        }


        public Task SendMessageAsync(IWsIqOptionMessageCreator msg)
        {
            if (!Client.IsRunning)
                Client.Start();

            return Client.Send(msg.CreateIqOptionMessage());
        }

      
        


        public void Dispose()
        {
            Client?.Dispose();
        }


       
    }
}