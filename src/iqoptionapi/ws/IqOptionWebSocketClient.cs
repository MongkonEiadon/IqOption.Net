using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using iqoptionapi.builders;
using iqoptionapi.models;
using iqoptionapi.ws.@base;
using iqoptionapi.ws.request;
using iqoptionapi.ws.result;
using IqOptionApi.extensions;
using IqOptionApi.Models;
using IqOptionApi.ws.request;
using Serilog;
using WebSocket4Net;
using InstrumentType = iqoptionapi.ws.model.InstrumentType;

namespace IqOptionApi.ws
{
    public class IqOptionWebSocketClient : IDisposable
    {
        //privates
        private readonly ILogger _logger = IqOptionLoggerFactory.CreateLogger();

        private WebSocket Client { get; }

        public IqOptionWebSocketClient(Action<IqOptionWebSocketClient> initialSetup = null,
            string host = "iqoption.com")
        {
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


            MessageReceivedObservable.Subscribe(x =>
            {
                var a = x.JsonAs<WsMessageBase<dynamic>>();

                if (a.Status != null && a.Status != 0 && a.Status != 2000)
                {
                    _logger.Error("Invalid response received " + a.AsJson());
                }
                else
                {

                    try
                    {
                        switch (a.Name?.ToLower())
                        {
                            case "heartbeat":
                                {
                                    var value1 = x.JsonAs<HeartBeat>();
                                    _heartbeat.OnNext(value1.Message);
                                    break;
                                }

                            case "timesync":
                                {
                                    var value2 = x.JsonAs<ServerTime>();
                                    ServerTime = value2.Message;
                                    break;
                                }
                            case "profile":
                                {
                                    //if (!a.Message.Equals(false))
                                    //{
                                    //    var profile = x.JsonAs<WsMessageBase<Profile>>().Message;
                                    //    _profileSubject.OnNext(profile);
                                    //    _balancesSubject.OnNext(profile.Balances.ToList());
                                    //}

                                    break;
                                }
                            case "instruments":
                                {
                                    var result = x.JsonAs<WsMessageBase<InstrumentType>>().Message;
                                    _logger.Verbose($"Received Inst. => instruments ({result.Type.ToString()})");
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

                            case "listinfodata":
                                {
                                    var result = x.JsonAs<WsMessageBase<InfoData[]>>();
                                    _infoDataSubject.OnNext(result?.Message);
                                    var info = result?.Message.FirstOrDefault();
                                    if (info != null)
                                        _logger.Verbose(
                                            $"info-received  => {info.UserId} {info.Win} {info.Direction} {info.Sum} {info.Active} @{info.Value} exp {info.ExpTime}({info.Expired})");
                                    break;
                                }

                            case "buycomplete":
                                {
                                    var result = x.JsonAs<BuyCompleteResultMessage>().Message;
                                    if (result.IsSuccessful)
                                    {
                                        var buyResult = x.JsonAs<BuyCompleteResultMessage>().Message.Result;
                                        _logger.Verbose(
                                            $"buycompleted   => {buyResult.UserId} {buyResult.Type} {buyResult.Direction} {buyResult.Price} {(ActivePair)buyResult.Act} @{buyResult.Value} ");
                                    }
                                    else
                                    {
                                        _logger.Warning($"{Profile?.UserId}\t{result.GetMessageDescription()}");
                                    }

                                    _buyResultSubject.OnNext(result.Result);

                                    break;
                                }
                            case "candles":
                                {
                                    var result = x.JsonAs<GetCandleItemsResultMessage>();
                                    if (result != null) CandleCollections = result.Message;
                                    break;
                                }

                            case "candle-generated":
                                {
                                    var candle = x.JsonAs<CurrentCandleInfoResultMessage>();
                                    if (candle != null)
                                    {
                                        _candleInfoSubject.OnNext(candle.Message);
                                        CurrentCandleInfo = candle.Message;
                                    }

                                    break;
                                }

                            case "live-deals":
                                {
                                    var deal = x.JsonAs<LiveDealResultMessage>();
                                    //if (deal != null)
                                    //{

                                    //}
                                    break;
                                }

                            case "balances":
                                {
                                    var balances = x.JsonAs<GetBalancesResultMessage>();

                                    _balancesSubject.OnNext(balances.Message);
                                    Balances = balances.Message;
                                    break;
                                }

                            case "order-placed-temp":
                                var value = x.JsonAs<OrderPlacedResultMessage>();
                                _orderPlacedSubject.OnNext(value);
                                break;

                            case "position-changed":
                                if (a.MicroserviceName == "portfolio")
                                {
                                    var positionChanged = x.JsonAs<PositionChangedPortfolioResultMessage>();
                                    _positionChangedPortfolioSubject.OnNext(positionChanged.Message);
                                }
                                else if (a.MicroserviceName == "trading")
                                {
                                    var positionChanged = x.JsonAs<PositionChangedTradingResultMessage>();
                                    _positionChangedTradingSubject.OnNext(positionChanged.Message);
                                }

                                break;

                            case "position-closed":
                                var positionClosed = x.JsonAs<PositionClosedResultMessage>();
                                _positionClosedSubject.OnNext(positionClosed.Message);
                                break;

                            case "position-batch-closed":
                                var positionBatchClosed = x.JsonAs<PositionBatchClosedResultMessage>();
                                _positionBatchClosedSubject.OnNext(positionBatchClosed.Message);
                                break;

                            case "positions":
                                var positions = x.JsonAs<PositionsReceivedResult>();
                                foreach (var position in positions.Message.positions)
                                {
                                    _positionSubject.OnNext(position);
                                }
                                _positionSubject.OnCompleted();
                                break;

                            case "order-changed":

                                if (a.MicroserviceName == "portfolio")
                                {
                                    var orderChanged = x.JsonAs<OrderChangedPortfolioResultMessage>();
                                    _orderChangedPortfolioSubject.OnNext(orderChanged.Message);
                                }
                                else if (a.MicroserviceName == "trading")
                                {
                                    var orderChanged = x.JsonAs<OrderChangedTradingResultMessage>();
                                    _orderChangedTradingSubject.OnNext(orderChanged.Message);
                                }
                                break;


                            default:
                                {
                                    _logger.Verbose(Profile?.Id + "    =>  " + a.AsJson());
                                    break;
                                }
                        }

                    }
                    catch (Exception exc)
                    {
                        _logger.Error("Error " + exc.Message, exc);
                        _logger.Error(x);
                    }
                }
            }, ex => { _logger.Error(ex.Message); });


            initialSetup?.Invoke(this);
        }

        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com") : this(
            x => x.OpenSecuredSocketAsync(secureToken), host)
        {

            this.Client.Closed += Client_Closed;
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            var ddt = "Client connection closed";
            if (ddt.Length > 0)
            {
            }
        }


