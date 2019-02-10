using System;
using System.ComponentModel;
using System.Threading.Tasks;
using IqOptionApi.models.CFD;
using IqOptionApi.Models;

namespace IqOptionApi.ws {
    public interface IIqWsClient : INotifyPropertyChanged, IDisposable {
        HeartBeat HeartBeat { get; }
        ServerTime ServerTime { get; }
        Profile Profile { get; }
        DigitalInfoData DigitalInfoData { get; }
        InfoData InfoData { get; }
        CurrentCandle CurrentCandle { get; }

        #region [Commands]

        /// <summary>
        ///  Sending to buy position
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

        Task<bool> OpenSecuredConnectionAsync(string token);

        #endregion


        /// <summary>
        ///     The token to make secured websocket channel
        /// </summary>
        string SecuredToken { get; }

        /// <summary>
        ///     All message that came from Web-Socket Channel
        /// </summary>
        IObservable<string> ChannelMessage { get; }
    }
}