using System;
using System.Threading.Tasks;
using IqOptionApi.http;
using IqOptionApi.Models;
using IqOptionApi.ws;

namespace IqOptionApi {
    public interface IIqOptionClient : IDisposable {
        IIqHttpClient HttpClient { get; }
        IIqWsClient WsClient { get; }
        Task<bool> ConnectAsync();


        /*
        #--------------------------------------------------------------------------------
        #--------------------------subscribe&unsubscribe---------------------------------
        #--------------------------------------------------------------------------------
         */

        #region [Subscribe&Unsubscribe]

        /// <summary>
        ///     The candles information after using <see cref="SubscribeCandlesAsync"/> onto specific <see cref="ActivePair"/>
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

        #endregion
    }
}