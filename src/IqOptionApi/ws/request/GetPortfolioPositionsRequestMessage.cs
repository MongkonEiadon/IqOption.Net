using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request
{
    public class GetPortfolioPositionsRequestMessage
    {
        [JsonProperty("name")]
        public string Name => "portfolio.get-positions";

        [JsonProperty("version")]
        public string Version => "3.0";

        [JsonProperty("body")]
        public GetPortfolioPositionsRequestMessageBody Body { get; set; }
    }

    public class GetPortfolioPositionsRequestMessageBody
    {
        [JsonProperty("offset")]
        public int offset { get; set; }
        [JsonProperty("limit")]
        public int limit { get; set; }
        [JsonProperty("user_balance_id")]
        public long user_balance_id { get; set; }
        [JsonProperty("instrument_types")]
        //"binary-options", "turbo", "cfd", forex, crypto, digital-option, multi-option
        public string[] instrument_types { get; set; }
    }


    public class GetPortfolioPositionsRequest : WsSendMessageBase<GetPortfolioPositionsRequestMessage>
    {
        public GetPortfolioPositionsRequest(string intrumentType, long userBalanceId)
        {
            Message = new GetPortfolioPositionsRequestMessage()
            {
                Body = new GetPortfolioPositionsRequestMessageBody()
                {
                    instrument_types = new string[] { intrumentType },
                    offset = 0,
                    limit = 30,
                    user_balance_id = userBalanceId

                }
            };
        }
    }
}
