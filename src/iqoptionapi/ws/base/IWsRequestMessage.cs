using Newtonsoft.Json;

namespace iqoptionapi.ws.request {
    public interface IWsRequestMessage<T> : IWsIqOptionMessageCreator {

        [JsonProperty("name")]
        string Name { get; set; }

        [JsonProperty("msg")]
        T Message { get; set; }
    }


    public interface IResponseMessage { }
}