﻿using System;
using System.Threading.Tasks;
using IqOptionApi.Http;
using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Models.DigitalOptions;
using IqOptionApi.Ws;

namespace IqOptionApi
{
    public interface IIqOptionClient : IDisposable
    {
        IqOptionWebSocketClient WsClient { get; }
        IqOptionHttpClient HttpClient { get; }
        IObservable<Profile> ProfileObservable { get; }
        Profile Profile { get; }
        bool IsConnected { get; }
        IObservable<bool> ConnectedObservable { get; }

        Task<bool> ConnectAsync();
        Task<Profile> GetProfileAsync();
        Task<bool> ChangeBalanceAsync(long balanceId);

        Task<BinaryOptionsResult> BuyAsync(ActivePair pair, int size, OrderDirection direction, DateTimeOffset expiration);


        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);
        Task<IObservable<CurrentCandle>> SubscribeRealtimeQuoteAsync(ActivePair pair, TimeFrame tf);

        Task UnSubscribeRealtimeData(ActivePair pair, TimeFrame tf);

        #region Subscribe

        /// <summary>
        /// Subscribe traders mood changed
        /// </summary>
        void SubscribeTradersMoodChanged(InstrumentType instrumentType, ActivePair active);

        /// <summary>
        /// Unsubscribe traders mood changed
        /// </summary>
        void UnSubscribeTradersMoodChanged(InstrumentType instrumentType, ActivePair active);

        #endregion

        #region PlacePositionCommands

        /// <summary>
        /// Place the DigitalOptions order
        /// </summary>
        /// <param name="pair">The Active pair, make sure your place with correct active.</param>
        /// <param name="direction">The position direction.</param>
        /// <param name="duration">The duration period in (1Min, 5Min, 15Min) from now</param>
        /// <param name="amount">The Amount of position</param>
        /// <returns></returns>
        Task<DigitalOptionsPlacedResult> PlaceDigitalOptions(ActivePair pair, OrderDirection direction,
            DigitalOptionsExpiryDuration duration, double amount);

        /// <summary>
        /// Place the DigitalOptions order from the instruments_id
        /// </summary>
        /// <param name="instrumentId">The Instrument identifier <example>doEURUSD201907191250PT5MPSPT</example></param>
        /// <param name="amount">The Amount of position</param>
        /// <returns></returns>
        Task<DigitalOptionsPlacedResult> PlaceDigitalOptions(string instrumentId, double amount);

        #endregion

    }
}