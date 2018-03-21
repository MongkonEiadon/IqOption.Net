using iqoptionapi.extensions;
using Newtonsoft.Json;

namespace iqoptionapi.ws {

    public interface IWsIqOptionMessage {
        string CreateIqOptionMessage();
    }
    internal class WsMessageBase<T> : IWsIqOptionMessage
            where T: class {


        [JsonProperty("name")]
        public virtual string Name { get; protected set; }

        [JsonProperty("msg")]
        public T Message { get; protected set; }
        

        public override string ToString() {
            return this.AsJson();
        }

        public virtual string CreateIqOptionMessage() {
            return this.AsJson();
        }
    }
}