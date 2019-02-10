using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dawn;
using IqOptionApi.Annotations;
using IqOptionApi.Extensions;
using IqOptionApi.Logging;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;
using IqOptionApi.ws.@base;
using IqOptionApi.ws.Request;
using Websocket.Client;

[assembly: InternalsVisibleTo("IqOptionApi.Unit", AllInternalsVisible = true)]

namespace IqOptionApi.ws {
    public partial class IqWsClient : IIqWsClient {
        private readonly ILog _logger = LogProvider.GetLogger("[ WS ]");
        private IWebsocketClient Client { get; }
        
        public string SecuredToken { get; private set; }

        #region [Subjects and Public Obs.]

        public IObservable<string> ChannelMessage => Client.MessageReceived;

        #endregion

        public IqWsClient(IWebsocketClient customizeClient = null) {
            Client = customizeClient ?? new WebsocketClient(new Uri("wss://iqoption.com/echo/websocket"));
            Client
                .MessageReceived
                .Subscribe(x => {
                    var msg = x.JsonAs<WsMessageBase<object>>();
                    switch (msg.MessageType) {
                        case EnumMessageType.ServerTime:
                            ServerTime = x.JsonAs<ServerTime>();
                            break;

                        case EnumMessageType.Heartbeat:
                            HeartBeat = x.JsonAs<HeartBeat>();
                            break;

                        case EnumMessageType.Profile:
                            Profile = x.JsonAs<WsMessageBase<Profile>>().Message;
                            _logger.Trace(L("Profile Updated"));
                            break;

                        case EnumMessageType.ListInfoData:
                            foreach (var info in x.JsonAs<WsMessageBase<InfoData[]>>().Message) {
                                InfoData = info;
                                _logger.Trace(L($"{info.ActiveId,5} {info.Direction,5} {info.Sum,2} {info.Win}"));
                            }

                            break;

                        //
                        case EnumMessageType.BuyComplete:
                            _logger.Info(L($"BuyComplete: {x}"));
                            break;

                        //CFD
                        case EnumMessageType.PositionChanged:
                            _logger.Info(L($"PositionChanged: {x}"));
                            DigitalInfoData = x.JsonAs<WsMessageBase<DigitalInfoData>>().Message;

                            break;

                        case EnumMessageType.Front: break;
                        default:
                            _logger.Trace(x);
                            break;
                    }
                });
        }



        #region [Privates]

        private string L(string msg) {
            var userId = $"[ User: {Profile?.UserId.ToString() ?? "????"} ]";
            return $"{userId,-15} | {msg}";
        }

        internal Task SendMessageAsync(IWsIqOptionMessageCreator msg) {
            if (!Client.IsRunning)
                Client.Start();

            return Client.Send(msg.CreateIqOptionMessage());
        }


        #endregion



        #region [Notifications]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public IObservable<Profile> ProfileUpdated => this.ToObservable(x => x.Profile);

        public void Dispose() {
            Client?.Dispose();
        }
    }
}