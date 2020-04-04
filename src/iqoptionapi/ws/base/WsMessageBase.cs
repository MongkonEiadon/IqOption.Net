using System;

using IqOptionApi.extensions;
using IqOptionApi.ws.request;
using Newtonsoft.Json;

namespace iqoptionapi.ws.@base {
    public class WsMessageBase<T> : IWsMessage<T>, IWsIqOptionMessageCreator
    {
        [JsonProperty("name")]
        public virtual string Name { get; set; }
        
        [JsonProperty("microserviceName")]
        public string MicroserviceName { get; set; }
        
        [JsonProperty("msg")]
        public virtual T Message { get; set; }

        [JsonProperty("version")] 
        public virtual string Version { get; set; } = "1.0";

        public virtual string CreateIqOptionMessage() {
            return this.AsJson();
        }

        public override string ToString() {
            return this.AsJson();
        }

        public TType MessageAs<TType>() => Message.ToString().JsonAs<TType>();

        public Object MessageAs(Type type) => Message.ToString().JsonAs(type);

    }
}