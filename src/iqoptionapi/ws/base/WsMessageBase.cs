using System.Diagnostics;
using IqOptionApi.extensions;
using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace IqOptionApi.ws.@base {
    public class WsMessageBase<T> : IWsRequestMessage<T>, IWsIqOptionMessageCreator
    {
        [JsonProperty("name")]
        public virtual string Name { get; set; }
        
        [JsonProperty("msg")]
        public virtual T Message { get; set; }

        public virtual string CreateIqOptionMessage() {
            return this.AsJson();
        }

        public override string ToString() {
            return this.AsJson();
        }

        public EnumMessageType MessageType
        {
            get
            {
                switch (Name.ToLower())
                {
                    case "candles": return EnumMessageType.Candles;
                    case "candle-generated": return EnumMessageType.CandleGenerated;
                    case "buycomplete": return EnumMessageType.BuyComplete;
                    case "front": return EnumMessageType.Front;
                    case "listinfodata": return EnumMessageType.ListInfoData;
                    case "chat-state-updated": return EnumMessageType.ChatStateUpdated;
                    case "user-tournament-position-changed": return EnumMessageType.UserTournamentPositionChanged;
                    case "activecommissionchange": return EnumMessageType.ActiveCommissionChanged;
                    case "profit-top-user-moved-up": return EnumMessageType.ProfitToUserMovedUp;
                    case "instruments": return EnumMessageType.Instruments;
                    case "timesync": return EnumMessageType.ServerTime;
                    case "profile": return EnumMessageType.Profile;
                    case "heartbeat": return EnumMessageType.Heartbeat;
                    

                    default: return EnumMessageType.Unknown;
                }
            }
        }
    }

    public enum EnumMessageType
    {
        Heartbeat,
        ServerTime,
        Profile,
        Instruments,
        ProfitToUserMovedUp,
        ActiveCommissionChanged,
        UserTournamentPositionChanged,
        ChatStateUpdated,
        Front,
        ListInfoData,
        BuyComplete,
        Candles,
        CandleGenerated,

        Unknown
    }
}