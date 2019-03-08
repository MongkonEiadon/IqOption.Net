using System;
using System.Threading.Tasks;
using IqOptionApi.http;
using IqOptionApi.Models;
using IqOptionApi.ws;

namespace IqOptionApi {
    public interface IIqOptionApi : IDisposable {
        /// <summary>
        ///     The Http channel
        /// </summary>
        IIqHttpClient HttpClient { get; }

        /// <summary>
        ///     The WebSocket channel
        /// </summary>
        IIqWsClient WsClient { get; }

        /*
        #--------------------------------------------------------------------------------
        #-------------------------------Commands-----------------------------------------
        #--------------------------------------------------------------------------------
         */

        #region [Commands]

        /// <summary>
        ///     Begin connect to IqOption server
        /// </summary>
        /// <returns></returns>
        Task<bool> ConnectAsync();

        /// <summary>
        ///     Get Profile
        /// </summary>
        /// <returns></returns>
        Task<Profile> GetProfileAsync();


        /// <summary>
        ///     Open buying position for Forex/Crypto/Options
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        Task BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset));

        /// <summary>
        ///  Change the balance Id
        /// </summary>
        /// <returns></returns>
        Task<bool> ChangeBalanceAsync(long balanceId);

        #endregion

        /*
        #--------------------------------------------------------------------------------
        #--------------------------subscribe&unsubscribe---------------------------------
        #--------------------------------------------------------------------------------
         */

        #region [Subscribe&Unsubscribe]

        /// <summary>
        ///     The candles information after using <see cref="SubscribeCandlesAsync" /> onto specific <see cref="ActivePair" />
        /// </summary>
        IObservable<CurrentCandle> CandleInfo { get; }

        /// <summary>
        ///     Subscribe candles information
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="timeFrame"></param>
        /// <returns></returns>
        Task SubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame);

        /// <summary>
        ///     Unsubscribe candles information
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="timeFrame"></param>
        /// <returns></returns>
        Task UnsubscribeCandlesAsync(ActivePair pair, TimeFrame timeFrame);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="timeFrame"></param>
        /// <param name="count"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame timeFrame, int count, DateTimeOffset to);

        #endregion
        
    }
}