        #region [Public's]

        public Task<bool> OpenSecuredSocketAsync(string ssid)
        {
            var tcs = new TaskCompletionSource<bool>();
            try
            {
                if (string.IsNullOrEmpty(ssid))
                    tcs.TrySetResult(false);

                SecureToken = ssid;
                var count = 0;
                var sub = ProfileObservable.Select(x => "Profile")
                    .Merge(HeartbeatObservable.Select(x => "Heartbeat"))
                    .Subscribe(x =>
                    {
                        if (count >= 2)
                        {
                            IsConnected = false;
                            tcs.TrySetResult(false);
                        }

                        if (x == "Profile") tcs.TrySetResult(true);

                        count++;
                    });

                SendMessageAsync(new SsidWsMessageBase(ssid)).ConfigureAwait(false);

                tcs.Task.ContinueWith(t =>
                {
                    IsConnected = t.Result;
                    sub.Dispose();
                });
            }
            catch (Exception ex)
            {
                IsConnected = false;
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public Task<bool> OpenWebSocketAsync()
        {
            if (Client.State == WebSocketState.Open)
                return Task.FromResult(true);
#if true //previous 46

            Client.Open();
            return Task.FromResult(true);

#else
            return Client.OpenAsync();
#endif
        }

        public IObservable<string> MessageReceivedObservable { get; }

        public string SecureToken { get; set; }

        public bool IsConnected { get; private set; }

        public async Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator)
        {
            if (await OpenWebSocketAsync())
            {
                _logger.Information($"send message   => :\t{messageCreator.CreateIqOptionMessage()}");
                Client.Send(messageCreator.CreateIqOptionMessage());
            }
        }

        #endregion

        #region [Profile]

        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();

        public Profile Profile { get; private set; }

        public IObservable<Profile> ProfileObservable => _profileSubject;

        #endregion

        #region [Instruments]

        private InstrumentResultSet _instrumentResultSet = new InstrumentResultSet();
        private readonly Subject<InstrumentResultSet> _instrumentResultSetSubject = new Subject<InstrumentResultSet>();

        public IObservable<InstrumentResultSet> InstrumentResultSetObservable =>
            _instrumentResultSetSubject.Publish().RefCount();


        public Task<InstrumentResultSet> GetInstrumentsAsync()
        {
            var tcs = new TaskCompletionSource<InstrumentResultSet>();
            try
            {
                _logger.Verbose(nameof(GetInstrumentsAsync));

                //subscribe for the lastest result
                InstrumentResultSetObservable
                    .Subscribe(x => { tcs.TrySetResult(x); });

                //clear before query new 
                _instrumentResultSet = new InstrumentResultSet();

                //execute them all
                Task.WhenAll(
                    SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(Models.EnumInstrumentType.Forex)),
                    SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(Models.EnumInstrumentType.CFD)),
                    SendMessageAsync(GetInstrumentWsMessageBase.CreateRequest(Models.EnumInstrumentType.Crypto))
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

        private readonly Subject<BuyResult> _buyResultSubject = new Subject<BuyResult>();
        public IObservable<BuyResult> BuyResultObservable => _buyResultSubject.Publish().RefCount();

        public Task<BuyResult> BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset))
        {
            var tcs = new TaskCompletionSource<BuyResult>();
            try
            {
                var obs = BuyResultObservable
                    .Where(x => x != null)
                    .Subscribe(x =>
                        tcs.TrySetResult(x));

                tcs.Task.ContinueWith(t =>
                {
                    if (t.Result != null) obs.Dispose();
                });

                //reduce second to 00s 
                if (expiration.Second % 60 != 0)
                    expiration = expiration.AddSeconds(60 - expiration.Second);

                SendMessageAsync(new BuyV2WsMessage(pair, size, direction, expiration, DateTimeOffset.Now)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #endregion

        #region [HeartBeat]

        private readonly Subject<DateTimeOffset> _heartbeat = new Subject<DateTimeOffset>();
        public IObservable<DateTimeOffset> HeartbeatObservable => _heartbeat.Publish().RefCount();

        #endregion

        #region [ServerTimes]

        public static DateTimeOffset ServerTime { get; private set; }

        #endregion

        #region [CandleCollections]

        private readonly Subject<CandleCollections> _candlesCollectionsSubject = new Subject<CandleCollections>();
        private CandleCollections _candleCollections;

        public CandleCollections CandleCollections
        {
            get => _candleCollections;
            set
            {
                _candleCollections = value;
                _candlesCollectionsSubject.OnNext(value);
            }
        }

        public IObservable<CandleCollections> CandlesObservable => _candlesCollectionsSubject.Publish().RefCount();

        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to)
        {
            var tcs = new TaskCompletionSource<CandleCollections>();
            try
            {
                var sub = CandlesObservable.Subscribe(x => { tcs.TrySetResult(x); });
                tcs.Task.ContinueWith(t =>
                {
                    sub.Dispose();

                    return t.Result;
                });

                SendMessageAsync(new GetCandleItemRequestMessage(pair, tf, count, to)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #endregion

        #region LiveDeal
        //private readonly Subject<CurrentCandle> _candleInfoSubject = new Subject<CurrentCandle>();
        //public CurrentCandle CurrentCandleInfo { get; set; }
        //public IObservable<CurrentCandle> RealTimeCandleInfoObservable => _candleInfoSubject.Publish().RefCount();

        #endregion

        #region [CurrentCandleInfo]

        private readonly Subject<CurrentCandle> _candleInfoSubject = new Subject<CurrentCandle>();
        public CurrentCandle CurrentCandleInfo { get; set; }
        public IObservable<CurrentCandle> RealTimeCandleInfoObservable => _candleInfoSubject.Publish().RefCount();

        public Task SubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame)
        {
            return SendMessageAsync(new SubscribeCandleRequest(pair, timeFrame));
        }

        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame)
        {
            throw new NotImplementedException();
            //return SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }

        public Task UnsubscribeTradersMoodChanged(string instrument, string assetId)
        {
            return SendMessageAsync(new UnsubscribeTradersMoodChanged(instrument, assetId));
        }
        #endregion

        #region Balances
        private readonly Subject<List<Balance>> _balancesSubject = new Subject<List<Balance>>();
        public List<Balance> Balances { get; set; }
        public IObservable<List<Balance>> BalancesObservable => _balancesSubject.Publish().RefCount();

        public Task GetBalancesAsync()
        {
            return SendMessageAsync(new GetBalancesRequest());
        }
        #endregion

        #region Positions
        private readonly Subject<PositionChangedPortfolio> _positionChangedPortfolioSubject = new Subject<PositionChangedPortfolio>();
        public IObservable<PositionChangedPortfolio> PositionChangedPortfolioObservable => _positionChangedPortfolioSubject.Publish().RefCount();
        public Task GetPortfolioPositionsAsync(long userBalanceId, string instrumentType)
        {
            return SendMessageAsync(new GetPortfolioPositionsRequest(instrumentType, userBalanceId));
        }

        #endregion

        #region PositionChangedPortfolio
        private readonly Subject<Position> _positionSubject = new Subject<Position>();
        public IObservable<Position> PositionObservable => _positionSubject.Publish().RefCount();

        public Task SubscribePositionChangedAsync(long userId, long userBalanceId, string instrumentType)
        {
            return SendMessageAsync(new SubscribePortfolioPositionChanged(userId, userBalanceId, instrumentType));
        }
        #endregion

        #region PositionChangedTrading
        private readonly Subject<PositionChangedTrading> _positionChangedTradingSubject = new Subject<PositionChangedTrading>();
        public IObservable<PositionChangedTrading> PositionChangedTradingObservable => _positionChangedTradingSubject.Publish().RefCount();
        #endregion

        #region PositionClosed
        private readonly Subject<PositionClosed> _positionClosedSubject = new Subject<PositionClosed>();
        public IObservable<PositionClosed> PositionClosedObservable => _positionClosedSubject.Publish().RefCount();

        public System.Threading.Tasks.Task ClosePosition(string positionId)
        {
            return SendMessageAsync(new ClosePositionRequest(positionId));
        }
        #endregion

        #region PositionBatch
        private readonly Subject<List<PositionClosed>> _positionBatchClosedSubject = new Subject<List<PositionClosed>>();
        public IObservable<List<PositionClosed>> PositionBatchClosedObservable => _positionBatchClosedSubject.Publish().RefCount();
        public System.Threading.Tasks.Task CloseBatchPositions(int[] positionId)
        {
            return SendMessageAsync(new ClosePositionBatchRequest(positionId));
        }

        #endregion

        #region OrderChangedPortfolio
        private readonly Subject<OrderChangedPortfolio> _orderChangedPortfolioSubject = new Subject<OrderChangedPortfolio>();
        public IObservable<OrderChangedPortfolio> OrderChangedPortfolioChangedObservable => _orderChangedPortfolioSubject.Publish().RefCount();
        public Task SubscribeOrderChangedAsync(long userId, long userBalanceId, string instrumentType)
        {
            return SendMessageAsync(new SubscribePortfolioOrderChanged(userId, userBalanceId, instrumentType));
        }
        #endregion

        #region OrderChangedTrading
        private readonly Subject<OrderChangedTrading> _orderChangedTradingSubject = new Subject<OrderChangedTrading>();
        public IObservable<OrderChangedTrading> OrderChangedTradingChangedObservable => _orderChangedTradingSubject.Publish().RefCount();
        #endregion
        #region InstrumentTypes
        private readonly Subject<EnumInstrumentType> _instrumentTypeTradingSubject = new Subject<EnumInstrumentType>();
        public IObservable<EnumInstrumentType> InstrumentTypeObservable => _instrumentTypeTradingSubject.Publish().RefCount();
        #endregion

        #region OrderPlaced
        private readonly Subject<OrderPlacedResultMessage> _orderPlacedSubject = new Subject<OrderPlacedResultMessage>();
        public IObservable<OrderPlacedResultMessage> OrderPlacedObservable => _orderPlacedSubject.Publish().RefCount();

        public async Task<PlaceOrderTempRequest> PlaceOrderAsync(decimal amount, Balance balance, OrderDirection orderDirection, iqoptionapi.ws.model.Instrument instrument)
        {
            PlaceOrderTempRequest placeOrderRequest = new PlaceOrderTempRequest(PlaceOrderBuilder.BuildPlaceOrderMarket(orderDirection, balance, amount, instrument)); ;
            await SendMessageAsync(placeOrderRequest);
            return placeOrderRequest;
        }
        #endregion

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}