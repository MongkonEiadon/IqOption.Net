using iqoptionapi.extensions;
using Newtonsoft.Json;

namespace iqoptionapi.ws.request {


    internal class WsRequestMessageBase<T> : IWsRequestMessage<T>, IWsIqOptionMessageCreator
            where T: class {


        [JsonProperty("name")]
        public virtual string Name { get; set; }
        

        [JsonProperty("msg")]
        public T Message { get; set; }
        

        public override string ToString() {
            return this.AsJson();
        }

        public virtual string CreateIqOptionMessage() {
            return this.AsJson();
        }
    }
}