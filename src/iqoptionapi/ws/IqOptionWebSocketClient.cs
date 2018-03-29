using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using iqoptionapi.extensions;
using iqoptionapi.models;
using iqoptionapi.ws.request;
using WebSocket4Net;
using Microsoft.Extensions.Logging;

namespace iqoptionapi.ws {
    public class IqOptionWebSocketClient : IDisposable {

        //privates
        private readonly ILogger _logger;
        private WebSocket Client { get; }

        #region [Public's]

        public IObservable<string> MessageReceivedObservable { get; }

        public IObservable<object> DataReceivedObservable { get; }
        public string SecureToken { get; set; }

        #endregion


        private long _timeSync;
        private Subject<long> _timeSyncSubject = new Subject<long>();
        public DateTime TimeSync => _timeSync.FromUnixToDateTime();

        #region [Profile]
        private Profile _profile;
        private Subject<Profile> _profileSubject = new Subject<Profile>();
        public Profile Profile {
            get => _profile;
            set {
                _profile = value;
                _profileSubject.OnNext(value);
            }
        }
        public IObservable<Profile> ProfileObservable => _profileSubject;

        #endregion

        #region [Instruments]
        private InstrumentResultSet _instrumentResultSet = new InstrumentResultSet();
        private Subject<InstrumentResultSet> _instrumentResultSetSubject = new Subject<InstrumentResultSet>();

        public IObservable<InstrumentResultSet> InstrumentResultSetObservable { get; }

        #endregion

        #region [InfoData]

        Subject<InfoData[]> _infoDataSubject = new Subject<InfoData[]>();
        public IObservable<InfoData[]> InfoDataObservable => _infoDataSubject.Publish().RefCount();


        #endregion

        #region [BuyV2]

        private Subject<BuyResult> _buyResulSjSubject = new Subject<BuyResult>();
        public IObservable<BuyResult> BuyResultObservable { get; }

        #endregion


        public IqOptionWebSocketClient(string secureToken, string host = "iqoption.com") {

            Client = new WebSocket(uri: $"wss://{host}/echo/websocket");
            _logger = IqOptionLoggerFactory.CreateLogger();
            this.SecureToken = secureToken;

            //set up shred obs.
            InstrumentResultSetObservable =
                _instrumentResultSetSubject.PublishLast().RefCount();

            BuyResultObservable =
                _buyResulSjSubject.Publish().RefCount();

            this.MessageReceivedObservable =
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
                var a = x.JsonAs<WsRequestMessageBase<object>>();
                switch (a.Name?.ToLower()) {

                    case "timesync": {
                        _timeSync = Int64.Parse(a.Message.ToString());
                        _timeSyncSubject.OnNext(_timeSync);
                        break;
                    }
                    case "profile": {
                        _profile = x.JsonAs<WsRequestMessageBase<Profile>>().Message;
                        _logger.LogTrace($"Received {_profile?.Email}'s profile!");
                       
                        break;
                    }
                    case "instruments": {
                        var result = x.JsonAs<WsRequestMessageBase<InstrumentsResult>>().Message;
                        _logger.LogInformation($"Recevied -> instruments ({result.Type.ToString()})");
                        _instrumentResultSet[result.Type] = result.Instruments;
                        _instrumentResultSetSubject.OnNext(_instrumentResultSet);

                        if (_instrumentResultSet.All(i => i.Value.Any())) {
                            _instrumentResultSetSubject.OnCompleted();
                        }

                        break;
                    }

                    case "listinfodata": {
                        var result = x.JsonAs<WsRequestMessageBase<InfoData[]>>();
                        _infoDataSubject.OnNext(result?.Message);
                        break;
                    }

                    case "buycomplete": {
                        var result = x.JsonAs<WsRequestMessageBase<WsMsgResult<object>>>().Message;
                        if (result.IsSuccessful) {
                            _logger.LogInformation("Buy Completed!");
                            var buyResult = x.JsonAs<WsRequestMessageBase<WsMsgResult<BuyResult>>>().Message.Result;
                            _buyResulSjSubject.OnNext(buyResult);
                        }
                        else {
                            var ex = string.Join(", ", result.Message.ToList());
                            _logger.LogError(ex);
                            _buyResulSjSubject.OnNext(BuyResult.BuyResultError(result.Message));
                        }

                        break;
                    }
                }
            }, onError: ex => { _logger.LogCritical(ex.Message); });

            //send ssid message
            OpenSecuredSocketAsync();
        }


        public async Task SendMessageAsync(IWsIqOptionMessageCreator messageCreator) {
            if (await OpenWebSocketAsync()) {
                _logger.LogTrace($"send msge => :\t{messageCreator.CreateIqOptionMessage()}");
                Client.Send(messageCreator.CreateIqOptionMessage());
            }
        }

        public Task<InstrumentResultSet> SendInstrumentsRequestAsync() {
            var tcs = new TaskCompletionSource<InstrumentResultSet>();
            try {

                //subscribe for the lastest result
                this.InstrumentResultSetObservable
                    .Subscribe(x => {
                        tcs.TrySetResult(x);
                    });

                //clear before query new 
                this._instrumentResultSet = new InstrumentResultSet();

                //execute them all
                Task.WhenAll(
                    SendMessageAsync(GetInstrumentWsRequestMessageBase.CreateRequest(InstrumentType.Forex)),
                    SendMessageAsync(GetInstrumentWsRequestMessageBase.CreateRequest(InstrumentType.CFD)),
                    SendMessageAsync(GetInstrumentWsRequestMessageBase.CreateRequest(InstrumentType.Crypto))
                );
            }

            catch (Exception ex) {
                tcs.TrySetException(ex);
            }

            return tcs.Task;
        }

        public Task<BuyResult> BuyAsync(
            ActivePair pair, 
            int size, 
            OrderDirection direction,
            DateTime expiration = default(DateTime)) {

            var tcs = new TaskCompletionSource<BuyResult>();
            try {
                
                BuyResultObservable
                    .Subscribe(x => {
                            tcs.TrySetResult(x);
                        }, 
                        ex => { tcs.TrySetException(ex); });

                expiration = this._instrumentResultSet.GetByActivityType(pair).Schedule[3].CloseDateTime;

                Task.Run(() => this.SendMessageAsync(new BuyV2WsRequestMessage(pair, size, direction, expiration, this.TimeSync.ToLocalTime())));

                

            }
            catch (Exception ex) {
                tcs.TrySetException(ex);

            }

            return tcs.Task;
        }



        public async Task<bool> OpenWebSocketAsync() {
            if (Client.State == WebSocketState.Open)
                return true;

            return await Client.OpenAsync();
        }


        public void Dispose() {
            Client?.Dispose();
        }

        private Task OpenSecuredSocketAsync() {
            return SendMessageAsync(new SsidWsRequestMessageBase(SecureToken));
        }


    }
}