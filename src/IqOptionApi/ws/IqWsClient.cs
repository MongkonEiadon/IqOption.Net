using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
using ReactiveUI;
using WebSocket4Net;

[assembly: InternalsVisibleTo("IqOptionApi.Unit", AllInternalsVisible = true)]

namespace IqOptionApi.ws {
    public partial class IqWsClient : ReactiveObject, IIqWsClient {
        private readonly ILog _logger = LogProvider.GetLogger("[ WSS ]");
        private readonly IConnectableObservable<string> _publishedMessageObservable;
        private readonly WebSocket Client;

        public IqWsClient() {
            //Client = customizeClient ?? new WebsocketClient(new Uri("wss://iqoption.com/echo/websocket"));
            Client = new WebSocket("wss://iqoption.com/echo/websocket");

            _publishedMessageObservable =
                Observable.Using(
                        () => Client,
                        _ => Observable
                            .FromEventPattern<EventHandler<MessageReceivedEventArgs>, MessageReceivedEventArgs>(
                                handler => _.MessageReceived += handler,
                                handler => _.MessageReceived -= handler))
                    .Select(x => x.EventArgs.Message)
                    .SubscribeOn(new EventLoopScheduler())
                    .Publish();

            _publishedMessageObservable.Connect();

            ChannelMessage
                .OnErrorResumeNext(_publishedMessageObservable)
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
                                    _logger.Trace(Get(msg.Name, "Profile Updated"));
                                    break;

                                case EnumMessageType.ListInfoData:
                                    foreach (var info in x.JsonAs<WsMessageBase<InfoData[]>>().Message) {
                                        InfoData = info;
                                        _logger.Trace(Get(msg.Name, $"{info.ActiveId,5} {info.Direction,5} {info.Sum,2} {info.Win}"));
                                    }

                                    break;
                                case EnumMessageType.CandleGenerated: {
                                    var candle = x.JsonAs<CurrentCandleInfoResultMessage>().Message;
                                    CurrentCandle = candle;
                                    break;
                                }

                                //Forex/Crypto/Options
                                case EnumMessageType.BuyComplete:
                                    var _msg = x.JsonAs<BuyCompleteResultMessage>().Message;
                                    if (_msg.IsSuccessful) {
                                        var b = _msg.Result;
                                        _logger.Debug(Get(msg.Name, $"{_msg.IsSuccessful} => OrderId: {b.Id}"));
                                        BuyResult = b;
                                    }
                                    else {
                                        _logger.Warn(Get(msg.Name, $"{_msg.IsSuccessful} => {string.Join(", ", _msg.Message.ToList())}" ));
                                    }

                                    break;

                                //CFD
                                case EnumMessageType.PositionChanged:
                                    _logger.Debug(Get(msg.Name, $"PositionChanged: {x}"));
                                    DigitalInfoData = x.JsonAs<WsMessageBase<DigitalInfoData>>().Message;

                                    break;

                                case EnumMessageType.OrderChanged:
                                    
                                    break;

                                case EnumMessageType.Front: break;
                                default:
                                    _logger.Trace(x);
                                    break;
                            }
                        }
                        catch (Exception ex) {
                            _logger.ErrorException(Get("RxMsgError", "Error"), ex);
                        }
                    },
                    onError: ex =>
                        _logger.ErrorException(Get("RxMsgError", "Error"), ex));
        }

        

        public string SecuredToken { get; private set; }

        #region [Subjects and Public Obs.]

        public IObservable<string> ChannelMessage => _publishedMessageObservable.RefCount();

        #endregion

        public void Dispose() {
            Client?.Dispose();
        }


        #region [Privates]
        
        private Task<bool> ConnectAsync()
        {
            if (Client.State == WebSocketState.Open)
                return Task.FromResult(true);

            return Client.OpenAsync();

        }

        private string prifix() {
            var prefix = "";
            if (Profile == null)
                prefix = "uid:????";
            else if (Profile.UserId > 0)
                prefix = $"uid:{Profile?.UserId}";
            else if (Profile.BalanceId > 0)
                prefix = $"bal:{Profile.BalanceId}";
            return $"{prefix.PadRight(13).Substring(0, 13)} |";
        }

        private string Get(string topic, string msg)  => $"{prifix()} {topic.PadLeft(13).Substring(0, 13)} << {msg}";
        private string Post(string topic, string msg) => $"{prifix()} {topic.PadLeft(13).Substring(0, 13)} >> {msg}";

        
        #endregion

        internal async Task SendMessageAsync<T>(IWsRequestMessage<T> msg) {
           
            try {
                if (await ConnectAsync()) {

                    // logging
                    _logger.Debug(Post(msg.Name, msg.CreateIqOptionMessage()));


                    Client.Send(msg.CreateIqOptionMessage());
                }
            }
            catch (Exception ex) {
                _logger.ErrorException(ex.Message, ex);
            }
            
        }
        


    }
}