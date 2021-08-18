using System;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
using IqOptionApi.Utilities;
using IqOptionApi.Ws.Request;

namespace IqOptionApi.Ws
{
    public partial class IqOptionWebSocketClient
    {

        #region [BuyV2]

        /// <summary>
        /// Place the binary position
        /// </summary>
        /// <param name="pair">The active pair to place position</param>
        /// <param name="size">The lot size</param>
        /// <param name="direction">The position direction</param>
        /// <param name="expiration">The expira</param>
        /// <returns></returns>
        public Task<BinaryOptionsResult> BuyAsync(
            ActivePair pair,
            int size,
            OrderDirection direction,
            int Duration)
        {

            //reduce second to 00s
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset exp = DateTimeOffset.Now;
            exp = DateTimeUtilities.GetExpirationTime(now,(BinaryOptionsDuration)Duration);

            // incasse of non-binary options
            var optionType = OptionType.Turbo;
            if (exp.Subtract(ServerTime).Minutes >= 5)
                optionType = OptionType.Binary;

            return SendMessageAsync(
                new BuyV2WsMessage(
                    Profile.BalanceId,
                    pair,
                    optionType,
                    direction,
                    exp,
                    size), BinaryOptionPlacedResultObservable);
        }

        #endregion
    }
}