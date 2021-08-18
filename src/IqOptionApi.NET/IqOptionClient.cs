using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using IqOptionApi.Http;
using IqOptionApi.Logging;
using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Ws;
using IqOptionApi.Ws.Base;
using Microsoft.Extensions.Logging;

namespace IqOptionApi
{
    public class IqOptionClient : IIqOptionClient
    {
        private readonly ILogger _logger;
        private bool IsSSIDLogin = false;

        #region [Ctor]

        public IqOptionClient(string username, string password)
        {
            Username = username;
            Password = password;
            _logger = IqOptionApiLog.Logger;

            //set up client
            HttpClient = new IqOptionHttpClient(username, password, "iqoption.com");
        }
        public IqOptionClient(string SSID)
        {
            this.IsSSIDLogin = true;
            _logger = IqOptionApiLog.Logger;
            if (WsClient != null) WsClient.Dispose();
            WsClient = new IqOptionWebSocketClient(SSID);

            //set up client
            HttpClient = new IqOptionHttpClient(SSID);
        }

        #endregion

        public Task<bool> ConnectAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            if (this.IsSSIDLogin)
            {
                SubscribeWebSocket();

                IsConnected = true;
                connectedSubject.OnNext(true);

                tcs.TrySetResult(true);
            }
            else 
            { 
                connectedSubject.OnNext(false);
                IsConnected = false;

                try
                {
                    string Message = null;
                    HttpClient
                        .LoginAsync()
                        .ContinueWith(t =>
                        {
                            if (t.Result != null && t.Result.IsSuccessful)
                            {
                                Message = $"{Username} logged in success!";
                                _logger.LogInformation(Message);

                                if (WsClient != null) WsClient.Dispose();
                                string SSID = t.Result.Data.Ssid;
                                WsClient = new IqOptionWebSocketClient(SSID);

                                SubscribeWebSocket();

                                IsConnected = true;
                                connectedSubject.OnNext(true);
                                tcs.TrySetResult(true);
                                return;
                            }

                            Message = $"{Username} logged in failed due to {t.Result?.Errors?.GetErrorMessage()}";
                            _logger.LogInformation(Message);
                            tcs.TrySetResult(false);
                        }).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }

            return tcs.Task;
        }

        public async Task<Profile> GetProfileAsync()
        {
            var result = await HttpClient.GetProfileAsync();
            return result.Result;
        }

        public async Task<bool> ChangeBalanceAsync(long balanceId)
        {
            var result = await HttpClient.ChangeBalanceAsync(balanceId);

            if (result == null)
            {
                return true;
            }

            if (result?.Message == null && !result.IsSuccessful)
            {
                _logger.LogError($"Change balance ({balanceId}) error : {result.Message}");
                return false;
            }

            _logger.LogInformation($"Change balance to {balanceId} successfully!");
            return true;
        }

        public Task<BinaryOptionsResult> BuyAsync(ActivePair pair, double size, OrderDirection direction,int expiration)
        {
            return WsClient?.BuyAsync(pair, size, direction, expiration);
        }

        public string BuyRequest(ActivePair pair, double size, OrderDirection direction, int expiration)
        {
            return WsClient?.BuyRequest(pair, size, direction, expiration);
        }

        public Task<BinaryOptionsResult> BuyAsync(ActivePair pair, double size, OrderDirection direction, DateTimeOffset expiration)
        {
            return WsClient?.BuyAsync(pair, size, direction, expiration);
        }

        public Task<Leaderboard> GetLeaderboard(Country country, int From=1, int To=64)
        {
            return WsClient?.GetLeaderboard(country, From, To);
        }

        public Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame timeFrame, int count,
            DateTimeOffset to)
        {
            return WsClient?.GetCandlesAsync(pair, timeFrame, count, to);
        }

        public Task<IObservable<CurrentCandle>> SubscribeRealtimeQuoteAsync(ActivePair pair, TimeFrame tf)
        {
            WsClient?.SubscribeQuoteAsync(pair, tf).ConfigureAwait(false);

            var stream = WsClient?
                .RealTimeCandleInfoObservable
                .Where(x => x.ActivePair == pair && x.TimeFrame == tf);

            return Task.FromResult(stream);
        }


