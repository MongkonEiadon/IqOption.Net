using IqOptionApi.Models;
using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request
{
    public class SubscribeCandleRequest : SubscribeMessageRequest<SubscribeCandleRequestBody>
    {
        public SubscribeCandleRequest(ActivePair pair, TimeFrame timeFrame)
        {
            Message = new SubscribeCandleRequestBody()
            {
                Parameters = new SubscribeCandleRequestParameter()
                {
                    Filter = new SubscribeCandleRequestParameter.CandleRequestFilter()
                    {
                        ActivePair = pair,
                        TimeFrame = timeFrame
                    }
                }
            };
        }
    }

    public class SubscribeCandleRequestBody
    {

        [JsonProperty("name")]
        public string Name { get; set; } = "candle-generated";

        [JsonProperty("params")]
        public SubscribeCandleRequestParameter Parameters { get; set; }
    }

    public class SubscribeCandleRequestParameter
    {

        [JsonProperty("routingFilters")]
        public CandleRequestFilter Filter { get; set; }


        public class CandleRequestFilter
        {
            [JsonProperty("active_id")]
            public ActivePair ActivePair { get; set; }

            [JsonProperty("size")]
            public TimeFrame TimeFrame { get; set; }
        }
    }


}
