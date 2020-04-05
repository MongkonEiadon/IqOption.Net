using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request
{
    public class SubscribePortfolioOrderChanged : SubscribeMessageRequest<SubscribePortfolioOrderChangedMessage>
    {
        public SubscribePortfolioOrderChanged(long userId, long userBalanceId, string instrumentType)
        {
            Message = new SubscribePortfolioOrderChangedMessage()
            {
                _params = new OrderChangedParams()
                {
                    routingFilters = new OrderChangedRoutingfilters()
                    {
                        user_id = userId,
                        user_balance_id = userBalanceId,
                        instrument_type = instrumentType
                    }
                }
            };
        }
    }

    public class SubscribePortfolioOrderChangedMessage
    {
        [JsonProperty("name")]
        public string Name => "portfolio.order-changed";

        [JsonProperty("version")]
        public string Version => "1.0";
        [JsonProperty("params")]
        public OrderChangedParams _params { get; set; }
    }

    public class OrderChangedParams
    {
        [JsonProperty("routingFilters")]
        public OrderChangedRoutingfilters routingFilters { get; set; }
    }

    public class OrderChangedRoutingfilters
    {
        [JsonProperty("user_id")]
        public long user_id { get; set; }
        [JsonProperty("user_balance_id")]
        public long user_balance_id { get; set; }
        [JsonProperty("instrument_type")]
        public string instrument_type { get; set; }
    }
}