        /// <inheritdoc/>
        public void SubscribeTradersMoodChanged(InstrumentType instrumentType, ActivePair active)
            => WsClient?.SubscribeTradersMoodChanged(instrumentType, active);

        /// <inheritdoc/>
        public void UnSubscribeTradersMoodChanged(InstrumentType instrumentType, ActivePair active)
            => WsClient?.UnSubscribeTradersMoodChanged(instrumentType, active);

        public void SubscribeLiveDealPlaced(InstrumentType instrumentType, ActivePair active)
            => WsClient?.SubscribeLiveDealPlaced(instrumentType, active);

        /// <inheritdoc/>
        public void UnSubscribeLiveDealPlaced(InstrumentType instrumentType, ActivePair active)
            => WsClient?.UnSubscribeLiveDealPlaced(instrumentType, active);

        /// <inheritdoc/>
        public Task UnSubscribeRealtimeData(ActivePair pair, TimeFrame tf)
            => WsClient?.UnsubscribeCandlesAsync(pair, tf);

        public void SubscribeInstrumentQuotes(ActivePair Pair, int ExpTime, InstrumentType Type)
            => WsClient?.SubscribeInstrumentQuotes(Pair, ExpTime, Type);

        /// <inheritdoc/>
        public Task<DigitalOptionsPlacedResult> PlaceDigitalOptions(ActivePair pair, OrderDirection direction,
            DigitalOptionsExpiryDuration duration, double amount)
            => WsClient?.PlaceDigitalOptions(pair, direction, duration, amount);


        /// <inheritdoc/>
        public Task<DigitalOptionsPlacedResult> PlaceDigitalOptions(string instrumentId, double amount)
            => WsClient?.PlaceDigitalOptions(instrumentId, amount);

        /// <inheritdoc/>
        public Task<HistoryPositions> GetHistoryPositions(List<InstrumentType> instrument_types, int limit, int offset)
            => WsClient?.GetHistoryPositions(instrument_types, limit, offset);

        public void Dispose()
        {
            connectedSubject?.Dispose();
            WsClient?.WebSocketClient.Stop(WebSocketCloseStatus.NormalClosure, "Disposed");
            WsClient?.Dispose();
        }


        /// <summary>
        ///     listen to all obs, for make all properties updated
        /// </summary>
        private void SubscribeWebSocket()
        {
            //subscribe profile
            HttpClient.ProfileObservable
                .Merge(WsClient.ProfileObservable)
                .DistinctUntilChanged()
                .Where(x => x != null)
                .Subscribe(x => Profile = x);

            //subscribe for instrument updated
            WsClient.InstrumentResultSetObservable
                .Subscribe(x => Instruments = x);
        }

        #region [Privates]

        private readonly Subject<Profile> _profileSubject = new Subject<Profile>();

        private readonly Subject<bool> connectedSubject = new Subject<bool>();
        private Profile _profile;

        #endregion

        #region [Publics]

        public string Username { get; }
        public string Password { get; }
        public IDictionary<InstrumentType, Instrument[]> Instruments { get; private set; }

        public Profile Profile
        {
            get => _profile;
            private set
            {
                _profileSubject.OnNext(value);
                _profile = value;
            }
        }

        public bool IsConnected { get; private set; }

        //clients
        public IqOptionHttpClient HttpClient { get; }
        public IqOptionWebSocketClient WsClient { get; private set; }

        //obs
        public IObservable<Profile> ProfileObservable => _profileSubject.AsObservable();
        public IObservable<bool> ConnectedObservable => connectedSubject.AsObservable();
        public IObservable<WsMessageBase<BinaryOptionsResult>> BuyResultObservable => WsClient?.BinaryOptionPlacedResultObservable;
        public IObservable<Leaderboard> LeaderBoardObservable => WsClient?.LeaderboardObservable;


        #endregion
    }
}