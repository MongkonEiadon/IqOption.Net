namespace IqOptionApi.Ws.Request
{
    public struct RequestMessageBodyType
    {
        public const string GetCandles = "get-candles";

        public const string OpenOptions = "binary-options.open-option";

        public const string GetLeaderboardDeals = "request-leaderboard-deals-client";

        public const string RequestUserProfile = "get-user-profile-client";

        public const string RequestInitializationData = "get-initialization-data";

    }
}