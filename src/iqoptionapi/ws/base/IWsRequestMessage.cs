using iqoptionapi.ws.@base;
using Newtonsoft.Json;

namespace IqOptionApi.ws.request {
    public interface IWsRequestMessage<T> : IWsIqOptionMessageCreator {

        [JsonProperty("name")]
        string Name { get; set; }

        [JsonProperty("msg")]
        T Message { get; set; }

        EnumMessageType MessageType { get; }
    }


    public interface IResponseMessage { }
}