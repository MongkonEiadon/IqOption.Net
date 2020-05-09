using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Timers;
using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
using Serilog;
using WebSocketSharp;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient : IDisposable
    {
        private readonly WebSocket _client;

        //privates
        private ILogger _logger;

        private readonly Timer SystemReconnectionTimer = new Timer(TimeSpan.FromMinutes(3).Ticks);

        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com")
        {
            SecureToken = secureToken;

            _client = new WebSocket($"wss://{host}/echo/websocket");
            _client.OnError += (sender, args) =>
            {
                var a = args;
            };

            _client.Connect();


            var scheduler = new EventLoopScheduler();
            MessageReceivedObservable =
                Observable.Using(
                        () => _client,
                        _ => Observable
                            .FromEventPattern<EventHandler<MessageEventArgs>, MessageEventArgs>(
                                handler => _.OnMessage += handler,
                                handler => _.OnMessage -= handler))
                    .Select(x => x.EventArgs.Data)
                    .SubscribeOn(scheduler)
                    .Publish()
                    .RefCount();

            // create logger context
            _logger = IqOptionLoggerFactory.CreateWebSocketLogger(Profile);

            _client.OnMessage += (sender, args) => SubscribeIncomingMessage(args.Data);

            SystemReconnectionTimer.AutoReset = true;
            SystemReconnectionTimer.Enabled = true;
            SystemReconnectionTimer.Elapsed += (sender, args) =>
            {
                _logger.Warning("System try to reconnect");
                SendMessageAsync(new SsidWsMessageBase(SecureToken)).ConfigureAwait(false);
            };


            // send secure token to connect to server
            SendMessageAsync(new SsidWsMessageBase(SecureToken)).ConfigureAwait(false);
        }

        public string SecureToken { get; }


        #region [Public's]

        public IObservable<string> MessageReceivedObservable { get; }

        public Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator)
        {
            var payload = messageCreator.CreateIqOptionMessage();
            
            _logger.ForContext("Topic", "request >>").Debug("{payload}", payload);
            
            _client.Send(payload);

            return Task.CompletedTask;
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
            DateTimeOffset expiration)
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

                // incasse of non-binary options
                var optionType = OptionType.Turbo;
                if (expiration.Subtract(ServerTime).Minutes >= 5)
                    optionType = OptionType.Binary;

                SendMessageAsync(
                        new BuyV2WsMessage(
                            Profile.BalanceId,
                            pair,
                            optionType,
                            direction,
                            expiration,
                            size))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        #endregion


        #region [CurrentCandleInfo]

        public IObservable<CurrentCandle> RealTimeCandleInfoObservable => _candleInfoSubject.AsObservable();

        public Task SubscribeQuoteAsync(ActivePair pair, TimeFrame timeFrame)
        {
            return SendMessageAsync(new SubscribeMessageRequest(pair, timeFrame));
        }

        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame)
        {
            return SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }

        #endregion
    }
}