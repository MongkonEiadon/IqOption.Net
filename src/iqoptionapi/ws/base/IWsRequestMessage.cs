using Newtonsoft.Json;

namespace IqOptionApi.ws.Request {
    public interface IWsRequestMessage<T> : IWsIqOptionMessageCreator {
        [JsonProperty("name")]
        string Name { get; set; }

        [JsonProperty("msg")]
        T Message { get; set; }
    }


    public interface IResponseMessage { }
}