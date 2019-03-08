using IqOptionApi.ws.@base;
using Newtonsoft.Json;

namespace IqOptionApi.ws.Request {
    internal class WsSendMessageBase<T> : WsMessageBase<T> where T : class {
        [JsonProperty("name")]
        public override string Name { get; set; } = "sendMessage";
    }
}