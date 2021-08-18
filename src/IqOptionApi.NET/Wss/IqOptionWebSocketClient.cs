﻿using System;
using System.Net.WebSockets;
using System.Reactive.Concurrency;
 using System.Reactive.Linq;
using System.Text;
using System.Threading;
 using System.Threading.Tasks;
 using IqOptionApi.Logging;
 using IqOptionApi.Models;
 using IqOptionApi.Ws.Base;
using IqOptionApi.Ws.Request;
 using Microsoft.Extensions.Logging;
 using Websocket.Client;
 using AsyncLock = IqOptionApi.Core.AsyncLock;
 using Timer = System.Timers.Timer;

 namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient : IDisposable
    {
        public IWebsocketClient WebSocketClient { get; private set; }
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
        private long _requestCounterTemp = 0;
        private DateTimeOffset _requestTime = DateTimeOffset.Now;
        private int CancelationTimeout = 10;

        /// <summary>
        /// Set global task timeout
        /// </summary>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        public void SetCancelationTimeout(int Timeout = 10)
        {
            this.CancelationTimeout = Timeout;
        }

        /// <summary>
        /// Commit the message with Fire-And-Forgot style
        /// </summary>
        /// <param name="messageCreator"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator, string requestPrefix = "")
        {
            try
            {
                using (await _asyncLock.WaitAsync(CancellationToken.None).ConfigureAwait(false))
                {
                    if (WebSocketClient == null || !WebSocketClient.IsRunning) return;
                    _requestCounter = _requestCounter + 1;
                    var payload = messageCreator.CreateIqOptionMessage($"{requestPrefix}{_requestCounter}");
                    WebSocketClient.Send(payload);
                    _logger.LogDebug("⬆ {payload}", payload);
                }
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Error: {0}", ex.Message);
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
            var token = new CancellationTokenSource(CancelationTimeout * 1000).Token;

            try
            {
                token.ThrowIfCancellationRequested();
                token.Register(() =>
                {
                    if (tcs.TrySetCanceled())
                    {
                        _logger.LogWarning(string.Format(
                            "Wait result for type '{0}', took long times {1} seconds. The result will send back with default {0}\n{2}",
                            typeof(TResult), CancelationTimeout, messageCreator));
                    }
                });
                
                observableResult.FirstAsync().Subscribe(x => {
                    tcs.TrySetResult(x);
                }, token);

                // send message
                SendMessageAsync(messageCreator).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
            
            return tcs.Task;
        }

        /// <summary>
        /// Commit the message to the IqOption Server, and wait for result back
        /// </summary>
        /// <param name="messageCreator">The message creator builder.</param>
        /// <param name="observableResult">The target observable that will trigger after message was incomming and simulate http request</param>
        /// <typeparam name="TResult">The expected result</typeparam>
        /// <returns></returns>
        public Task<TResult> SendMessageAsync<TResult>(
            IWsIqOptionMessageCreator messageCreator,
            IObservable<WsMessageBase<TResult>> observableResult)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var token = new CancellationTokenSource(CancelationTimeout * 1000).Token;

            try
            {
                token.ThrowIfCancellationRequested();
                token.Register(() =>
                {
                    if (tcs.TrySetCanceled())
                    {
                        _logger.LogWarning(string.Format(
                            "Wait result for type '{0}', took long times {1} seconds. The result will send back with default {0}\n{2}",
                            typeof(TResult), CancelationTimeout, messageCreator));
                        tcs.TrySetException(new Exception(string.Format("Cancellation Requested, took long times {0} seconds", CancelationTimeout)));
                    }
                });

                observableResult.Subscribe(x =>
                {
                    if (x.RequestId == messageCreator.GetRequestID()) tcs.TrySetResult(x.Message);
                });
                SendMessageAsync(messageCreator).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
            return tcs.Task;
        }

        public string SendMessageRequest(IWsIqOptionMessageCreator messageCreator)
        {
            if (WebSocketClient == null || !WebSocketClient.IsRunning) return null;
            long RequestID = this._requestCounter + Interlocked.Increment(ref this._requestCounterTemp);
            string payload = messageCreator.CreateIqOptionMessage($"{RequestID}");
            Task.Run(() => WebSocketClient.Send(payload));
            _logger.LogDebug("⬆ {payload}", payload);
            return messageCreator.GetRequestID();
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
            try
            {
                var factory = new Func<ClientWebSocket>(() =>
                {
                    var client = new ClientWebSocket
                    {
                        Options = {
                            KeepAliveInterval = TimeSpan.FromSeconds(500),
                        }
                    };
                    return client;
                });

                Uri URI = new Uri("wss://iqoption.com/echo/websocket");
                WebSocketClient = new WebsocketClient(URI, factory);
                WebSocketClient.ReconnectTimeout = TimeSpan.FromSeconds(30);
                WebSocketClient.ErrorReconnectTimeout = TimeSpan.FromSeconds(30);
                WebSocketClient.DisconnectionHappened.Subscribe(info =>
                {
                    _logger.LogError($"Disconnection happened, type: {info.Type}");
                });
                WebSocketClient.ReconnectionHappened.Subscribe(type =>
                {
                    _logger.LogError($"Reconnection happened, type: {type}");
                });

                var scheduler = new EventLoopScheduler();
                MessageReceivedObservable = WebSocketClient.MessageReceived
                    .Where(x => x.Text != null)
                    .Select(x => x.Text)
                    .SubscribeOn(scheduler)
                    .Publish()
                    .RefCount();

                WebSocketClient.MessageReceived
                    .Where(x => x.Text != null)
                    .ObserveOn(TaskPoolScheduler.Default)
                    .Subscribe(Response => SubscribeIncomingMessage(Response.Text));

                WebSocketClient.Start().Wait();

                SystemReconnectionTimer.AutoReset = true;
                SystemReconnectionTimer.Enabled = true;
                SystemReconnectionTimer.Elapsed += (sender, args) =>
                {
                    _logger.LogWarning("System try to reconnect");
                    SendMessageAsync(new SsidWsMessageBase(secureToken)).ConfigureAwait(false);
                };

                // send secure token to connect to server
                SendMessageAsync(new SsidWsMessageBase(secureToken), ProfileObservable).Wait();

                _logger.LogInformation("Websocket started");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }

        private void InitialSubscribeOrderChanged()
        {
            foreach (var instru in new []{ InstrumentType.Crypto, InstrumentType.Forex, InstrumentType.BinaryOption, InstrumentType.DigitalOption, InstrumentType.TurboOption})
            {
                //Task.Run(() => SubscribeOrderChanged(instru));
            }
        }

        public DateTimeOffset GetServerTime()
        {
            return ServerTime;
        }
    }
}