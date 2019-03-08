using System;
using System.ComponentModel;
using System.Threading.Tasks;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;
using ReactiveUI;

namespace IqOptionApi.ws {
    public interface IIqWsClient : IReactiveObject, IDisposable {
        /// <summary>
        ///     The token to make secured websocket channel
        /// </summary>
        string SecuredToken { get; }

        /// <summary>
        ///     All message that came from Web-Socket Channel
        /// </summary>
        IObservable<string> ChannelMessage { get; }

        #region [Channel Names]

        /// <summary>
        /// HeartBeat ticker from server
        /// </summary>
        HeartBeat HeartBeat { get; }

        /// <summary>
        /// The Server time that come from server
        /// </summary>
        ServerTime ServerTime { get;  }

        /// <summary>
        /// The client profile will update automatically
        /// </summary>
        Profile Profile { get; }

        /// <summary>
        /// The Digital Information data after client open the position will set automatically
        /// </summary>
        DigitalInfoData DigitalInfoData { get; }

        /// <summary>
        /// The Information data after client Open/Expired for Forex/CFD/Options will set automatically
        /// </summary>
        InfoData InfoData { get; }

        /// <summary>
        /// Current candles information after using <see cref="IIqWsClient.SubscribeCandlesAsync"/>
        /// </summary>
        CurrentCandle CurrentCandle { get; }

        /// <summary>
        /// The result notification after user open position
        /// </summary>
        BuyResult BuyResult { get; }

        #endregion

        #region [Commands]

        /// <summary>
        ///     Sending to buy position
        /// </summary>
        /// <param name="pair">The buying active</param>
        /// <param name="size">The buying size</param>
        /// <param name="direction">The buying direction</param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        Task BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            DateTimeOffset expiration = default(DateTimeOffset));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="tf"></param>
        /// <param name="count"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);

        /// <summary>
        /// Open secured connection with specific token
        /// </summary>
        /// <param name="token"></param>
        void OpenSecuredConnection(string token);

        #endregion
    }
}