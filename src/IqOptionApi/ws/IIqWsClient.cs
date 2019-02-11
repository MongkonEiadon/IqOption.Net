using System;
using System.ComponentModel;
using System.Threading.Tasks;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;

namespace IqOptionApi.ws {
    public interface IIqWsClient : INotifyPropertyChanged, IDisposable {
        /// <summary>
        ///     The token to make secured websocket channel
        /// </summary>
        string SecuredToken { get; }

        /// <summary>
        ///     All message that came from Web-Socket Channel
        /// </summary>
        IObservable<string> ChannelMessage { get; }

        #region [Channel Names]

        HeartBeat HeartBeat { get; }
        ServerTime ServerTime { get; }
        Profile Profile { get; }
        DigitalInfoData DigitalInfoData { get; }
        InfoData InfoData { get; }
        CurrentCandle CurrentCandle { get; }
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
        Task<BuyResult> BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            DateTimeOffset expiration = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="tf"></param>
        /// <param name="count"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        Task<CandleCollections> GetCandlesAsync(ActivePair pair, TimeFrame tf, int count, DateTimeOffset to);

        Task<bool> OpenSecuredConnectionAsync(string token);

        #endregion
    }
}