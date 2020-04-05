using IqOptionApi.Models;
using IqOptionApi.Ws.Base;
using Newtonsoft.Json;

namespace IqOptionApi.Ws
{
    public class GetCandleItemsResultMessage : WsMessageBase<CandleCollections>
    {
        [JsonProperty("request_id")] public string RequestId { get; set; }
    }

    public class CurrentCandleInfoResultMessage : WsMessageBase<CurrentCandle>
    {
    }
}