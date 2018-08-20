using iqoptionapi.models;
using iqoptionapi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws {


    public class GetCandleItemsResultMessage : WsMessageBase<Candles> {

        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }

}