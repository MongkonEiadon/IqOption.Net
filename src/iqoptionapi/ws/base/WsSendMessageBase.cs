using iqoptionapi.ws.@base;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request {
    internal class WsSendMessageBase<T> : WsMessageBase<T> where T : class {
        [JsonProperty("name")]
        public override string Name { get; set; } = "sendMessage";
    }
}