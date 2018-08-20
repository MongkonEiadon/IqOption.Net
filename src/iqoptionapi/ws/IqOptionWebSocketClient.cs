using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws.request;
using Microsoft.Extensions.Logging;
using WebSocket4Net;


namespace iqoptionapi.ws {

    public class IqOptionWebSocketClient : IDisposable {
        //privates
        private readonly ILogger _logger = IqOptionLoggerFactory.CreateLogger();
        private  WebSocket Client { get; }

        public IqOptionWebSocketClient(Action<IqOptionWebSocketClient> initialSetup = null, string host = "iqoption.com") {

            Client = new WebSocket($"wss://{host}/echo/websocket");

            MessageReceivedObservable =
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
                switch (a.Name?.ToLower())
                {
                    case "heartbeat": {
                        var value = x.JsonAs<HeartBeat>();
                        _heartbeat.OnNext(value.HearBeatDateTime);
                        break;
                    }

                    case "timesync": {
                        var value = x.JsonAs<ServerTime>();
                        ServerTime = value.ServerDateTime;
                        break;
                    }
                    case "profile": {
                        if (!a.Message.Equals(false)) {
                            var profile = x.JsonAs<WsMessageBase<Profile>>().Message;
                            _profileSubject.OnNext(profile);
                        }

                        break;
                    }
                    case "instruments": {
                        var result = x.JsonAs<WsMessageBase<InstrumentsResult>>().Message;
                        _logger.LogTrace($"Received Inst. => instruments ({result.Type.ToString()})");
                        _instrumentResultSet[result.Type] = result.Instruments;
                        _instrumentResultSetSubject.OnNext(_instrumentResultSet);

                        if (_instrumentResultSet.All(i => i.Value.Any())) _instrumentResultSetSubject.OnCompleted();

                        break;
                    }

                    case "profit-top-user-moved-up":
                        {
                            break;
                        }

                    case "activecommissionchange":
                        {
                            break;
                        }

                    case "user-tournament-position-changed":
                        {
                            break;
                        }

                    case "chat-state-updated":
                        {
                            break;
                        }
                    case "front":
                        {
                            break;
                        }

                    case "listinfodata": {
                        var result = x.JsonAs<WsMessageBase<InfoData[]>>();
                        _infoDataSubject.OnNext(result?.Message);
                        var info = result?.Message.FirstOrDefault();
                        if (info != null)
                            _logger.LogInformation(
                                $"info-received  => {info.UserId} {info.Win} {info.Direction} {info.Sum} {info.Active} @{info.Value} exp {info.ExpTime}({info.Expired.ToShortTimeString()})");
                        break;
                    }

                    case "buycomplete": {
                        var result = x.JsonAs<WsMessageBase<WsMessageWithSuccessfulResult<object>>>().Message;
                        if (result.IsSuccessful) {
                            var buyResult = x.JsonAs<WsMessageBase<WsMessageWithSuccessfulResult<BuyResult>>>().Message
                                .Result;
                            _logger.LogInformation(
                                $"buycompleted   => {buyResult.UserId} {buyResult.Type} {buyResult.Direction} {buyResult.Price} {(ActivePair) buyResult.Act} @{buyResult.Value} ");
                            _buyResulSjSubject.OnNext(buyResult);
                        }
                        else {
                            var ex = string.Join(", ", result.Message?.ToList());
                            _logger.LogWarning($"{this.Profile?.UserId}\t{ex}", ex);
                            _buyResulSjSubject.OnNext(BuyResult.BuyResultError(result.Message));
                        }

                        break;
                    }
                    case "candles": {

                        var result = x.JsonAs<GetCandleItemsResultMessage>();
                        if (result != null) {
                            CandleCollections = result.Message;
                        }
                        break;
                    }

                    case "candle-generated": {

                        var candle = x.JsonAs<CurrentCandleInfoResultMessage>();
                        if (candle != null) {
                            _candleInfoSubject.OnNext(candle.Message);
                            CurrentCandleInfo = candle.Message;
                        }

                        break;
                    }

                    default: {
                        _logger.LogDebug(Profile?.Id + "    =>  " + a.AsJson());
                        break;
                    }
                }
            }, ex => { _logger.LogCritical(ex.Message); });

            
            initialSetup?.Invoke(this);
        }

        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com") : this(x => x.OpenSecuredSocketAsync(secureToken), host) { }


        #region [Public's]

        public Task<bool> OpenSecuredSocketAsync(string ssid) {

            var tcs = new TaskCompletionSource<bool>();
            try {

                if (string.IsNullOrEmpty(ssid))
                    tcs.TrySetResult(false);

                SecureToken = ssid;
                var count = 0;
                var sub = ProfileObservable.Select(x => "Profile")
                    .Merge(HeartbeatObservable.Select(x => "Heartbeat"))
                    .Subscribe(x => {
                        if (count >= 2) {
                            IsConnected = false;
                            tcs.TrySetResult(false);
                        }

                        if (x == "Profile") {
                            tcs.TrySetResult(true);
                        }

                        count++;

                    });

                SendMessageAsync(new SsidWsMessageBase(ssid)).ConfigureAwait(false);

                tcs.Task.ContinueWith(t => {
                    this.IsConnected = t.Result;
                    sub.Dispose();
                });

            }
            catch (Exception ex) {
                IsConnected = false;
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }
        public async Task<bool> OpenWebSocketAsync() {
            if (Client.State == WebSocketState.Open)
                return true;

            return await Client.OpenAsync();
        }
        public IObservable<string> MessageReceivedObservable { get; }
        
        public string SecureToken { get; set; }

        public bool IsConnected { get; private set; }

        public async Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator)
        {
            if (await OpenWebSocketAsync())
            {
                _logger.LogInformation($"send message   => :\t{messageCreator.CreateIqOptionMessage()}");
                Client.Send(messageCreator.CreateIqOptionMessage());
            }
        }


        #endregion

        #region [Profile]

        private Profile _profile;
        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();

        public Profile Profile { get; private set; }

        public IObservable<Profile> ProfileObservable => _profileSubject;

        #endregion

        #region [Instruments]

        private InstrumentResultSet _instrumentResultSet = new InstrumentResultSet();
        private readonly Subject<InstrumentResultSet> _instrumentResultSetSubject = new Subject<InstrumentResultSet>();

        public IObservable<InstrumentResultSet> InstrumentResultSetObservable =>
            _instrumentResultSetSubject.Publish().RefCount();


        public Task<InstrumentResultSet> SendInstrumentsRequestAsync()
        {
            var tcs = new TaskCompletionSource<InstrumentResultSet>();
            try
            {
                _logger.LogDebug(nameof(SendInstrumentsRequestAsync));

                //subscribe for the lastest result
                InstrumentResultSetObservable
                    .Subscribe(x => { tcs.TrySetResult(x); });

                //clear before query new 
                _instrumentResultSet = new InstrumentResultSet();

                //execute them all
                Task.WhenAll(
                    SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(InstrumentType.Forex)),
                    SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(InstrumentType.CFD)),
                    SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(InstrumentType.Crypto))
                );
            }

            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }


        #endregion

        #region [InfoData]

        private readonly Subject<InfoData[]> _infoDataSubject = new Subject<InfoData[]>();
        public IObservable<InfoData[]> InfoDataObservable => _infoDataSubject.Publish().RefCount();

        #endregion

        #region [BuyV2]

        private readonly Subject<BuyResult> _buyResulSjSubject = new Subject<BuyResult>();
        public IObservable<BuyResult> BuyResultObservable => _buyResulSjSubject.Publish().RefCount();
        public Task<BuyResult> BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            DateTime expiration = default(DateTime))
        {
            var tcs = new TaskCompletionSource<BuyResult>();
            try
            {
                var obs = BuyResultObservable
                    .Where(x => x != null)
                    .Subscribe(x => tcs.TrySetResult(x));

                tcs.Task.ContinueWith(t => {
                    if (t.Result != null)
                    {
                        obs.Dispose();
                    }
                });

                //reduce second to 00s 
                expiration = expiration.AddSeconds(60 - expiration.Second);

                SendMessageAsync(new BuyV2WsMessage(pair, size, direction, expiration, DateTime.Now)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #endregion

        #region [HeartBeat]
        private Subject<DateTimeOffset> _heartbeat = new Subject<DateTimeOffset>();
        public IObservable<DateTimeOffset> HeartbeatObservable => _heartbeat.Publish().RefCount();


        #endregion

        #region [ServerTimes]
        
        public static DateTimeOffset ServerTime { get; private set; }

        #endregion

        #region [CandleCollections]

        private readonly Subject<CandleCollections> _candlesCollectionsSubject = new Subject<CandleCollections>();
        private CandleCollections _candleCollections;
        public CandleCollections CandleCollections {
            get => _candleCollections;
            set {
                _candleCollections = value;
                _candlesCollectionsSubject.OnNext(value);
            }
        }
        public IObservable<CandleCollections> CandlesObservable => _candlesCollectionsSubject.Publish().RefCount();

        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to) {

            var tcs = new TaskCompletionSource<CandleCollections>();
            try {

                var sub = CandlesObservable.Subscribe(x => {

                    tcs.TrySetResult(x);
                });
                tcs.Task.ContinueWith(t => {
                    sub.Dispose();

                    return t.Result;
                });

                this.SendMessageAsync(new GetCandleItemRequestMessage(pair, tf, count, to)).ConfigureAwait(false);

            }
            catch (Exception ex) {

                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #endregion


        #region [CurrentCandleInfo]

        private Subject<CurrentCandle> _candleInfoSubject=new Subject<CurrentCandle>();
        public CurrentCandle CurrentCandleInfo { get; set; }
        public IObservable<CurrentCandle> RealTimeCandleInfoObservable => _candleInfoSubject.Publish().RefCount();

        public Task SubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return this.SendMessageAsync(new SubscribeMessageRequest(pair, timeFrame));
        }

        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame) {
            return this.SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }

        #endregion

        public void Dispose() {
            Client?.Dispose();
        }
    }

    
}