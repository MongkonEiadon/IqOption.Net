using iqoptionapi.models;
using iqoptionapi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws {


    public class GetCandleItemsResultMessage : WsMessageBase<CandleCollections> {

        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }

    public class CurrentCandleInfoResultMessage : WsMessageBase<CurrentCandle> {

    }



}