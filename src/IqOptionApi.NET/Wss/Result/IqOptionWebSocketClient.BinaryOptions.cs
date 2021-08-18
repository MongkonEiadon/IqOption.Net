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
            double size,
            OrderDirection direction,
            int Duration)
        {

            //reduce second to 00s
            DateTimeOffset exp = ExpirationUtilities.GetBinaryExpiration(ServerTime, Duration);

            // incasse of non-binary options
            var optionType = OptionType.Turbo;
            if (exp.Subtract(ServerTime).Minutes >= 5) optionType = OptionType.Binary;
            return SendMessageAsync(
                new BuyV2WsMessage(
                    Profile.BalanceId,
                    pair,
                    optionType,
                    direction,
                    exp,
                    size), BinaryOptionPlacedResultObservable);
        }

        public Task<BinaryOptionsResult> BuyAsync(
            ActivePair pair,
            double size,
            OrderDirection direction,
            DateTimeOffset expiration)
        {

            //reduce second to 00s 
            expiration = ExpirationUtilities.GetBinaryExpiration(ServerTime, expiration);

            // incasse of non-binary options
            var optionType = OptionType.Turbo;
            if (expiration.Subtract(ServerTime).Minutes >= 5) optionType = OptionType.Binary;

            return SendMessageAsync(
                new BuyV2WsMessage(
                    Profile.BalanceId,
                    pair,
                    optionType,
                    direction,
                    expiration,
                    size), BinaryOptionPlacedResultObservable);
        }

        public string BuyRequest(ActivePair Pair, double Size, OrderDirection Direction, int Duration)
        {
            DateTimeOffset exp = ExpirationUtilities.GetBinaryExpiration(ServerTime, Duration);
            var optionType = OptionType.Turbo;
            if (exp.Subtract(ServerTime).Minutes >= 5) optionType = OptionType.Binary;
            return SendMessageRequest(new BuyV2WsMessage(Profile.BalanceId, Pair, optionType, Direction, exp, Size));
        }
        #endregion

        #region [Leaderboard]
        public Task<Leaderboard> GetLeaderboard(Country CountryID, int From = 1, int To = 64)
        {
            return SendMessageAsync(
                new GetLeaderboardWsMessage(
                    CountryID,
                    Profile.CountryId, From, To), LeaderboardObservable);
        }
        public Task<UserProfile> GetUserProfile(long UserID)
        {
            return SendMessageAsync(
                new GetUserProfileWsMessage(UserID), UserProfileObservable);
        }
        #endregion

        #region [Get Initialization Data]
        public Task<InitializationData> GetInitializationData()
        {
            return SendMessageAsync(
                new GetInitializationDataWsMessage(), InitializationDataObservable);
        }
        #endregion

    }
}