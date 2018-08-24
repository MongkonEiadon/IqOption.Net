using IqOptionApi.extensions;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request {


    public class WsMessageBase<T> : IWsRequestMessage<T>, IWsIqOptionMessageCreator
    {
        [JsonProperty("name")]
        public virtual string Name { get; set; }
        
        [JsonProperty("msg")]
        public virtual T Message { get; set; }

        public virtual string CreateIqOptionMessage() {
            return this.AsJson();
        }

        public override string ToString() {
            return this.AsJson();
        }
    }
}