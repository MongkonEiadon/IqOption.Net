using System;
using System.Threading.Tasks;
using IqOptionApi.Models;
using IqOptionApi.Models.BinaryOptions;
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
            DateTimeOffset expiration)
        {

            //reduce second to 00s 
            if (expiration.Second % 60 != 0)
                expiration = expiration.AddSeconds(60 - expiration.Second);

            // incasse of non-binary options
            var optionType = OptionType.Turbo;
            if (expiration.Subtract(ServerTime).Minutes >= 5)
                optionType = OptionType.Binary;

            return SendMessageAsync(
                new BuyV2WsMessage(
                    Profile.BalanceId,
                    pair,
                    optionType,
                    direction,
                    expiration,
                    size), BinaryOptionPlacedResultObservable);
        }

        #endregion
    }
}