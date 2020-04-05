using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request
{
    public class SubscribePortfolioPositionChanged : SubscribeMessageRequest<SubscribePortfolioPositionChangedMessage>
    {
        public SubscribePortfolioPositionChanged(long userId, long userBalanceId, string instrumentType)
        {
            Message = new SubscribePortfolioPositionChangedMessage()
            {
                _params = new PositionChangedParams()
                {
                    routingFilters = new PositionChangedRoutingfilters()
                    {
                        user_id = userId,
                        user_balance_id = userBalanceId,
                        instrument_type = instrumentType
                    }
                }
            };
        }
    }

    public class SubscribePortfolioPositionChangedMessage
    {
        [JsonProperty("name")]
        public string Name => "portfolio.position-changed";

        [JsonProperty("version")]
        public string Version => "1.0";
        [JsonProperty("params")]
        public PositionChangedParams _params { get; set; }
    }

    public class PositionChangedParams
    {
        [JsonProperty("routingFilters")]
        public PositionChangedRoutingfilters routingFilters { get; set; }
    }

    public class PositionChangedRoutingfilters
    {
        [JsonProperty("user_id")]
        public long user_id { get; set; }
        [JsonProperty("user_balance_id")]
        public long user_balance_id { get; set; }
        [JsonProperty("instrument_type")]
        public string instrument_type { get; set; }
    }
}
