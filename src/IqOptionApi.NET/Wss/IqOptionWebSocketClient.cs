﻿using System;
 using System.Reactive.Concurrency;
 using System.Reactive.Linq;
 using System.Threading;
 using System.Threading.Tasks;
 using IqOptionApi.Logging;
 using IqOptionApi.Models;
 using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
 using Microsoft.Extensions.Logging;
 using WebSocketSharp;
 using AsyncLock = IqOptionApi.Core.AsyncLock;
 using Timer = System.Timers.Timer;

 namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient : IDisposable
    {
        public WebSocket WebSocketClient { get; private set; }
        private readonly ILogger _logger;
        private readonly Timer SystemReconnectionTimer = new Timer(TimeSpan.FromMinutes(3).Ticks);

        public IqOptionWebSocketClient(string secureToken)
        {
            _logger = IqOptionApiLog.Logger;
            SecureToken = secureToken;
            
            InitialSocket(secureToken);
        }

        public string SecureToken { get; }


        #region [Public's]

        public IObservable<string> MessageReceivedObservable { get; private set; }
        private readonly AsyncLock _asyncLock = new AsyncLock();

        private long _requestCounter = 0;
        /// <summary>
        /// Commit the message with Fire-And-Forgot style
        /// </summary>
        /// <param name="messageCreator"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator, string requestPrefix = "")
        {
            using (await _asyncLock.WaitAsync(CancellationToken.None).ConfigureAwait(false))
            {
                _requestCounter = _requestCounter + 1;
                var payload = messageCreator.CreateIqOptionMessage($"{requestPrefix}{_requestCounter}");
                WebSocketClient.Send(payload);
                _logger.LogDebug("⬆ {payload}", payload);
            }
        }

        /// <summary>
        /// Commit the message to the IqOption Server, and wait for result back
        /// </summary>
        /// <param name="messageCreator">The message creator builder.</param>
        /// <param name="observableResult">The target observable that will trigger after message was incomming</param>
        /// <typeparam name="TResult">The expected result</typeparam>
        /// <returns></returns>
        public Task<TResult> SendMessageAsync<TResult>(
            IWsIqOptionMessageCreator messageCreator,
            IObservable<TResult> observableResult)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var token = new CancellationTokenSource(5000).Token;

            try
            {
                token.ThrowIfCancellationRequested();
                token.Register(() =>
                {
                    if (tcs.TrySetCanceled())
                    {
                        _logger.LogWarning(string.Format(
                            "Wait result for type '{0}', took long times {1} seconds. The result will send back with default {0}\n{2}",
                            typeof(TResult), 5000, messageCreator));
                    }
                });
                
                observableResult
                    .FirstAsync()
                    .Subscribe(x => { tcs.TrySetResult(x); }, token);

                // send message
                SendMessageAsync(messageCreator).ConfigureAwait(false);
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

        
        /// <summary>
        /// Subscribe to the realtime quotes, after called this method
        /// The <see cref="ActivePair"/> with specific <see cref="TimeFrame"/> will received every single tick
        /// </summary>
        /// <param name="pair">The Active pair to subscribe</param>
        /// <param name="timeFrame">The Time frame to subscribe</param>
        /// <returns></returns>
        public Task SubscribeQuoteAsync(ActivePair pair, TimeFrame timeFrame)
        {
            return SendMessageAsync(new SubscribeMessageRequest(pair, timeFrame), "s_");
        }

        /// <summary>
        /// UnSubscribe to the realtime quotes, after called this method
        /// The <see cref="ActivePair"/> with specific <see cref="TimeFrame"/> will not received anymore
        /// </summary>
        /// <param name="pair">The Active pair to unsubscribe</param>
        /// <param name="timeFrame">The Time frame to unsubscribe</param>
        /// <returns></returns>
        public Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame)
        {
            return SendMessageAsync(new UnSubscribeMessageRequest(pair, timeFrame));
        }

        #endregion

        protected virtual void InitialSocket(string secureToken)
        {
            WebSocketClient = new WebSocket("wss://iqoption.com/echo/websocket");
            WebSocketClient.OnError += (sender, args) =>
            {
                _logger.LogError($"WebSocket Error : {args.Message}");
            };
            
            WebSocketClient.Connect();

            var scheduler = new EventLoopScheduler();
            MessageReceivedObservable =
                Observable.Using(
                        () => WebSocketClient,
                        _ => Observable
                            .FromEventPattern<EventHandler<MessageEventArgs>, MessageEventArgs>(
                                handler => _.OnMessage += handler,
                                handler => _.OnMessage -= handler))
                    .Select(x => x.EventArgs.Data)
                    .SubscribeOn(scheduler)
                    .Publish()
                    .RefCount();

            WebSocketClient.OnMessage += (sender, args) => SubscribeIncomingMessage(args.Data);

            SystemReconnectionTimer.AutoReset = true;
            SystemReconnectionTimer.Enabled = true;
            SystemReconnectionTimer.Elapsed += (sender, args) =>
            {
                _logger.LogWarning("System try to reconnect");
                SendMessageAsync(new SsidWsMessageBase(secureToken)).ConfigureAwait(false);
            };
            
            // send secure token to connect to server
            SendMessageAsync(new SsidWsMessageBase(secureToken), ProfileObservable).Wait();

            // send order changed subscribe
            InitialSubscribeOrderChanged();
        }

        private void InitialSubscribeOrderChanged()
        {
            foreach (var instru in new []{ InstrumentType.Crypto, InstrumentType.Forex, InstrumentType.BinaryOption, InstrumentType.DigitalOption, InstrumentType.TurboOption})
            {
                //Task.Run(() => SubscribeOrderChanged(instru));
            }
        }
    }
}