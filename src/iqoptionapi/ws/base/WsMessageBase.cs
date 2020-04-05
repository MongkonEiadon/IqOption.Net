using IqOptionApi.extensions;
using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.@base
{
    public class WsMessageBase<T> : IWsRequestMessage<T>, IWsIqOptionMessageCreator
    {
        [JsonProperty("name")]
        public virtual string Name { get; set; }

        [JsonProperty("request_id")]
        public virtual string RequestId { get; set; }
        [JsonProperty("microserviceName")]
        public virtual string MicroserviceName { get; set; }

        [JsonProperty("msg")]
        public virtual T Message { get; set; }
        [JsonProperty("status")]
        public virtual int? Status { get; set; }

        public virtual string CreateIqOptionMessage()
        {
            return this.AsJson();
        }

        public override string ToString()
        {
            return this.AsJson();
        }
    }
}