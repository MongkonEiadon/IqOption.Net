using Newtonsoft.Json;

namespace iqoptionapi.ws.request {
    internal class WsSendMessageBase<T> : WsMessageBase<T> where T : class {
        [JsonProperty("name")]
        public override string Name { get; set; } = "sendMessage";
    }
}