using System;
using IqOptionApi.extensions;
using Newtonsoft.Json;

namespace IqOptionApi.Ws.Base
{
    public class WsMessageBase<T> : IWsMessage<T>, IWsIqOptionMessageCreator
    {
        [JsonProperty("microserviceName")] public string MicroserviceName { get; set; }

        [JsonProperty("version")] public virtual string Version { get; set; } = "1.0";

        [JsonProperty("name")] public virtual string Name { get; set; }

        [JsonProperty("msg")] public virtual T Message { get; set; }
        
        [JsonProperty("status")] public virtual int StatusCode { get; set; }
        
        [JsonProperty("request_id")] public virtual long RequestId { get; set; }
        
        public virtual string CreateIqOptionMessage(long? requestId)
        {
            requestId = RequestId;
            return this.AsJson();
        }

        public override string ToString()
        {
            return this.AsJson();
        }

        public TType MessageAs<TType>()
        {
            return Message.ToString().JsonAs<TType>();
        }

        public object MessageAs(Type type)
        {
            return Message.ToString().JsonAs(type);
        }
    }
}