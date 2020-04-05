using Newtonsoft.Json;

namespace IqOptionApi.ws.request
{
    internal class GetBalancesMessage
    {
        [JsonProperty("name")]
        public string Name => "get-balances";

        [JsonProperty("version")]
        public string Version => "1.0";

        [JsonProperty("body")]
        public GetBalancesRequestBody Body { get; set; }


        internal class GetBalancesRequestBody
        {
            [JsonProperty("types_ids")]
            public int[] Type => new int[] { 1, 4, 2 };
            [JsonProperty("tournament_statuses_id")]
            public int[] TournamentStatusesId => new int[] { 2, 3 };
        }
    }

    internal class GetBalancesRequest : WsSendMessageBase<GetBalancesMessage>
    {
        public GetBalancesRequest()
        {
            Message = new GetBalancesMessage()
            {
                Body = new GetBalancesMessage.GetBalancesRequestBody()
            };
        }
    }
}