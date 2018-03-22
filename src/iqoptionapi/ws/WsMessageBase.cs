using iqoptionapi.extensions;
using Newtonsoft.Json;

namespace iqoptionapi.ws {

    public interface IWsIqOptionMessage {
        string CreateIqOptionMessage();
    }

    public interface IWsIqOptionMessage<T> : IWsIqOptionMessage where T : class
    {
        [JsonProperty("name")]
        string Name { get; set; }

        [JsonProperty("msg")]
        T Message { get; set; }

    }


    internal class WsMessageBase<T> : IWsIqOptionMessage<T>
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