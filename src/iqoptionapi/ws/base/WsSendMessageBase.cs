using iqoptionapi.ws.@base;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request
{

    public class WsSendMessageBase<T> : WsMessageBase<T> where T : class
    {
        private static long Counter = 1;

        [JsonProperty("name")]
        public override string Name { get; set; } = "sendMessage";

        public WsSendMessageBase() : base()
        {
            RequestId = Counter++.ToString();
        }
    }
}