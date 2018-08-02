using Newtonsoft.Json;

namespace iqoptionapi.ws.request {
    internal class WsRequestSendMessageBase<T> : WsRequestMessageBase<T> where T : class {
        [JsonProperty("name")]
        public override string Name { get; set; } = "sendMessage";
    }
}