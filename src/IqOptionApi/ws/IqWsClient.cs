using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IqOptionApi.Annotations;
using IqOptionApi.Extensions;
using IqOptionApi.Logging;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;
using IqOptionApi.ws.@base;
using IqOptionApi.ws.result;
using IqOptionApi.ws.Request;
using Websocket.Client;

[assembly: InternalsVisibleTo("IqOptionApi.Unit", AllInternalsVisible = true)]

namespace IqOptionApi.ws {
    public partial class IqWsClient : IIqWsClient {
        private readonly ILog _logger = LogProvider.GetLogger("[ WSS ]");

        public IqWsClient(IWebsocketClient customizeClient = null) {
            Client = customizeClient ?? new WebsocketClient(new Uri("wss://iqoption.com/echo/websocket"));
            Client
                .MessageReceived
                .Subscribe(x => {
                    try {
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
                                _logger.Trace(L(msg.Name, "Profile Updated"));
                                break;

                            case EnumMessageType.ListInfoData:
                                foreach (var info in x.JsonAs<WsMessageBase<InfoData[]>>().Message) {
                                    InfoData = info;
                                    _logger.Trace(L(msg.Name,
                                        $"{info.ActiveId,5} {info.Direction,5} {info.Sum,2} {info.Win}"));
                                }

                                break;
                            case EnumMessageType.CandleGenerated: {
                                var candle = x.JsonAs<CurrentCandleInfoResultMessage>().Message;
                                CurrentCandle = candle;
                                break;
                            }

                            //Forex/Crypto/Options
                            case EnumMessageType.BuyComplete:
                                var buyComplete = x.JsonAs<BuyCompleteResultMessage>().Message;
                                _logger.Debug(L(msg.Name,
                                    $"IsSuccess: {buyComplete.IsSuccessful} => {buyComplete.Result.AsJson()}"));
                                BuyResult = buyComplete.Result;
                                break;

                            //CFD
                            case EnumMessageType.PositionChanged:
                                _logger.Debug(L(msg.Name, $"PositionChanged: {x}"));
                                DigitalInfoData = x.JsonAs<WsMessageBase<DigitalInfoData>>().Message;

                                break;

                            case EnumMessageType.Front: break;
                            default:
                                _logger.Trace(x);
                                break;
                        }
                    }
                    catch (Exception ex) {
                        _logger.ErrorException(L("RxMsgError","Error"), ex);
                    }
                });
        }

        private IWebsocketClient Client { get; }

        public IObservable<Profile> ProfileUpdated => this.ToObservable(x => x.Profile);

        public string SecuredToken { get; private set; }

        #region [Subjects and Public Obs.]

        public IObservable<string> ChannelMessage => Client.MessageReceived;

        #endregion

        public void Dispose() {
            Client?.Dispose();
        }


        #region [Privates]

        private string L(string topic, string msg) {
            var prefix = "";
            if (Profile == null)
                prefix = "uid:????";
            else if (Profile.UserId > 0)
                prefix = $"uid:{Profile?.UserId}";
            else if (Profile.BalanceId > 0)
                prefix = $"bal:{Profile.BalanceId}";
            return $"{prefix.PadRight(13).Substring(0, 13)} | " +
                   $"{topic.PadLeft(13).Substring(0,13)} > {msg}";
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
    }
}