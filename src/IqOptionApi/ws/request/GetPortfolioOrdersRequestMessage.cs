using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request
{
    public class GetPortfolioOrdersRequestMessage
    {
        [JsonProperty("name")]
        public string Name => "portfolio.get-orders";

        [JsonProperty("version")]
        public string Version => "1.0";

        [JsonProperty("body")]
        public GetPortfolioOrdersRequestMessageBody Body { get; set; }
    }

    public class GetPortfolioOrdersRequestMessageBody
    {
        [JsonProperty("user_balance_id")]
        public int user_balance_id { get; set; }
        [JsonProperty("kind")]
        public string kind { get; set; }
    }


    public class GetPortfolioOrdersRequest : WsSendMessageBase<GetPortfolioOrdersRequestMessage>
    {
        public GetPortfolioOrdersRequest(string intrumentType, int userBalanceId)
        {
            Message = new GetPortfolioOrdersRequestMessage()
            {
                Body = new GetPortfolioOrdersRequestMessageBody()
                {
                    user_balance_id = userBalanceId,
                    kind = "deferred"

                }
            };
        }
    }
}
