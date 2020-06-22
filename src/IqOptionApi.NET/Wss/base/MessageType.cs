namespace IqOptionApi.Ws.Base
{
    public struct MessageType
    {
        public const string Front = "front";
        public const string Profile = "profile";
        public const string Heartbeat = "heartbeat";

        public const string SendMessage = "sendMessage";
        public const string TimeSync = "timeSync";
        public const string OptionOpened = "option-opened";
        public const string OptionClosed = "option-closed";
        public const string OptionArchived = "option-archived";

        public const string SocketOptionOpened = "socket-option-opened";
        public const string SubscribeOrderChanged = "order-changed";
        public const string SubscribePortfolioChanged = "position-changed";
        public const string TraderMoodChanged = "traders-mood-changed";
        public const string SubscribeMessage = "subscribeMessage";
        public const string UnsubscribeMessage = "unsubscribeMessage";

        public const string UserTournamentPositionChanged = "user-tournament-position-changed";


        public const string Getinstruments = "get-instruments";
        public const string Ssid = "ssid";

        public const string BalanceChanged = "balance-changed";
        public const string Candles = "candles";

        public const string Quotes = "candle-generated";

        public const string PlacedDigitalOptions = "digital-option-placed";
        public const string PlacedBinaryOptions = "option";
    }
}