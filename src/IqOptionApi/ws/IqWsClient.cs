using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dawn;
using IqOptionApi.Extensions;
using IqOptionApi.Logging;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;
using IqOptionApi.ws.@base;
using IqOptionApi.ws.request;
using RestSharp.Extensions;
using Websocket.Client;

[assembly:InternalsVisibleTo("IqOptionApi.Unit", AllInternalsVisible = true)]
namespace IqOptionApi.ws
{
    public class IqWsClient : IIqWsClient, IDisposable {
        private readonly ILog _logger = LogProvider.GetLogger("[ WS ]");
        private IWebsocketClient Client { get; }

        public string SecuredToken { get; private set; }

        #region [Subjects and Public Obs.]

        private Profile _p { get; set; }
        private readonly Subject<ServerTime> _serverTime = new Subject<ServerTime>();
        private readonly Subject<HeartBeat> _heartBeat = new Subject<HeartBeat>();
        private readonly Subject<BuyResult> _buyResult = new Subject<BuyResult>();
        private readonly Subject<InfoData> _infoData = new Subject<InfoData>();
        private readonly Subject<Profile> _profile = new Subject<Profile>();
        private readonly Subject<DigitalInfoData> _cfdInfo = new Subject<DigitalInfoData>();

        public IObservable<ServerTime> ServerTime => _serverTime.AsObservable();
        public IObservable<HeartBeat> Heartbeat => _heartBeat.AsObservable();
        public IObservable<BuyResult> BuyResult => _buyResult.AsObservable();
        public IObservable<InfoData> InfoData => _infoData.AsObservable();
        public IObservable<Profile> ProfileUpdated => _profile.AsObservable();
        public IObservable<string> ChannelMessage => Client.MessageReceived;
        public IObservable<DigitalInfoData> DigitalInfoData => _cfdInfo.AsObservable();

        #endregion

        public IqWsClient(IWebsocketClient customizeClient = null)
        {
            Client = customizeClient ?? new WebsocketClient(new Uri("wss://iqoption.com/echo/websocket"));
            Client
                .MessageReceived
                .Subscribe(x => {
                    var msg = x.JsonAs<WsMessageBase<object>>();
                    switch (msg.MessageType) {
                        case EnumMessageType.ServerTime:
                            _serverTime.OnNext(x.JsonAs<ServerTime>());
                            break;

                        case EnumMessageType.Heartbeat:
                            _heartBeat.OnNext(x.JsonAs<HeartBeat>());
                            break;

                        case EnumMessageType.Profile:
                            _p = x.JsonAs<WsMessageBase<Profile>>().Message;
                            _profile.OnNext(_p); _logger.Trace(L($"Profile Updated"));
                            break;

                        case EnumMessageType.ListInfoData:
                            foreach (var info in x.JsonAs<WsMessageBase<InfoData[]>>().Message) {
                                _infoData.OnNext(info);
                                _logger.Trace(L($"{info.ActiveId, 5} {info.Direction, 5} {info.Sum, 2} {info.Win}"));
                            }

                            break;

                        //
                        case EnumMessageType.BuyComplete:
                            _logger.Info(L($"BuyComplete: {x}"));
                            break;
                        
                        //CFD
                        case EnumMessageType.PositionChanged:
                            _logger.Info(L($"PositionChanged: {x}"));
                            _cfdInfo.OnNext(x.JsonAs<WsMessageBase<DigitalInfoData>>().Message);
                            break;
                            
                        case EnumMessageType.Front: break;
                        default:
                            _logger.Trace(message: x);
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

        public Task<bool> OpenSecuredConnectionAsync(string token) {

            var tcs = new TaskCompletionSource<bool>();
            try {

                Guard.Argument(token, nameof(token)).NotNull().NotEmpty();

                SecuredToken = token;

                var limit = 0;
                ProfileUpdated.Select(x => nameof(ProfileUpdated))
                    .Merge(Heartbeat.Select(x => nameof(Heartbeat)))
                    .Subscribe(x => {
                        if (limit >= 2) {
                            tcs.TrySetResult(false);
                        }

                        if (x == nameof(ProfileUpdated)) {
                            tcs.TrySetResult(true);
                        }

                        limit++;
                    });

                SendMessageAsync(new SsidWsMessageBase(SecuredToken)).ConfigureAwait(false);
                
            }
            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            finally
            {
                SecuredToken = token;
            }

            return tcs.Task;
        }

        private string L(string msg) {
            var userId = $"[ User: {_p?.UserId.ToString() ?? "????"} ]";
            return $"{userId,-15} | {msg}";
        }
    }
}